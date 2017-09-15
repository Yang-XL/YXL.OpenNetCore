using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace LoggerExtensions.Setting
{
    internal class LoggerSettings : ILoggerSettings
    {
        private readonly IConfiguration _configuration;

        public LoggerSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            ChangeToken = _configuration.GetReloadToken();
        }
        
        public bool IncludeScopes
        {
            get
            {
                var value = _configuration["IncludeScopes"];
                if (string.IsNullOrEmpty(value))
                    return false;
                if (bool.TryParse(value, out bool includeScopes))
                    return includeScopes;
                var message = $"Configuration value '{value}' for setting '{nameof(IncludeScopes)}' is not supported.";
                throw new InvalidOperationException(message);
            }
        }

        public string FileLogPath => string.IsNullOrEmpty(_configuration["FileLogPath"])?Path.Combine(AppContext.BaseDirectory,"Logs"): _configuration["FileLogPath"];

        public IChangeToken ChangeToken { get; private set; }

        public bool TryGetSwitch(string name, out LogLevel level)
        {
            var switches = _configuration.GetSection("LogLevel");
            if (switches == null)
            {
                level = LogLevel.None;
                return false;
            }

            var value = switches[name];
            if (string.IsNullOrEmpty(value))
            {
                level = LogLevel.None;
                return false;
            }
            if (Enum.TryParse(value, true, out level))
                return true;
            var message = $"Configuration value '{value}' for category '{name}' is not supported.";
            throw new InvalidOperationException(message);
        }

        public ILoggerSettings Reload()
        {
            ChangeToken = null;
            return new LoggerSettings(_configuration);
        }
    }
}