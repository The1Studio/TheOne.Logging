#if THEONE_DI
#nullable enable
namespace TheOne.Logging.DI
{
    using TheOne.DI;

    public static class LoggerManagerDI
    {
        public static void AddLoggerManager(this DependencyContainer container)
        {
            if (container.Contains<ILoggerManager>()) return;
            if (!container.Contains<LogLevel>())
            {
                container.Add(LogLevel.Info);
            }
            container.AddInterfaces<UnityLoggerManager>();
            container.AddInterfaces(container.Get<ILoggerManager>().GetDefaultLogger());
        }
    }
}
#endif