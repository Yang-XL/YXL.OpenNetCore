// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： ClientRespository.cs
// 项目名称： 
// 创建时间：2017-08-25
// 负责人：YXL
// ===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Repository.Implementation;
using IService.PermissionSystem;
using PermissionSystem.Models;
using PermissionSystem;
using ViewModels.OpenPlatform;
using ViewModels.OpenPlatform.ClientModel;

namespace Service.PermissionSystem
{
    public class ClientService : EfRepository<Client>, IClientService
    {
        private readonly IClientUrisService _clientUrisService;
        public ClientService(PermissionSystemContext context, IClientUrisService clientUrisService) : base(context)
        {
            _clientUrisService = clientUrisService;
        }

        public async Task<ClientViewModel> SingleAsyncByID(Guid clientID)
        {
            var client = await SingleAsync(a => a.ID == clientID);
            var clientUris = await _clientUrisService.QueryAsync(a => a.ClientiID == clientID);
            var model = client.ToModel();
            model.Uris = clientUris.Select(a => a.ToModel());
            return model;
        }

        public Task<int> Save(ClientViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}