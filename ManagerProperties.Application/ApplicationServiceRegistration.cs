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

            return services;
        }
    }
}
