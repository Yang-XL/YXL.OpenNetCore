// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ApplicationParameterEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-25
// 负责人：杨小乐
// ===================================================================
using System;
using System.Collections.Generic;
namespace PermissionSystem.Models
{
	/// <summary>
	///ApplicationParameter数据实体
	/// </summary>
	public class ApplicationParameter : BaseEntity
	{
		
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

		///<summary>
		///编码
		///</summary>
		public string KeyValue { get; set; }

		///<summary>
		///名称
		///</summary>
		public string DataValue { get; set; }

		///<summary>
		///描述
		///</summary>
		public string Description { get; set; }
    
		#endregion	
        
        #region 子表
        #endregion
        
        #region 父表
        #endregion
        
        

	}
	
}