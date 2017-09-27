// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： UserRoleRespository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Repository.Implementation;
using IService;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Models;
using PermissionSystem;
namespace Service.PermissionSystem
{
    public class UserRoleService  :EfRepository< UserRole>,IUserRoleService
    {
        private readonly IUserRoleJurisdictionService _userRoleJurisdictionService;
      public UserRoleService(PermissionSystemContext context, IUserRoleJurisdictionService userRoleJurisdictionService):base(context)
      {
          _userRoleJurisdictionService = userRoleJurisdictionService;
      }

        public async Task<bool> IsPermissionsMenu(Guid userId, Guid menuId)
        {
            return await (from ur in Queryable()
                join urm in _userRoleJurisdictionService.Queryable() on ur.RoleID equals urm.RoleID
                where ur.UserID == userId && urm.MenuID == menuId
                select urm).AnyAsync();
        }
    }
}