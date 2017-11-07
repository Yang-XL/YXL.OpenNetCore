// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： UserRoleJurisdictionEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///UserRoleJurisdiction数据实体
	/// </summary>
	public class UserRoleJurisdiction : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///角色
		///</summary>
		public Guid RoleID { get; set; }

		///<summary>
		///应用程序
		///</summary>
		public Guid ApplicationID { get; set; }

		///<summary>
		///菜单
		///</summary>
		public Guid MenuID { get; set; }

		///<summary>
		///创建时间
		///</summary>
		public DateTime CreateDate { get; set; }
    
		#endregion	
        
        #region 子表
        #endregion
        
        #region 父表
          public virtual Application ApplicationID_Model { get; set; }     
          public virtual Menu MenuID_Model { get; set; }     
          public virtual Role RoleID_Model { get; set; }     
        #endregion
        
        

	}
	
}