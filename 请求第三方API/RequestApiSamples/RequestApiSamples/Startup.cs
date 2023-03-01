using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using RequestApiSamples.ApiServices;
using RequestApiSamples.CustomHandlers;
using RequestApiSamples.Refit;

namespace RequestApiSamples
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
            services.AddHttpClient();

            services.AddHttpClient("api1", httpClient =>
            {
                httpClient.BaseAddress = new Uri("http://localhost:56774/");
                httpClient.DefaultRequestHeaders.Add("appsecret", "xcode.me");

            }).AddHttpMessageHandler<ValidateHeaderHandler>();

            services.AddHttpClient("api2", httpClient =>
            {
                httpClient.BaseAddress = new Uri("http://localhost:56774/");
                httpClient.DefaultRequestHeaders.Add("appsecret", "azure.me");
            });
            services.AddHttpClient<ProductClientService>();
            services.AddHttpClient("api3", httpClient =>
            {
                httpClient.BaseAddress = new Uri("http://localhost:56774/");
                httpClient.DefaultRequestHeaders.Add("appsecret", "azure.me");
            }).AddTypedClient(httpClient => RestService.For<IProductClientService>(httpClient));

            services.AddRefitClient<IProductClientService>().ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:56774/"));

            services.AddHttpClient<ProductClientService>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, s => TimeSpan.FromSeconds(3)))
                .AddTransientHttpErrorPolicy(p=>p.CircuitBreakerAsync(5,TimeSpan.FromSeconds(30)));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
