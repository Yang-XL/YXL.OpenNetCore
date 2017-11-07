using System;
using AutoMapper;
using Core.Utility.Extensions;
using PermissionSystem.Models;
using ViewModels.OpenPlatform.ClientModel;

namespace ViewModels.OpenPlatform
{
    public class OpenPlatformProfile : Profile
    {
        public OpenPlatformProfile()
        {
            #region Client

            CreateMap<ClientViewModel, Client>()
                .ForMember(m => m.PyCode, map => map.MapFrom(vm => vm.Name.ToPyCode()))
                .ForMember(m => m.UpdateDate, map => map.MapFrom(vm => DateTime.Now));
            CreateMap<Client, ClientViewModel>();

            CreateMap<ClientUrisViewModel, ClientUris>();
            CreateMap<ClientUris, ClientUrisViewModel>();

            #endregion
        }

        public override string ProfileName => "OpenPlatform";
    }
}