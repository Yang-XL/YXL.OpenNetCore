using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Core.Infrastructure;

namespace Core.Plugins.AssemblyLoaders
{
   public  class LoadContext : AssemblyLoadContext
    {
    
        protected override Assembly Load(AssemblyName assemblyName)
        {
            try
            {
                // load it directly
                return Assembly.Load(assemblyName);
            }
            catch
            {
                //// if failed, try to load it from reference directory under plugin directory
                //var pluginManager = ServiceFactory.ServiceProvider.GetService(typeof(PluginManager));


                //foreach (var plugin in pluginManager.Plugins)
                //{
                //    var path = plugin.ReferenceAssemblyPath(assemblyName.Name);
                //    if (path != null)
                //    {
                //        return LoadFromAssemblyPath(path);
                //    }
                //}
                throw;
            }
        }
    }
}
