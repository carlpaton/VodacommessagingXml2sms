using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.DI;
using VodacommessagingXml2sms.Controllers;

namespace Tests.ControllerTests
{
    [TestClass]
    public class SendLogControllerTests
    {
        [TestMethod]
        public void SendLog_Get()
        {
            // Act
            var injectOptions = new InjectOptions();

            var controller = new SendLogController(
                injectOptions.Authentication,
                injectOptions.GenerateQueryString,
                injectOptions.GenerateUrl,
                injectOptions.SmsRequest);

            // Arrange
            string waterMark = "201342443";
            var response = controller.Get(waterMark);

            // Assert
            Assert.IsTrue(response != "");
            Assert.IsTrue(response.Length > 10);
        }
    }
}
