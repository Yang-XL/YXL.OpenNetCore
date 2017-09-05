using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using IService;

namespace IdentitySite.Services.IdentityService
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator

    {
        private readonly IUserService _userService;

        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            _userService = userService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        { 
            var user = await _userService.SingleAsync(a=>a.LoginName == context.UserName&&a.Password == context.Password);
            if (user != null)
            {
                context.Result = new GrantValidationResult(user.ID.ToString("N"), OidcConstants.AuthenticationMethods.Password);
            }

        }
    }
}
