// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： PayOrderEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///PayOrder数据实体
	/// </summary>
	public class PayOrder : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///
		///</summary>
		public Guid ClientID { get; set; }

		///<summary>
		///
		///</summary>
		public byte[] OrderNumber { get; set; }

		///<summary>
		///
		///</summary>
		public decimal Amount { get; set; }

		///<summary>
		///
		///</summary>
		public string Remark { get; set; }

		///<summary>
		///
		///</summary>
		public DateTime CreateDate { get; set; }

		///<summary>
		///
		///</summary>
		public DateTime RecordDate { get; set; }
    
		#endregion	
        
        #region 子表
          public virtual IEnumerable<PaySerial> PaySerial_PayOrderIDList { get; set; }          
        #endregion
        
        #region 父表
          public virtual Client ClientID_Model { get; set; }     
        #endregion
        
        

	}
	
}