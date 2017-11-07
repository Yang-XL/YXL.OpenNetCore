// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： PaySerialEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PermissionSystem.Models;
namespace PermissionSystem.Mapping
{
	/// <summary>
	///PaySerial数据实体
	/// </summary>
	internal class PaySerialMapping
	{  
        
       public static void Mapping(EntityTypeBuilder<PaySerial> table)
       {
            table.HasKey(a=>a.ID); 
            table.Property(a=>a.ID).IsRequired();
            table.Property(a=>a.PayOrderID).IsRequired();
            table.Property(a=>a.Amount).IsRequired();
            table.Property(a=>a.SerialNumber);
            table.Property(a=>a.IsSuccess).IsRequired();
            table.Property(a=>a.RecordDate).IsRequired();
       
        
            table.HasOne(a => a.PayOrderID_Model).WithMany(a => a.PaySerial_PayOrderIDList);
        
        
      
       }
	}
	
}