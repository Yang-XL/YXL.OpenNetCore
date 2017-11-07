// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： MenuEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///Menu数据实体
	/// </summary>
	public class Menu : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///应用程序外键
		///</summary>
		public Guid ApplicationID { get; set; }

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
		///导航菜单显示在导航条
		///</summary>
		public bool IsNav { get; set; }

		///<summary>
		///父级菜单
		///</summary>
		public Guid ParentID { get; set; }

		///<summary>
		///显示顺序
		///</summary>
		public int ShowIndex { get; set; }

		///<summary>
		///区域
		///</summary>
		public string AreaName { get; set; }

		///<summary>
		///控制器
		///</summary>
		public string ControllerName { get; set; }

		///<summary>
		///方法
		///</summary>
		public string ActionName { get; set; }

		///<summary>
		///描述
		///</summary>
		public string Description { get; set; }

		///<summary>
		///图标样式
		///</summary>
		public string IconCss { get; set; }

		///<summary>
		///创建时间
		///</summary>
		public DateTime CreateDate { get; set; }
    
		#endregion	
        
        #region 子表
          public virtual IEnumerable<UserRoleJurisdiction> UserRoleJurisdiction_MenuIDList { get; set; }          
        #endregion
        
        #region 父表
          public virtual Application ApplicationID_Model { get; set; }     
        #endregion
        
        

	}
	
}