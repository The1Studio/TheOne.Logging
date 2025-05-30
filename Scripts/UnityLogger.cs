﻿#nullable enable
namespace TheOne.Logging
{
    using System;

    public class UnityLogger : Logger
    {
        public UnityLogger(string name, LogConfig config) : base(name, config)
        {
        }

        protected sealed override void Debug(string message)
        {
            UnityEngine.Debug.unityLogger.Log(nameof(TheOne), message);
        }

        protected sealed override void Info(string message)
        {
            UnityEngine.Debug.unityLogger.Log(nameof(TheOne), message);
        }

        protected sealed override void Warning(string message)
        {
            UnityEngine.Debug.unityLogger.LogWarning(nameof(TheOne), message);
        }

        protected sealed override void Error(string message)
        {
            UnityEngine.Debug.unityLogger.LogError(nameof(TheOne), message);
        }

        protected sealed override void Critical(string message)
        {
            UnityEngine.Debug.unityLogger.LogError(nameof(TheOne), message);
        }

        protected sealed override void Exception(Exception exception)
        {
            UnityEngine.Debug.unityLogger.LogException(exception);
        }
    }
}