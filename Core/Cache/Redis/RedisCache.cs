using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Core.Cache
{
    public class RedisCache : ICache
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly string _instance;
        protected IDatabase _cache;
        private readonly CacheOption _option;

        public RedisCache(IOptions<CacheOption>  options)
        {
            _option = options.Value;
            _connection = ConnectionMultiplexer.Connect(_option.Configuration);
            _cache = _connection.GetDatabase(_option.DataBase);
            _instance = _option.InstanceName;
        }
        /// <summary>
        ///     验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return _cache.KeyExists(GetKeyForRedis(key));
        }

        public async Task<bool> ExistsAsync(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return await _cache.KeyExistsAsync(GetKeyForRedis(key));
        }


        /// <summary>
        ///     添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        public bool Add(string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return _cache.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
        }

        public async Task<bool> AddAsync(string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return await _cache.StringSetAsync(GetKeyForRedis(key),
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
        }

        /// <summary>
        ///     添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间,Redis中无效）</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return _cache.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)),
                expiressAbsoulte);
        }

        public async Task<bool> AddAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return await _cache.StringSetAsync(GetKeyForRedis(key),
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiressAbsoulte);
        }

        /// <summary>
        ///     添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间,Redis中无效）</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return _cache.StringSet(GetKeyForRedis(key), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)),
                expiresIn);
        }

        public async Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return await _cache.StringSetAsync(GetKeyForRedis(key),
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiresIn);
        }


        /// <summary>
        ///     删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return _cache.KeyDelete(GetKeyForRedis(key));
        }

        public async Task<bool> RemoveAsync(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return await _cache.KeyDeleteAsync(GetKeyForRedis(key));
        }

        /// <summary>
        ///     批量删除缓存
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        public void Remove(IEnumerable<string> keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            foreach (var key in keys)
                Remove(key);
        }

        public async Task RemoveAsync(IEnumerable<string> keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            foreach (var key in keys)
                await RemoveAsync(key);
        }


        /// <summary>
        ///     获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public T Get<T>(string key) where T : class
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var value = _cache.StringGet(GetKeyForRedis(key));

            if (!value.HasValue)
                return default(T);

            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            var value = await _cache.StringGetAsync(GetKeyForRedis(key));
            if (!value.HasValue)
                return default(T);
            return JsonConvert.DeserializeObject<T>(value);
        }

        public T Get<T>(Func<T> fun, string key) where T : class
        {
            if (Exists(key))
                return Get<T>(key);
            var data = fun();
            Add(GetKeyForRedis(key), data);
            return data;
        }

        public async Task<T> GetAsync<T>(Func<T> fun, string key) where T : class
        {
            if (Exists(key))
                return await GetAsync<T>(key);
            var data = fun();
            await AddAsync(GetKeyForRedis(key), data);
            return data;
        }

        private string GetKeyForRedis(string key)
        {
            return _instance + key;
        }

        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}