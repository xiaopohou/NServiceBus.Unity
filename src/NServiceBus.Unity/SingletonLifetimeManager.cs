﻿namespace NServiceBus.Unity
{
    using System;
    using Microsoft.Practices.ObjectBuilder2;
    using Microsoft.Practices.Unity;

    [Janitor.SkipWeaving]
    class SingletonLifetimeManager : LifetimeManager, IRequiresRecovery, IDisposable
    {
        SingletonInstanceStore instanceStore;

        public SingletonLifetimeManager(SingletonInstanceStore instanceStore)
        {
            this.instanceStore = instanceStore;
        }

        public override object GetValue()
        {
            return instanceStore.GetValue();
        }

        public override void SetValue(object newValue)
        {
            instanceStore.SetValue(newValue);
        }

        public override void RemoveValue()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Recover()
        {
            instanceStore.Recover();
        }

        protected virtual void Dispose(bool disposing)
        {
            instanceStore.Remove();
        }
    }
}