using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Core.Utility.Locker;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Core.Plugins
{
    public class PluginManager
    {
        private const string PluginsPath = "~/Plugins";
        private const string PluginDescriptionFileName = "PluginDescription.json";


        /// <summary>
        /// 所有被引用过的插件集合
        /// </summary>
        public static IEnumerable<PluginDescriptor> ReferencedPlugins { get; set; }

        /// <summary>
        /// 版本不兼容的插件集合
        /// </summary>
        public static IEnumerable<string> IncompatiblePlugins { get; set; }


        #region Fields

        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
        private static DirectoryInfo _shadowCopyFolder;
        private static bool _clearShadowDirectoryOnStartup;

        #endregion

        


        public static void Initialize()
        {
            using (new WriteLockDisposable(Locker))
            {
                //找到插件目录
                //从插件的描述文件中加载已经启用的插件
                //
                


            }
        }

        private PluginDescriptor GetAppSettings(string filePath) 
        {
            IConfiguration config = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = filePath, ReloadOnChange = true })
                .Build();
            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<PluginDescriptor>(config)
                .BuildServiceProvider()
                .GetService<IOptions<PluginDescriptor>>()
                .Value;
            return appconfig;
        }
    }
}
