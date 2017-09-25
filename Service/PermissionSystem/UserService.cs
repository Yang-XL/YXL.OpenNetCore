// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： UserRespository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Repository.Implementation;
using IService;
using PermissionSystem.Models;
using PermissionSystem;
using Sakura.AspNetCore;
using ViewModels.AdminWeb;
using ViewModels.AdminWeb.User;

namespace Service.PermissionSystem
{
    public class UserService  :EfRepository< User>,IUserService
    {
        private readonly IUserRoleService _userRoleService;
      public UserService(PermissionSystemContext context, IUserRoleService userRoleService):base(context)
      {
          _userRoleService = userRoleService;
      }

        public async Task SaveUser(UserViewModel model)
        {
            var isUpdate = true;
            var entity = await SingleAsync(model.ID);

            if (entity == null)
            {
                isUpdate = false;
                entity = new User();
            }
            entity = model.ToEntity(entity);
            var userRoles = new List<UserRole>();
            foreach (var roleId in model.RoleID.Split(','))
            {
                userRoles.Add(new UserRole
                {
                    ID = Guid.NewGuid(),
                    RoleID = new Guid(roleId),
                    UserID = entity.ID,
                    CreateDate = DateTime.Now,
                });
            }
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (isUpdate)
                        await UpdateAsync(entity, false);
                    else
                        await InsertAsync(entity, false);
                    await _userRoleService.DeleteAsync(a => a.UserID == entity.ID, false);
                    if (userRoles.Any())
                        await _userRoleService.InsertAsync(userRoles, false);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }
            


        public bool ValidateCredentials(string loginName, string password)
        {
            return true;
        }

        public User FindByExternalProvider(string provider, Guid userId)
        {
            return new User {Name = "ZZ", ID = userId};
        }
    }
}