using ManagerProperties.Application.Utilities.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace ManagerProperties.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services) 
        {
            services.AddTransient<IMediator, SimpleMediator>();

            
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMediator))
                .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            /*
            services.AddScoped<IRequestHandler<CommandCreateOwner, Guid>, 
                UseCaseCreateOwner>();
            services.AddScoped<IRequestHandler<GetOwnerDetail, OwnerDetailDTO>,
                UseCaseGetOwnerDetail>();
            services.AddScoped<IRequestHandler<GetAllOwners, List<GetAllOwnersDTO>>,
                UseCaseGetAllOwner>();
            services.AddScoped<IRequestHandler<CommandUpdateOwner>, UseCaseUpdateOwner>();
            services.AddScoped<IRequestHandler<CommandDeleteOwner>, UseCaseDeleteOwner>();

            services.AddScoped<IRequestHandler<CommandCreateProperty, Guid>,
                UseCaseCreateProperty>();
            */

            return services;
        }
    }
}
