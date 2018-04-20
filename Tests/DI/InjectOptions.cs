using Microsoft.Extensions.Configuration;
using VodacommessagingXml2sms.Interfaces;
using VodacommessagingXml2sms.Services;

namespace Tests.DI
{
    public class InjectOptions
    {
        public IAuthentication Authentication { get; private set; }
        public IGenerateQueryString GenerateQueryString { get; private set; }
        public IGenerateUrl GenerateUrl { get; private set; }
        public ISmsRequest SmsRequest { get; private set; }

        public InjectOptions()
        {
            var appSettings = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", true, true)
             .Build();

            Authentication = new Authentication(appSettings["AppSettings:Username"], appSettings["AppSettings:Password"]);
            GenerateQueryString = new GenerateQueryString();
            GenerateUrl = new GenerateUrl(appSettings["AppSettings:SmsGateway"]);

            SmsRequest = new SmsRequest(appSettings["AppSettings:ReponseType"])
            {
                GenerateUrl = GenerateUrl,
                Authentication = Authentication
            };
        }
    }
}
