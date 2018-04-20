using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.DI;
using VodacommessagingXml2sms.Services;

namespace Tests.ServiceTests
{
    [TestClass]
    public class AuthenticationTests
    {
        [TestMethod]
        public void SetHeader_WithUserConfigValues()
        {
            // Act
            var injectOptions = new InjectOptions();
            var objAuthentication = injectOptions.Authentication;

            // Arrange
            var headerValue = objAuthentication.SetHeader();

            // Assert
            Assert.IsTrue(headerValue != "");
            Assert.IsTrue(headerValue.Length > 10);
        }

        [TestMethod]
        public void SetHeader_WithExampleData()
        {
            // Act
            var objAuthentication = new Authentication("username", "password");

            // Arrange
            var headerValue = objAuthentication.SetHeader();

            // Assert
            Assert.IsTrue(headerValue.Equals("Basic dXNlcm5hbWU6cGFzc3dvcmQ=", System.StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
