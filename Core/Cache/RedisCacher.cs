using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace Core.Cache
{
    public class RedisCacher : ICacher
    {
        protected IDatabase _cache;
        private readonly ConnectionMultiplexer _connection;
        private readonly string _instance;

        public RedisCacher(CacheOption options, int database = 0)
        {
            _connection = ConnectionMultiplexer.Connect(options.Configuration);
            _cache = _connection.GetDatabase(database);
            _instance = options.InstanceName;
        }
        public bool Set(object obj, string key, DateTimeOffset option)
        {
            throw new NotImplementedException();
        }

        public bool Set(object obj, string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key) where T : new()
        {
            throw new NotImplementedException();
        }
    }
}
