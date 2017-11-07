// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ApiEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///Api数据实体
	/// </summary>
	internal class ApiMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<Api> table)
       {
            table.HasKey(a=>a.Id); 
            table.Property(a=>a.Id).IsRequired();
            table.Property(a=>a.MethodName).IsRequired();
            table.Property(a=>a.DisplayName).IsRequired();
            table.Property(a=>a.Area);
            table.Property(a=>a.Controller).IsRequired();
            table.Property(a=>a.Action).IsRequired();
            table.Property(a=>a.Assembly).IsRequired();
            table.Property(a=>a.ApplicationID).IsRequired();
            table.Property(a=>a.CreateDate).IsRequired();
            table.Property(a=>a.UpdateDate).IsRequired();
            table.Property(a=>a.ApiType);
       
            table.HasMany(a => a.ClientApi_ApiIDList).WithOne(a => a.ApiID_Model);       
        
            table.HasOne(a => a.ApplicationID_Model).WithMany(a => a.Api_ApplicationIDList);
        
        
      
       }
	}
	
}