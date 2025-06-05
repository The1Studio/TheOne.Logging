#nullable enable
namespace UniT.Logging
{
    using System;

    public sealed class UnityLogger : Logger
    {
        public UnityLogger(string name, LogConfig config) : base(name, config)
        {
        }

        protected override void Debug(string message)
        {
            UnityEngine.Debug.unityLogger.Log(nameof(UniT), message);
        }

        protected override void Info(string message)
        {
            UnityEngine.Debug.unityLogger.Log(nameof(UniT), message);
        }

        protected override void Warning(string message)
        {
            UnityEngine.Debug.unityLogger.LogWarning(nameof(UniT), message);
        }

        protected override void Error(string message)
        {
            UnityEngine.Debug.unityLogger.LogError(nameof(UniT), message);
        }

        protected override void Critical(string message)
        {
            UnityEngine.Debug.unityLogger.LogError(nameof(UniT), message);
        }

        protected override void Exception(Exception exception)
        {
            UnityEngine.Debug.unityLogger.LogException(exception);
        }
    }
}