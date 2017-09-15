// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010
// 文件： RoleEntity.cs
// 项目名称： 
// 创建时间：2017-08-30
// 负责人：YXL
// ===================================================================
using System;
using System.ComponentModel.DataAnnotations;
using ViewModels;

namespace ViewModels.AdminWeb.Roles
{
    /// <summary>
    ///RoleViewModel
    /// </summary>
    public class RoleViewModel : BaseViewModel
    {

        #region 公共属性
        ///<summary>
        ///主键
        ///</summary>
        [Display(Name = "主键")]
        [Required(ErrorMessage = "此项不能为空")]
        public Guid ID { get; set; }

        ///<summary>
        ///编码
        ///</summary>
        [Display(Name = "编码")]
        [Required(ErrorMessage = "此项不能为空")]
        public string Code { get; set; }

        ///<summary>
        ///名称
        ///</summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "此项不能为空")]
        public string Name { get; set; }

        ///<summary>
        ///拼音码
        ///</summary>
        [Display(Name = "拼音码")]
        [Required(ErrorMessage = "此项不能为空")]
        public string PyCode { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        [Display(Name = "创建时间")]
        [Required(ErrorMessage = "此项不能为空")]
        public DateTime CreateDate { get; set; }

        ///<summary>
        ///显示顺序
        ///</summary>
        [Display(Name = "显示顺序")]
        public int ShowIndex { get; set; }

        ///<summary>
        ///描述
        ///</summary>
        [Display(Name = "描述")]
        public string Description { get; set; }

        #endregion
    }

}