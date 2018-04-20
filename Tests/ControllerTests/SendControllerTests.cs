using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedModels;
using System;
using System.Collections.Generic;
using Tests.DI;
using VodacommessagingXml2sms.Controllers;

namespace Tests.ControllerTests
{
    [TestClass]
    public class SendControllerTests
    {
        [TestMethod]
        public void Send_PostSingleSms()
        {
            // Act
            var injectOptions = new InjectOptions();

            var controller = new SendController(
                injectOptions.Authentication, 
                injectOptions.GenerateQueryString, 
                injectOptions.GenerateUrl,
                injectOptions.SmsRequest);

            // Arrange
            var body = new List<SmsModel>
            {
                new SmsModel() { Message = "this is a test message " + DateTime.Now.ToString("yyyyMMddhhsss"), Number = "27831111111" }
            };
            var response = controller.Post(body);

            // Assert
            Assert.IsTrue(response != "");
            Assert.IsTrue(response.Length > 10);
        }

        [TestMethod]
        public void Send_PostMuiltipleSms()
        {
            // Act
            var injectOptions = new InjectOptions();

            var controller = new SendController(
                injectOptions.Authentication,
                injectOptions.GenerateQueryString,
                injectOptions.GenerateUrl,
                injectOptions.SmsRequest);

            // Arrange
            var body = new List<SmsModel>
            {
                new SmsModel() { Message = "message 1 " + DateTime.Now.ToString("yyyyMMddhhsss"), Number = "27831111111" },
                new SmsModel() { Message = "message 2 " + DateTime.Now.ToString("yyyyMMddhhsss"), Number = "27831111111" }
            };
            var response = controller.Post(body);

            // Assert
            Assert.IsTrue(response != "");
            Assert.IsTrue(response.Length > 10);
        }
    }
}
