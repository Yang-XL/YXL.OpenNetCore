using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.OpenPlatform.ClientModel
{

    /// <summary>
    ///ClientViewModel
    /// </summary>
    public class ClientViewModel : BaseViewModel
    {
        #region 基本属性
        ///<summary>
        ///主键
        ///</summary>
        [Display(Name = "主键")]
        [Required(ErrorMessage = "主键不能为空")]
        public Guid ID { get; set; }

        ///<summary>
        ///拼音码
        ///</summary>
        [Display(Name = "拼音码")]
        public string PyCode { get; set; }

        ///<summary>
        ///AppKey
        ///</summary>
        [Display(Name = "AppKey")]
        [Required(ErrorMessage = "AppKey不能为空")]
        public string AppKey { get; set; }

        ///<summary>
        ///秘钥
        ///</summary>
        [Display(Name = "AppSecrets")]
        [Required(ErrorMessage = "AppSecrets不能为空")]
        public string AppSecrets { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }

        ///<summary>
        ///最后更新时间
        ///</summary>
        [Display(Name = "最后更新时间")]
        public DateTime UpdateDate { get; set; }

        ///<summary>
        ///名字
        ///</summary>
        [Display(Name = "名字")]
        [Required(ErrorMessage = "名字不能为空")]
        public string Name { get; set; }

        ///<summary>
        ///是否启用
        ///</summary>
        [Display(Name = "是否启用")]
        [Required(ErrorMessage = "是否启用不能为空")]
        public bool Avaiable { get; set; }

        ///<summary>
        ///备注
        ///</summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        ///<summary>
        ///是否允许访问内应用接口
        ///</summary>
        [Display(Name = "是否开通接口访问")]
        [Required(ErrorMessage = "是否允许访问内应用接口不能为空")]
        public bool IsAllowApi { get; set; }

        ///<summary>
        ///是否允许支付
        ///</summary>
        [Display(Name = "是否开通支付中心")]
        [Required(ErrorMessage = "是否允许支付不能为空")]
        public bool IsAllowPay { get; set; }
        #endregion

        public IEnumerable<ClientUrisViewModel> Uris;
    }
}
