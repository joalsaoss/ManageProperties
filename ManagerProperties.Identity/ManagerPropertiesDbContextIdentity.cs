using ManagerProperties.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagerProperties.Identity
{
    public class ManagerPropertiesDbContextIdentity : IdentityDbContext<User>
    {
        public ManagerPropertiesDbContextIdentity(DbContextOptions<ManagerPropertiesDbContextIdentity> options) : 
            base(options)
        {
        }

        protected ManagerPropertiesDbContextIdentity()
        {
        }
    }
}
