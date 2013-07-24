using LinkMe.Framework.Utility.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test.Utilities
{
    [TestClass]
    public class XmlUtilsTest
        : TestClass
    {
        [TestMethod]
        public void TestStripInvalid()
        {
            Assert.AreEqual(null, XmlExtensions.StripInvalidCharsFromXml(null));
            Assert.AreEqual("", "".StripInvalidCharsFromXml());
            Assert.AreEqual("<normal>XML!</normal>", "<normal>XML!</normal>".StripInvalidCharsFromXml());
            Assert.AreEqual("spaces\tabs\rand\n.~ all OK", "spaces\tabs\rand\n.~ all OK".StripInvalidCharsFromXml());
            Assert.AreEqual("now try  and  or  and how about ?", "now try \a and \0 or \v and how about \xFFFF?".StripInvalidCharsFromXml());
            Assert.AreEqual("and of course", "\0and\xc of course\0".StripInvalidCharsFromXml());
        }

        [TestMethod]
        public void TestStripNonAscii()
        {
            Assert.AreEqual("abcd", "abcd".StripInvalidAsciiCharsForXml());

            string expected = "	Cons";
            string complexData = "·" + expected;
            Assert.AreEqual(expected, complexData.StripInvalidAsciiCharsForXml());

            expected = "	?Cons?";
            complexData = "·" + expected;
            Assert.AreEqual(expected, complexData.StripInvalidAsciiCharsForXml());
        }
    }
}
