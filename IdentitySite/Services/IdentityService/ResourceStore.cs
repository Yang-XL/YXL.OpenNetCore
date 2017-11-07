using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IService.PermissionSystem;
using Microsoft.Extensions.Logging;
using ViewModels.IdentitySite;

namespace IdentitySite.Services.IdentityService
{
    public class ResourceStore : IResourceStore
    {
        private readonly IApiService _apiService;
        private readonly ILogger _logger;

        public ResourceStore(IApiService apiService, ILoggerFactory loggerFactory)
        {
            _apiService = apiService;
            _logger = loggerFactory.CreateLogger<ResourceStore>();
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(
            IEnumerable<string> scopeNames)
        {
            var list = await _apiService.QueryAsync(a => a.ApiType == 1 &&
                                                         scopeNames.Contains(a.MethodName,
                                                             StringComparer.CurrentCultureIgnoreCase));
            return list.Select(a => a.ToIdentityResource());
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var list = await _apiService.QueryAsync(a => a.ApiType == 1 &&
                                                         scopeNames.Contains(a.MethodName,
                                                             StringComparer.CurrentCultureIgnoreCase));
            return list.Select(a => a.ToApiResourcel());
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var model = await _apiService.SingleAsync(
                a => string.Equals(a.MethodName, name, StringComparison.CurrentCultureIgnoreCase));
            return model.ToApiResourcel();
        }


        public async Task<Resources> GetAllResourcesAsync()
        {
            var mode = await _apiService.QueryAsync();
            var identityMode = from n in mode where n.ApiType == 0 select n.ToIdentityResource();
            var apiMode = from n in mode where n.ApiType == 1 select n.ToApiResourcel();
            return new Resources(identityMode, apiMode);
        }
    }
}