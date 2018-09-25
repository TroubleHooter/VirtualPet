
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using VirtualPet.Application;

namespace VirtualPet.WebApi.IoC.Registrations
{
    public class DbContextRegistration
    {
        public static void Register(Container container)
        {
            container.Register<VirtualPetDbContext>(Lifestyle.Scoped);

                //container.Register<DbContext>(() =>
                //{
                //    var options = new DbContextOptions<VirtualPetDbContext>();
                //    return options;
                //}
        }
    }
}
