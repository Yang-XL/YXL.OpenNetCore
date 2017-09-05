// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ClientEntity.cs
// 项目名称： 
// 创建时间：2017-08-28
// 负责人：YXL
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///Client数据实体
	/// </summary>
	public class Client : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///AppKey
		///</summary>
		public string AppKey { get; set; }

		///<summary>
		///秘钥
		///</summary>
		public string AppSecrets { get; set; }

		///<summary>
		///创建时间
		///</summary>
		public DateTime CreateDate { get; set; }

		///<summary>
		///主域名
		///</summary>
		public string ClientUri { get; set; }

		///<summary>
		///最后更新时间
		///</summary>
		public DateTime UpdateDate { get; set; }

		///<summary>
		///
		///</summary>
		public string Name { get; set; }

		///<summary>
		///
		///</summary>
		public string LogOutUri { get; set; }
    
		#endregion	
        
        #region 子表
          public virtual IEnumerable<ClientApi> ClientApi_ClientIDList { get; set; }          
          public virtual IEnumerable<ClientGrantTypes> ClientGrantTypes_ClientIDList { get; set; }          
        #endregion
        
        #region 父表
        #endregion
        
        

	}
	
}