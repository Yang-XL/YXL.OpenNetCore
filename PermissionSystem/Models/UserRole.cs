// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： UserRoleEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///UserRole数据实体
	/// </summary>
	public class UserRole : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///用户主键
		///</summary>
		public Guid UserID { get; set; }

		///<summary>
		///角色主键
		///</summary>
		public Guid RoleID { get; set; }

		///<summary>
		///创建时间
		///</summary>
		public DateTime CreateDate { get; set; }
    
		#endregion	
        
        #region 子表
        #endregion
        
        #region 父表
          public virtual Role RoleID_Model { get; set; }     
          public virtual User UserID_Model { get; set; }     
        #endregion
        
        

	}
	
}