#if UNIT_VCONTAINER
#nullable enable
namespace UniT.Logging.DI
{
    using VContainer;

    public static class LoggerManagerVContainer
    {
        public static void RegisterLoggerManager(this IContainerBuilder builder)
        {
            builder.RegisterLoggerManager(LogLevel.Info);
        }

        public static void RegisterLoggerManager(
            this IContainerBuilder builder,
            LogLevel               logLevel
        )
        {
            if (builder.Exists(typeof(ILoggerManager), true)) return;
            builder.Register<UnityLoggerManager>(Lifetime.Singleton)
                .WithParameter(logLevel)
                .AsImplementedInterfaces();
            builder.Register<ILogger>(container => container.Resolve<ILoggerManager>().GetDefaultLogger(), Lifetime.Singleton);
        }
    }
}
#endif