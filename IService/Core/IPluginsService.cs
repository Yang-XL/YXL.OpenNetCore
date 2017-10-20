using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Plugins;
using Sakura.AspNetCore;

namespace IService.Core
{
    public interface IPluginsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="queryString"></param>
        /// <param name="isInstalled"></param>
        /// <returns></returns>
        Task<IPagedList<PluginDescriptor>> PluginsPaged(int pageSize, int pageIndex, string queryString,bool? isInstalled);
    }
}
