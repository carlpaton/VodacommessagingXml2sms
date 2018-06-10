//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;

//namespace VodacommessagingXml2sms.Controllers
//{
//    [Route("api/[controller]")]
//    public class EnvValuesController : Controller
//    {
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            try
//            {
//                var USERNAME_ENVIRONMENT = Environment.GetEnvironmentVariable("USERNAME_ENVIRONMENT");
//                var PASSWORD_ENVIRONMENT = Environment.GetEnvironmentVariable("PASSWORD_ENVIRONMENT");
//                var SMSGW_ENVIRONMENT = Environment.GetEnvironmentVariable("SMSGW_ENVIRONMENT");

//                //Environment.SetEnvironmentVariable("SMSGW_ENVIRONMENT", "");

//                return new string[] { SMSGW_ENVIRONMENT, PASSWORD_ENVIRONMENT, SMSGW_ENVIRONMENT };
//            }
//            catch (Exception ex)
//            {
//                return new string[] { ex.Message, ex.StackTrace };
//            }
//        }
//    }
//}