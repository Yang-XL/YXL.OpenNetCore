// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ClientEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
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
            table.Property(a=>a.CreateDate).IsRequired();
            table.Property(a=>a.UpdateDate).IsRequired();
            table.Property(a=>a.Name).IsRequired();
            table.Property(a=>a.Avaiable).IsRequired();
            table.Property(a=>a.Remark);
       
            table.HasMany(a => a.ClientApi_ClientIDList).WithOne(a => a.ClientID_Model);       
            table.HasMany(a => a.ClientGrantTypes_ClientIDList).WithOne(a => a.ClientID_Model);       
            table.HasMany(a => a.ClientUris_ClientiIDList).WithOne(a => a.ClientiID_Model);       
            table.HasMany(a => a.PayOrder_ClientIDList).WithOne(a => a.ClientID_Model);       
        
        
        
      
       }
	}
	
}