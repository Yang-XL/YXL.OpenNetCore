using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Middleware
{
    public class WebSeoMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public WebSeoMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<WebSeoMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("User IP: " + context.Connection.RemoteIpAddress);
            

            await _next.Invoke(context);
        }

    }
}
