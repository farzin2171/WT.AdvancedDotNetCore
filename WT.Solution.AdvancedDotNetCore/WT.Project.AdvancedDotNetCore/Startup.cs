using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using WT.Project.AdvancedDotNetCore.Infrastructure;
using WT.Project.AdvancedDotNetCore.Infrastructure.Extentions;
using WT.Project.AdvancedDotNetCore.Infrastructure.Middlewares;
using WT.Project.AdvancedDotNetCore.Services;

namespace WT.Project.AdvancedDotNetCore
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

            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources";
            });
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(
                opt =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("fr"),
                        new CultureInfo("es"),
                    };
                    opt.DefaultRequestCulture = new RequestCulture("en");
                    opt.SupportedCultures = supportedCultures;
                    opt.SupportedUICultures = supportedCultures;

                });
            services.AddControllersWithViews(options =>
            {
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options => {
                   options.LoginPath = "/auth/login";
               });

            services.AddHttpContextAccessor();

            services.AddSingleton<IUsers, Users>();
            services.AddSingleton<IEmployees, Employees>();

            services.AddHostedService<ThumbnailGenerator>();

            services.AddStartupTask<InformationStartupTask>();

            services.AddTransient<NameRoutingMiddleware>();

            //services.AddSingleton<ICacheService, InMemoryCacheService>();

            //To use Redis Cache 
            // https://www.youtube.com/watch?v=jwek4w6als4
            //1- install StackExchange.Redis
            //2- install redis
            //docker run -p 6379:6379 --name redis-master -e REDIS_REPLICATRION_MODE=master -e ALLOW_EMPTY_PASSWORD=yes bitnami/redis:latest
            //https://redisdesktop.com/
            //3- docker ps

            services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(Configuration.GetValue<string>("RedisConnection")));

            services.AddSingleton<ICacheService, RedisCacheService>();

            services.AddHostedService<RadisSubscriber>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseNameRouting();
            app.UseRouting();
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}
