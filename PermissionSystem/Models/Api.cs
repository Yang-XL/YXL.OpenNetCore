// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ApiEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///Api数据实体
	/// </summary>
	public class Api : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid Id { get; set; }

		///<summary>
		///方法名
		///</summary>
		public string MethodName { get; set; }

		///<summary>
		///名字
		///</summary>
		public string DisplayName { get; set; }

		///<summary>
		///区域
		///</summary>
		public string Area { get; set; }

		///<summary>
		///控制器
		///</summary>
		public string Controller { get; set; }

		///<summary>
		///方法
		///</summary>
		public string Action { get; set; }

		///<summary>
		///程序集
		///</summary>
		public string Assembly { get; set; }

		///<summary>
		///应用外键
		///</summary>
		public Guid ApplicationID { get; set; }

		///<summary>
		///创建时间
		///</summary>
		public DateTime CreateDate { get; set; }

		///<summary>
		///更新时间
		///</summary>
		public DateTime UpdateDate { get; set; }

		///<summary>
		///IdentityService4区分为普通Api资源和身份资源, identity 为0 api 为1
		///</summary>
		public int ApiType { get; set; }
    
		#endregion	
        
        #region 子表
          public virtual IEnumerable<ClientApi> ClientApi_ApiIDList { get; set; }          
        #endregion
        
        #region 父表
          public virtual Application ApplicationID_Model { get; set; }     
        #endregion
        
        

	}
	
}