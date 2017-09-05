using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var http = "http://www.yxl.com:5001/signin-aad";

            var uri = new Uri(http);
            var a = uri.Host;
            Assert.AreEqual("www.yxl.com",a);
        }
    }
}
