#nullable enable
namespace UniT.Logging
{
    using UnityEngine.Scripting;

    public sealed class UnityLoggerManager : LoggerManager<UnityLogger>
    {
        [Preserve]
        public UnityLoggerManager(LogLevel level) : base(level)
        {
        }

        protected override UnityLogger CreateLogger(string name, LogConfig config) => new UnityLogger(name, config);
    }
}