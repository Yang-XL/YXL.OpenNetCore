// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： GrantTypeEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///GrantType数据实体
	/// </summary>
	public class GrantType : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///名称
		///</summary>
		public string Name { get; set; }

		///<summary>
		///枚举名
		///</summary>
		public string ValueData { get; set; }
    
		#endregion	
        
        #region 子表
          public virtual IEnumerable<ClientGrantTypes> ClientGrantTypes_GrantTypeIDList { get; set; }          
        #endregion
        
        #region 父表
        #endregion
        
        

	}
	
}