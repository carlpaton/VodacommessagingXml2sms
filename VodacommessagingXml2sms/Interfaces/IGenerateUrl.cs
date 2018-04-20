namespace VodacommessagingXml2sms.Interfaces
{
    public interface IGenerateUrl
    {
        string Querystring { get; set; }

        string GenerateSend();
        string GenerateSendLog();
    }
}
