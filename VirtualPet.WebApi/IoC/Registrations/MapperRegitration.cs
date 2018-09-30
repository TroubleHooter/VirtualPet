
using System.Collections.Generic;
using SimpleInjector;
using VirtualPet.Application.Dtos;
using VirtualPet.Application.Entities;
using VirtualPet.Application.Mappers;

namespace VirtualPet.WebApi.IoC.Registrations
{
    public static class MapperRegitration
    {
        public static void Register(Container container)
        {
            container.Register< IMapper<Pet, PetDto>, PetMapper>();
            container.Register<IMapper<List<Pet>, List<PetDto>>, PetsMapper>();

            //var profiles = Assembly.GetExecutingAssembly()
            //    .GetTypes()
            //    .Where(x => typeof(AutoMapper.Profile).IsAssignableFrom(x));

            //var config = new MapperConfiguration(cfg =>
            //{
            //    foreach (var profile in profiles)
            //    {
            //        cfg.AddProfile(Activator.CreateInstance(profile) as AutoMapper.Profile);
            //    }
            //});

            //container.RegisterInstance(config);
            //container.Register<IMapper>(() => config.CreateMapper(container.GetInstance));
        }
    }
}
