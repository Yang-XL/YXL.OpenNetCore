using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Mvc;

namespace AdminSite.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService _userService;
        private readonly IOrganizationService _organizationService;

        public UserController(IUserService userService, IOrganizationService organizationService)
        {
            _userService = userService;
            _organizationService = organizationService;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 按照部门组织树形用户
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OrganizationZTreeUsers()
        {
            var users = await _userService.QueryAsync();
            var orgnization = await _organizationService.QueryAsync();
            var userList = from n in users select new {
                id = n.ID,
                pId = n.OrganizationID.ToString(),
                name =n.LockoutEnabled? $"< span style = 'margin-right:0px;' >{n.Name}</ span >< span style = 'color:red;margin-right:0px;' > (被锁定) </ span >" : n.Name,
                open = true,
                chkDisabled = true
            };
            var orgnizationList = from n in orgnization select new {
                id = n.ID,
                pId = n.ParentOrganizationID.ToString(),
                name = n.Name,
                open = true,
                chkDisabled = false
            };
            return Json(userList.Union(orgnizationList));
        }

        
    }
}