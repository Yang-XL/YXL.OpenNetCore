using System;
using System.Collections.Generic;
using System.Text;
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
        }
    }
}
