using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IService;
using LoggerExtensions;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace AdminSite.Core
{
    public class AdminRouter : IRouter
    {

        private readonly IMenuService _menuService;
        private readonly ILogger _logger;
        private readonly IRouter _router;
        public AdminRouter(IMenuService menuService, ILogger<AdminRouter> logger, IServiceProvider serviceProvider)
        {
            _menuService = menuService;
            _logger = logger;
            _router = serviceProvider.GetRequiredService<MvcRouteHandler>();
        }
        /// <summary>
        /// 对URL过滤
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async   Task RouteAsync(RouteContext context)
        {
            var requestedUrl = context.HttpContext.Request.Path.Value;

            foreach (var d in context.RouteData.Values)
            {
                _logger.Debug($"{d.Key}：{d.Value}", "自定义路由");
            }

            _logger.Debug(requestedUrl, "自定义路由");

            await _router.RouteAsync(context);
        }
        /// <summary>
        /// 生成对外显示的URL路径
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            
            return new VirtualPathData(this, context.HttpContext.Request.Path);
        }
    }

    
}
