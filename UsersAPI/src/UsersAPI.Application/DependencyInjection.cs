using Microsoft.Extensions.DependencyInjection;
using UsersAPI.Application.DTOs.Auth.Login;

namespace UsersAPI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<LoginHandler>();

        return services;
    }
}
