// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ApplicationEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///Application数据实体
	/// </summary>
	internal class ApplicationMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<Application> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.Code).IsRequired();
            table.Property(a=>a.Name).IsRequired();
            table.Property(a=>a.PyCode).IsRequired();
            table.Property(a=>a.CreateDate).IsRequired();
            table.Property(a=>a.ShowIndex);
            table.Property(a=>a.Description);
       
              table.HasMany(a => a.Api_ApplicationIDList).WithOne(a => a.ApplicationID_Model);       
              table.HasMany(a => a.Menu_ApplicationIDList).WithOne(a => a.ApplicationID_Model);       
              table.HasMany(a => a.UserRoleJurisdiction_ApplicationIDList).WithOne(a => a.ApplicationID_Model);       
        
        
        
      
       }
	}
	
}