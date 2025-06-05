#nullable enable
namespace UniT.Logging
{
    using UnityEngine.Scripting;

    public sealed class UnityLoggerManager : LoggerManager
    {
        [Preserve]
        public UnityLoggerManager(LogLevel level) : base(level)
        {
        }

        protected override ILogger CreateLogger(string name, LogConfig config) => new UnityLogger(name, config);
    }
}