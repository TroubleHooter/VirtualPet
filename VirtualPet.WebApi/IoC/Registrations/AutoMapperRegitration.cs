using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using SimpleInjector;

namespace VirtualPet.WebApi.IoC.Registrations
{
    public static class AutoMapperRegitration
    {
        public static void Register(Container container)
        {
            var profiles = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => typeof(AutoMapper.Profile).IsAssignableFrom(x));

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as AutoMapper.Profile);
                }
            });

            container.RegisterInstance(config);
            container.Register<IMapper>(() => config.CreateMapper(container.GetInstance));
        }
    }
}
