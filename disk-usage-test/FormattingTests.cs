using disk_usage;
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

        [TestMethod]
        public void ExplorerLabelTestEmpty()
        {
            var input = ByteSizeLib.ByteSize.FromBytes(0);
            var expected = "";
            var result = Formatting.ExplorerLabel(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EllipsisTest()
        {
            //arrange
            var input = "A long string was found in the woods";

            var expected = "A long string was found...";

            //act
            var result = input.Ellipsis(23);

            //assert
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void EllipsisTestNegative()
        {
            //arrange
            var input = "A long string was found in the woods";

            var expected = "A long string was found...";

            //act
            var result = input.Ellipsis(-23);

            //assert
            Assert.AreEqual(expected, result); //when given a negative length, consider length as positive
        }

        [TestMethod]
        public void EllipsisTestZero()
        {
            //arrange
            var input = "A long string was found in the woods";

            var expected = "...";

            //act
            var result = input.Ellipsis(0);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EllipsisTestEmptyString()
        {
            //arrange
            var input = "";

            var expected = "";

            //act
            var result = input.Ellipsis();

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ClampTestInverted()
        {
            var result = Formatting.Clamp(6, 100, 0);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void ClampTest()
        {
            var result = Formatting.Clamp(6, 0, 100);
            Assert.AreEqual(6, result);
        }


        [TestMethod]
        public void ClampTestOver()
        {
            var result = Formatting.Clamp(106, 0, 100);
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void ClampTestUnder()
        {
            var result = Formatting.Clamp(6, 50, 100);
            Assert.AreEqual(50, result);
        }

        [TestMethod]
        public void ClampTestCriteriaSame()
        {
            var result = Formatting.Clamp(6, 60, 60);
            Assert.AreEqual(60, result);
        }
    }
}