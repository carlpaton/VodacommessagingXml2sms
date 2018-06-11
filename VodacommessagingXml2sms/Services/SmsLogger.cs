using System;
using VodacommessagingXml2sms.Interfaces;

namespace VodacommessagingXml2sms.Services
{
    public class SmsLogger : ISmsLogger
    {
        public void WriteLine(string[] values)
        {
            var result = string.Join(" ", values);
            Console.WriteLine(result);
        }
    }
}
