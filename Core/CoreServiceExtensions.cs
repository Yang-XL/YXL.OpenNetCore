using System;
using System.Collections.Generic;
using System.Linq;
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
            services.AddScoped<IFileManager, FileManager.FileManager>();
            services.AddScoped<ITypeFinder, TypeFinder>();
            //services.AddScoped<IPluginFinder, PluginFinder>();

            services.AddScoped<IAssemblyLoader, NetCoreAssemblyLoader>();
            //services.AddScoped<IPluginLoader, PluginLoader>();
            return services;
        }


        public static IServiceCollection UserRedisCache(this IServiceCollection service ,IConfiguration configuration)
        {
            

            return service;
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
