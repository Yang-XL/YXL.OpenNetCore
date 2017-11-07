using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.AdminWeb.Login;

namespace AdminSite.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseAdminController
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IUserService _userService;

        public AccountController(IUserService userService, IUserRoleService userRoleService,
            IServiceProvider serviceProvider)
        {
            _userService = userService;
            _userRoleService = userRoleService;
        }

        public IActionResult Login(string ReturnUrl)
        {
            var mode = new LoginViewModel {ReturnUrl = ReturnUrl};
            return View(mode);
        }

        [HttpPost]
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
                identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleID.ToString()));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), new AuthenticationProperties {IsPersistent = model.Remember});

            if (!string.IsNullOrEmpty(model.ReturnUrl))
                return Redirect(model.ReturnUrl);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult NotFind()
        {
            return View();
        }
    }
}