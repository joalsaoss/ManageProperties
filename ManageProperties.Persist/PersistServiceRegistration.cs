using ManageProperties.Persist.Repositories;
using ManageProperties.Persist.UnitOfWork;
using ManagerProperties.Application.Contracts.Persists;
using ManagerProperties.Application.Contracts.Repositories;
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

            services.AddScoped<IRepositoryOwners, RepositoryOwners>();
            services.AddScoped<IRepositoryProperties, RepositoryProperties>();
            services.AddScoped<IRepositoryPropertyImages, RepositoryPropertyImages>();
            services.AddScoped<IRepositoryPropertyTraces, RepositoryPropertyTraces>();
            services.AddScoped<IUnitOfWork, UnitOfWorkEFCore>();

            return services;
        }
    }
}
