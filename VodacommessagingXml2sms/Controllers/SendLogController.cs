using Microsoft.AspNetCore.Mvc;
using System;
using VodacommessagingXml2sms.Interfaces;

namespace VodacommessagingXml2sms.Controllers
{
    [Route("api/[controller]")]
    public class SendLogController : Controller
    {
        private readonly IAuthentication _authentication;
        private readonly IGenerateQueryString _generateQueryString;
        private readonly IGenerateUrl _generateUrl;
        private readonly ISmsRequest _smsRequest;
        private readonly ISmsLogger _logger;

        public SendLogController(IAuthentication authentication, IGenerateQueryString generateQueryString, IGenerateUrl generateUrl, ISmsRequest smsRequest, ISmsLogger logger)
        {
            _authentication = authentication;
            _generateQueryString = generateQueryString;
            _generateUrl = generateUrl;
            _smsRequest = smsRequest;
            _logger = logger;
        }

        // GET api/sendlog/200707882
        [HttpGet("{id}")]
        public string Get(string id)
        {
            _logger.WriteLine(new string[] { "SendLog Request: ", id });

            _generateQueryString.Watermark = Convert.ToInt64(id);
            _generateUrl.Querystring = _generateQueryString.ForSendLog();

            _smsRequest.GenerateUrl = _generateUrl;
            _smsRequest.Authentication = _authentication;
            _smsRequest.WaterMarkId = id;
            var response = _smsRequest.SendLogResponse();

            _logger.WriteLine(new string[] { "SendLog Response: ", response });
            return response;
        }
    }
}