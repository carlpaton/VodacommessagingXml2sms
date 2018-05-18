using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedModels;
using System.Collections.Generic;
using VodacommessagingXml2sms.Interfaces;

namespace VodacommessagingXml2sms.Controllers
{
    [Route("api/[controller]")]
    public class SendController : Controller
    {
        private readonly IAuthentication _authentication;
        private readonly IGenerateQueryString _generateQueryString;
        private readonly IGenerateUrl _generateUrl;
        private readonly ISmsRequest _smsRequest;

        public SendController(IAuthentication authentication, IGenerateQueryString generateQueryString, IGenerateUrl generateUrl, ISmsRequest smsRequest)
        {
            _authentication = authentication;
            _generateQueryString = generateQueryString;
            _generateUrl = generateUrl;
            _smsRequest = smsRequest;
        }

        // POST api/send
        [HttpPost]
        public string Post([FromBody]List<SmsModel> value)
        {
            //TODO ~ add ILogger
            System.Console.WriteLine("Send Request: {0}", JsonConvert.SerializeObject(value));

            _generateQueryString.SmsModel = value;
            _generateUrl.Querystring = _generateQueryString.ForSend();

            _smsRequest.GenerateUrl = _generateUrl;
            _smsRequest.Authentication = _authentication;
            var response = _smsRequest.SendResponse();

            //TODO ~ add ILogger
            System.Console.WriteLine("Send Response: {0}", response);
            return response;
        }
    }
}