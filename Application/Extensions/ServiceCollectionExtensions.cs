﻿using Application.Abstract.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg => cfg
                .RegisterServicesFromAssembly(AssemblyReference.Assembly))
            .AddAutoMapper(AssemblyReference.Assembly)
            ;

        return services;
    }
}
