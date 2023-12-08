using Microsoft.AspNetCore.Identity;

namespace AuthenticationApi.Models
{
    public class DWUser : IdentityUser
    {
        public Guid Tenant { get; set; }
        public string Badge { get; set; }
    }
}
