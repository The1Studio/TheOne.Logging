#if THEONE_VCONTAINER
#nullable enable
namespace TheOne.Logging.DI
{
    using VContainer;

    public static class LoggerManagerVContainer
    {
        public static void RegisterLoggerManager(this IContainerBuilder builder)
        {
            if (builder.Exists(typeof(ILoggerManager), true)) return;
            if (!builder.Exists(typeof(LogLevel)))
            {
                builder.Register(_ => LogLevel.Info, Lifetime.Singleton);
            }
            builder.Register<UnityLoggerManager>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ILogger>(container => container.Resolve<ILoggerManager>().GetDefaultLogger(), Lifetime.Singleton);
        }
    }
}
#endif