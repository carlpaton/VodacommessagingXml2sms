using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
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
            var mockMode = (appSettings["AppSettings:MockMode"] == "" ? Environment.GetEnvironmentVariable("MOCKMODE_ENVIRONMENT") : appSettings["AppSettings:MockMode"]);


            services.AddTransient<IAuthentication>(sp => new Authentication(userName, password));
            services.AddTransient<IGenerateQueryString>(sp => new GenerateQueryString());
            services.AddTransient<IGenerateUrl>(sp => new GenerateUrl(gateway));
            services.AddTransient<ISmsRequest>(sp => new SmsRequest(responseType, mockMode));
            services.AddTransient<ISmsLogger>(sp => new SmsLogger());

            services.AddMvc();

            //Swashbuckle (Swagger)
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Vodacommessaging Xml2sms", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //In the Configure method, insert middleware to expose the generated Swagger as JSON endpoint(s)
            app.UseSwagger();

            //Optionally insert the swagger-ui middleware if you want to expose interactive documentation, specifying the Swagger JSON endpoint(s) to power it from.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vodacommessaging Xml2sms V1");
            });
        }
    }
}
