using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IService.PermissionSystem;
using Microsoft.Extensions.Logging;
using ViewModels.Mapper;

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

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(FindIdentityResourcesByScope(scopeNames));
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(FindApiResourcesByScope(scopeNames));
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var model = await _apiService.SingleAsync(a => a.MethodName == name);
            return model.ToApiResourceModel();
        }


        public async Task<Resources> GetAllResourcesAsync()
        {
            var mode = await _apiService.GetAsync(a => true);
            var identityMode = from n in mode where n.ApiType == 0 select  n.ToIdentityResourceModel();
            var apiMode = from n in mode where n.ApiType == 1 select n.ToApiResourceModel();
            return new Resources(identityMode, apiMode);
        }


        private  IEnumerable<IdentityResource> FindIdentityResourcesByScope(IEnumerable<string> scopeNames)
        {
            var models =  _apiService.Get(a => a.ApiType == 0);
            foreach (var _identityResource in models)
                if (scopeNames.Contains(_identityResource.MethodName, StringComparer.OrdinalIgnoreCase))
                    yield return _identityResource.ToIdentityResourceModel();
        }

        private IEnumerable<ApiResource> FindApiResourcesByScope(IEnumerable<string> scopeNames)
        {

            var models = _apiService.Get(a => a.ApiType == 1);
            foreach (var _identityResource in models)
                if (scopeNames.Contains(_identityResource.MethodName, StringComparer.OrdinalIgnoreCase))
                    yield return _identityResource.ToApiResourceModel();
        }
    }
}