using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.OpenPlatform.ClientModel
{
    /// <summary>
    ///     ClientUrisViewModel
    /// </summary>
    public class ClientUrisViewModel : BaseViewModel
    {
        #region 基本属性

        /// <summary>
        ///     主键
        /// </summary>
        [Display(Name = "主键")]
        [Required(ErrorMessage = "主键不能为空")]
        public Guid ID { get; set; }

        /// <summary>
        ///     商户ID
        /// </summary>
        [Display(Name = "商户ID")]
        [Required(ErrorMessage = "商户ID不能为空")]
        public Guid ClientiID { get; set; }

        /// <summary>
        ///     授权地址
        /// </summary>
        [Display(Name = "授权地址")]
        [Required(ErrorMessage = "授权地址不能为空")]
        public string ClentUri { get; set; }

        /// <summary>
        ///     地址类型
        /// </summary>
        [Display(Name = "地址类型")]
        [Required(ErrorMessage = "地址类型不能为空")]
        public int UriType { get; set; }

        #endregion
    }
}