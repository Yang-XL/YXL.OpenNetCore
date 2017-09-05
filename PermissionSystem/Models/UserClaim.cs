// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： UserClaimEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///UserClaim数据实体
	/// </summary>
	public class UserClaim : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///用户外键
		///</summary>
		public Guid UserID { get; set; }

		///<summary>
		///类型
		///</summary>
		public string ClaimType { get; set; }

		///<summary>
		///值
		///</summary>
		public string ClaimValue { get; set; }

		///<summary>
		///创建时间
		///</summary>
		public DateTime CreateDate { get; set; }

		///<summary>
		///最后修改时间
		///</summary>
		public DateTime UpdateDate { get; set; }
    
		#endregion	
        
        #region 子表
        #endregion
        
        #region 父表
          public virtual User UserID_Model { get; set; }     
        #endregion
        
        

	}
	
}