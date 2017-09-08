using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdminSite.Core
{
    public class AdminRouter : IRouter
    {
        private readonly IMenuService _menuService;
        private readonly ILogger _logger;
        private readonly IRouter _defaultRouter;
        public AdminRouter(IMenuService menuService, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            _menuService = menuService;
            _defaultRouter = serviceProvider.GetRequiredService<MvcRouteHandler>();
            _logger = loggerFactory.CreateLogger<AdminRouter>();
        }

        public  async Task RouteAsync(RouteContext context)
        {
            var requestedUrl = context.HttpContext.Request.Path.Value.TrimEnd('/');


            if (requestedUrl.ToLower().StartsWith("/about"))
            {
                context.RouteData.Values["controller"] = "Home";
                context.RouteData.Values["action"] = "About";
            }


            var url = context.RouteData.Values;

            foreach (var item in context.RouteData.Values)
            {
                _logger.LogDebug("Key is {Key}，Value is {Value}",item.Key,item.Value);
            }
            
            await _defaultRouter.RouteAsync(context);
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }
    }
}
