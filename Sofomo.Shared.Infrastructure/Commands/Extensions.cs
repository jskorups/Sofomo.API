using Microsoft.Extensions.DependencyInjection;
using Sofomo.Shared.Abstraction.Commands;

namespace Sofomo.Shared.Infrastructure.Commands;

public static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.Scan(s => s.FromApplicationDependencies()
                            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                            .AsImplementedInterfaces()
                            .WithScopedLifetime());

        return services;
    }
}