#if UNIT_VCONTAINER
#nullable enable
namespace UniT.Logging.DI
{
    using VContainer;

    public static class LoggerManagerVContainer
    {
        public static void RegisterLoggerManager(this IContainerBuilder builder)
        {
            if (builder.Exists(typeof(ILoggerManager), true)) return;
            if (!builder.Exists(typeof(LogLevel)))
            {
                builder.RegisterInstance(LogLevel.Info);
            }
            builder.Register<UnityLoggerManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ILogger>(container => container.Resolve<ILoggerManager>().GetDefaultLogger(), Lifetime.Singleton);
        }
    }
}
#endif