#nullable enable
namespace TheOne.Logging
{
    using UnityEngine.Scripting;

    public sealed class UnityLoggerManager : LoggerManager
    {
        [Preserve]
        public UnityLoggerManager(LogLevel logLevel) : base(logLevel)
        {
        }

        protected override ILogger CreateLogger(string name, LogLevel logLevel) => new UnityLogger(name, logLevel);
    }
}