// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ClientGrantTypesEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///ClientGrantTypes数据实体
	/// </summary>
	internal class ClientGrantTypesMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<ClientGrantTypes> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.ClientID).IsRequired();
            table.Property(a=>a.GrantTypeID).IsRequired();
       
        
              table.HasOne(a => a.ClientID_Model).WithMany(a => a.ClientGrantTypes_ClientIDList);
              table.HasOne(a => a.GrantTypeID_Model).WithMany(a => a.ClientGrantTypes_GrantTypeIDList);
        
        
      
       }
	}
	
}