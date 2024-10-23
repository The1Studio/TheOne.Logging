#if THEONE_ZENJECT
#nullable enable
namespace TheOne.Logging.DI
{
    using Zenject;

    public static class LoggerManagerZenject
    {
        public static void BindLoggerManager(this DiContainer container)
        {
            if (container.HasBinding<ILoggerManager>()) return;
            if (!container.HasBinding<LogLevel>())
            {
                container.BindInstance(LogLevel.Info);
            }
            container.BindInterfacesTo<UnityLoggerManager>().AsSingle();
            container.Bind<ILogger>().FromMethod(ctx => ctx.Container.Resolve<ILoggerManager>().GetLogger(ctx.ObjectType)).AsTransient();
        }
    }
}
#endif