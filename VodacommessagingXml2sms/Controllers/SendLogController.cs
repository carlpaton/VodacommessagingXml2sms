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

        public SendLogController(IAuthentication authentication, IGenerateQueryString generateQueryString, IGenerateUrl generateUrl, ISmsRequest smsRequest)
        {
            _authentication = authentication;
            _generateQueryString = generateQueryString;
            _generateUrl = generateUrl;
            _smsRequest = smsRequest;
        }

        // GET api/sendlog/200707882
        [HttpGet("{id}")]
        public string Get(string id)
        {
            _generateQueryString.Watermark = Convert.ToInt64(id);
            _generateUrl.Querystring = _generateQueryString.ForSendLog();

            _smsRequest.GenerateUrl = _generateUrl;
            _smsRequest.Authentication = _authentication;

            return _smsRequest.SendLogResponse();
        }
    }
}