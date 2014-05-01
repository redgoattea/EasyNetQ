using System;
using System.Linq;
using SimpleInjector;

namespace EasyNetQ.DI
{
    public class SimpleInjectorAdapter : IContainer
    {
        private readonly Container _container;

        public SimpleInjectorAdapter(Container container)
        {
            _container = container;
        }

        public TService Resolve<TService>() where TService : class
        {
            return _container.GetInstance<TService>();
        }

        public IServiceRegister Register<TService>(Func<IServiceProvider, TService> serviceCreator) where TService : class
        {
            if(!IsRegistered<TService>())
                _container.RegisterSingle(() => serviceCreator(this));
            
            return this;
        }

        public IServiceRegister Register<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            if(!IsRegistered<TService>())           
                _container.RegisterSingle<TService, TImplementation>();
            
            return this;
        }

        private bool IsRegistered<TService>()
        {
            return _container.GetCurrentRegistrations().Any(x => x.ServiceType == typeof(TService));
        }
    }
}
