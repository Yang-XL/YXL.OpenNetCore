using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Core.FileManager;

namespace Core.Log.FileLog
{
    public class FileLogger : ILogger
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly string _BaseDirctory = Path.Combine(AppContext.BaseDirectory, "Log");
        private readonly string _categoryName;
        private readonly string _dirPath;
        private readonly IFileManager _FileManager;
        private Func<string, LogLevel, bool> _filter;


        public FileLogger(IHttpContextAccessor accessor, 
            string dirPath,
            Func<string, LogLevel, bool> filter,
            string categoryName,
            IFileManager fileManager)
        {
            _accessor = accessor;
            _dirPath = dirPath ?? _BaseDirctory;
            _filter = filter;
            _categoryName = categoryName;
            _FileManager = fileManager;
        }

        public Func<string, LogLevel, bool> Filter
        {
            get => _filter;
            set => _filter = value ?? throw new ArgumentNullException(nameof(value));
        }

        public bool IncludeScopes { get; set; }

        public string Name => _categoryName;


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
                return;

            message = $"{logLevel}: {message}";

            if (exception != null)
                message += Environment.NewLine + Environment.NewLine + exception;

            var log = CreateLogEntry(eventId, exception, message, logLevel); //实际记录

            if (string.IsNullOrEmpty(message))
                return;
            
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); 
            _FileManager.CreateDirectory(_dirPath);
            var filename = Path.Combine(_dirPath, DateTime.Now.ToString("yyyy-MM-dd") + ".log"); 
            _FileManager.AppendText(filename, time + "  " + _categoryName+"   "  + message );
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _filter == null || _filter(_categoryName, logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NoopDisposable.Instance;
        }

        private LogEntry CreateLogEntry(EventId id, Exception e, string message, LogLevel level)
        {
            var redisEntry = new LogEntry
            {
                Date = DateTime.UtcNow,
                Host = Environment.MachineName,
                Level = level.ToString(),
                Eventid = id.Id.ToString(),
                Exception = e,
                Message = message ?? e?.Message
                //AppName = _appName,
                //Logger = _categoryName
            };
            if (_accessor.HttpContext != null)
            {
                redisEntry.Method = _accessor.HttpContext.Request.Method;
                redisEntry.Uri = _accessor.HttpContext.Request.Scheme + "://" + _accessor.HttpContext.Request.Host +
                                 _accessor.HttpContext.Request.Path + _accessor.HttpContext.Request.QueryString.Value;
                redisEntry.Ipaddress = _accessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            }
            return redisEntry;
        }

        private class NoopDisposable : IDisposable
        {
            public static readonly NoopDisposable Instance = new NoopDisposable();

            public void Dispose()
            {
            }
        }
    }
}