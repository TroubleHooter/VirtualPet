

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using VirtualPet.Application;
using VirtualPet.Application.Config;
using VirtualPet.WebApi.IoC;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace VirtualPet.WebApi
{
    public class Startup
    {
        private readonly Container container = IoCHelper.BuildContainer();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var conString = Configuration.GetConnectionString("DefaultConnection");

            var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
            var connectionStrings = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            services.AddMvc(config =>
            {
                config.CacheProfiles.Add("Never",
                    new CacheProfile
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<VirtualPetDbContext>();

            //services.AddDbContext<VirtualPetDbContext>(builder =>
            //{
            //    if (!builder.IsConfigured)

            //    {
            //        builder.UseSqlServer(connectionStrings.VirtualPet);
            //    }
            //});

            IoCHelper.ConfigureMvcToUseSimpleInjector(container, services, connectionStrings);
            IoCHelper.RegisterDependencies(container, services, connectionStrings);

           // IntegrateSimpleInjector(services);

        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<VirtualPetDbContext>();
                context.Database.EnsureCreated();
            }

            container.Verify();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<XmlModel, Entity>();
            //    cfg.CreateMap<Entity, XmlModel>();
            //});

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        //private void InitializeContainer(IApplicationBuilder app)
        //{
        //    container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();


         
        //}
    }
}
