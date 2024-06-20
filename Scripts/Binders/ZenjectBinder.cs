#if UNIT_ZENJECT
#nullable enable
namespace UniT.Logging
{
    using Zenject;

    public static class ZenjectBinder
    {
        public static void BindLoggerManager(
            this DiContainer container,
            LogLevel         logLevel = LogLevel.Info
        )
        {
            var loggerManager = (ILoggerManager)new UnityLoggerManager(logLevel);
            container.BindInterfacesTo(loggerManager.GetType()).FromInstance(loggerManager).AsSingle();
            var logger = loggerManager.GetDefaultLogger();
            container.BindInterfacesTo(logger.GetType()).FromInstance(logger).AsSingle();
        }
    }
}
#endif