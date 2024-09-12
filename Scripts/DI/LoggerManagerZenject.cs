#if UNIT_ZENJECT
#nullable enable
namespace UniT.Logging.DI
{
    using Zenject;

    public static class LoggerManagerZenject
    {
        public static void BindLoggerManager(this DiContainer container)
        {
            container.BindLoggerManager(LogLevel.Info);
        }

        public static void BindLoggerManager(
            this DiContainer container,
            LogLevel         logLevel
        )
        {
            if (container.HasBinding<ILoggerManager>()) return;
            container.BindInterfacesTo<UnityLoggerManager>()
                .AsSingle()
                .WithArguments(logLevel);
            container.Bind<ILogger>()
                .FromMethod(ctx => ctx.Container.Resolve<ILoggerManager>().GetLogger(ctx.ObjectType))
                .AsTransient();
        }
    }
}
#endif