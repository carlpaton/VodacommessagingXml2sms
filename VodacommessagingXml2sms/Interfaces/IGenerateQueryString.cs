using SharedModels;
using System.Collections.Generic;

namespace VodacommessagingXml2sms.Interfaces
{
    public interface IGenerateQueryString
    {
        List<SmsModel> SmsModel { get; set; }
        long Watermark { get; set; }

        string ForSend();
        string ForSendLog();
    }
}
