// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ApplicationParameterEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
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