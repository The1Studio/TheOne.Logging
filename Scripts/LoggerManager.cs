#nullable enable
namespace UniT.Logging
{
    using System.Collections.Generic;
    using System.Linq;
    using UniT.Extensions;

    public abstract class LoggerManager<TLogger> : ILoggerManager where TLogger : ILogger
    {
        private readonly LogLevel level;

        private readonly Dictionary<string, TLogger> loggers = new Dictionary<string, TLogger>();

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
            return this.loggers.Values.Cast<ILogger>();
        }

        protected abstract TLogger CreateLogger(string name, LogConfig config);
    }
}