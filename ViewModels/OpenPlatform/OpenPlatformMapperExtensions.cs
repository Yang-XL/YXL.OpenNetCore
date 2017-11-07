using System;
using ViewModels.Mapper;
using ViewModels.OpenPlatform.ClientModel;
using PermissionSystem.Models;

namespace ViewModels.OpenPlatform
{
    /// <summary>
    /// </summary>
    public static class OpenPlatformMapperExtensions
    {
        #region Client

        public static Client ToEntity(this ClientViewModel model)
        {
            return model.MapTo<ClientViewModel, Client>();
        }

        public static Client ToEntity(this ClientViewModel model,
            Client entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return model.MapTo(entity);
        }

        public static ClientViewModel ToModel(this Client entity)
        {
            return entity.MapTo<Client, ClientViewModel>();
        }

        #endregion

        #region ClientUris

        public static ClientUris ToEntity(this ClientUrisViewModel model)
        {
            return model.MapTo<ClientUrisViewModel, ClientUris>();
        }

        public static ClientUris ToEntity(this ClientUrisViewModel model,
            ClientUris entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return model.MapTo(entity);
        }

        public static ClientUrisViewModel ToModel(this ClientUris entity)
        {
            return entity.MapTo<ClientUris, ClientUrisViewModel>();
        }

        #endregion
    }
}