using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ApiVP.Repository.Security;

namespace ApiVP.Repository.Context
{
    public class ApplicationSecurityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationSecurityDbContext(DbContextOptions<ApplicationSecurityDbContext> options) : base(options)
        {

        }

    }
}