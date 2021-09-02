using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using SportStore.Web.Context;
using SportStore.Web.Models;
using Microsoft.EntityFrameworkCore;
using SportStore.Web.Repositories;

namespace SportStore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Cart>(sp=> SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddRazorPages();
            services.AddDbContext<SportStoreContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("SportStoreProducts")));
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            //services.AddTransient<IProductRepository, FakeRepository>();
            services.AddMvc(opt => opt.EnableEndpointRouting = false);//.AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddMemoryCache();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name:null,
                    template: "{category}/Page{productPage:int}",
                    defaults:new {controller = "Product",action = "Products" }
                    );
                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new { controller = "Product", action = "Products", productPage = 1 });
                routes.MapRoute(
                    name:null,
                    template: "{category}",
                    defaults: new { controller = "Product", action = "Products", productPage = 1 });
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Product", action = "Products", productPage = 1 });

                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
            //app.UseRouting();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute("default", "{controller=Product}/{action=Products}");
            //});
            SeedData.EnsurePopulated(app);
        }
    }
}
