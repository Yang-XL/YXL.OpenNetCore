// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： LogEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///Log数据实体
	/// </summary>
	internal class LogMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<Log> table)
       {
            table.Property(a=>a.CreateDate).IsRequired();
            table.Property(a=>a.LogLeve).IsRequired();
            table.Property(a=>a.ShortMessage);
            table.Property(a=>a.FullMessage);
            table.Property(a=>a.IpAddress);
            table.Property(a=>a.PageUrl);
            table.Property(a=>a.ReferrerUrl);
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
       
        
        
        
      
       }
	}
	
}