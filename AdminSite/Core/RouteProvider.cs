using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;


namespace AdminSite.Core
{
    public class RouteProvider : IRouteProvider
    {
        public readonly IServiceProvider _ServiceProvider;
        public RouteProvider(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
        }
        public void RegisterRoutes(IRouteBuilder routes)
        {
            //foreach (var router in _ServiceProvider.GetServices<IRouter>())
            //{
            //    if (!routes.Routes.Contains(router))
            //    {
            //        routes.Routes.Add(router);
            //    }
            //}
            routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            
            routes.MapRoute( "areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
         
            //routes.Routes.Add(_router);
        }
        public int Order => 0;
    }
 
}
