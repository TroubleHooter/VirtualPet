using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using MediatR.Pipeline;
using SimpleInjector;

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
            container.Collection.Register(typeof(INotificationHandler<>), assemblies);
            container.RegisterInstance(Console.Out);
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>),
            });
            container.Collection.Register(typeof(IRequestPreProcessor<>), assemblies);
            container.Collection.Register(typeof(IRequestPostProcessor<,>), assemblies);
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).Assembly;
            //yield return typeof(GetSectionTemplateQuery).Assembly;
        }
    }
}
