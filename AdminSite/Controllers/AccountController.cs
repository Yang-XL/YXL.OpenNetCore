using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminSite.SiteAttributes;
using IdentityServer4.Extensions;
using IService;
using IService.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PermissionSystem.Models;
using ViewModels.AdminWeb.Login;

namespace AdminSite.Controllers
{
    
    public class AccountController : BaseAdminController
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public AccountController(IUserService userService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
        }
    
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            var mode = new LoginViewModel{ReturnUrl = ReturnUrl };
            return View(mode);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userService.SingleAsync(a => a.NormalizedLoginName == model.NormalizedLoginName);
            if (user == null)
            {
                ModelState.TryAddModelError(nameof(model.CustomizeErrorMessage), "用户名或密码错误");
                return View(model);
            }
            if (user.Password != model.LoginPassword)
            {
                user.AccessFailedCount += 1;
                await _userService.UpdateAsync(user);
                ModelState.TryAddModelError(nameof(model.CustomizeErrorMessage), "用户名或密码错误");
                return View(model);
            }
            if (user.LockoutEnd > DateTime.Now || user.LockoutEnabled)
            {
                user.AccessFailedCount += 1;
                await _userService.UpdateAsync(user);
                ModelState.TryAddModelError(nameof(model.CustomizeErrorMessage), "账户被锁定");
                return View(model);
            }

            var roles = await _userRoleService.QueryAsync(a => a.UserID == user.ID);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleID.ToString()));
            }
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), new AuthenticationProperties {IsPersistent = model.Remember});
        
            if (!string.IsNullOrEmpty(model.ReturnUrl))
                return Redirect(model.ReturnUrl);
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}