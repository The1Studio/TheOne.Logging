#nullable enable
namespace TheOne.Logging
{
    using System.Collections.Generic;
    using TheOne.Extensions;

    public abstract class LoggerManager : ILoggerManager
    {
        private readonly LogLevel logLevel;

        private readonly Dictionary<string, ILogger> loggers = new Dictionary<string, ILogger>();

        protected LoggerManager(LogLevel logLevel)
        {
            this.logLevel = logLevel;
        }

        ILogger ILoggerManager.GetLogger(string name)
        {
            return this.loggers.GetOrAdd(name, () => this.CreateLogger(name, this.logLevel));
        }

        IEnumerable<ILogger> ILoggerManager.GetAllLoggers()
        {
            return this.loggers.Values;
        }

        protected abstract ILogger CreateLogger(string name, LogLevel logLevel);
    }
}