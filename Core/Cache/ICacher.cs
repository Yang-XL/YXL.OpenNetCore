using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cache
{
    public interface ICacher
    {
        bool Set(object obj, string key, DateTimeOffset option);

        bool Set(object obj, string key);

        T Get<T>(string key) where T : new();
    }
}
