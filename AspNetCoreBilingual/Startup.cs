using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreBilingual
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services
                .AddLocalization(o => o.ResourcesPath = "Resources")
                .AddRouting()
                .AddControllersWithViews();            

            //https://stackoverflow.com/questions/60019136/asp-net-core-3-1-shared-localization-not-working-for-version-3-1
            //https://stackoverflow.com/questions/59941181/net-core-3-1-localization-culture-by-url-localhost-en-area-controller-action
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseRequestLocalization();
            app.UseStaticFiles();
            app.UseRouting();

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("fr"),
            };

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            var requestProvider = new RouteDataRequestCultureProvider();
            localizationOptions.RequestCultureProviders.Insert(0, requestProvider);

            //app.UseEndpoints(endpoints =>
            //{
            //    //endpoints.MapRazorPages();                
            //    endpoints.MapControllerRoute("default", "{culture=en}/{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseRouter(routes =>
            {
                routes.MapMiddlewareRoute("{culture=en}/{*mvcRoute}", subApp =>
                {
                    subApp.UseRequestLocalization(localizationOptions);
                    subApp.UseRouting();
                    subApp.UseEndpoints(mvcRoutes =>
                    {
                        mvcRoutes.MapControllerRoute("default", "{culture=en}/{controller=Home}/{action=Index}/{id?}");
                    });
                });
            });

            //https://forums.asp.net/t/2159027.aspx?How+can+I+use+globalization+and+localization+in+net+core+3+0+preview8+            
        }
    }
}