// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ClientUrisEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///ClientUris数据实体
	/// </summary>
	internal class ClientUrisMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<ClientUris> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.ClientiID).IsRequired();
            table.Property(a=>a.ClentUri).IsRequired();
            table.Property(a=>a.UriType).IsRequired();
       
        
            table.HasOne(a => a.ClientiID_Model).WithMany(a => a.ClientUris_ClientiIDList);
        
        
      
       }
	}
	
}