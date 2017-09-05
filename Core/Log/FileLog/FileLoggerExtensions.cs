using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Core.FileManager;

namespace Core.Log.FileLog
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFileLogger(
            this ILoggerFactory factory, 
            IFileManager fileManager, 
            IHttpContextAccessor accessor,
            ILogSetting settings)
        {
            factory.AddProvider(new FileLoggerProvider(fileManager, accessor, settings));
            return factory;
        }

        public static ILoggerFactory AddFileLogger(this ILoggerFactory factory,
            IFileManager fileManager,
            IHttpContextAccessor accessor, 
            IConfiguration configuration)
        {
            var settings = new ConfigurationFileLogSetting(configuration);
            return factory.AddFileLogger(fileManager, accessor, settings);
        }
    }
}