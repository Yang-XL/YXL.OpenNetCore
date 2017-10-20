using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Plugins;
using Microsoft.AspNetCore.Mvc;

namespace AdminSite.Controllers
{
    public class PluginsController : Controller
    {
        private readonly IPluginFinder _pluginFinder;
        private readonly IServiceProvider _serviceProvider;
        public PluginsController(IPluginFinder pluginFinder, IServiceProvider serviceProvider)
        {
            
            _pluginFinder = pluginFinder;
            _serviceProvider = serviceProvider;
        }
        public IActionResult Index()
        {
            _pluginFinder.ReloadPlugins();
           
            

            return View();
        }
    }
}