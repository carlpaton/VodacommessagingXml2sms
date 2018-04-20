using SharedModels;
using System.Collections.Generic;
using System.Text;
using System.Web;
using VodacommessagingXml2sms.Interfaces;

namespace VodacommessagingXml2sms.Services
{
    public class GenerateQueryString : IGenerateQueryString
    {
        public List<SmsModel> SmsModel { get; set; }
        public long Watermark { get; set; }

        public string ForSend()
        {
            var sb = new StringBuilder();
            int counter = 1;

            if (SmsModel.Count > 1)
            {
                #region Multiple Message [GET]
                foreach (var item in SmsModel)
                {
                    if (counter == 1)
                        sb.Append("?");

                    sb.AppendFormat("number{0}={1}&message{0}={2}", counter, item.Number, HttpUtility.UrlEncode(item.Message));

                    if (counter >= 1 && counter != SmsModel.Count)
                        sb.Append("&");

                    counter++;
                }
                #endregion
            }
            else
            {
                #region Single Message [GET]
                var smsModel = SmsModel[0];
                sb.AppendFormat("?number={0}&message={1}", smsModel.Number, HttpUtility.UrlEncode(smsModel.Message));
                #endregion
            }

            var qs = sb.ToString();
            return qs;
        }

        public string ForSendLog()
        {
            return string.Format("?watermark={0}", Watermark);
        }
    }
}
