#nullable enable
namespace UniT.Logging
{
    using System;

    public interface ILogger
    {
        public string Name { get; }

        public LogConfig Config { get; }

        public void Debug(string message);

        public void Info(string message);

        public void Warning(string message);

        public void Error(string message);

        public void Critical(string message);

        public void Log(string message, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                {
                    this.Debug(message);
                    break;
                }
                case LogLevel.Info:
                {
                    this.Info(message);
                    break;
                }
                case LogLevel.Warning:
                {
                    this.Warning(message);
                    break;
                }
                case LogLevel.Error:
                {
                    this.Error(message);
                    break;
                }
                case LogLevel.Critical:
                {
                    this.Critical(message);
                    break;
                }
                case LogLevel.None: break;
                default:            throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        public void Debug(Func<string> messageBuilder);

        public void Info(Func<string> messageBuilder);

        public void Warning(Func<string> messageBuilder);

        public void Error(Func<string> messageBuilder);

        public void Critical(Func<string> messageBuilder);

        public void Log(Func<string> messageBuilder, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                {
                    this.Debug(messageBuilder);
                    break;
                }
                case LogLevel.Info:
                {
                    this.Info(messageBuilder);
                    break;
                }
                case LogLevel.Warning:
                {
                    this.Warning(messageBuilder);
                    break;
                }
                case LogLevel.Error:
                {
                    this.Error(messageBuilder);
                    break;
                }
                case LogLevel.Critical:
                {
                    this.Critical(messageBuilder);
                    break;
                }
                case LogLevel.None: break;
                default:            throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        public void Exception(Exception exception);

        public void Debug(object message) => this.Debug(message.ToString());

        public void Info(object message) => this.Info(message.ToString());

        public void Warning(object message) => this.Warning(message.ToString());

        public void Error(object message) => this.Error(message.ToString());

        public void Critical(object message) => this.Critical(message.ToString());

        public void Log(object message, LogLevel level) => this.Log(message.ToString(), level);

        public void Log(Exception exception) => this.Exception(exception);
    }
}