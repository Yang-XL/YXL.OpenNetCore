using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure
{
  public   class Singleton
    {
        static Singleton()
        {
            allSingletons = new Dictionary<Type, object>();
        }

        public static readonly IDictionary<Type, object> allSingletons;

        /// <summary>
        /// 字典类型Singl
        /// </summary>
        public static IDictionary<Type, object> AllSingletons => allSingletons;
    }

    public class Singleton<T> : Singleton
    {
        static T instance;

        /// <summary>
        /// 指定类型的单例
        /// T 只有一个实例
        /// </summary>
        public static T Instance
        {
            get => instance;
            set
            {
                instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }

    public class SingletonList<T> : Singleton<IList<T>>
    {
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        /// <summary>
        /// 单例集合 对象集合只有一个实例
        /// </summary>
        public new static IList<T> Instance => Singleton<IList<T>>.Instance;
       
        /// <summary>
        /// 单例字典
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
        {
            static SingletonDictionary()
            {
                Singleton<Dictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
            }

            public new static IDictionary<TKey, TValue> Instance { get; } = Singleton<Dictionary<TKey, TValue>>.Instance;
        }
    }
}
