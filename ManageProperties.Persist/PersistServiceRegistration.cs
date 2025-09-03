using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ManageProperties.Persist
{
    public static class PersistServiceRegistration
    {
        public static IServiceCollection AddPersistService(this IServiceCollection services)
        {
            services.AddDbContext<ManagePropertiesDbContext>(options => 
                options.UseSqlServer("name=ManagePropertiesConnectionString"));

            return services;
        }
    }
}
