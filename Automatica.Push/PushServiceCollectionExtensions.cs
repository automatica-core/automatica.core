﻿using AsyncKeyedLock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Automatica.Push
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAutomaticaPushServices(this IServiceCollection services, IConfiguration configuration,
            bool isElectronActive)
        {
            services.AddSingleton(new AsyncKeyedLocker<string>(o =>
            {
                o.PoolSize = 20;
                o.PoolInitialFill = 1;
            }));
        }
    }
}
