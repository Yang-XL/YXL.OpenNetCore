using System;
using Microsoft.Extensions.Logging;
using PermissionSystem.Models;

namespace LoggerExtensions
{
    public static class LoggerExtensions
    {
        private static readonly Func<Log, Exception, string> _messageFormatter = MessageFormatter;

        public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, Log log, Exception e,
            Func<Log, Exception, string> formatter)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            logger.Log(logLevel, eventId, log, e, _messageFormatter);
        }


        public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, string message, Exception e,
            Func<Log, Exception, string> formatter, string keyWord = "", string shortMessage = "")
        {
            var log = new Log
            {
                ID = Guid.NewGuid(),
                CreateDate = DateTime.UtcNow,
                FullMessage = message,
                ShortMessage = shortMessage,
                KeyWord = keyWord
            };
            Log(logger, logLevel, eventId, log, e, formatter);
        }


        private static string MessageFormatter(Log state, Exception error)
        {
            return state.ToString();
        }


        #region Debug

        public static void Debug(this ILogger logger, Log log)
        {
            Log(logger, LogLevel.Debug, 0, log, null, MessageFormatter);
        }

        public static void Debug(this ILogger logger, string message, string keyWord = "", string shortMessage = "")
        {
            Log(logger, LogLevel.Debug, 0, message, null, MessageFormatter, keyWord, shortMessage);
        }

        #endregion

        #region Critical

        public static void Critical(this ILogger logger, Log log)
        {
            Log(logger, LogLevel.Critical, 0, log, null, MessageFormatter);
        }

        public static void Critical(this ILogger logger, string message, string keyWord = "",
            string shortMessage = "")
        {
            Log(logger, LogLevel.Critical, 0, message, null, MessageFormatter, keyWord, shortMessage);
        }

        #endregion

        #region Error

        public static void Error(this ILogger logger, Log log)
        {
            Log(logger, LogLevel.Error, 0, log, null, MessageFormatter);
        }

        public static void Error(this ILogger logger, string message, string keyWord = "", string shortMessage = "")
        {
            Log(logger, LogLevel.Error, 0, message, null, MessageFormatter, keyWord, shortMessage);
        }

        #endregion

        #region Information

        public static void Information(this ILogger logger, Log log)
        {
            Log(logger, LogLevel.Information, 0, log, null, MessageFormatter);
        }

        public static void Information(this ILogger logger, string message, string keyWord = "",
            string shortMessage = "")
        {
            Log(logger, LogLevel.Information, 0, message, null, MessageFormatter, keyWord, shortMessage);
        }

        #endregion

        #region Warning

        public static void Warning(this ILogger logger, Log log)
        {
            Log(logger, LogLevel.Warning, 0, log, null, MessageFormatter);
        }

        public static void Warning(this ILogger logger, string message, string keyWord = "",
            string shortMessage = "")
        {
            Log(logger, LogLevel.Warning, 0, message, null, MessageFormatter, keyWord, shortMessage);
        }

        #endregion

        #region Trace

        public static void Trace(this ILogger logger, Log log)
        {
            Log(logger, LogLevel.Trace, 0, log, null, MessageFormatter);
        }

        public static void Trace(this ILogger logger, string message, string keyWord = "", string shortMessage = "")
        {
            Log(logger, LogLevel.Trace, 0, message, null, MessageFormatter, keyWord, shortMessage);
        }

        #endregion
    }
}