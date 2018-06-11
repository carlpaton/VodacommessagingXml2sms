using Newtonsoft.Json;
using SharedModels;
using System;
using System.Collections.Generic;
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

        readonly string _responseType;
        readonly string _mockMode;

        #region only used with mocking
        public List<SmsModel> SmsModel { get; set; }
        public string WaterMarkId { get; set; }
        #endregion

        public SmsRequest(string responseType = "", string mockMode = "0")
        {
            _responseType = responseType;
            _mockMode = mockMode;
        }

        public string SendResponse()
        {
            var apiReponse = "";

            if (_mockMode.Equals("0"))
                apiReponse = DoWebRequest(GenerateUrl.GenerateSend());

            if (_mockMode.Equals("1")) //mock request
                apiReponse = Mocking.MockSmsRequest.MockSendResponse(SmsModel);

            return FormatResponse(apiReponse);
        }

        public string SendLogResponse()
        {
            var apiReponse = "";

            if (_mockMode.Equals("0"))
                apiReponse = DoWebRequest(GenerateUrl.GenerateSendLog());

            if (_mockMode.Equals("1")) //mock request
                apiReponse = Mocking.MockSmsRequest.MockSendLogResponse(WaterMarkId);

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
