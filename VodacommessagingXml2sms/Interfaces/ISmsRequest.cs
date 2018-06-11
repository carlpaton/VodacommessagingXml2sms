using SharedModels;
using System.Collections.Generic;

namespace VodacommessagingXml2sms.Interfaces
{
    public interface ISmsRequest
    {
        IGenerateUrl GenerateUrl { get; set; }
        IAuthentication Authentication { get; set; }

        string WaterMarkId { get; set; }
        List<SmsModel> SmsModel { get; set; }

        string SendResponse();
        string SendLogResponse();
    }
}
