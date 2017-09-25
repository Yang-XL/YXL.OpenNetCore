using System;
using System.Collections.Generic;
using System.Text;
using Core.Repository.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mongo.Models
{
    public  class PermissionSystemLogs : BaseModel
    {
        #region 公共属性
       

        ///<summary>
        ///关键子,一般用于快速查找
        ///</summary>
        public string KeyWord { get; set; }

        ///<summary>
        ///简短描述
        ///</summary>
        public string ShortMessage { get; set; }

        ///<summary>
        ///完整信息
        ///</summary>
        public string FullMessage { get; set; }

        ///<summary>
        ///日志等级
        ///</summary>
        public int LogLeve { get; set; }

        ///<summary>
        ///客户端IP地址
        ///</summary>
        public string IpAddress { get; set; }

        ///<summary>
        ///服务器内网IP
        ///</summary>
        public string ServerIpAddress { get; set; }

        ///<summary>
        ///记录日志的页面地址
        ///</summary>
        public string PageUrl { get; set; }

        ///<summary>
        ///上个页面地址
        ///</summary>
        public string ReferrerUrl { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        public DateTime CreateDate { get; set; }
        
        #endregion
       
    }
}
