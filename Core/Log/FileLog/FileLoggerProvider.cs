using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Core.FileManager;

namespace Core.Log.FileLog
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly string _dirPath;
        private readonly IFileManager _fileManager;
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly bool _isLegacy;

        private readonly ConcurrentDictionary<string, FileLogger> _loggers =
            new ConcurrentDictionary<string, FileLogger>();

        private ILogSetting _logSetting;


        public FileLoggerProvider(IFileManager fileManager, IHttpContextAccessor accessor, IConfiguration configuration)
        {
            if (configuration != null)
            {
                _logSetting = new ConfigurationFileLogSetting(configuration);

                if (_logSetting.ChangeToken != null)
                    _logSetting.ChangeToken.RegisterChangeCallback(OnConfigurationReload, null);
            }
            else
            {
                _logSetting = new BaseLogSetting();
            }

            _fileManager = fileManager;
            _accessor = accessor;
            _dirPath = Path.Combine(AppContext.BaseDirectory, "Log");
            _isLegacy = false;
        }

        public FileLoggerProvider(IFileManager fileManager,
            IHttpContextAccessor accessor,
            ILogSetting logSetting) : this(fileManager, accessor, logSetting, null)
        {
            _isLegacy = true;
        }


        public FileLoggerProvider(
            IFileManager fileManager,
            IHttpContextAccessor accessor,
            ILogSetting logSetting,
            Func<string, LogLevel, bool> filter) : this(fileManager, accessor, logSetting, filter, null)
        {
        }

        public FileLoggerProvider(
            IFileManager fileManager,
            IHttpContextAccessor accessor,
            ILogSetting logSetting,
            Func<string, LogLevel, bool> filter,
            string dirPath)
        {
            _filter = filter;
            _fileManager = fileManager;
            _accessor = accessor;
            _logSetting = logSetting;
            _dirPath = string.IsNullOrEmpty(dirPath) ? Path.Combine(AppContext.BaseDirectory, "Log") : dirPath;
            if (_logSetting.ChangeToken != null)
                logSetting.ChangeToken.RegisterChangeCallback(OnConfigurationReload, null);
        }

        public IDictionary<string, LogLevel> LogLevels { get; set; } = new Dictionary<string, LogLevel>();

        public void Dispose()
        {
            _fileManager.Dispose();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_accessor, _dirPath, GetFilter(categoryName), categoryName, _fileManager);
        }

        #region Private Method

        private Func<string, LogLevel, bool> GetFilter(string name)
        {
            if (_filter != null)
                return _filter;
            foreach (var keyPrefix in GetKeyPrefixes(name))
            {
                LogLevel level;
                if (_logSetting.TryGetSwitch(keyPrefix, out level))
                    return (n, l) => l >= level;
            }
            return (n, l) => false;
        }

        private IEnumerable<string> GetKeyPrefixes(string name)
        {
            int lastIndexOfDot;
            for (; !string.IsNullOrEmpty(name); name = name.Substring(0, lastIndexOfDot))
            {
                yield return name;
                lastIndexOfDot = name.LastIndexOf('.');
                if (lastIndexOfDot == -1)
                {
                    yield return "Default";
                    break;
                }
            }
        }


        private void OnConfigurationReload(object state)
        {
            try
            {
                // The settings object needs to change here, because the old one is probably holding on
                // to an old change token.
                _logSetting = _logSetting.Reload();

                var includeScopes = _logSetting?.IncludeScopes ?? false;
                foreach (var logger in _loggers.Values)
                {
                    if (_isLegacy)
                        logger.Filter = GetFilter(logger.Name);
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
                if (_logSetting?.ChangeToken != null)
                    _logSetting.ChangeToken.RegisterChangeCallback(OnConfigurationReload, null);
            }
        }

        #endregion
    }
}