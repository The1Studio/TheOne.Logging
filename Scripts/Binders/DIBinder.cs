#if UNIT_DI
#nullable enable
namespace UniT.Logging
{
    using UniT.DI;

    public static class DIBinder
    {
        public static void AddLoggerManager(
            this DependencyContainer container,
            LogLevel                 logLevel = LogLevel.Info
        )
        {
            var loggerManager = (ILoggerManager)new UnityLoggerManager(logLevel);
            container.AddInterfaces(loggerManager);
            container.AddInterfaces(loggerManager.GetDefaultLogger());
        }
    }
}
#endif