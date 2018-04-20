using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.DI;

namespace Tests.ServiceTests
{
    [TestClass]
    public class GenerateQueryStringTests
    {
        [TestMethod]
        public void GenerateQueryString_Go()
        {
            // Act
            // Arrange
            var queryString = InjectQueryString.Go();

            // Assert
            Assert.IsTrue(queryString.Length == 102);
        }
    }
}
