#if !THEONE_LOGGING_DEBUG && !THEONE_LOGGING_INFO && !THEONE_LOGGING_WARNING && !THEONE_LOGGING_ERROR && !THEONE_LOGGING_CRITICAL && !THEONE_LOGGING_EXCEPTION && !THEONE_LOGGING_NONE
#define THEONE_LOGGING_INFO
#endif
#nullable enable
namespace TheOne.Logging
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

        protected internal void Exception(Exception exception, string context);
    }

    public static class LoggerExtensions
    {
        #if !THEONE_LOGGING_DEBUG
        [Conditional("THEONE_LOGGING_DEBUG")]
        #endif
        public static void Debug(this ILogger logger, string message, [CallerMemberName] string context = "")
        {
            if (logger.LogLevel > LogLevel.Debug) return;
            logger.Debug(message, context);
        }

        #if !THEONE_LOGGING_DEBUG && !THEONE_LOGGING_INFO
        [Conditional("THEONE_LOGGING_INFO")]
        #endif
        public static void Info(this ILogger logger, string message, [CallerMemberName] string context = "")
        {
            if (logger.LogLevel > LogLevel.Info) return;
            logger.Info(message, context);
        }

        #if !THEONE_LOGGING_DEBUG && !THEONE_LOGGING_INFO && !THEONE_LOGGING_WARNING
        [Conditional("THEONE_LOGGING_WARNING")]
        #endif
        public static void Warning(this ILogger logger, string message, [CallerMemberName] string context = "")
        {
            if (logger.LogLevel > LogLevel.Warning) return;
            logger.Warning(message, context);
        }

        #if !THEONE_LOGGING_DEBUG && !THEONE_LOGGING_INFO && !THEONE_LOGGING_WARNING && !THEONE_LOGGING_ERROR
        [Conditional("THEONE_LOGGING_ERROR")]
        #endif
        public static void Error(this ILogger logger, string message, [CallerMemberName] string context = "")
        {
            if (logger.LogLevel > LogLevel.Error) return;
            logger.Error(message, context);
        }

        #if !THEONE_LOGGING_DEBUG && !THEONE_LOGGING_INFO && !THEONE_LOGGING_WARNING && !THEONE_LOGGING_ERROR && !THEONE_LOGGING_CRITICAL
        [Conditional("THEONE_LOGGING_CRITICAL")]
        #endif
        public static void Critical(this ILogger logger, string message, [CallerMemberName] string context = "")
        {
            if (logger.LogLevel > LogLevel.Critical) return;
            logger.Critical(message, context);
        }

        #if !THEONE_LOGGING_DEBUG && !THEONE_LOGGING_INFO && !THEONE_LOGGING_WARNING && !THEONE_LOGGING_ERROR && !THEONE_LOGGING_CRITICAL && !THEONE_LOGGING_EXCEPTION
        [Conditional("THEONE_LOGGING_EXCEPTION")]
        #endif
        public static void Exception(this ILogger logger, Exception exception, [CallerMemberName] string context = "")
        {
            if (logger.LogLevel > LogLevel.Exception) return;
            logger.Exception(exception, context);
        }
    }
}