using Microsoft.AspNetCore.Routing;

namespace AdminSite.Core
{

    public interface IRouteProvider
    {
        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routes">Routes</param>
        void RegisterRoutes(IRouteBuilder routes);

        int Order { get;  }
    }
    
}
