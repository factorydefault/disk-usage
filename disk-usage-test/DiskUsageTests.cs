using System;
using System.Linq;
using disk_usage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace disk_usage_test
{
    [TestClass()]
    public class DiskUsageTests
    {
        [TestMethod()]
        public void CreateAndAddPath()
        {
            var list = new DiskUsage(saveToDisk: false);

            Assert.AreEqual(0,list.Paths.Count);

            list.AddPathToList("C:\\", "C");

            Assert.AreEqual(1, list.Paths.Count);

            //dont add duplicates

            list.AddPathToList("C:\\", "C");

            Assert.AreEqual(1, list.Paths.Count);

            //now remove 

            list.RemovePathFromList("C:\\");

            Assert.AreEqual(0, list.Paths.Count);

        }

        [TestMethod()]
        public void CheckLocation()
        {
            var list = new DiskUsage(saveToDisk: false);

            var local = new PathRecord {FriendlyName = "F", Path = "F:\\"};

            Assert.AreEqual(PathLocation.Local, local.Location());

            var remote = new PathRecord { FriendlyName = "Share", Path = "\\\\Server\\Share" };

            Assert.AreEqual(PathLocation.Remote, remote.Location());

            var os = new PathRecord { FriendlyName = "OS Disk", Path = $"{Windows.InstallDirectory}" };

            Assert.AreEqual(PathLocation.Os, os.Location());



        }

        PathRecord _testPathRecord;

        PathRecord TestPathRecord
        {
            get
            {
                if (_testPathRecord == null)
                {
                    _testPathRecord = new PathRecord
                    {
                        FriendlyName = "Test Disk",
                        Path = $"\\\\test\\test",
                        DiskAttributes = new DiskAttributes(9000, 10000, 5000)
                    };
                    //_testPathRecord.RequestDiskInfo();
                }
                return _testPathRecord;
            }

        }


        [TestMethod]
        public void Disk()
        {
            var pr = TestPathRecord;

            var fillLevel = pr.FillLevel;

            Console.WriteLine($"Fill level: {fillLevel}");

            bool isWithinRange = fillLevel >= 0 && fillLevel <= 100;

            Assert.IsInstanceOfType(fillLevel,typeof(int));
            Assert.IsTrue(isWithinRange);

            Assert.AreEqual(50.0,pr.FillPercentageDbl);

        }

        [TestMethod]
        public void FreePlusUsedEqualsCapacity()
        {
            var pr = TestPathRecord;

            var sum = pr.FreeSpace + pr.UsedSpace;

            var capacity = pr.Capacity;

            Assert.AreEqual(capacity,sum);
        }

        [TestMethod]
        public void AvailableBytes()
        {
            var attr = TestPathRecord.DiskAttributes;

            Console.WriteLine(attr.AsStringMessage());
            
            Assert.AreEqual(9000, (int) attr.AvailableBytes);
        }

        [TestMethod]
        public void ZeroCapacity()
        {
            Assert.IsFalse(TestPathRecord.HasZeroCapacity);

            var pr = new PathRecord {DiskAttributes = new DiskAttributes(0, 0, 0)};

            Assert.IsTrue(pr.HasZeroCapacity);

        }


        [TestMethod]
        public void LowDiskSpace()
        {
            Assert.IsFalse(TestPathRecord.HasLowDiskSpace);

            var pr = new PathRecord {DiskAttributes = new DiskAttributes(20, 512, 32)}; //94%
            
            Console.WriteLine(pr.FillLevel);

            Assert.IsTrue(pr.HasLowDiskSpace);

            var pr2 = new PathRecord { DiskAttributes = new DiskAttributes(20, 512, 52) }; //90%

            Console.WriteLine(pr2.FillLevel);

            Assert.IsFalse(pr2.HasLowDiskSpace);
        }

        [TestMethod]
        public void DiskInfoAync()
        {
            var pr = new PathRecord {Path = Windows.InstallDirectory, FriendlyName = "Test"};

            Assert.IsTrue(pr.HasZeroCapacity);

            pr.RequestInfoTask.Wait();
                        
            Assert.IsFalse(pr.HasZeroCapacity);
        }
    }
}