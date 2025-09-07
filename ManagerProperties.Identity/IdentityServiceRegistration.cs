using ManagerProperties.Application.Contracts.Identity;
using ManagerProperties.Identity.Models;
using ManagerProperties.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ManagerProperties.Identity
{
    public static class IdentityServiceRegistration
    {
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddAuthentication(IdentityConstants.BearerScheme)
                .AddBearerToken(IdentityConstants.BearerScheme);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsManager", policy => policy.RequireClaim("IsManager"));
            });

            services.AddDbContext<ManagerPropertiesDbContextIdentity>(options =>
            {
                options.UseSqlServer("name=ConnectionStrings:ManagePropertiesConnectionString",
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<ManagerPropertiesDbContextIdentity>()
                .AddApiEndpoints();

            services.AddTransient<IUserService, UserServices>();
            services.AddHttpContextAccessor();
        }
    }
}
