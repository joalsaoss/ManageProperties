using ManagerProperties.Application.UseCases.Owners.Commands.CreateOwner;
using ManagerProperties.Application.UseCases.Owners.Queries.GetOwnerDetails;
using ManagerProperties.Application.Utilities.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace ManagerProperties.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services) 
        {
            services.AddTransient<IMediator, SimpleMediator>();
            services.AddScoped<IRequestHandler<CommandCreateOwner, Guid>, 
                UseCaseCreateOwner>();
            services.AddScoped<IRequestHandler<GetOwnerDetail, OwnerDetailDTO>,
                UseCaseGetOwnerDetail>();

            return services;
        }
    }
}
