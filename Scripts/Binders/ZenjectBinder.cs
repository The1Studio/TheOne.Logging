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