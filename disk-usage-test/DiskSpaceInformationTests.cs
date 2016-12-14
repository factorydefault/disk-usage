using disk_usage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace disk_usage_test
{
    [TestClass]
    public class DiskSpaceInformationTests
    {
        [TestMethod]
        public void PercentageAddsTo100()
        {
            var obj = new DiskAttributes(0,0,0);
            var result = obj.PercentageFree + obj.PercentageFilled;
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void UsedPercentage()
        {
            var obj = new DiskAttributes(150, 400, 200);
            Assert.AreEqual(50, obj.PercentageFilled);
        }

    }
}
