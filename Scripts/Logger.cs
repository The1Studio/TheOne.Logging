#nullable enable
namespace TheOne.Logging
{
    using System;
    using System.Runtime.CompilerServices;

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
            this.Debug(this.Wrap(message));
        }

        void ILogger.Info(string message)
        {
            if (this.Config.Level > LogLevel.Info) return;
            this.Info(this.Wrap(message));
        }

        void ILogger.Warning(string message)
        {
            if (this.Config.Level > LogLevel.Warning) return;
            this.Warning(this.Wrap(message));
        }

        void ILogger.Error(string message)
        {
            if (this.Config.Level > LogLevel.Error) return;
            this.Error(this.Wrap(message));
        }

        void ILogger.Critical(string message)
        {
            if (this.Config.Level > LogLevel.Critical) return;
            this.Critical(this.Wrap(message));
        }

        void ILogger.Exception(Exception exception)
        {
            if (this.Config.Level > LogLevel.Exception) return;
            this.Exception(exception);
        }

        protected virtual string Wrap(string message, [CallerMemberName] string logLevel = "") => $"[{logLevel}] {message}";

        protected abstract void Debug(string message);

        protected abstract void Info(string message);

        protected abstract void Warning(string message);

        protected abstract void Error(string message);

        protected abstract void Critical(string message);

        protected abstract void Exception(Exception exception);
    }
}