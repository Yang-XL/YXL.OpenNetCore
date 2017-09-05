// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： UserClaimEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///UserClaim数据实体
	/// </summary>
	internal class UserClaimMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<UserClaim> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.UserID).IsRequired();
            table.Property(a=>a.ClaimType).IsRequired();
            table.Property(a=>a.ClaimValue).IsRequired();
            table.Property(a=>a.CreateDate).IsRequired();
            table.Property(a=>a.UpdateDate).IsRequired();
       
        
              table.HasOne(a => a.UserID_Model).WithMany(a => a.UserClaim_UserIDList);
        
        
      
       }
	}
	
}