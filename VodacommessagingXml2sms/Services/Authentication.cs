using System.Text;
using VodacommessagingXml2sms.Interfaces;

namespace VodacommessagingXml2sms.Services
{
    public class Authentication : IAuthentication
    {
        private readonly string _username;
        private readonly string _password;

        public Authentication(string username = "", string password = "")
        {
            _username = username;
            _password = password;
        }

        /// <summary>
        /// String to encode: ‘username:password’
        /// Encoded string: ‘dXNlcm5hbWU6cGFzc3dvcmQ=’
        /// Authorization header: ‘Basic dXNlcm5hbWU6cGFzc3dvcmQ=’
        /// </summary>
        /// <returns></returns>
        public string SetHeader()
        {
            var auth = string.Format("{0}:{1}", _username, _password);
            var plainTextBytes = Encoding.ASCII.GetBytes(auth);

            var base64Encoded = System.Convert.ToBase64String(plainTextBytes);
            var completeHeader = string.Format("Basic {0}", base64Encoded);

            return completeHeader;
        }
    }
}