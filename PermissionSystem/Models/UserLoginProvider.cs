// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： UserLoginProviderEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///UserLoginProvider数据实体
	/// </summary>
	public class UserLoginProvider : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///登录提供商（QQ、微信）
		///</summary>
		public string LoginProvider { get; set; }

		///<summary>
		///登录的唯一提供商标识。
		///</summary>
		public string ProviderKey { get; set; }

		///<summary>
		///提供方给定显示名称
		///</summary>
		public string ProviderDisplayName { get; set; }

		///<summary>
		///
		///</summary>
		public Guid UserID { get; set; }
    
		#endregion	
        
        #region 子表
        #endregion
        
        #region 父表
          public virtual User UserID_Model { get; set; }     
        #endregion
        
        

	}
	
}