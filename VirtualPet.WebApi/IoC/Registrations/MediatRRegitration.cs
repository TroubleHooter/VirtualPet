using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using MediatR;
using MediatR.Pipeline;
using SimpleInjector;
using VirtualPet.Application.Queries;

namespace VirtualPet.WebApi.IoC.Registrations
{
    public static class MediatRRegistration
    {
        public static void Register(Container container)
        {
            var assemblies = GetAssemblies().ToArray();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IRequestHandler<>), assemblies);
            //container.Register(typeof(IRequestHandler<,>),
            //    new[] { typeof(IRequestHandler<,>).Assembly });

            var notificationHandlerTypes = container.GetTypesToRegister(typeof(INotificationHandler<>), assemblies, new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false,
            });
            container.Collection.Register(typeof(INotificationHandler<>), notificationHandlerTypes);

          // container.Collection.Register(typeof(INotificationHandler<>), assemblies);
            container.RegisterInstance(Console.Out);
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>),
           
            });
            container.Collection.Register(typeof(IRequestPreProcessor<>), assemblies);
            container.Collection.Register(typeof(IRequestPostProcessor<,>), assemblies);
            //container.Collection.Register(typeof(IRequestPreProcessor<>), new[] { typeof(GenericRequestPreProcessor<>) });
            //container.Collection.Register(typeof(IRequestPostProcessor<,>), new[] { typeof(GenericRequestPostProcessor<,>), typeof(ConstrainedRequestPostProcessor<,>) });

            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);
            //container.RegisterInstance(new ServiceFactory(container.GetInstance), Lifestyle.Singleton);
            //container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).Assembly;
            //yield return typeof(GetSectionTemplateQuery).Assembly;
            yield return typeof(GetOwnedPetsQuery).GetTypeInfo().Assembly;
        }
    }
}
