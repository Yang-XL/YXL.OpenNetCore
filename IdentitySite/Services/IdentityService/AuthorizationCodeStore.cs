using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace IdentitySite.Services.IdentityService
{
    public class AuthorizationCodeStore:IAuthorizationCodeStore
    {
        private List<AuthorizationCode> AuthorizationCodeList;

        public AuthorizationCodeStore()
        {
            AuthorizationCodeList = new List<AuthorizationCode>
            {
                new AuthorizationCode
                {

                },
                new AuthorizationCode
                {

                },
            };
        }


        public Task<string> StoreAuthorizationCodeAsync(AuthorizationCode code)
        {
            
            throw new NotImplementedException();
        }

        public Task<AuthorizationCode> GetAuthorizationCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAuthorizationCodeAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}
