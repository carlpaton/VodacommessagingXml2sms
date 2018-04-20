namespace VodacommessagingXml2sms.Interfaces
{
    public interface ISmsRequest
    {
        IGenerateUrl GenerateUrl { get; set; }
        IAuthentication Authentication { get; set; }

        string SendResponse();
        string SendLogResponse();
    }
}
