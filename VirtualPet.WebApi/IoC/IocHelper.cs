using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            //services.AddTransient<IConnectionStringProvider>((ctx) =>
            //{
            //    return new ConnectionStringProvider
            //    {
            //        ConnectionString = connectionStrings.MasterTrust
            //    };
            //});

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        public static void RegisterDependencies(Container container, IServiceCollection services, ConnectionStrings connectionStrings)
        {
            AutoMapperRegitration.Register(container);
            MediatRRegistration.Register(container);
        }

        public static void RegisterInternalDepedenciesWithSimpleInjector(Container container, IApplicationBuilder app)
        {
            container.RegisterInstance(app.ApplicationServices.GetService<ILoggerFactory>());
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
