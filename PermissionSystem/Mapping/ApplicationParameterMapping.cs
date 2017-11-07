// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ApplicationParameterEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///ApplicationParameter数据实体
	/// </summary>
	internal class ApplicationParameterMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<ApplicationParameter> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.KeyValue).IsRequired();
            table.Property(a=>a.DataValue).IsRequired();
            table.Property(a=>a.Description);
       
        
        
        
      
       }
	}
	
}