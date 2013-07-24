using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test.Utilities
{
    [TestClass]
    public class RegexUtilTest
    {
        [TestMethod]
        public void Test1357()
        {
            Assert.AreEqual("4[0-9]|3[0-9]|2[0-9]|1[3-9]|5[0-7]", RegexUtil.RegexForRange(13, 57));
        }

        [TestMethod]
        public void Test19832011()
        {
            Assert.AreEqual("200[0-9]|199[0-9]|198[3-9]|201[0-1]", RegexUtil.RegexForRange(1983, 2011));
        }

        [TestMethod]
        public void Test99112()
        {
            Assert.AreEqual("99|10[0-9]|11[0-2]", RegexUtil.RegexForRange(99, 112));
        }
    }
}
