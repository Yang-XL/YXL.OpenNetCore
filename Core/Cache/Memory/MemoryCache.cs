using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Cache.Memory
{
    public class MemoryCache : ICache
    {
        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(string key)
        {
            throw new NotImplementedException();
        }

        public bool Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(string key, object value)
        {
            throw new NotImplementedException();
        }

        public bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        public bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public T Get<T>(Func<T> fun, string key) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(Func<T> fun, string key) where T : class
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }
    }
}
