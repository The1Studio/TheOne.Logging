#nullable enable
namespace UniT.Logging
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

        void ILogger.Debug(string message, string context)
        {
            if (this.Config.Level > LogLevel.Debug) return;
            this.Debug(this.Wrap(message, context));
        }

        void ILogger.Info(string message, string context)
        {
            if (this.Config.Level > LogLevel.Info) return;
            this.Info(this.Wrap(message, context));
        }

        void ILogger.Warning(string message, string context)
        {
            if (this.Config.Level > LogLevel.Warning) return;
            this.Warning(this.Wrap(message, context));
        }

        void ILogger.Error(string message, string context)
        {
            if (this.Config.Level > LogLevel.Error) return;
            this.Error(this.Wrap(message, context));
        }

        void ILogger.Critical(string message, string context)
        {
            if (this.Config.Level > LogLevel.Critical) return;
            this.Critical(this.Wrap(message, context));
        }

        void ILogger.Debug(Func<string> messageBuilder, string context)
        {
            if (this.Config.Level > LogLevel.Debug) return;
            this.Debug(this.Wrap(messageBuilder(), context));
        }

        void ILogger.Info(Func<string> messageBuilder, string context)
        {
            if (this.Config.Level > LogLevel.Info) return;
            this.Info(this.Wrap(messageBuilder(), context));
        }

        void ILogger.Warning(Func<string> messageBuilder, string context)
        {
            if (this.Config.Level > LogLevel.Warning) return;
            this.Warning(this.Wrap(messageBuilder(), context));
        }

        void ILogger.Error(Func<string> messageBuilder, string context)
        {
            if (this.Config.Level > LogLevel.Error) return;
            this.Error(this.Wrap(messageBuilder(), context));
        }

        void ILogger.Critical(Func<string> messageBuilder, string context)
        {
            if (this.Config.Level > LogLevel.Critical) return;
            this.Critical(this.Wrap(messageBuilder(), context));
        }

        void ILogger.Exception(Exception exception)
        {
            if (this.Config.Level > LogLevel.Exception) return;
            this.Exception(exception);
        }

        protected virtual string Wrap(string message, string context, [CallerMemberName] string logLevel = "") => $"{$"[{logLevel}]",-10} [{this.Name}] [{context}] {message}";

        protected abstract void Debug(string message);

        protected abstract void Info(string message);

        protected abstract void Warning(string message);

        protected abstract void Error(string message);

        protected abstract void Critical(string message);

        protected abstract void Exception(Exception exception);
    }
}