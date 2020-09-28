using LibPureCoolLink.Net.Model;
using NUnit.Framework;

namespace LibPureCoolLink.Tests.Model
{
    public class DysonAccountTests
    {
        private DysonAccount _account;

        [SetUp]
        public void Setup()
        {
            _account = new DysonAccount("appapi.cp.dyson.com",
                "test@test.de",
                "test",
                "de");
        }

        [Test]
        public void TestLogin()
        {
            var isLoggedIn = _account.Login();
            Assert.IsTrue(isLoggedIn);
        }
    }
}