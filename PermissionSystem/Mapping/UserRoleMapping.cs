// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： UserRoleEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///UserRole数据实体
	/// </summary>
	internal class UserRoleMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<UserRole> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.UserID).IsRequired();
            table.Property(a=>a.RoleID).IsRequired();
            table.Property(a=>a.CreateDate).IsRequired();
       
        
            table.HasOne(a => a.RoleID_Model).WithMany(a => a.UserRole_RoleIDList);
            table.HasOne(a => a.UserID_Model).WithMany(a => a.UserRole_UserIDList);
        
        
      
       }
	}
	
}