// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： LogEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///Log数据实体
	/// </summary>
	public class Log : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///创建时间
		///</summary>
		public DateTime CreateDate { get; set; }

		///<summary>
		///日志等级
		///</summary>
		public int LogLeve { get; set; }

		///<summary>
		///简短描述
		///</summary>
		public string ShortMessage { get; set; }

		///<summary>
		///完整信息
		///</summary>
		public string FullMessage { get; set; }

		///<summary>
		///客户端IP地址
		///</summary>
		public string IpAddress { get; set; }

		///<summary>
		///记录日志的页面地址
		///</summary>
		public string PageUrl { get; set; }

		///<summary>
		///上个页面地址
		///</summary>
		public string ReferrerUrl { get; set; }

		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }
    
		#endregion	
        
        #region 子表
        #endregion
        
        #region 父表
        #endregion
        
        

	}
	
}