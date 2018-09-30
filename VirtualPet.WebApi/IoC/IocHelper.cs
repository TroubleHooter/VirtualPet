using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using VirtualPet.Application.Config;
using VirtualPet.WebApi.IoC.Registrations;

namespace VirtualPet.WebApi.IoC
{
    public static class IoCHelper
    {
        public static Container BuildContainer()
        {
            return new Container
            {
                Options =
                        {
                            DefaultLifestyle = Lifestyle.Scoped,
                            DefaultScopedLifestyle = new AsyncScopedLifestyle() ,
                            ConstructorResolutionBehavior = new LeastGreedyConstructorBehavior()
                        }
            };
        }

        public static void ConfigureMvcToUseSimpleInjector(Container container, IServiceCollection services, ConnectionStrings connectionStrings)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            container.RegisterSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(container));
            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        public static void RegisterDependencies(Container container, IServiceCollection services, ConnectionStrings connectionStrings)
        {
            MapperRegitration.Register(container);
            MediatRRegistration.Register(container);
        }
    }

    public class LeastGreedyConstructorBehavior : IConstructorResolutionBehavior
    {
        public ConstructorInfo GetConstructor(Type implementationType) => (
                from ctor in implementationType.GetConstructors()
                orderby ctor.GetParameters().Length
                select ctor)
            .First();
    }
}
