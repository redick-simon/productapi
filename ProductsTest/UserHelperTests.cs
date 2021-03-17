using NUnit.Framework;
using ProductWebApi.Authentication;
using ProductWebApi.Models;

namespace ProductsTest
{
    [TestFixture]
    public class UserHelperTests
    {
        private IProductService _mockService;
        private UserHelper _helper;

        [SetUp]
        public void Setup()
        {
            _mockService = new MockService();
            _helper = new UserHelper(_mockService);
        }
        
        [Test]
        [TestCase("test", "1234", true)]
        [TestCase("test1", "1234", false)]
        [TestCase("Test", "1234", true)]
        [TestCase("Test", "1235", false)]
        public void TestValidate(string username, string password, bool expected)
        {
            bool actual = UserHelper.Validate(username, password);

            Assert.AreEqual(expected, actual);
        }
    }
}