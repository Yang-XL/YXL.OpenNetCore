using System.Collections.Generic;

namespace Core.Plugins
{
    public interface IPluginFinder
    {

        /// <summary>
        /// 获取插件
        /// </summary>
        /// <param name="pluginState">插件状态</param>
        /// <param name="group">组</param>
        /// <returns>Plugin descriptors</returns>
        IEnumerable<PluginDescriptor> GetPluginDescriptors(PluginsState pluginState = PluginsState.InstalledOnly, string group = null);
        
        /// <summary>
        /// 获取插件
        /// </summary>
        /// <param name="systemName">插件名</param>
        /// <param name="pluginState">插件状态</param>
        /// <returns>>Plugin descriptor</returns>
        PluginDescriptor GetPluginDescriptorBySystemName(string systemName, PluginsState pluginState = PluginsState.InstalledOnly);
        
        /// <summary>
        /// 重载所有插件
        /// </summary>
        void ReloadPlugins();
    }
}