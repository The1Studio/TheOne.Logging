#if UNIT_DI
#nullable enable
namespace UniT.Logging.DI
{
    using UniT.DI;

    public static class LoggerManagerDI
    {
        public static void AddLoggerManager(this DependencyContainer container)
        {
            container.AddLoggerManager(LogLevel.Info);
        }

        public static void AddLoggerManager(
            this DependencyContainer container,
            LogLevel                 logLevel
        )
        {
            if (container.Contains<ILoggerManager>()) return;
            var loggerManager = (ILoggerManager)new UnityLoggerManager(logLevel);
            container.AddInterfaces(loggerManager);
            container.AddInterfaces(loggerManager.GetDefaultLogger());
        }
    }
}
#endif