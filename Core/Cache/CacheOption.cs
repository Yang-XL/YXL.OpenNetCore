using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace Core.Cache
{
   public  class CacheOption
    {
        /// <summary>
        /// 是否使用Redis
        /// </summary>
       public  bool UserRedis { get; set; }
        /// <summary>
        /// Redis 连接
        /// </summary>
        public ConfigurationOptions Configuration { get; set; }

        /// <summary>
        /// Redis实例
        /// </summary>
        public  string InstanceName { get; set; }

    }
}
