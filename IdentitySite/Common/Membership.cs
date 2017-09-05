using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using PermissionSystem.Models;

namespace IdentitySite.Common
{
    public static class MembershipHelper
    {
        public static string ServiceBillType ="http://schemas.microsoft.com/ws/2008/06/identity/claims/serviceBillNumber";
        public static ClaimsPrincipal CreateIdentity(User user, string authenticationType = CookieAuthenticationDefaults.AuthenticationScheme)
        {
            var _identity = new ClaimsIdentity(authenticationType);
            _identity.AddClaim(new Claim(ClaimTypes.Name, user.LoginName));
            _identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
         
            return new ClaimsPrincipal (_identity);
        }

        public static Claim[] Claims(User user)
        {
            return new []
            {
                new Claim(ClaimTypes.Name, user.LoginName),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Role,"Admin")
            };
        }

        public static User GetCurrUser(IEnumerable<Claim> claims)
        {
            var result = new User
            {
                ID = new Guid(claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value),
                LoginName = claims.First(a => a.Type == ClaimTypes.Name).Value,
                Name = claims.First(a => a.Type == ClaimTypes.GivenName).Value,
            };
            return result;
        }
    }
}