using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace VodacommessagingXml2sms.Controllers
{
    [Route("api/[controller]")]
    public class EnvValuesController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            try
            {
                var gw = Environment.GetEnvironmentVariable("SMSGW_ENVIRONMENT");

                var _smsGateway2 = "https://smsgw1.gsm.co.za";
                return new string[] { _smsGateway2, gw };
            }
            catch (Exception ex)
            {
                return new string[] { ex.Message, ex.StackTrace };
            }
        }
    }
}