// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： RoleEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///Role数据实体
	/// </summary>
	internal class RoleMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<Role> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.Code).IsRequired();
            table.Property(a=>a.Name).IsRequired();
            table.Property(a=>a.PyCode).IsRequired();
            table.Property(a=>a.CreateDate).IsRequired();
            table.Property(a=>a.ShowIndex);
            table.Property(a=>a.Description);
       
            table.HasMany(a => a.UserRole_RoleIDList).WithOne(a => a.RoleID_Model);       
            table.HasMany(a => a.UserRoleJurisdiction_RoleIDList).WithOne(a => a.RoleID_Model);       
        
        
        
      
       }
	}
	
}