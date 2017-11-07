// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ClientUrisEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///ClientUris数据实体
	/// </summary>
	public class ClientUris : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///商户ID
		///</summary>
		public Guid ClientiID { get; set; }

		///<summary>
		///授权地址
		///</summary>
		public string ClentUri { get; set; }

		///<summary>
		///地址类型
		///</summary>
		public int UriType { get; set; }
    
		#endregion	
        
        #region 子表
        #endregion
        
        #region 父表
          public virtual Client ClientiID_Model { get; set; }     
        #endregion
        
        

	}
	
}