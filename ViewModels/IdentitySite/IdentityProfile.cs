using AutoMapper;
using IdentityServer4.Models;
using PermissionSystem.Models;

namespace ViewModels.IdentitySite
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile() : base("IdentityProfile")
        {
            CreateMap<Api, IdentityResource>()
                .ForMember(m => m.Name, map => map.MapFrom(vm => vm.MethodName))
                .ForMember(m => m.DisplayName, map => map.MapFrom(vm => vm.DisplayName));

            CreateMap<Api, ApiResource>()
                .ForMember(m => m.Name, map => map.MapFrom(vm => vm.MethodName))
                .ForMember(m => m.DisplayName, map => map.MapFrom(vm => vm.DisplayName))
                .ForMember(m => m.Scopes, map => map.MapFrom(vm => new[] {new Scope(vm.MethodName)}));

            CreateMap<PermissionSystem.Models.Client, IdentityServer4.Models.Client>()
                .ForMember(m => m.ClientId, map => map.MapFrom(vm => vm.AppKey))
                .ForMember(m => m.ClientName, map => map.MapFrom(vm => vm.Name))
                .ForMember(m => m.ClientSecrets, map => map.MapFrom(vm => new[] {new Secret {Value = vm.AppSecrets}}));




            //return new Client
            //{
            //    ClientId = client.AppKey,
            //    ClientSecrets = new[] { new Secret(client.AppSecrets) },
            //    ClientName = client.Name,
            //    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
            //    RedirectUris = { client.ClientUri },
            //    ClientUri = client.ClientUri,
            //    PostLogoutRedirectUris = { client.LogOutUri },
            //    RequireConsent = false,
            //    AllowedScopes = new List<string>
            //    {
            //        IdentityServerConstants.StandardScopes.OpenId
            //    }
            //};
        }
    }
}