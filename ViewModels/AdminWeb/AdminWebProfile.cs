using System;
using AutoMapper;
using Core.Utility.Extensions;
using PermissionSystem.Models;
using ViewModels.AdminWeb.Application;
using ViewModels.AdminWeb.Nav;
using ViewModels.AdminWeb.Organization;
using ViewModels.AdminWeb.Roles;
using ViewModels.AdminWeb.User;

namespace ViewModels.AdminWeb
{
    public class AdminWebProfile : Profile
    {
        public AdminWebProfile()
        {
            #region Menu

            CreateMap<MenuViewModel, Menu>()
                .ForMember(m => m.PyCode, map => map.MapFrom(vm => vm.Name.ToPyCode()))
                .ForMember(m => m.IsNav, map => map.MapFrom(vm => vm.MenuType != 3));

            CreateMap<Menu, MenuViewModel>()
                .ForMember(vm => vm.MenuType,
                    map => map.MapFrom(m => m.IsNav ? (string.IsNullOrEmpty(m.ActionName) ? 1 : 2) : 3));

            #endregion

            #region PermissionSystem.Models.Application

            CreateMap<PermissionSystem.Models.Application, ApplicationViewModel>();

            CreateMap<ApplicationViewModel, PermissionSystem.Models.Application>()
                .ForMember(m => m.PyCode, map => map.MapFrom(vm => vm.Name.ToPyCode()));

            #endregion

            #region Role

            CreateMap<Role, RoleViewModel>();
            CreateMap<RoleViewModel, Role>()
                .ForMember(m => m.PyCode, map => map.MapFrom(vm => vm.Name.ToPyCode()));

            #endregion

            #region PermissionSystem.Models.Organization

            CreateMap<PermissionSystem.Models.Organization, OrganizationViewModel>();
            CreateMap<OrganizationViewModel, PermissionSystem.Models.Organization>()
                .ForMember(m => m.PyCode, map => map.MapFrom(vm => vm.Name.ToPyCode()));

            #endregion

            #region PermissionSystem.Models.User

            CreateMap<PermissionSystem.Models.User, UserViewModel>();
            CreateMap<UserViewModel, PermissionSystem.Models.User>()
                .ForMember(m => m.PyCode, map => map.MapFrom(vm => vm.Name.ToPyCode()))
                .ForMember(m => m.NormalizedLoginName, map => map.MapFrom(vm => vm.LoginName.ToUpper()))
                .ForMember(m => m.NormalizedEmail, map => map.MapFrom(vm => vm.Email.ToUpper()))
                .ForMember(m => m.CreateDate,
                    map => map.MapFrom(vm => vm.CreateDate == default(DateTime) ? DateTime.Now : vm.CreateDate))
                .ForMember(m => m.UpdateDate,
                    map => map.MapFrom(vm => vm.UpdateDate == default(DateTime) ? DateTime.Now : vm.UpdateDate))
                .ForMember(m => m.LockoutEnd,
                    map => map.MapFrom(vm => vm.LockoutEnd == default(DateTime) ? DateTime.Now : vm.LockoutEnd));

            #endregion
        }

        public override string ProfileName => "AdminWebProfile";
    }
}