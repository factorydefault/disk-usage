using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace disk_usage.Tests
{
    [TestClass]
    public class WindowsTests
    {
        [TestMethod]
        public void OSNameTestWin10()
        {
            var version = new Version(10, 1);
            var result = version.Name();
            Assert.AreEqual(OSName.Windows10, result);
        }
        [TestMethod]
        public void OSNameTestWin8()
        {
            var version = new Version(6, 2);
            var result = version.Name();
            Assert.AreEqual(OSName.Windows8, result);
        }
        [TestMethod]
        public void OSNameTestWin7()
        {
            var version = new Version(6, 1);
            var result = version.Name();
            Assert.AreEqual(OSName.Windows7, result);
        }
    }
}