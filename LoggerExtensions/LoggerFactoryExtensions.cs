using IService;
using LoggerExtensions.FileLogger;
using LoggerExtensions.Setting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LoggerExtensions
{
    public static class LoggerFactoryExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="factory">The <see cref="ILoggerFactory" /> to use.</param>
        /// <param name="settings">The settings to apply to created <see cref="Logger" />'s.</param>
        /// <param name="logService"></param>
        /// <returns></returns>
        public static ILoggerFactory AddLog(this ILoggerFactory factory, ILoggerSettings settings,
            ILogService logService)
        {
            factory.AddProvider(new LoggerProvider(settings, logService));
            return factory;
        }

        /// <summary>
        /// </summary>
        /// <param name="factory">The <see cref="ILoggerFactory" /> to use.</param>
        /// <param name="configuration">The <see cref="IConfiguration" /> to use for <see cref="ILoggerSettings" />.</param>
        /// <param name="logService"></param>
        /// <returns></returns>
        public static ILoggerFactory AddLog(this ILoggerFactory factory, IConfiguration configuration,
            ILogService logService)
        {
            var settings = new LoggerSettings(configuration);
            return factory.AddLog(settings, logService);
        }
    }
}