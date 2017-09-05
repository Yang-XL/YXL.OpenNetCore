using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IService.PermissionSystem;

namespace IdentitySite.Services.IdentityService
{
    public class ClientStore : IClientStore
    {
        private readonly IClientService _clientService;
        //private readonly IList<Client> Clients;

        public ClientStore(IClientService clientService)
        {
            _clientService = clientService;
        }

        //public ClientStore()
        //{
        //    //Clients = new List<Client>
        //    //{
        //    //    new Client
        //    //    {
        //    //        ClientId = "mvc",
        //    //        ClientSecrets = new[] {new Secret("mvc Secrets")},
        //    //        ClientName = "MVC Client",
        //    //        AllowedGrantTypes = GrantTypes.Implicit,
        //    //        // where to redirect to after login
        //    //        RedirectUris = {"http://127.0.0.1:5001/signin-oidc"},
        //    //        // where to redirect to after logout
        //    //        PostLogoutRedirectUris = {"http://127.0.0.1:5001/"},
        //    //        AllowedScopes = new List<string>
        //    //        {
        //    //            IdentityServerConstants.StandardScopes.OpenId,
        //    //            IdentityServerConstants.StandardScopes.Profile,
        //    //            "用户基础信息",
        //    //            "用户头像"
        //    //        }
        //    //    }
        //    //};
        //}

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = await _clientService.SingleAsync(a => a.AppKey == clientId);
            return new Client
            {
                ClientId = client.AppKey,
                ClientSecrets = new[] {new Secret(client.AppSecrets)},
                ClientName = client.Name,
                AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                RedirectUris = {client.ClientUri},
                ClientUri =  client.ClientUri,
                PostLogoutRedirectUris = {client.LogOutUri},
                RequireConsent =  false,
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId
                }
            };
        }
    }
}