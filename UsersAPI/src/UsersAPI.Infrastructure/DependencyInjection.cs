using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UsersAPI.Application.Interfaces;
using UsersAPI.Application.UseCases.CreateUser;
using UsersAPI.Infrastructure.Persistence;
using UsersAPI.Infrastructure.Repositories;
using UsersAPI.Infrastructure.Security;

namespace UsersAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<UsersDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, SimplePasswordHasher>();
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();

        return services;
    }
}
