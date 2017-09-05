// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ClientGrantTypesEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///ClientGrantTypes数据实体
	/// </summary>
	public class ClientGrantTypes : BaseEntity
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
		public Guid GrantTypeID { get; set; }
    
		#endregion	
        
        #region 子表
        #endregion
        
        #region 父表
          public virtual Client ClientID_Model { get; set; }     
          public virtual GrantType GrantTypeID_Model { get; set; }     
        #endregion
        
        

	}
	
}