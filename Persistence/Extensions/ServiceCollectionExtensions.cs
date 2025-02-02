﻿using Application.Abstract.Interfaces.Base;
using Application.Abstract.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Abstractions;
using Persistence.Repositories;
using Persistence.Repositories.Base;

namespace Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IProjectRepository, ProjectRepository>()
            .AddScoped<IProjectMemberRepository, ProjectMemberRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            ;

        return services;
    }
}
