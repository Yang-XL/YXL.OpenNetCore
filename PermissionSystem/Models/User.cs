// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： UserEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///User数据实体
	/// </summary>
	public class User : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///公司ID
		///</summary>
		public Guid OrganizationID { get; set; }

		///<summary>
		///名称
		///</summary>
		public string Name { get; set; }

		///<summary>
		///拼音码
		///</summary>
		public string PyCode { get; set; }

		///<summary>
		///登录名
		///</summary>
		public string LoginName { get; set; }

		///<summary>
		///标准化登录名，和Login区分尽是全大写
		///</summary>
		public string NormalizedLoginName { get; set; }

		///<summary>
		///密码
		///</summary>
		public string Password { get; set; }

		///<summary>
		///是否锁定
		///</summary>
		public bool LockoutEnabled { get; set; }

		///<summary>
		///锁定结束时间
		///</summary>
		public DateTime LockoutEnd { get; set; }

		///<summary>
		///尝试登陆次数
		///</summary>
		public int AccessFailedCount { get; set; }

		///<summary>
		///手机号
		///</summary>
		public string MobilePhone { get; set; }

		///<summary>
		///手机号是否确认
		///</summary>
		public bool MobilePhoneConfirmed { get; set; }

		///<summary>
		///电子邮箱
		///</summary>
		public string NormalizedEmail { get; set; }

		///<summary>
		///电子邮箱
		///</summary>
		public string Email { get; set; }

		///<summary>
		///电子邮箱是否确认
		///</summary>
		public bool EmailConfirmed { get; set; }

		///<summary>
		///用户头像
		///</summary>
		public string ImgUrl { get; set; }

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
          public virtual IEnumerable<UserRole> UserRole_UserIDList { get; set; }          
        #endregion
        
        #region 父表
          public virtual Organization OrganizationID_Model { get; set; }     
        #endregion
        
        

	}
	
}