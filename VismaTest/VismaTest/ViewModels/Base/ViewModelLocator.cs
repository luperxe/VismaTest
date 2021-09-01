using VismaTest.Services.HttpConnector;
using VismaTest.Services.Navigation;
using VismaTest.Services.WebServices;
using VismaTest.Services.WebServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace VismaTest.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer container;
        private static readonly ViewModelLocator instance = new ViewModelLocator();

        public static ViewModelLocator Instance
        {
            get
            {
                return instance;
            }
        }

        public ViewModelLocator()
        {
            container = new UnityContainer();

            container.RegisterType<INavigationService, NavigationService>();
            container.RegisterType<IHttpService, HttpService>(new InjectionConstructor());
            container.RegisterType<IVTService, VTService>();
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }

        public void Register<T>(T instance)
        {
            container.RegisterInstance<T>(instance);
        }

        public void Register<TInterface, T>() where T : TInterface
        {
            container.RegisterType<TInterface, T>();
        }

        public void RegisterSingleton<TInterface, T>() where T : TInterface
        {
            container.RegisterType<TInterface, T>(new ContainerControlledLifetimeManager());
        }
    }
}
