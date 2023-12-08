using AuthenticationApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi.Context
{
    public class AccountDbContext : IdentityDbContext<DWUser>
    {
        public AccountDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
