using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;


namespace AdminSite.Core
{
    public class RouteProvider : IRouteProvider
    {
        public readonly IRouter _router;
        public RouteProvider(IRouter router)
        {
            _router = router;
        }
        public void RegisterRoutes(IRouteBuilder routes)
        {
            //routes.Routes.Add(_router);
            routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        }
        public int Order => 0;
    }
 
}
