// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： RoleRespository.cs
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
using PermissionSystem;
using PermissionSystem.Models;
using ViewModels.AdminWeb;
using ViewModels.AdminWeb.Roles;

namespace Service.PermissionSystem
{
    public class RoleService : EfRepository<Role>, IRoleService
    {
        private readonly IUserRoleJurisdictionService _roleJurisdiction;

        public RoleService(PermissionSystemContext context,
            IUserRoleJurisdictionService roleJurisdiction) : base(context)
        {
            _roleJurisdiction = roleJurisdiction;
        }

        public async Task SaveRole(RoleViewModel model)
        {
            var isUpdate = true;
            var entity = await SingleAsync(a=>a.ID == model.ID);
            if (entity == null)
            {
                entity = new Role
                {
                    ID = Guid.NewGuid(),
                    CreateDate = DateTime.Now
                };
                isUpdate = false;
            }
            entity = model.ToEntity(entity);
            var roleJurisdictionList = new List<UserRoleJurisdiction>();

            foreach (var roleMenu in model.RoleMenus)
            {
                var array = roleMenu.Split("|");
                roleJurisdictionList.Add(new UserRoleJurisdiction
                {
                    ID = Guid.NewGuid(),
                    ApplicationID = new Guid(array[1]),
                    MenuID = new Guid(array[0]),
                    RoleID = entity.ID,
                    CreateDate = DateTime.Now
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
                    await _roleJurisdiction.DeleteAsync(a => a.RoleID == entity.ID, false);
                    if (roleJurisdictionList.Any())
                        await _roleJurisdiction.InsertAsync(roleJurisdictionList, false);
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
    }
}