// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： MenuEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///Menu数据实体
	/// </summary>
	internal class MenuMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<Menu> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.ApplicationID).IsRequired();
            table.Property(a=>a.Code).IsRequired();
            table.Property(a=>a.Name).IsRequired();
            table.Property(a=>a.PyCode).IsRequired();
            table.Property(a=>a.IsNav);
            table.Property(a=>a.ParentID);
            table.Property(a=>a.ShowIndex);
            table.Property(a=>a.AreaName);
            table.Property(a=>a.ControllerName);
            table.Property(a=>a.ActionName);
            table.Property(a=>a.Description);
            table.Property(a=>a.IconCss);
            table.Property(a=>a.CreateDate).IsRequired();
       
            table.HasMany(a => a.UserRoleJurisdiction_MenuIDList).WithOne(a => a.MenuID_Model);       
        
            table.HasOne(a => a.ApplicationID_Model).WithMany(a => a.Menu_ApplicationIDList);
        
        
      
       }
	}
	
}