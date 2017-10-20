using System.Linq;
using System.Threading.Tasks;
using Core.Plugins;
using IService.Core;
using Sakura.AspNetCore;

namespace Service.Core
{
    public class PluginsService : IPluginsService
    {
        public readonly IPluginFinder _pluginFinder;

        public PluginsService(IPluginFinder pluginFinder)
        {
            _pluginFinder = pluginFinder;
        }


        public Task<IPagedList<PluginDescriptor>> PluginsPaged(int pageSize, int pageIndex, string queryString,
            bool? isInstalled)
        {
            return Task.Run<IPagedList<PluginDescriptor>>(() =>
            {
                var list = _pluginFinder.GetPluginDescriptors();
                if (isInstalled.HasValue)
                    list = list.Where(a => a.Installed == isInstalled);
                if (!string.IsNullOrEmpty(queryString))
                    list = list.Where(a => a.FriendlyName.Contains(queryString));
                return list.ToPagedList(pageSize, pageIndex);
            });
        }
    }
}