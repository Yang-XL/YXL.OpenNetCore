using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IService;
using Microsoft.Extensions.Logging;
using PermissionSystem.Models;

namespace IdentitySite.Services.IdentityService
{
    public class ProfileService : IProfileService
    {
        private ILogger _logger;
        private readonly IUserService _userService;

        public ProfileService(IUserService userService, ILoggerFactory loggerFactory)
        {
            _userService = userService;
            _logger = loggerFactory.CreateLogger<ProfileService>();
        }


        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.FindFirst("sub")?.Value;
            if (sub != null)
            {
                var user = await _userService.SingleAsync(new Guid(sub));

                //Call custom function to get the claims from the custom database.
                var cp = await CreateIdentity(user);

                //...Optionaly remove any claims that don't need to be sent...
                context.IssuedClaims = cp.Claims.ToList();
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return  Task.Run(() => { context.IsActive = true; });
        }


        private Task<ClaimsIdentity> CreateIdentity(User user, string authenticationType = "MEIDONG.ASP.NET.ROLE")
        {
            return Task.Run(() =>
            {
                var _identity = new ClaimsIdentity(authenticationType);
                _identity.AddClaim(new Claim(ClaimTypes.Name, user.LoginName));
                _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
                return _identity;
            });
        }
    }
}