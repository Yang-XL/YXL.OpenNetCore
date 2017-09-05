using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace IdentitySite.Services.IdentityService
{
    /// <summary>
    /// 用于存储AuthorizationCode和RefreshToken等等，默认实现是存储在内存中，
    /// </summary>
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly IList<PersistedGrant> _persistedGrantList;

        public PersistedGrantStore()
        {
            _persistedGrantList = new List<PersistedGrant>();
        }


        public  Task StoreAsync(PersistedGrant grant)
        {
            return  Task.Run(()=> _persistedGrantList.Add(grant));
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            return await Task.FromResult(_persistedGrantList.FirstOrDefault(a => a.Key == key));
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            return  await Task.FromResult(_persistedGrantList.Where(a => a.SubjectId == subjectId));
        }

        public async Task RemoveAsync(string key)
        {
            var p = await GetAsync(key);
            _persistedGrantList.Remove(p);
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            return Task.Run(() =>
            {
               
            });
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            return Task.Run(() =>
            {

            });
        }
    }
}
