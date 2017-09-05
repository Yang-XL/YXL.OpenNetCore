using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Core.Log.FileLog
{
    public class ConfigurationFileLogSetting : ILogSetting
    {
        private readonly IConfiguration _configuration;

        public ConfigurationFileLogSetting(IConfiguration configuration)
        {
            _configuration = configuration;
            ChangeToken = configuration.GetReloadToken();
        }

        public bool IncludeScopes
        {
            get
            {
                bool includeScopes;
                var value = _configuration["IncludeScopes"];
                if (string.IsNullOrEmpty(value))
                    return false;
                if (bool.TryParse(value, out includeScopes))
                    return includeScopes;
                var message = $"Configuration value '{value}' for setting '{nameof(IncludeScopes)}' is not supported.";
                throw new InvalidOperationException(message);
            }
        }

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
            {
                return true;
            }
            var message = $"Configuration value '{value}' for category '{name}' is not supported.";
            throw new InvalidOperationException(message);
        }

        public ILogSetting Reload()
        {
            ChangeToken = null;
            return new ConfigurationFileLogSetting(_configuration);
        }

    }
}