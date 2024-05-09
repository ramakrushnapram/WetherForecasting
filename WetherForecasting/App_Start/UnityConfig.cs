using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using WeatherForcasting.Service;
using WetherForecasting.Utility;

namespace WetherForecasting
{
    public static class UnityConfig
    {
        internal static readonly IUnityContainer Container;

        public static void RegisterComponents()
        {
			var container = new Unity.UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IWetherForecastinRepository, WeatherForecastRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}