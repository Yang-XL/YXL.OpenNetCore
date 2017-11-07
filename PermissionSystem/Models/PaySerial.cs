// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： PaySerialEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///PaySerial数据实体
	/// </summary>
	public class PaySerial : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///
		///</summary>
		public Guid PayOrderID { get; set; }

		///<summary>
		///
		///</summary>
		public decimal Amount { get; set; }

		///<summary>
		///
		///</summary>
		public decimal SerialNumber { get; set; }

		///<summary>
		///
		///</summary>
		public bool IsSuccess { get; set; }

		///<summary>
		///
		///</summary>
		public DateTime RecordDate { get; set; }
    
		#endregion	
        
        #region 子表
        #endregion
        
        #region 父表
          public virtual PayOrder PayOrderID_Model { get; set; }     
        #endregion
        
        

	}
	
}