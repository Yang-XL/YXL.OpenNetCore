using AutoMapper;
using ViewModels.AdminWeb;
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