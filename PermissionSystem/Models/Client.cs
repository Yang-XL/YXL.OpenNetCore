// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： ClientEntity.cs
// 项目名称：Asp.Net Core 2.0 mvc 开源权限系统Demo 
// 创建时间：2017-10-26
// 负责人：杨小乐
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
        ///拼音码
        ///</summary>
        public string PyCode { get; set; }

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
        ///最后更新时间
        ///</summary>
        public DateTime UpdateDate { get; set; }

        ///<summary>
        ///名字
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///是否启用
        ///</summary>
        public bool Avaiable { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        public string Remark { get; set; }

        ///<summary>
        ///是否允许访问内应用接口
        ///</summary>
        public bool IsAllowApi { get; set; }

        ///<summary>
        ///是否允许支付
        ///</summary>
        public bool IsAllowPay { get; set; }

        #endregion

        #region 子表
        public virtual IEnumerable<ClientApi> ClientApi_ClientIDList { get; set; }
        public virtual IEnumerable<ClientGrantTypes> ClientGrantTypes_ClientIDList { get; set; }
        public virtual IEnumerable<ClientUris> ClientUris_ClientiIDList { get; set; }
        public virtual IEnumerable<PayOrder> PayOrder_ClientIDList { get; set; }
        #endregion

        #region 父表
        #endregion



    }

}