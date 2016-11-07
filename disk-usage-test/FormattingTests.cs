using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace disk_usage.Tests
{
    [TestClass]
    public class FormattingTests
    {
        [TestMethod]
        public void ExplorerLabelTestMB()
        {
            var input = ByteSizeLib.ByteSize.FromMegaBytes(120.321);
            var expected = "120 MB";
            var result = Formatting.ExplorerLabel(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExplorerLabelTestGB()
        {
            var input = ByteSizeLib.ByteSize.FromGigaBytes(120.321);
            var expected = "120.3 GB";
            var result = Formatting.ExplorerLabel(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExplorerLabelTestTB()
        {
            var input = ByteSizeLib.ByteSize.FromTeraBytes(1.328);
            var expected = "1.33 TB";
            var result = Formatting.ExplorerLabel(input);
            Assert.AreEqual(expected, result);
        }

    }
}