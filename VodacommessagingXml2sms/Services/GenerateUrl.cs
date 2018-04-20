using System.Text;
using VodacommessagingXml2sms.Interfaces;

namespace VodacommessagingXml2sms.Services
{
    public class GenerateUrl : IGenerateUrl
    {
        public string Querystring { get; set; }

        string _smsGateway;

        public GenerateUrl(string smsGateway = "")
        {
            _smsGateway = smsGateway;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// https://smsgwX.gsm.co.za/xml/send?number=+27721234567&message=Message+Text
        /// </returns>
        public string GenerateSend()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}/xml/send", _smsGateway);
            sb.Append(Querystring);

            return sb.ToString();
        }

        public string GenerateSendLog()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}/xml/sendlog", _smsGateway);
            sb.Append(Querystring);

            return sb.ToString();
        }
    }
}
