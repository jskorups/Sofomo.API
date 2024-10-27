using Microsoft.Extensions.DependencyInjection;
using Sofomo.Shared.Abstraction.Queries;

namespace Sofomo.Shared.Infrastructure.Queries
{
    public static class Extensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

            services.Scan(s => s.FromApplicationDependencies()
                           .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                       .AsImplementedInterfaces()
                       .WithScopedLifetime());

            return services;
        }
    }
}
