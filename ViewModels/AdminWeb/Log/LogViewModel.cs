// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： LogEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-09-15
// 负责人：杨小乐
// ===================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using ViewModels;

namespace ViewModels.AdminWeb.Log
{
	/// <summary>
	///Log数据实体
	/// </summary>
	public class LogViewModel : BaseViewModel
	{
		#region 公共属性
		///<summary>
		///主键
		///</summary>
		public Guid ID { get; set; }

        ///<summary>
        ///关键子,一般用于快速查找
        ///</summary>
        [Display(Name = "关键字")]
        public string KeyWord { get; set; }

        ///<summary>
        ///简短描述
        ///</summary>
        [Display(Name = "简短描述")]
        public string ShortMessage { get; set; }

        ///<summary>
        ///完整信息
        ///</summary>
        [Display(Name = "完整信息")]
        public string FullMessage { get; set; }

        ///<summary>
        ///日志等级
        ///</summary>
        [Display(Name = "日志等级")]
        public int LogLeve { get; set; }

	    ///<summary>
	    ///日志等级
	    ///</summary>
	    [Display(Name = "日志等级")]
	    public string LogLeveName
	    {
	        get
	        {
                if (Enum.TryParse(LogLeve.ToString(), true, out LogLevel logLevel))
                {
                    return logLevel.ToString();
                }
                return "";
	        }
	    }

        ///<summary>
        ///客户端IP地址
        ///</summary>

        [Display(Name = "客户端IP地址")]
        public string IpAddress { get; set; }

        ///<summary>
        ///服务器内网IP
        ///</summary>
        [Display(Name = "服务器内网IP")]
        public string ServerIpAddress { get; set; }

        ///<summary>
        ///记录日志的页面地址
        ///</summary>
        [Display(Name = "记录日志的页面地址")]
        public string PageUrl { get; set; }

        ///<summary>
        ///上个页面地址
        ///</summary>
        [Display(Name = "上个页面地址")]
        public string ReferrerUrl { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }
    
		#endregion	
	}
	
}