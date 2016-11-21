using Microsoft.VisualStudio.TestTools.UnitTesting;
using scm = System.ComponentModel;

namespace disk_usage.Tests
{
    [TestClass]
    public class EnumsTests
    {
        public enum TestEnum
        {
            [scm.Description("First Item")]
            One,
            [scm.Description("Second Item")]
            Two,
            Three,
            [scm.Description("")]
            Four
        }

        [TestMethod]
        public void GetDescriptionTest()
        {
            var input = TestEnum.One;
            var result = input.GetDescription();
            Assert.AreEqual("First Item", result);
        }

        [TestMethod]
        public void GetDescriptionTestTwo()
        {
            var input = TestEnum.Two;
            var result = input.GetDescription();
            Assert.AreEqual("Second Item", result);
        }

        [TestMethod]
        public void GetDescriptionTestThree()
        {
            var input = TestEnum.Three; //enum has no description
            var result = input.GetDescription();
            Assert.AreEqual(nameof(TestEnum.Three), result);
        }

        [TestMethod]
        public void GetDescriptionTestFour()
        {
            var input = TestEnum.Four; //enum has no description
            var result = input.GetDescription();
            Assert.AreEqual(nameof(TestEnum.Four), result);
        }
    }
}