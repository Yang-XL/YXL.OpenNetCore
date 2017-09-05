using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using PermissionSystem.Models;

namespace ViewModels.AdminWeb.Nav
{
    public class MenuViewModel : BaseViewModel
    {
        [Display(Name = "主键")]
        public Guid ID { get; set; }


        [Display(Name = "编码")]
        public string Code { get; set; }


        [Display(Name = "名称")]
        public string Name { get; set; }


        [Display(Name = "拼音码")]
        public string PyCode { get; set; }


        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }


        [Display(Name = "显示顺序")]
        public int ShowIndex { get; set; }

        [Display(Name = "区域名")]
        public string AreaName { get; set; }

        [Display(Name = "控制器")]
        public string ControllerName { get; set; }


        [Display(Name = "方法")]
        public string ActionName { get; set; }


        [Display(Name = "描述")]
        public string Description { get; set; }


        [Display(Name = "图标样式")]
        public string IconCss { get; set; }


        [Display(Name = "导航菜单", Description = "导航菜单：将显示在导航，非导航菜：为上级菜单的功能权限菜单")]
        public bool IsNav { get; set; }

        [Display(Name = "应用程序", Description = "菜单所属应用程序名称")]
        public string ApplicationName { get; set; }

        [Display(Name = "父级菜单", Description = "父级菜单")]
        public string ParentMenuName { get; set; }


        [Display(Name = "应用程序外键")]
        public Guid ApplicationID { get; set; }
        

        [Display(Name = "父级菜单")]
        public Guid ParentID { get; set; }
        


        [Display(Name = "父级权限", Description = "父级权限")]
        public Guid ParentAuthoritys { get; set; }
        
    }
}