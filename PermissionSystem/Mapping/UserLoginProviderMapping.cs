// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： UserLoginProviderEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///UserLoginProvider数据实体
	/// </summary>
	internal class UserLoginProviderMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<UserLoginProvider> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.LoginProvider).IsRequired();
            table.Property(a=>a.ProviderKey).IsRequired();
            table.Property(a=>a.ProviderDisplayName);
            table.Property(a=>a.UserID).IsRequired();
       
        
              table.HasOne(a => a.UserID_Model).WithMany(a => a.UserLoginProvider_UserIDList);
        
        
      
       }
	}
	
}