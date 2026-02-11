#nullable enable
namespace UniT.Logging
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public sealed class UnityLogger : ILogger
    {
        private readonly string name;

        private LogLevel logLevel;

        public UnityLogger(string name, LogLevel logLevel)
        {
            this.name     = name;
            this.logLevel = logLevel;
        }

        string ILogger.Name => this.name;

        LogLevel ILogger.LogLevel { get => this.logLevel; set => this.logLevel = value; }

        void ILogger.Debug(string message, string context)
        {
            Debug.unityLogger.Log(nameof(UniT), this.Wrap(message, context));
        }

        void ILogger.Info(string message, string context)
        {
            Debug.unityLogger.Log(nameof(UniT), this.Wrap(message, context));
        }

        void ILogger.Warning(string message, string context)
        {
            Debug.unityLogger.LogWarning(nameof(UniT), this.Wrap(message, context));
        }

        void ILogger.Error(string message, string context)
        {
            Debug.unityLogger.LogError(nameof(UniT), this.Wrap(message, context));
        }

        void ILogger.Critical(string message, string context)
        {
            Debug.unityLogger.LogError(nameof(UniT), this.Wrap(message, context));
        }

        void ILogger.Exception(Exception exception)
        {
            Debug.unityLogger.LogException(exception);
        }

        private string Wrap(string message, string context, [CallerMemberName] string logLevel = "") => $"{$"[{logLevel}]",-10} [{this.name}] [{context}] {message}";
    }
}