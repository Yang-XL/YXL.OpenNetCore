// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ClientEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///Client数据实体
	/// </summary>
	internal class ClientMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<Client> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.AppKey).IsRequired();
            table.Property(a=>a.AppSecrets).IsRequired();
            table.Property(a=>a.CreateDate);
            table.Property(a=>a.ClientUri);
            table.Property(a=>a.UpdateDate);
            table.Property(a=>a.Name);
            table.Property(a=>a.LogOutUri);
       
              table.HasMany(a => a.ClientApi_ClientIDList).WithOne(a => a.ClientID_Model);       
              table.HasMany(a => a.ClientGrantTypes_ClientIDList).WithOne(a => a.ClientID_Model);       
        
        
        
      
       }
	}
	
}