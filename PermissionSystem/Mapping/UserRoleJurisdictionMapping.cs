// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： UserRoleJurisdictionEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///UserRoleJurisdiction数据实体
	/// </summary>
	internal class UserRoleJurisdictionMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<UserRoleJurisdiction> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.RoleID);
            table.Property(a=>a.ApplicationID).IsRequired();
            table.Property(a=>a.MenuID);
            table.Property(a=>a.CreateDate);
       
        
              table.HasOne(a => a.ApplicationID_Model).WithMany(a => a.UserRoleJurisdiction_ApplicationIDList);
              table.HasOne(a => a.MenuID_Model).WithMany(a => a.UserRoleJurisdiction_MenuIDList);
              table.HasOne(a => a.RoleID_Model).WithMany(a => a.UserRoleJurisdiction_RoleIDList);
        
        
      
       }
	}
	
}