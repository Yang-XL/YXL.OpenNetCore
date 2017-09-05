// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： UserEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///User数据实体
	/// </summary>
	internal class UserMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<User> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.OrganizationID).IsRequired();
            table.Property(a=>a.Name).IsRequired();
            table.Property(a=>a.PyCode).IsRequired();
            table.Property(a=>a.LoginName).IsRequired();
            table.Property(a=>a.NormalizedLoginName).IsRequired();
            table.Property(a=>a.Password).IsRequired();
            table.Property(a=>a.LockoutEnabled).IsRequired();
            table.Property(a=>a.LockoutEnd).IsRequired();
            table.Property(a=>a.AccessFailedCount).IsRequired();
            table.Property(a=>a.MobilePhone);
            table.Property(a=>a.MobilePhoneConfirmed);
            table.Property(a=>a.NormalizedEmail);
            table.Property(a=>a.Email);
            table.Property(a=>a.EmailConfirmed);
            table.Property(a=>a.ImgUrl);
            table.Property(a=>a.CreateDate).IsRequired();
            table.Property(a=>a.UpdateDate).IsRequired();
       
              table.HasMany(a => a.UserClaim_UserIDList).WithOne(a => a.UserID_Model);       
              table.HasMany(a => a.UserLoginProvider_UserIDList).WithOne(a => a.UserID_Model);       
              table.HasMany(a => a.UserRole_UserIDList).WithOne(a => a.UserID_Model);       
              table.HasMany(a => a.UserToken_UserIDList).WithOne(a => a.UserID_Model);       
        
              table.HasOne(a => a.OrganizationID_Model).WithMany(a => a.User_OrganizationIDList);
        
        
      
       }
	}
	
}