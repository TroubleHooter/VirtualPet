
using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using VirtualPet.Data;
using VirtualPet.Data.Repositories;

namespace VirtualPet.Api
{
    public class Startup
    {
        private Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var conString = Configuration.GetConnectionString("DefaultConnection");

            //var container = new Container();

            // container.Register<DbContextOptions, DbContextOptions>();



            //services.AddDbContext<VirtualPetContext>(options => options.UseSqlServer(conString));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            IntegrateSimpleInjector(services);

            //services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            //services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_container));

            //services.AddDbContext<>()


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
            InitializeContainer(app);

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

        private void InitializeContainer(IApplicationBuilder app)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<PetRepository, PetRepository>();
            container.Register<DbContext, VirtualPetContext>(Lifestyle.Scoped);
        }
    }
}
