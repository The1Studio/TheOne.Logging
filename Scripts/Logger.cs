#nullable enable
namespace UniT.Logging
{
    using System;

    public abstract class Logger : ILogger
    {
        public string Name { get; }

        public LogConfig Config { get; }

        protected Logger(string name, LogConfig config)
        {
            this.Name   = name;
            this.Config = config;
        }

        void ILogger.Debug(string message)
        {
            if (this.Config.Level > LogLevel.Debug) return;
            this.Debug($"[{LogLevel.Debug}] {message}");
        }

        void ILogger.Info(string message)
        {
            if (this.Config.Level > LogLevel.Info) return;
            this.Info($"[{LogLevel.Info}] {message}");
        }

        void ILogger.Warning(string message)
        {
            if (this.Config.Level > LogLevel.Warning) return;
            this.Warning($"[{LogLevel.Warning}] {message}");
        }

        void ILogger.Error(string message)
        {
            if (this.Config.Level > LogLevel.Error) return;
            this.Error($"[{LogLevel.Error}] {message}");
        }

        void ILogger.Critical(string message)
        {
            if (this.Config.Level > LogLevel.Critical) return;
            this.Critical($"[{LogLevel.Critical}] {message}");
        }

        void ILogger.Exception(Exception exception)
        {
            if (this.Config.Level > LogLevel.Exception) return;
            this.Exception(exception);
        }

        protected abstract void Debug(string message);

        protected abstract void Info(string message);

        protected abstract void Warning(string message);

        protected abstract void Error(string message);

        protected abstract void Critical(string message);

        protected abstract void Exception(Exception exception);
    }
}