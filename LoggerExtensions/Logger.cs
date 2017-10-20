using System;
using System.IO;
using IService;
using LoggerExtensions.Setting;
using Microsoft.Extensions.Logging;

namespace LoggerExtensions.FileLogger
{
    public class Logger : ILogger
    {
        private readonly ILogService _logService;
      

        public string LogDirctory { get; set; } = Path.Combine(AppContext.BaseDirectory, "Logs");

        public Logger(string categoryName,
            Func<string, LogLevel, bool> filter,
            ILogService logService)
        {
            CategoryName = categoryName;
            _logService = logService;
            Filter = filter ?? ((category, logLevel) => true);
        }

        public string CategoryName { get; }
        public bool IncludeScopes { get; set; }

        private Func<string, LogLevel, bool> _filter;
        public Func<string, LogLevel, bool> Filter
        {
            get => _filter;
            set => _filter = value ?? throw new ArgumentNullException(nameof(value));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            _logService.Log(CategoryName,logLevel, eventId, state, exception, formatter);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None && Filter(CategoryName, logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            return LogScope.Push(CategoryName, state);
        }
    }
}