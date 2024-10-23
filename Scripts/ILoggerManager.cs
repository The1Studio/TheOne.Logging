#nullable enable
namespace TheOne.Logging
{
    using System;
    using System.Collections.Generic;

    public interface ILoggerManager
    {
        public ILogger GetLogger(string name);

        public ILogger GetLogger(Type ownerType) => this.GetLogger(ownerType.Name);

        public ILogger GetLogger(object owner) => this.GetLogger(owner.GetType());

        public ILogger GetLogger<T>() => this.GetLogger(typeof(T));

        public ILogger GetDefaultLogger() => this.GetLogger(nameof(TheOne));

        public IEnumerable<ILogger> GetAllLoggers();
    }
}