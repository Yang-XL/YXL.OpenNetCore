using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels.AdminWeb.Application
{
   public  class ApplicationViewModel : BaseViewModel
    {
        #region 公共属性
        ///<summary>
        ///主键
        ///</summary>
        [Display(Name = "主键")]
        
        public Guid ID { get; set; }

        ///<summary>
        ///编码
        ///</summary>
        [Display(Name = "编码")]
        [Required(ErrorMessage = "编码不允许为空")]
        public string Code { get; set; }

        ///<summary>
        ///名称
        ///</summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = "名称不允许为空")]
        public string Name { get; set; }

        ///<summary>
        ///拼音码
        ///</summary>
        [Display(Name = "拼音码")]
        public string PyCode { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        [Display(Name = "创建时间")]
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
