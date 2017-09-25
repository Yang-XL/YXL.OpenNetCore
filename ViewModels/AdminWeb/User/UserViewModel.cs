using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels.AdminWeb.User
{
    public class UserViewModel : BaseViewModel
    {
        #region 基本属性

        /// <summary>
        ///     主键
        /// </summary>
        [Required(ErrorMessage = "此项不能为空")]
        [Display(Name = "主键")]
        public Guid ID { get; set; }

        /// <summary>
        ///     公司ID
        /// </summary>
        [Required(ErrorMessage = "此项不能为空")]
        [Display(Name = "公司ID")]
        public Guid OrganizationID { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        [Required(ErrorMessage = "此项不能为空")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        ///     拼音码
        /// </summary>
        [Display(Name = "拼音码")]
        public string PyCode { get; set; }

        /// <summary>
        ///     登录名
        /// </summary>
        [Required(ErrorMessage = "此项不能为空")]
        [Display(Name = "登录名")]
        public string LoginName { get; set; }

        /// <summary>
        ///     密码
        /// </summary>
        [Required(ErrorMessage = "此项不能为空")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        ///     是否锁定
        /// </summary>
        [Display(Name = "是否锁定")]
        public bool LockoutEnabled { get; set; }

        /// <summary>
        ///     锁定结束时间
        /// </summary>
        [Display(Name = "解锁时间")]
        public DateTime LockoutEnd { get; set; }

        /// <summary>
        ///     尝试登陆次数
        /// </summary>
        [Display(Name = "尝试登陆次数")]
        public int AccessFailedCount { get; set; }

        /// <summary>
        ///     手机号
        /// </summary>
        [Display(Name = "手机号")]
        public string MobilePhone { get; set; }

        /// <summary>
        ///     手机号是否确认
        /// </summary>
        [Display(Name = "手机号是否确认")]
        public bool MobilePhoneConfirmed { get; set; }

        /// <summary>
        ///     电子邮箱
        /// </summary>
        [Display(Name = "电子邮箱")]
        public string Email { get; set; }

        /// <summary>
        ///     电子邮箱是否确认
        /// </summary>
        [Display(Name = "手机号是否确认")]
        public bool EmailConfirmed { get; set; }

        /// <summary>
        ///     用户头像
        /// </summary>
        [Display(Name = "手机号是否确认")]
        public string ImgUrl { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime UpdateDate { get; set; }

        #endregion


        #region View 属性

        /// <summary>
        ///     公司ID
        /// </summary>
        [Display(Name = "所属部门")]
        public string OrganizationName { get; set; }

        
        [Display(Name = "用户角色")]
        public string RoleID { get; set; }
        /// <summary>
        /// 用户角色绑定列表
        /// </summary>
        public IEnumerable<SelectListItem> Roles { get; set; }
        

        #endregion
    }
}