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
        private readonly ISmsLogger _logger;

        public SendController(IAuthentication authentication, IGenerateQueryString generateQueryString, IGenerateUrl generateUrl, ISmsRequest smsRequest, ISmsLogger logger)
        {
            _authentication = authentication;
            _generateQueryString = generateQueryString;
            _generateUrl = generateUrl;
            _smsRequest = smsRequest;
            _logger = logger;
        }

        // POST api/send
        [HttpPost]
        public string Post([FromBody]List<SmsModel> value)
        {
            _logger.WriteLine(new string[] { "Send Request: ", JsonConvert.SerializeObject(value) });

            _generateQueryString.SmsModel = value;
            _generateUrl.Querystring = _generateQueryString.ForSend();

            _smsRequest.GenerateUrl = _generateUrl;
            _smsRequest.Authentication = _authentication;
            _smsRequest.SmsModel = value;
            var response = _smsRequest.SendResponse();

            _logger.WriteLine(new string[] { "Send Response: ", response });
            return response;
        }
    }
}