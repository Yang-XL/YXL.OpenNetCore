using System;
using System.Collections.Generic;
using System.Linq;
using Core.Cache;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Core.FileManager;
using Core.Infrastructure;
using Core.Middleware;
using Core.Plugins;
using Core.Plugins.AssemblyLoaders;

namespace Core
{
   public static class CoreServiceExtensions
    {
       
        public static IServiceCollection UserCore(this IServiceCollection services)
        {
            services.AddSingleton<IFileManager, FileManager.FileManager>();
            services.AddSingleton<ITypeFinder, TypeFinder>();
            services.AddSingleton<IAssemblyLoader, NetCoreAssemblyLoader>();
            services.AddSingleton<IPluginFinder, PluginFinder>();
            return services;
        }


        public static IServiceCollection UserRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheOption>(configuration.GetSection("Cache"));
            services.AddSingleton<ICache, RedisCache>();
            return services;
        }


        public static IApplicationBuilder UserCore(this IApplicationBuilder app)
        {
            app.UseMiddleware<WebSeoMiddleware>();
            return app;
        }


        //private static void RegisterMapperConfiguration(ITypeFinder typeFinder)
        //{
        //    var mcTypes = typeFinder.FindClassesOfType<IMapperConfiguration>();
        //    var mcInstances = new List<IMapperConfiguration>();
        //    foreach (var mcType in mcTypes)
        //        mcInstances.Add((IMapperConfiguration)Activator.CreateInstance(mcType));
        //    //sort
        //    mcInstances = mcInstances.AsQueryable().OrderBy(t => t.Order).ToList();
        //    //get configurations
        //    var configurationActions = new List<Action<IMapperConfigurationExpression>>();
        //    foreach (var mc in mcInstances)
        //        configurationActions.Add(mc.GetConfiguration());
        //    //register
        //    AutoMapperConfiguration.Init(configurationActions);
        //}
    }
}
