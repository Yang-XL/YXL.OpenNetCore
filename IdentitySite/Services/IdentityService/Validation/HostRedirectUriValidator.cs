using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IdentitySite.Services.IdentityService.Validation
{
    public class HostRedirectUriValidator : IRedirectUriValidator
    {
        public readonly ILogger _logger;

        public HostRedirectUriValidator(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HostRedirectUriValidator>();
        }

        protected bool StringCollectionContainsString(IEnumerable<string> urls, string requestedUrL)
        {
            if (urls.IsNullOrEmpty()) return false;
            _logger.LogDebug("正在验证 requestedUrL for {requestedUrL}", requestedUrL);
            var requestedUri = new Uri(requestedUrL);
            _logger.LogDebug("正在验证 requestedUri for {requestedUri}", requestedUri);
            foreach (var url in urls)
            {
                var clientUri = new Uri(url);
                if (clientUri.Host == requestedUri.Host) return true;
            }
            return false;
        }
        public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
        {
           _logger.LogDebug("验证RedirectUri：{0},Client.RedirectUris：{1}。结果：{2}", requestedUri, client.RedirectUris, StringCollectionContainsString(client.RedirectUris, requestedUri));

            return Task.FromResult(StringCollectionContainsString(client.RedirectUris, requestedUri));
        }

        public Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {
            _logger.LogDebug("验证PostLogoutRedirectUris：{0},Client.PostLogoutRedirectUris：{1}。结果：{2}", requestedUri, client.PostLogoutRedirectUris, StringCollectionContainsString(client.PostLogoutRedirectUris, requestedUri));
            return Task.FromResult(StringCollectionContainsString(client.PostLogoutRedirectUris, requestedUri));
        }
    }
}