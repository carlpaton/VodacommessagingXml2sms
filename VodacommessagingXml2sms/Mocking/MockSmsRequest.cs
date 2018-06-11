using Newtonsoft.Json;
using SharedModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VodacommessagingXml2sms.Mocking
{
    public static class MockSmsRequest
    {
        public static string MockSendLogResponse(string WaterMarkId)
        {
            //using XmlSerializer & SubmitResultModel added nodes in the XML that the SMS Gateway does not
            //this is no good for sandbox, it was easier to build the xml manually

            var list = ReadMock().Where(x => x.Key >= Convert.ToInt32(WaterMarkId)).ToList();

            var xml = "<?xml version=\"1.0\"?><aatsms>";
            foreach (var item in list)
            {
                xml += "<message type=\"sent\" key=\"" + item.Key + "\" tonumber=\"" + item.ToNumber + "\" message=\"" + item.Message + "\" timesent=\"" + item.Timesent.ToString("dd-MMM-yyyy hh:mm:ss") + "\" timedelivered=\"" + item.Timedelivered.ToString("dd-MMM-yyyy hh:mm:ss") + "\" delivered=\"1\" status=\"0\" statusdescription=\"Delivered\" />";
            }
            xml += "</aatsms>";

            return xml;
        }
        public static string MockSendResponse(List<SmsModel> SmsModel)
        {
            //using XmlSerializer & SubmitResultModel added nodes in the XML that the SMS Gateway does not
            //this is no good for sandbox, it was easier to build the xml manually

            var random = new Random();
            var randomNumber = random.Next(1, 1000);

            var list = ReadMock();
            var latestObj = list.OrderByDescending(x => x.Key).FirstOrDefault();
            if (latestObj != null)
                randomNumber = latestObj.Key + 1;

            var xml = "<?xml version=\"1.0\"?><aatsms>";
            foreach (var item in SmsModel)
            {
                list.Add(new MockSmsModel()
                {
                    Key = randomNumber,
                    Message = item.Message,
                    ToNumber = item.Number,
                    Timesent = DateTime.Now,
                    Timedelivered = DateTime.Now.AddSeconds(5)
                });

                xml += "<submitresult action=\"enqueued\" key=\"" + randomNumber + "\" result=\"1\" number=\"" + item.Number + "\" />";
                randomNumber++;
            }
            xml += "</aatsms>";

            PersistMock(list);
            return xml;
        }

        private static void PersistMock(List<MockSmsModel> list)
        {
            var json = JsonConvert.SerializeObject(list);
            File.WriteAllText("MockSmsModel.json", json);
        }

        private static List<MockSmsModel> ReadMock()
        {
            var file = "MockSmsModel.json";
            var obj = new List<MockSmsModel>();

            if (File.Exists(file))
            {
                var json = File.ReadAllText(file);
                obj = JsonConvert.DeserializeObject<List<MockSmsModel>>(json);
            }
            return obj;
        }
    }

    public class MockSmsModel
    {
        public int Key { get; set; }
        public string Message { get; set; }
        public string ToNumber { get; set; }
        public DateTime Timesent { get; set; }
        public DateTime Timedelivered { get; set; }
    }
}
