// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： OrganizationEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///Organization数据实体
	/// </summary>
	public class Organization : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///编码
		///</summary>
		public string Code { get; set; }

		///<summary>
		///名称
		///</summary>
		public string Name { get; set; }

		///<summary>
		///拼音码
		///</summary>
		public string PyCode { get; set; }

		///<summary>
		///描述
		///</summary>
		public string Description { get; set; }

		///<summary>
		///显示顺序
		///</summary>
		public int ShowIndex { get; set; }

		///<summary>
		///创建时间
		///</summary>
		public DateTime CreateDate { get; set; }

		///<summary>
		///管理者、经理
		///</summary>
		public Guid Leader { get; set; }
    
		#endregion	
        
        #region 子表
          public virtual IEnumerable<User> User_OrganizationIDList { get; set; }          
        #endregion
        
        #region 父表
        #endregion
        
        

	}
	
}