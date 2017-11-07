// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： IClientRepository.cs
// 项目名称： 
// 创建时间：2017-08-25
// 负责人：YXL
// ===================================================================

using System;
using System.Threading.Tasks;
using Core.Repository;
using PermissionSystem.Models;
using ViewModels.OpenPlatform.ClientModel;

namespace IService.PermissionSystem
{
    public interface IClientService : IEfRepository<Client>
    {
        Task<ClientViewModel> SingleAsyncByID(Guid clientID);
        Task<int> Save(ClientViewModel model);
    }
}