using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cross.StockInfo.Common.IoC
{
    /// <summary>
    /// For mvvmcross framework 
    /// </summary>
    public class AutofacMvxIocProvider :  IContainerProvider
    {
        private IContainer container;

        private ContainerBuilder builder;


        public AutofacMvxIocProvider(ContainerBuilder builder)
        {
            this.builder = builder ?? throw new ArgumentNullException("builder");
        
        }

        public virtual void CallbackWhenRegistered<T>(Action action)
        {
            this.CallbackWhenRegistered(typeof(T), action);
        }

        public virtual void CallbackWhenRegistered(Type type, Action action)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            this.container.ComponentRegistry.Registered += (sender, args) =>
            {
                if (args.ComponentRegistration.Services.OfType<TypedService>().Any(x => x.ServiceType == type))
                {
                    action();
                }
            };
        }


        public virtual bool CanResolve<T>()
            where T : class
        {
            return this.CanResolve(typeof(T));
        }

      
        public virtual bool CanResolve(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return this.container.IsRegistered(type);
        }

      
        public virtual T Create<T>()
            where T : class
        {
            return (T)this.Create(typeof(T));
        }

       
        public virtual object Create(Type type)
        {
            return this.Resolve(type);
        }

      
        public virtual T GetSingleton<T>()
            where T : class
        {
            return (T)this.GetSingleton(typeof(T));
        }

      
        public virtual object GetSingleton(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var service = new TypedService(type);
            IComponentRegistration registration;
            if (!this.container.ComponentRegistry.TryGetRegistration(service, out registration))
            {
                throw new ComponentNotRegisteredException(service);
            }

            if (registration.Sharing != InstanceSharing.Shared || !(registration.Lifetime is RootScopeLifetime))
            {
                // Ensure the dependency is registered as a singleton WITHOUT resolving the dependency twice.
                throw new DependencyResolutionException(string.Format("Type not registered as singleton", type));
            }

            return this.Resolve(type);
        }
     

      
        public virtual void RegisterSingleton<TInterface>(TInterface theObject)
            where TInterface : class
        {
            this.RegisterSingleton(typeof(TInterface), theObject);
        }

      
        public virtual void RegisterSingleton<TInterface>(Func<TInterface> theConstructor)
            where TInterface : class
        {
            this.RegisterSingleton(typeof(TInterface), theConstructor);
        }

     
        public virtual void RegisterSingleton(Type tInterface, object theObject)
        {
            if (tInterface == null)
            {
                throw new ArgumentNullException(nameof(tInterface));
            }

            if (theObject == null)
            {
                throw new ArgumentNullException(nameof(theObject));
            }

            var cb = new ContainerBuilder();

            // You can't inject properties on a pre-constructed instance.
            cb.RegisterInstance(theObject).As(tInterface).AsSelf().SingleInstance().PropertiesAutowired();
        }

       
        public virtual void RegisterSingleton(Type tInterface, Func<object> theConstructor)
        {
            if (tInterface == null)
            {
                throw new ArgumentNullException(nameof(tInterface));
            }

            if (theConstructor == null)
            {
                throw new ArgumentNullException(nameof(theConstructor));
            }

            var cb = new ContainerBuilder();

            var type = theConstructor.GetMethodInfo().ReturnType;
            var regType = cb.RegisterType(type).As(tInterface).AsSelf().SingleInstance().PropertiesAutowired();
         
        }

    
        public virtual void RegisterType<TFrom, TTo>()
            where TFrom : class
            where TTo : class, TFrom
        {
            this.RegisterType(typeof(TFrom), typeof(TTo));
        }

      
        public virtual void RegisterType<TInterface>(Func<TInterface> constructor)
            where TInterface : class
        {
            if (constructor == null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            var cb = new ContainerBuilder();
            var x = cb.Register(c => constructor()).AsSelf().PropertiesAutowired();           
        }

       
        public virtual void RegisterType(Type t, Func<object> constructor)
        {
            if (t == null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            if (constructor == null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            var cb = new ContainerBuilder();
            var type = constructor.GetMethodInfo().ReturnType;
            var x = cb.Register(c => constructor()).As(t).AsSelf().PropertiesAutowired();          
        }

      
        public virtual void RegisterType(Type tFrom, Type tTo)
        {
            if (tFrom == null)
            {
                throw new ArgumentNullException(nameof(tFrom));
            }

            if (tTo == null)
            {
                throw new ArgumentNullException(nameof(tTo));
            }

            var cb = new ContainerBuilder();
            var x = cb.RegisterType(tTo).As(tFrom).AsSelf().PropertiesAutowired();
        }

       
        public virtual T Resolve<T>()
            where T : class
        {
            return (T)this.Resolve(typeof(T));
        }

    
        public virtual object Resolve(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            try
            {
                return this.container.Resolve(type);
            }
            catch (DependencyResolutionException ex)
            {
                throw new Exception(string.Format("Could not resolve {0}. See InnerException for details", type.FullName), ex);
            }
        }

     
        public virtual bool TryResolve<T>(out T resolved)
            where T : class
        {
            return this.container.TryResolve(out resolved);
        }

     
        public virtual bool TryResolve(Type type, out object resolved)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return this.container.TryResolve(type, out resolved);
        }

      
        public ILifetimeScope BeginLifetimeScope()
        {
            return container.BeginLifetimeScope();
        }
    
        public void Build()
        {
            container = builder.Build();
        }
    }

}
