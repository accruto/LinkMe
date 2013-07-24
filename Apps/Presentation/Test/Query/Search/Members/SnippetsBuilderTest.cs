using LinkMe.Apps.Presentation.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Presentation.Test.Query.Search.Members
{
    [TestClass]
    public class SnippetsBuilderTest
    {
        private static readonly string HalfMinusTwo = new string('x', SnippetsBuilder.MAX_TOTAL_CHARS / 2 - 2);
        private SnippetsBuilder _builder;

        [TestInitialize]
        public void TestInitialize()
        {
            _builder = new SnippetsBuilder("... ", " ...", " ... ");
        }

        [TestMethod]
        public void NoSnippets()
        {
            Assert.AreEqual("", _builder.ToString());
        }

        [TestMethod]
        public void One()
        {
            _builder.Add("One.");
            Assert.AreEqual("One.", _builder.ToString());
        }

        [TestMethod]
        public void OneTwoThree()
        {
            _builder.Add("One.");
            _builder.Add("Two.");
            _builder.Add("Three.");
            Assert.AreEqual("One. Two. Three.", _builder.ToString());
        }

        [TestMethod]
        public void OneXTwoThree()
        {
            _builder.Add("One ...");
            _builder.Add("Two.");
            _builder.Add("Three.");
            Assert.AreEqual("One ... Two. Three.", _builder.ToString());
        }

        [TestMethod]
        public void OneXTwoXThree()
        {
            _builder.Add("One ...");
            _builder.Add("Two ...");
            _builder.Add("Three.");
            Assert.AreEqual("One ... Two ... Three.", _builder.ToString());
        }

        [TestMethod]
        public void OneXXTwoXThree()
        {
            _builder.Add("One ...");
            _builder.Add("... Two.");
            _builder.Add("... Three.");
            Assert.AreEqual("One ... Two. ... Three.", _builder.ToString());
        }

        [TestMethod]
        public void XOneXXTwoXThreeX()
        {
            _builder.Add("... One ...");
            _builder.Add("... Two.");
            _builder.Add("... Three ...");
            Assert.AreEqual("... One ... Two. ... Three ...", _builder.ToString());
        }

        [TestMethod]
        public void Half2Half2Z()
        {
            _builder.Add(HalfMinusTwo);
            _builder.Add(HalfMinusTwo);
            _builder.Add("z.");
            string combined = _builder.ToString();
            Assert.AreEqual(SnippetsBuilder.MAX_TOTAL_CHARS, combined.Length);
            Assert.AreEqual(HalfMinusTwo + " " + HalfMinusTwo + " z.", combined);
        }

        [TestMethod, Ignore]
        public void Half2Half2Zz()
        {
            _builder.Add(HalfMinusTwo);
            _builder.Add(HalfMinusTwo);
            _builder.Add("zz.");
            string combined = _builder.ToString();
            Assert.AreEqual(HalfMinusTwo + " " + HalfMinusTwo, combined);
        }

        [TestMethod, Ignore]
        public void Half2Half2More()
        {
            _builder.Add(HalfMinusTwo);
            _builder.Add(HalfMinusTwo);
            _builder.Add("an end.");
            string combined = _builder.ToString();
            Assert.AreEqual(HalfMinusTwo + " " + HalfMinusTwo, combined);
        }

        [TestMethod, Ignore]
        public void Half2Half2MoreX()
        {
            _builder.Add(HalfMinusTwo);
            _builder.Add(HalfMinusTwo);
            _builder.Add("a b c d e ...");
            string combined = _builder.ToString();
            Assert.AreEqual(HalfMinusTwo + " " + HalfMinusTwo, combined);
        }

        [TestMethod, Ignore]
        public void Half2Half2XXMore()
        {
            // Exactly the maxmimum number of characters.
            string secondHalf = HalfMinusTwo.Substring(0, HalfMinusTwo.Length - 1) + " ...";
            _builder.Add(HalfMinusTwo);
            _builder.Add(secondHalf);
            _builder.Add("... the end.");
            string combined = _builder.ToString();
            Assert.AreEqual(HalfMinusTwo + " " + secondHalf, combined);
        }

        [TestMethod]
        public void CombineSnippetsBug()
        {
            // Bug found in UAT

            var builder = new SnippetsBuilder("<strong>...</strong> ", " <strong>...</strong>", " <strong>...</strong> ");

            builder.Add(new string('x', 100) +
                "<strong>...</strong> to prepare <span class=\"highlighted-word\">test</span> strategies,"
                + " <span class=\"highlighted-word\">test</span> plans including <span class=\"highlightedWord\">test</span> matrixes"
                + " for <span class=\"highlighted-word\">test</span> coverage, <span class=\"highlightedWord\">test</span> cases"
                + " <strong>...</strong>");
            builder.Add(
                "<strong>...</strong> background in both <span class=\"highlighted-word\">test</span> planing and execution <strong>...</strong>");
            builder.Add("2008: Snr <span class=\"highlighted-word\">Test</span> Analyst, <strong>...</strong>");

            string combined = builder.ToString();
            Assert.AreEqual("<strong>...</strong> background in both <span class=\"highlighted-word\">test</span> planing and"
                + " execution <strong>...</strong> 2008: Snr <span class=\"highlighted-word\">Test</span> Analyst, <strong>...</strong>",
                combined);
            Assert.IsTrue(combined.Length < SnippetsBuilder.MAX_TOTAL_CHARS + 1);
        }
    }
}
