#nullable enable
namespace UniT.Logging
{
    using System;
    using System.Runtime.CompilerServices;

    public interface ILogger
    {
        public string Name { get; }

        public LogConfig Config { get; }

        public void Debug(string message, [CallerMemberName] string context = "");

        public void Info(string message, [CallerMemberName] string context = "");

        public void Warning(string message, [CallerMemberName] string context = "");

        public void Error(string message, [CallerMemberName] string context = "");

        public void Critical(string message, [CallerMemberName] string context = "");

        public void Log(string message, LogLevel level, [CallerMemberName] string context = "")
        {
            switch (level)
            {
                case LogLevel.Debug:
                {
                    this.Debug(message, context);
                    break;
                }
                case LogLevel.Info:
                {
                    this.Info(message, context);
                    break;
                }
                case LogLevel.Warning:
                {
                    this.Warning(message, context);
                    break;
                }
                case LogLevel.Error:
                {
                    this.Error(message, context);
                    break;
                }
                case LogLevel.Critical:
                {
                    this.Critical(message, context);
                    break;
                }
                case LogLevel.None: break;
                default:            throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        public void Debug(Func<string> messageBuilder, [CallerMemberName] string context = "");

        public void Info(Func<string> messageBuilder, [CallerMemberName] string context = "");

        public void Warning(Func<string> messageBuilder, [CallerMemberName] string context = "");

        public void Error(Func<string> messageBuilder, [CallerMemberName] string context = "");

        public void Critical(Func<string> messageBuilder, [CallerMemberName] string context = "");

        public void Log(Func<string> messageBuilder, LogLevel level, [CallerMemberName] string context = "")
        {
            switch (level)
            {
                case LogLevel.Debug:
                {
                    this.Debug(messageBuilder, context);
                    break;
                }
                case LogLevel.Info:
                {
                    this.Info(messageBuilder, context);
                    break;
                }
                case LogLevel.Warning:
                {
                    this.Warning(messageBuilder, context);
                    break;
                }
                case LogLevel.Error:
                {
                    this.Error(messageBuilder, context);
                    break;
                }
                case LogLevel.Critical:
                {
                    this.Critical(messageBuilder, context);
                    break;
                }
                case LogLevel.None: break;
                default:            throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }

        public void Exception(Exception exception);

        public void Debug(object message, [CallerMemberName] string context = "") => this.Debug(message.ToString(), context);

        public void Info(object message, [CallerMemberName] string context = "") => this.Info(message.ToString(), context);

        public void Warning(object message, [CallerMemberName] string context = "") => this.Warning(message.ToString(), context);

        public void Error(object message, [CallerMemberName] string context = "") => this.Error(message.ToString(), context);

        public void Critical(object message, [CallerMemberName] string context = "") => this.Critical(message.ToString(), context);

        public void Log(object message, LogLevel level, [CallerMemberName] string context = "") => this.Log(message.ToString(), level, context);

        public void Log(Exception exception) => this.Exception(exception);
    }
}