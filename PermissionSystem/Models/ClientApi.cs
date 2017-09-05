// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ClientApiEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///ClientApi数据实体
	/// </summary>
	public class ClientApi : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid 主键 { get; set; }

		///<summary>
		///最大调用次数
		///</summary>
		public int MaxInvokeCount { get; set; }

		///<summary>
		///已调用次数
		///</summary>
		public int InvokeCount { get; set; }

		///<summary>
		///创建时间
		///</summary>
		public DateTime CreateDate { get; set; }

		///<summary>
		///更新时间
		///</summary>
		public DateTime UpdateDate { get; set; }

		///<summary>
		///接口外键
		///</summary>
		public Guid ApiID { get; set; }

		///<summary>
		///客户端外键
		///</summary>
		public Guid ClientID { get; set; }
    
		#endregion	
        
        #region 子表
        #endregion
        
        #region 父表
          public virtual Api ApiID_Model { get; set; }     
          public virtual Client ClientID_Model { get; set; }     
        #endregion
        
        

	}
	
}