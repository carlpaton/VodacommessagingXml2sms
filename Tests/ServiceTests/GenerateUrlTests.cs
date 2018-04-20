using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.DI;

namespace Tests.ServiceTests
{
    [TestClass]
    public class GenerateUrlTests
    {
        [TestMethod]
        public void SetHeader_WithUserConfigValues()
        {
            // Act
            var injectOptions = new InjectOptions();
            var objGenerateUrl = injectOptions.GenerateUrl;

            // Arrange
            var url = objGenerateUrl.GenerateSend();

            // Assert
            Assert.IsTrue(url != "");
            Assert.IsTrue(url.Length > 10);
        }
    }
}
