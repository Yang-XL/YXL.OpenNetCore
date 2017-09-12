using System.Collections.Generic;
using AutoMapper;
using Core.Utility.Extensions;
using IdentityServer4.Models;
using PermissionSystem.Models;
using ViewModels.AdminWeb;
using ViewModels.AdminWeb.Application;
using ViewModels.AdminWeb.Nav;
using ViewModels.IdentitySite;

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
                cfg.AddProfile<AdminWebProfile>();
                cfg.AddProfile<IdentityProfile>();
            });
            return Mapper = MapperConfiguration.CreateMapper();
        }
    }
}