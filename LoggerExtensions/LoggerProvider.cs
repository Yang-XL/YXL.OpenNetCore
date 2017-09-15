using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using IService;
using LoggerExtensions.Setting;
using Microsoft.Extensions.Logging;

namespace LoggerExtensions.FileLogger
{
    public class LoggerProvider : ILoggerProvider
    {
        private static readonly Func<string, LogLevel, bool> trueFilter = (cat, level) => true;
        private static readonly Func<string, LogLevel, bool> falseFilter = (cat, level) => false;
        private readonly Func<string, LogLevel, bool> _filter;

        private readonly ConcurrentDictionary<string, Logger> _loggers = new ConcurrentDictionary<string, Logger>();

        private readonly ILogService _logService;
        private bool _includeScopes;

        private ILoggerSettings _settings;

        public LoggerProvider(Func<string, LogLevel, bool> filter, bool includeScopes, ILogService logService)
        {
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
            _includeScopes = includeScopes;
            _logService = logService;
        }

        public LoggerProvider(ILoggerSettings settings, ILogService logService)
        {
            _logService = logService;
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _includeScopes = settings.IncludeScopes;
            if (_settings.ChangeToken != null)
                _settings.ChangeToken.RegisterChangeCallback(OnConfigurationReload, null);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, CreateLoggerImplementation);
        }

        public void Dispose()
        {
        }

        #region Private Helper

        private void OnConfigurationReload(object state)
        {
            try
            {
                // The settings object needs to change here, because the old one is probably holding on
                // to an old change token.
                _settings = _settings.Reload();
                var includeScopes = _settings?.IncludeScopes ?? false;
                foreach (var logger in _loggers.Values)
                {
                    logger.Filter = GetFilter(logger.CategoryName, _settings);
                    logger.IncludeScopes = includeScopes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while loading configuration changes.{Environment.NewLine}{ex}");
            }
            finally
            {
                // The token will change each time it reloads, so we need to register again.
                if (_settings?.ChangeToken != null)
                    _settings.ChangeToken.RegisterChangeCallback(OnConfigurationReload, null);
            }
        }

        private Func<string, LogLevel, bool> GetFilter(string name, ILoggerSettings settings)
        {
            if (_filter != null)
                return _filter;

            if (settings != null)
                foreach (var prefix in GetKeyPrefixes(name))
                    if (settings.TryGetSwitch(prefix, out LogLevel level))
                        return (n, l) => l >= level;

            return falseFilter;
        }

        private IEnumerable<string> GetKeyPrefixes(string name)
        {
            while (!string.IsNullOrEmpty(name))
            {
                yield return name;
                var lastIndexOfDot = name.LastIndexOf('.');
                if (lastIndexOfDot == -1)
                {
                    yield return "Default";
                    break;
                }
                name = name.Substring(0, lastIndexOfDot);
            }
        }

        private Logger CreateLoggerImplementation(string name)
        {
            return new Logger(name, GetFilter(name, _settings), _logService);
        }

        #endregion
    }
}