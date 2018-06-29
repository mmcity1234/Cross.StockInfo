using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Cross.StockInfo.ViewModels;
using Autofac;
using Cross.StockInfo.Common.IoC;

namespace Cross.StockInfo
{
    public static class BootApplication 
    {
        
        public static void Initialize()
        {
            CreateIoCContainer();
        }
        private static void CreateIoCContainer()
        {
            var builder = new ContainerBuilder();

            // This is an important step that ensures all the ViewModel's are loaded into the container.
            // Without this, it was observed that MvvmCross wouldn't register them by itself; needs more investigation.

            Assembly assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("RestApi"))
                .AsSelf()
                .PropertiesAutowired();

            //builder.RegisterAssemblyTypes(assembly)
            //  .Where(t => t.Name.EndsWith("ViewModel"))
            //  .AsSelf();

            builder.RegisterAssemblyTypes(assembly)
                .AssignableTo<BaseViewModel>()
                .As<IViewModel, BaseViewModel>()               
                .AsSelf()
                .PropertiesAutowired();

            AutofacMvxIocProvider container = new AutofacMvxIocProvider(builder);
            IocProvider.Instance.InitializeContainer(container);
            IocProvider.Instance.Container.Build();          
        }
    }
}