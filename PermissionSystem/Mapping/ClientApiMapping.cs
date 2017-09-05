// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ClientApiEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///ClientApi数据实体
	/// </summary>
	internal class ClientApiMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<ClientApi> table)
       {
            table.HasKey(a=>a.主键); 
            table.Property(a=>a.主键).IsRequired();
            table.Property(a=>a.MaxInvokeCount).IsRequired();
            table.Property(a=>a.InvokeCount).IsRequired();
            table.Property(a=>a.CreateDate).IsRequired();
            table.Property(a=>a.UpdateDate).IsRequired();
            table.Property(a=>a.ApiID).IsRequired();
            table.Property(a=>a.ClientID).IsRequired();
       
        
              table.HasOne(a => a.ApiID_Model).WithMany(a => a.ClientApi_ApiIDList);
              table.HasOne(a => a.ClientID_Model).WithMany(a => a.ClientApi_ClientIDList);
        
        
      
       }
	}
	
}