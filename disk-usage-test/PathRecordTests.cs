using disk_usage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace disk_usage_test
{
    [TestClass]
    public class PathRecordTests
    {
        [TestMethod]
        public void LocalRegexTest()
        {
            const string input = "C:\\";
            var result = PathRecord.LocalRegex.IsMatch(input);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void LocalRegexTestFail()
        {
            const string input = "\\\\Server\\Share\\";
            var result = PathRecord.LocalRegex.IsMatch(input);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void NetworkRegexTest()
        {
            const string input = "\\\\Server\\Share\\";
            var result = PathRecord.UncNamedRegex.IsMatch(input);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void NetworkRegexTestFail()
        {

            const string input = "C:\\";
            var result = PathRecord.UncNamedRegex.IsMatch(input);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void NetworkRegexIp()
        {

            const string input = "\\\\0.0.0.0\\Share\\";
            var result = PathRecord.UncNamedRegex.IsMatch(input);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void PathRecordShortcut1()
        {
            var pr = new PathRecord { Path = "C:\\" };
            var result = pr.ShortcutName;

            Assert.AreEqual("C", result);
        }

        [TestMethod]
        public void PathRecordShortcut2()
        {
            var pr = new PathRecord { Path = "\\\\192.169.10.1\\Share\\" };
            var result = pr.ShortcutName;

            Assert.AreEqual("192_169_10_1 Share", result);
        }

        [TestMethod]
        public void PathRecordShortcut3()
        {
            var pr = new PathRecord { Path = "\\\\192.169.10.1\\Share\\Share 2\\" };
            var result = pr.ShortcutName;

            Assert.AreEqual("192_169_10_1 Share Share 2", result);
        }

        [TestMethod]
        public void PathRecordShortcut4()
        {
            var pr = new PathRecord
            {
                Path = "\\\\192.169.10.1\\Share\\Share 2\\",
                FriendlyName = "Te*stin|g:Inv?alid\\Chars.<Text>"
            };
            var result = pr.ShortcutName;

            Assert.AreEqual("TestingInvalid Chars_Text", result);
        }

        [TestMethod]
        public void InvalidPathRecordPath()
        {
            var pr = new PathRecord { Path = "invalidpath" };
            pr.RequestDiskInfo();
            Assert.AreEqual(0, pr.Capacity.Bytes); //expect zero capacity

        }

    }
}