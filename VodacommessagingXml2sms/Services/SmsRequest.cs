using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Xml;
using VodacommessagingXml2sms.Interfaces;

namespace VodacommessagingXml2sms.Services
{
    public class SmsRequest : ISmsRequest
    {
        public IGenerateUrl GenerateUrl { get; set; }
        public IAuthentication Authentication { get; set; }

        string _responseType;

        public SmsRequest(string responseType = "")
        {
            _responseType = responseType;
        }

        public string SendResponse()
        {
            var apiReponse = DoWebRequest(GenerateUrl.GenerateSend());
            return FormatResponse(apiReponse);
        }

        public string SendLogResponse()
        {
            var apiReponse = DoWebRequest(GenerateUrl.GenerateSendLog());
            return FormatResponse(apiReponse);
        }

        #region helpers
        string DoWebRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", Authentication.SetHeader());

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        string FormatResponse(string v)
        {
            if (_responseType.Equals("xml", StringComparison.CurrentCultureIgnoreCase))
                return v;
            else
            {
                //assume json 

                var doc = new XmlDocument();
                doc.LoadXml(v);

                return JsonConvert.SerializeXmlNode(doc);
            }
        }
        #endregion
    }
}
