using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using Core.FileManager;
using Core.Plugins.AssemblyLoaders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;

namespace Core.Plugins
{
    public class PluginFinder : IPluginFinder
    {
        private const string PluginsPath = "Plugins";
        private const string PluginDescriptionFileName = "PluginDescription.json";
        private readonly ApplicationPartManager _applicationPartManager;
        private readonly IAssemblyLoader _assemblyLoader;

        private readonly IFileManager _fileManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IServiceProvider _serviceProvider;
        private readonly IStartup _startup;


        public PluginFinder(IFileManager fileManager, ApplicationPartManager applicationPartManager,
            IAssemblyLoader assemblyLoader, IHostingEnvironment hostingEnvironment, IServiceProvider serviceProvider, IStartup startup)
        {
            _fileManager = fileManager;
            _applicationPartManager = applicationPartManager;
            _assemblyLoader = assemblyLoader;
            _hostingEnvironment = hostingEnvironment;
            _serviceProvider = serviceProvider;
            _startup = startup;
        }

        public IList<PluginDescriptor> _plugins { get; set; } = new List<PluginDescriptor>();


        private void LoadPlugin()
        {
            var pluginsDirectory = new DirectoryInfo(Path.Combine(_hostingEnvironment.ContentRootPath, PluginsPath));
            foreach (var pluginDirectory in pluginsDirectory.GetDirectories())
            {
                var configFile = pluginDirectory.GetFiles()
                    .FirstOrDefault(a => string.Equals(a.Name, PluginDescriptionFileName,
                        StringComparison.OrdinalIgnoreCase));
                if (configFile != null && configFile.Exists)
                {
                    var config = new ConfigurationBuilder()
                       .AddJsonFile(configFile.FullName,true,true)
                        .Build();
                    var plugin = config.Get<PluginDescriptor>();
                   
                    plugin.ConfigSetting = config;
                    _plugins.Add(plugin);
                    if (plugin.Installed)
                        foreach (var file in pluginDirectory.GetFiles())
                            if (string.Equals(file.Extension, ".dll", StringComparison.OrdinalIgnoreCase))
                            {
                                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                                if (_applicationPartManager.ApplicationParts.FirstOrDefault(a => string.Equals(a.Name,
                                        assembly.GetName().Name, StringComparison.OrdinalIgnoreCase)) != null)
                                    continue;
                                
                                _applicationPartManager.ApplicationParts.Add(new AssemblyPart(assembly));
                                var controllerFeature = new ControllerFeature();
                                _applicationPartManager.PopulateFeature(controllerFeature);

                                foreach (var controller in controllerFeature.Controllers)
                                {
                                   
                                }

                            }
                }
            }
        }


        #region Interface Method

        public IEnumerable<PluginDescriptor> GetPluginDescriptors(PluginsState pluginState = PluginsState.InstalledOnly,
            string group = null)
        {
            return _plugins.Where(p => CheckLoadMode(p, pluginState) && CheckGroup(p, group));
        }


        public PluginDescriptor GetPluginDescriptorBySystemName(string systemName,
            PluginsState pluginState = PluginsState.InstalledOnly)
        {
            return _plugins.FirstOrDefault(p => CheckLoadMode(p, pluginState) && p.SystemName == systemName);
        }

        public void ReloadPlugins()
        {
            LoadPlugin();
        }

        #endregion


        #region Check

        protected virtual bool CheckLoadMode(PluginDescriptor pluginDescriptor, PluginsState state)
        {
            if (pluginDescriptor == null)
                throw new ArgumentNullException("pluginDescriptor");

            switch (state)
            {
                case PluginsState.All:
                    //no filering
                    return true;
                case PluginsState.InstalledOnly:
                    return pluginDescriptor.Installed;
                case PluginsState.NotInstalledOnly:
                    return !pluginDescriptor.Installed;
                default:
                    throw new Exception("Not supported LoadPluginsMode");
            }
        }


        protected virtual bool CheckGroup(PluginDescriptor pluginDescriptor, string group)
        {
            if (pluginDescriptor == null)
                throw new ArgumentNullException("pluginDescriptor");

            if (string.IsNullOrEmpty(group))
                return true;

            return group.Equals(pluginDescriptor.Group, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}