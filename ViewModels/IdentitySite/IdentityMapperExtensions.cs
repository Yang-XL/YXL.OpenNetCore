using System;
using System.Collections.Generic;
using System.Text;
using IdentityServer4.Models;
using PermissionSystem.Models;
using ViewModels.AdminWeb.Nav;
using ViewModels.Mapper;

namespace ViewModels.IdentitySite
{
    public static class IdentityMapperExtensions
    {
        #region Api

        public static ApiResource ToApiResourcel(this Api entity)
        {
            return entity.MapTo<Api, ApiResource>();
        }
        public static IdentityResource ToIdentityResource(this Api entity)
        {
            return entity.MapTo<Api, IdentityResource>();
        }
        #endregion

        #region Client

        public static IdentityServer4.Models.Client ToIdentityClient(this PermissionSystem.Models.Client entity)
        {
            return entity.MapTo<PermissionSystem.Models.Client, IdentityServer4.Models.Client>();
        }

        #endregion
    }
}
