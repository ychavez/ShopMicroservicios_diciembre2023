using AuthenticationApi.DTO;
using AuthenticationApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await UserExists(registerDTO.UserName.ToLower()))
                return BadRequest();

            var user = new DWUser
            {
                UserName = registerDTO.UserName.ToLower(),
                Email = registerDTO.Email,
                Badge = registerDTO.Badge,
                Tenant = registerDTO.Tenant
            };

            var result = await userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new UserDTO { UserName = registerDTO.UserName, Token = await GetToken(user) });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            if (!await UserExists(loginDTO.UserName))
                return Unauthorized();

            var user = await userManager.Users.SingleAsync(x => x.UserName == loginDTO.UserName.ToLower());

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, true);

            if (!result.Succeeded)
                return Unauthorized();

            return Ok(new UserDTO { Token = await GetToken(user), UserName = user.UserName! });
        }

        private async Task<bool> UserExists(string username)
           => await userManager.Users.AnyAsync(x => x.UserName == username);

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
