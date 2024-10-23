using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Sofomo.Shared.Infrastructure.Swagger;

public static class Extensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Sofomo Weather API",
                Version = "v1"
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
    {
        app.UseSwagger();

        return app;
    }
}