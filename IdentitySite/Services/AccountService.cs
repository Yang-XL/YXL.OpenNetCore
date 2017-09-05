using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using  Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ViewModels.IdentitySite.Account;
using ViewModels.IdentitySite.Options;

namespace IdentitySite.Services
{
    public class AccountService
    {
        private readonly IClientStore _clientStore;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly AccountOption _options;
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        public AccountService(IHttpContextAccessor httpContextAccessor, 
            IIdentityServerInteractionService interaction,
            IClientStore clientStore, IOptions<AccountOption> options, IAuthenticationSchemeProvider schemeProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _options = options.Value;
        }

        public async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null)
                return new LoginViewModel
                {
                    EnableLocalLogin = false,
                    ReturnUrl = returnUrl,
                    LoginName = context.LoginHint,
                    ExternalProviders = new[] {new ExternalProvider {AuthenticationScheme = context.IdP}}
                };


            //var schemes = _httpContextAccessor.HttpContext.Authentication.GetAuthenticationSchemes();
            var schemes = await _schemeProvider.GetRequestHandlerSchemesAsync();
            //非Windows登录的provider
               var providers = (from n in schemes
                where !string.IsNullOrEmpty(n.DisplayName) &&
                      !_options.WindowsAuthenticationSchemes.Contains(n.Name)
                select new ExternalProvider
                {
                    DisplayName = n.DisplayName,
                    AuthenticationScheme = n.Name
                }).ToList();


            //如果允许Windows登录。且用户已经用Windows登录过。直接放行，返回原windows provider
            if (_options.WindowsAuthenticationEnabled)
            {
                var windowsSchemes =
                    schemes.Where(s => _options.WindowsAuthenticationSchemes.Contains(s.Name));
                if (windowsSchemes.Any())
                    providers.Add(new ExternalProvider
                    {
                        AuthenticationScheme = _options.WindowsAuthenticationSchemes.First(),
                        DisplayName = _options.WindowsAuthenticationDisplayName
                    });
            }

            var allowLocal = true;
            if (context?.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;
                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                        providers = providers
                            .Where(provider => client.IdentityProviderRestrictions.Contains(provider
                                .AuthenticationScheme)).ToList();
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = _options.AllowRememberLogin,
                EnableLocalLogin = allowLocal && _options.AllowLocalLogin,
                ReturnUrl = returnUrl,
                LoginName = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        public async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.LoginName = model.LoginName;
            vm.Remember = model.Remember;
            return vm;
        }

        public async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel {LogoutId = logoutId, ShowLogoutPrompt = _options.ShowLogoutPrompt};

            var user = await _httpContextAccessor.HttpContext.GetIdentityServerUserAsync();
            if (user == null || user.Identity.IsAuthenticated == false)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        public async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);
            var client = await _clientStore.FindClientByIdAsync(logout.ClientId);
            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = _options.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = client?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            var user = await _httpContextAccessor.HttpContext.GetIdentityServerUserAsync();
            if (user != null)
            {
                var idp = user.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServerConstants.LocalIdentityProvider)
                {
                    if (vm.LogoutId == null)
                        vm.LogoutId = await _interaction.CreateLogoutContextAsync();

                    vm.ExternalAuthenticationScheme = idp;
                }
            }

            return vm;
        }
    }
}