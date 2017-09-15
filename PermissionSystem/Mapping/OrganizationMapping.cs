// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： OrganizationEntity.cs
// 项目名称： 
// 创建时间：2017-09-13
// 负责人：YXL
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///Organization数据实体
	/// </summary>
	internal class OrganizationMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<Organization> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.Code).IsRequired();
            table.Property(a=>a.Name).IsRequired();
            table.Property(a=>a.PyCode).IsRequired();
            table.Property(a=>a.Description);
            table.Property(a=>a.ShowIndex);
            table.Property(a=>a.CreateDate).IsRequired();
            table.Property(a=>a.Leader);
            table.Property(a=>a.ParentOrganizationID);
           table.HasMany(a => a.User_OrganizationIDList).WithOne(a => a.OrganizationID_Model);       
        
        
        
      
       }
	}
	
}