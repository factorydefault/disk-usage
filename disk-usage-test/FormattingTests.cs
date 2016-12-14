using disk_usage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace disk_usage_test
{
    [TestClass]
    public class FormattingTests
    {
        [TestMethod]
        public void ExplorerLabelTestMb()
        {
            var input = ByteSizeLib.ByteSize.FromMegaBytes(120.321);
            const string expected = "120 MB";
            var result = input.ExplorerLabel();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExplorerLabelTestGb()
        {
            var input = ByteSizeLib.ByteSize.FromGigaBytes(120.321);
            const string expected = "120.3 GB";
            var result = input.ExplorerLabel();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExplorerLabelTestTb()
        {
            var input = ByteSizeLib.ByteSize.FromTeraBytes(1.328);
            const string expected = "1.33 TB";
            var result = input.ExplorerLabel();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExplorerLabelTestEmpty()
        {
            var input = ByteSizeLib.ByteSize.FromBytes(0);
            const string expected = "";
            var result = input.ExplorerLabel();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EllipsisTest()
        {
            //arrange
            const string input = "A long string was found in the woods";

            const string expected = "A long string was found...";

            //act
            var result = input.Ellipsis(23);

            //assert
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void EllipsisTestNegative()
        {
            //arrange
            const string input = "A long string was found in the woods";

            const string expected = "A long string was found...";

            //act
            var result = input.Ellipsis(-23);

            //assert
            Assert.AreEqual(expected, result); //when given a negative length, consider length as positive
        }

        [TestMethod]
        public void EllipsisTestZero()
        {
            //arrange
            const string input = "A long string was found in the woods";

            const string expected = "...";

            //act
            var result = input.Ellipsis(0);

            //assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EllipsisTestEmptyString()
        {
            //arrange
            const string input = "";

            const string expected = "";

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
            var result = Formatting.Clamp(6);
            Assert.AreEqual(6, result);
        }


        [TestMethod]
        public void ClampTestOver()
        {
            var result = Formatting.Clamp(106);
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void ClampTestUnder()
        {
            var result = Formatting.Clamp(6, 50);
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