using AuthenticationApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController(UserManager<DWUser> userManager,
        SignInManager<DWUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration) : ControllerBase
    {

        [HttpPost("Roles")]
        public async Task<ActionResult> CreateRole(string role)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
            return NoContent();
        }

        [HttpPost("AddUserToRole")]
        public async Task<ActionResult> AddUserToRole(string role, string username)
        {
            var user = userManager.FindByNameAsync(username);

            await userManager.AddToRoleAsync(await user, role);

            return NoContent();
        }


        private async Task<string> GetToken(DWUser user)
        {
            var now = DateTime.UtcNow;
            var key = configuration.GetValue<string>("Identity:Key");

            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
              new Claim(JwtRegisteredClaimNames.Jti, user.Id),
              new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(),ClaimValueTypes.Integer64),
              new Claim(JwtRegisteredClaimNames.Email, user.Email!),
              new Claim("Tenant",user.Tenant.ToString())
            };

            var roles = await userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

            var signinKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!));

            var toketDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = now.AddMinutes(15),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
            };

            var encodedJWT = new JwtSecurityTokenHandler();
            
            var token = encodedJWT.CreateToken(toketDescriptor);

            return encodedJWT.WriteToken(token);
        }

    }
}
