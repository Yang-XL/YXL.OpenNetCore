using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.FileManager;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Log.DbLog
{
   public  class DbLogger : ILogger
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly string _categoryName;
        //private  readonly PermissionSystemContext
        private Func<string, LogLevel, bool> _filter;
        public DbLogger(Func<string, LogLevel, bool> filter)
        {
            _filter = filter;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
        private class NoopDisposable : IDisposable

        {
            public static NoopDisposable Instance = new NoopDisposable();
            public void Dispose()
            {
            }

        }
    }
}
