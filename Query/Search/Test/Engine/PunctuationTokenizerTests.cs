using System;
using System.Linq;
using LinkMe.Framework.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Engine
{
    [TestClass]
    public class PunctuationTokenizerTests
    {
        private readonly ITextAnnotator _tokenizer = new PunctuationTokenizer();

        [TestMethod]
        public void A()
        {
            CollectionAssert.AreEqual(new[] {"A"}, GetTokens("A"));
        }

        [TestMethod]
        public void AX()
        {
            CollectionAssert.AreEqual(new[] { "A" }, GetTokens("A,"));
        }

        [TestMethod]
        public void XA()
        {
            CollectionAssert.AreEqual(new[] { "A" }, GetTokens(",A"));
        }

        [TestMethod]
        public void X()
        {
            CollectionAssert.AreEqual(new string[0], GetTokens(","));
        }

        [TestMethod]
        public void AXB()
        {
            CollectionAssert.AreEqual(new[] { "A", "B" }, GetTokens("A,B"));
        }

        [TestMethod]
        public void AXBX()
        {
            CollectionAssert.AreEqual(new[] { "A", "B" }, GetTokens("A,B,"));
        }

        [TestMethod]
        public void AXXB()
        {
            CollectionAssert.AreEqual(new[] { "A", "B" }, GetTokens("A,,B"));
        }

        [TestMethod]
        public void XXA()
        {
            CollectionAssert.AreEqual(new[] { "A" }, GetTokens(",,A"));
        }

        [TestMethod]
        public void AXX()
        {
            CollectionAssert.AreEqual(new[] { "A" }, GetTokens("A,,"));
        }

        [TestMethod]
        public void AXBXX()
        {
            CollectionAssert.AreEqual(new[] { "A", "B" }, GetTokens("A,B,,"));
        }

        [TestMethod]
        public void A_()
        {
            CollectionAssert.AreEqual(new[] { "A" }, GetTokens("A-"));
        }

        [TestMethod]
        public void _A()
        {
            CollectionAssert.AreEqual(new[] { "A" }, GetTokens("-A"));
        }

        [TestMethod]
        public void _()
        {
            CollectionAssert.AreEqual(new string[0], GetTokens("-"));
        }

        [TestMethod]
        public void A_B()
        {
            CollectionAssert.AreEqual(new[] { "A-B" }, GetTokens("A-B"));
        }

        [TestMethod]
        public void AX_B()
        {
            CollectionAssert.AreEqual(new[] { "A ","B" }, GetTokens("A -B"));
        }

        [TestMethod]
        public void A_XB()
        {
            CollectionAssert.AreEqual(new[] { "A", " B" }, GetTokens("A- B"));
        }

        [TestMethod]
        public void AX_XB()
        {
            CollectionAssert.AreEqual(new[] { "A ", " B" }, GetTokens("A - B"));
        }

        [TestMethod]
        public void A_B_()
        {
            CollectionAssert.AreEqual(new[] { "A-B" }, GetTokens("A-B-"));
        }

        [TestMethod]
        public void A__B()
        {
            CollectionAssert.AreEqual(new[] { "A", "B" }, GetTokens("A--B"));
        }

        [TestMethod]
        public void __A()
        {
            CollectionAssert.AreEqual(new[] { "A" }, GetTokens("--A"));
        }

        [TestMethod]
        public void A__()
        {
            CollectionAssert.AreEqual(new[] { "A" }, GetTokens("A--"));
        }

        [TestMethod]
        public void A_B__()
        {
            CollectionAssert.AreEqual(new[] { "A-B" }, GetTokens("A-B--"));
        }

        private string[] GetTokens(string text)
        {
            var fragment = new TextFragment(text);
            _tokenizer.AnnotateText(fragment);
            return fragment.Annotations.Select(a => a.Term).ToArray();
        }
    }
}
