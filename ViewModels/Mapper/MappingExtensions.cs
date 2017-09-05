using IdentityServer4.Models;
using PermissionSystem.Models;
using PermissionSystem.Models;
using ViewModels.AdminWeb.Application;
using ViewModels.AdminWeb.Nav;

namespace ViewModels.Mapper
{
    public static class MappingExtensions
    {
        public static Menu ToEntity(this MenuViewModel model)
        {
            return model.MapTo<MenuViewModel, Menu>();
        }

        public static Menu ToEntity(this MenuViewModel model, Menu entity)
        {
            return model.MapTo(entity);
        }

        public static MenuViewModel ToModel(this Menu entity)
        {
            return entity.MapTo<Menu, MenuViewModel>();
        }


        public static Application ToEntity(this ApplicationViewModel model)
        {
            return model.MapTo<ApplicationViewModel, Application>();
        }

        public static Application ToEntity(this ApplicationViewModel model, Application entity)
        {
            return model.MapTo(entity);
        }

        public static ApplicationViewModel ToModel(this Application entity)
        {
            return entity.MapTo<Application, ApplicationViewModel>();
        }


        public static IdentityResource ToIdentityResourceModel(this Api entity)
        {
            return entity.MapTo<Api, IdentityResource>();
        }
        public static ApiResource ToApiResourceModel(this Api entity)
        {
            return entity.MapTo<Api, ApiResource>();
        }
    }
}