#nullable enable
namespace TheOne.Logging
{
    using System.Collections.Generic;
    using TheOne.Extensions;

    public abstract class LoggerManager : ILoggerManager
    {
        private readonly LogLevel level;

        private readonly Dictionary<string, ILogger> loggers = new Dictionary<string, ILogger>();

        protected LoggerManager(LogLevel level)
        {
            this.level = level;
        }

        ILogger ILoggerManager.GetLogger(string name)
        {
            return this.loggers.GetOrAdd(name, () => this.CreateLogger(name, new LogConfig { Level = this.level }));
        }

        IEnumerable<ILogger> ILoggerManager.GetAllLoggers()
        {
            return this.loggers.Values;
        }

        protected abstract ILogger CreateLogger(string name, LogConfig config);
    }
}