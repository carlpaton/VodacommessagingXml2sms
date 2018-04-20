using SharedModels;
using System.Collections.Generic;
using VodacommessagingXml2sms.Services;

namespace Tests.DI
{
    public static class InjectQueryString
    {
        public static string Go()
        {
            var body = new List<SmsModel>
            {
                new SmsModel() { Message = "this is a test message", Number = "0831111111" },
                new SmsModel() { Message = "and this is another test", Number = "0831111111" }
            };
            var obj = new GenerateQueryString
            {
                SmsModel = body
            };

            return obj.ForSend();
        }
    }
}
