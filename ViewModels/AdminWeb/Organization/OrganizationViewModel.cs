using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels.AdminWeb.Organization
{

    /// <summary>
    ///OrganizationViewModel
    /// </summary>
    public class OrganizationViewModel : BaseViewModel
    {

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
        public string PyCode { get; set; }

        ///<summary>
        ///描述
        ///</summary>
        [Display(Name = "描述")]
        public string Description { get; set; }

        ///<summary>
        ///显示顺序
        ///</summary>
        [Display(Name = "显示顺序")]
        public int ShowIndex { get; set; }

        ///<summary>
        ///创建时间
        ///</summary>
        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }


        ///<summary>
        ///管理者、经理
        ///</summary>
        [Display(Name = "管理者、经理")]
        [Required(ErrorMessage = "此项不能为空")]
        public Guid? Leader { get; set; }
        [Display(Name = "管理者、经理")]
        public string LeaderName { get; set; }
        [Display(Name = "管理者、经理")]
        public IEnumerable<SelectListItem> LeaderSelectListItems { get; set; }


        /// <summary>
        /// 上级部门
        /// </summary>
        [Display(Name = "上级部门")]

        [Required(ErrorMessage = "此项不能为空")]
        public Guid ParentOrganizationID { get; set; }
        [Display(Name = "上级部门")]
        public string ParentOrganizationName { get; set; }

    }
}
