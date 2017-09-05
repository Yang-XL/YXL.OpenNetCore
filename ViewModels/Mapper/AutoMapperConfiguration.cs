using System.Collections.Generic;
using AutoMapper;
using Core.Utility.Extensions;
using IdentityServer4.Models;
using PermissionSystem.Models;
using ViewModels.AdminWeb.Application;
using ViewModels.AdminWeb.Nav;

namespace ViewModels.Mapper
{
    public static class AutoMapperConfiguration
    {
        /// <summary>
        ///     Mapper
        /// </summary>
        public static IMapper Mapper { get; private set; }

        /// <summary>
        ///     Mapper configuration
        /// </summary>
        public static MapperConfiguration MapperConfiguration { get; private set; }

        public static IMapper Init()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MenuViewModel, Menu>()
                    .ForMember(m => m.ParentID,
                        map => map.MapFrom(vm => vm.IsNav ? vm.ParentID : vm.ParentAuthoritys))
                    .ForMember(m => m.PyCode,
                        map => map.MapFrom(vm => vm.Name.ToPyCode()));;
                cfg.CreateMap<Menu, MenuViewModel>();

                cfg.CreateMap<Application, ApplicationViewModel>();
                cfg.CreateMap<ApplicationViewModel, Application>();

                
                //    .ForMember(m => m.MethodName, map => map.MapFrom(vm => vm.Name))
                //    .ForMember(m=>m.DisplayName, map => map.MapFrom(vm => vm.DisplayName));
                //cfg.CreateMap<ApiResource, Api>()
                //    .ForMember(m => m.MethodName, map => map.MapFrom(vm => vm.Name))
                //    .ForMember(m => m.DisplayName, map => map.MapFrom(vm => vm.DisplayName));


                cfg.CreateMap<Api, IdentityResource>()
                    .ForMember(m => m.Name, map => map.MapFrom(vm => vm.MethodName))
                    .ForMember(m => m.DisplayName, map => map.MapFrom(vm => vm.DisplayName));
                cfg.CreateMap<Api, ApiResource>()
                    .ForMember(m => m.Name, map => map.MapFrom(vm => vm.MethodName))
                    .ForMember(m => m.DisplayName, map => map.MapFrom(vm => vm.DisplayName))
                    .ForMember(m=>m.Scopes,map=>map.MapFrom(vm =>new[]{ new Scope(vm.MethodName)}));

            });
            return Mapper = MapperConfiguration.CreateMapper();
        }
    }
}