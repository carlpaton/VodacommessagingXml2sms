using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using VodacommessagingXml2sms.Interfaces;
using VodacommessagingXml2sms.Services;

namespace VodacommessagingXml2sms
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
            //DI ~ Appsettings
            var appSettings = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", true, true)
             .Build();

            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1
            //Transient objects are always different; a new instance is provided to every controller and every service.
            //Scoped objects are the same within a request, but different across different requests
            //Singleton objects are the same for every object and every request (regardless of whether an instance is provided in ConfigureServices)

            var userName = (appSettings["AppSettings:Username"] == "" ? Environment.GetEnvironmentVariable("USERNAME_ENVIRONMENT") : appSettings["AppSettings:Username"]);
            var password = (appSettings["AppSettings:Password"] == "" ? Environment.GetEnvironmentVariable("PASSWORD_ENVIRONMENT") : appSettings["AppSettings:Password"]);
            var gateway = (appSettings["AppSettings:SmsGateway"] == "" ? Environment.GetEnvironmentVariable("SMSGW_ENVIRONMENT") : appSettings["AppSettings:SmsGateway"]);
            var responseType = (appSettings["AppSettings:ReponseType"] == "" ? Environment.GetEnvironmentVariable("RESPONSETYPE_ENVIRONMENT") : appSettings["AppSettings:ReponseType"]);

            //TODO ~ implement the use of mock mode, this to mock the sms gateways response to allow the developer to build their application that will call this docker container to send SMS
            var mockMode = (appSettings["AppSettings:MockMode"] == "" ? Environment.GetEnvironmentVariable("MOCKMODE_ENVIRONMENT") : appSettings["AppSettings:MockMode"]);

            services.AddTransient<IAuthentication>(sp => new Authentication(userName, password));
            services.AddTransient<IGenerateQueryString>(sp => new GenerateQueryString());
            services.AddTransient<IGenerateUrl>(sp => new GenerateUrl(gateway));
            services.AddTransient<ISmsRequest>(sp => new SmsRequest(responseType));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
