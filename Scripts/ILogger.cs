#if !UNIT_LOGGING_DEBUG && !UNIT_LOGGING_INFO && !UNIT_LOGGING_WARNING && !UNIT_LOGGING_ERROR && !UNIT_LOGGING_CRITICAL && !UNIT_LOGGING_EXCEPTION && !UNIT_LOGGING_NONE
#define UNIT_LOGGING_INFO
#endif
#nullable enable
namespace UniT.Logging
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public interface ILogger
    {
        public string Name { get; }

        public LogLevel LogLevel { get; set; }

        protected internal void Debug(string message, string context);

        protected internal void Info(string message, string context);

        protected internal void Warning(string message, string context);

        protected internal void Error(string message, string context);

        protected internal void Critical(string message, string context);

        protected internal void Exception(Exception exception);
    }

    public static class LoggerExtensions
    {
        #if !UNIT_LOGGING_DEBUG
        [Conditional("UNIT_LOGGING_DEBUG")]
        #endif
        public static void Debug(this ILogger logger, string message, [CallerMemberName] string context = "")
        {
            if (logger.LogLevel > LogLevel.Debug) return;
            logger.Debug(message, context);
        }

        #if !UNIT_LOGGING_DEBUG && !UNIT_LOGGING_INFO
        [Conditional("UNIT_LOGGING_INFO")]
        #endif
        public static void Info(this ILogger logger, string message, [CallerMemberName] string context = "")
        {
            if (logger.LogLevel > LogLevel.Info) return;
            logger.Info(message, context);
        }

        #if !UNIT_LOGGING_DEBUG && !UNIT_LOGGING_INFO && !UNIT_LOGGING_WARNING
        [Conditional("UNIT_LOGGING_WARNING")]
        #endif
        public static void Warning(this ILogger logger, string message, [CallerMemberName] string context = "")
        {
            if (logger.LogLevel > LogLevel.Warning) return;
            logger.Warning(message, context);
        }

        #if !UNIT_LOGGING_DEBUG && !UNIT_LOGGING_INFO && !UNIT_LOGGING_WARNING && !UNIT_LOGGING_ERROR
        [Conditional("UNIT_LOGGING_ERROR")]
        #endif
        public static void Error(this ILogger logger, string message, [CallerMemberName] string context = "")
        {
            if (logger.LogLevel > LogLevel.Error) return;
            logger.Error(message, context);
        }

        #if !UNIT_LOGGING_DEBUG && !UNIT_LOGGING_INFO && !UNIT_LOGGING_WARNING && !UNIT_LOGGING_ERROR && !UNIT_LOGGING_CRITICAL
        [Conditional("UNIT_LOGGING_CRITICAL")]
        #endif
        public static void Critical(this ILogger logger, string message, [CallerMemberName] string context = "")
        {
            if (logger.LogLevel > LogLevel.Critical) return;
            logger.Critical(message, context);
        }

        #if !UNIT_LOGGING_DEBUG && !UNIT_LOGGING_INFO && !UNIT_LOGGING_WARNING && !UNIT_LOGGING_ERROR && !UNIT_LOGGING_CRITICAL && !UNIT_LOGGING_EXCEPTION
        [Conditional("UNIT_LOGGING_EXCEPTION")]
        #endif
        public static void Exception(this ILogger logger, Exception exception)
        {
            if (logger.LogLevel > LogLevel.Exception) return;
            logger.Exception(exception);
        }
    }
}