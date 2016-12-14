using System;
using disk_usage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace disk_usage_test
{
    [TestClass]
    public class WindowsTests
    {
        [TestMethod]
        public void OsNameTestWin10()
        {
            var version = new Version(10, 1);
            var result = version.Name();
            Assert.AreEqual(OsName.Windows10, result);
        }
        [TestMethod]
        public void OsNameTestWin8()
        {
            var version = new Version(6, 2);
            var result = version.Name();
            Assert.AreEqual(OsName.Windows8, result);
        }
        [TestMethod]
        public void OsNameTestWin7()
        {
            var version = new Version(6, 1);
            var result = version.Name();
            Assert.AreEqual(OsName.Windows7, result);
        }
    }
}