using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Services;
using IdentitySite.Common;
using IdentitySite.Services;
using IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViewModels.IdentitySite.Account;
using ViewModels.IdentitySite.Options;

namespace IdentitySite.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly IEventService _eventService;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ILogger _logger;
        private readonly AccountOption _options;
        private readonly IUserService _userService;

        public AccountController(IUserService userService,
            IIdentityServerInteractionService interaction,
            AccountService accountService,
            IOptions<AccountOption> options,
            ILoggerFactory loggerFactory,
            IHttpContextAccessor httpContext, IEventService eventService)
        {
            _userService = userService;
            _interaction = interaction;
            _accountService = accountService;
            _eventService = eventService;
            _options = options.Value;
            _logger = loggerFactory.CreateLogger<AccountController>();
            ;
        }


        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await _accountService.BuildLoginViewModelAsync(returnUrl);
            if (vm.IsExternalLoginOnly)
                return await ExternalLogin(vm.ExternalLoginScheme, returnUrl);
            return View(vm);
        }

        /// <summary>
        ///     Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                //验证用户名密码
                if (_userService.ValidateCredentials(model.LoginName, model.LoginPassword))
                {
                    AuthenticationProperties props = null;
                    //设置过期时间
                    if (_options.AllowRememberLogin && model.Remember)
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(_options.RememberMeLoginDuration)
                        };
                    ;

                    // issue authentication cookie with subject ID and username
                    var user = await _userService.SingleAsync(a => a.LoginName == model.LoginName);

                    //var identity = MembershipHelper.CreateIdentity(user);
                    //await _eventService.RaiseAsync(new UserLoginSuccessEvent(user.LoginName, user.ID.ToString(), user.Name));
                    await HttpContext.SignInAsync(user.ID.ToString(), user.Name, props, MembershipHelper.Claims(user));

                    var result = _interaction.IsValidReturnUrl(model.ReturnUrl);
                    return Redirect(result ? model.ReturnUrl : "~/");
                }
                ModelState.AddModelError(nameof(model.LoginName), _options.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            var vm = await _accountService.BuildLoginViewModelAsync(model);
            return View(vm);
        }


        /// <summary>
        ///     Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = await _accountService.BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
                return await Logout(vm);

            return View(vm);
        }

        /// <summary>
        ///     Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            var vm = await _accountService.BuildLoggedOutViewModelAsync(model.LogoutId);
            if (vm.TriggerExternalSignout)
            {
                var url = Url.Action("Logout", new {logoutId = vm.LogoutId});
                try
                {
                    // hack: try/catch to handle social providers that throw
                    await HttpContext.SignOutAsync(vm.ExternalAuthenticationScheme,
                        new AuthenticationProperties {RedirectUri = url});
                }
                catch (NotSupportedException) // this is for the external providers that don't have signout
                {
                }
                catch (InvalidOperationException) // this is for Windows/Negotiate
                {
                }
            }

            // delete local authentication cookie
            await HttpContext.SignOutAsync();

            return View("LoggedOut", vm);
        }

        /// <summary>
        ///     initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            returnUrl = Url.Action("ExternalLoginCallback", new {returnUrl});

            // windows authentication is modeled as external in the asp.net core authentication manager, so we need special handling
            if (_options.WindowsAuthenticationSchemes.Contains(provider))
                if (HttpContext.User is WindowsPrincipal)
                {
                    var props = new AuthenticationProperties();
                    props.Items.Add("scheme", HttpContext.User.Identity.AuthenticationType);

                    var id = new ClaimsIdentity(provider);
                    id.AddClaim(new Claim(ClaimTypes.NameIdentifier, HttpContext.User.Identity.Name));
                    id.AddClaim(new Claim(ClaimTypes.Name, HttpContext.User.Identity.Name));

                    await HttpContext.SignInAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme,
                        new ClaimsPrincipal(id), props);
                    return Redirect(returnUrl);
                }
                else
                {
                    // this triggers all of the windows auth schemes we're supporting so the browser can use what it supports
                    return new ChallengeResult(_options.WindowsAuthenticationSchemes);
                }
            {
                // start challenge and roundtrip the return URL
                var props = new AuthenticationProperties
                {
                    RedirectUri = returnUrl,
                    Items = {{"scheme", provider}}
                };
                return new ChallengeResult(provider, props);
            }
        }

        /// <summary>
        ///     Post processing of external authentication
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            // read external identity from the temporary cookie
            var info = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            var tempUser = info?.Principal;
            if (tempUser == null)
                throw new Exception("External authentication error");

            // retrieve claims of the external user
            var claims = tempUser.Claims.ToList();

            // try to determine the unique id of the external user - the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            if (userIdClaim == null)
                userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new Exception("Unknown userid");

            // remove the user id claim from the claims collection and move to the userId property
            // also set the name of the external authentication provider
            claims.Remove(userIdClaim);
            var provider = info.Properties.Items["scheme"];
            var userId = userIdClaim.Value;

            // check if the external user is already provisioned
            var user = await _userService.SingleAsync(a => a.ID == new Guid(userId));
            //var user = _users.FindByExternalProvider(provider, userId);
            if (user == null)
            {
                // this sample simply auto-provisions new external user
                // another common approach is to start a registrations workflow first
                //user = _users.AutoProvisionUser(provider, userId, claims);
            }

            var additionalClaims = new List<Claim>();

            // if the external system sent a session id claim, copy it over
            var sid = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
                additionalClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));

            // if the external provider issued an id_token, we'll keep it for signout
            AuthenticationProperties props = null;
            var id_token = info.Properties.GetTokenValue("id_token");
            if (id_token != null)
            {
                props = new AuthenticationProperties();
                props.StoreTokens(new[] {new AuthenticationToken {Name = "id_token", Value = id_token}});
            }

            // issue authentication cookie for user
            //await  HttpContext.SignInAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme, additionalClaims.ToArray())
            //await HttpContext.SignInAsync(user.ID.ToString(), user.Name, provider, props, additionalClaims.ToArray());

            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

            // validate return URL and redirect back to authorization endpoint
            if (_interaction.IsValidReturnUrl(returnUrl))
                return Redirect(returnUrl);

            return Redirect("~/");
        }
    }
}