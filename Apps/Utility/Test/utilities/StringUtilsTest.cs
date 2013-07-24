using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using LinkMe.Framework.Utility;
using LinkMe.Utility.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test.Utilities
{
    [TestClass]
    public class StringUtilsTest
        : TestClass
    {
        [TestMethod]
        public void TestLineBreaksToHtml()
        {
            Assert.AreEqual(null, HtmlUtil.LineBreaksToHtml(null));
            Assert.AreEqual("", HtmlUtil.LineBreaksToHtml(""));
            Assert.AreEqual("No line breaks", HtmlUtil.LineBreaksToHtml("No line breaks"));
            Assert.AreEqual("Some <br />line<br />breaks<br /><br /><br />in this one",
                HtmlUtil.LineBreaksToHtml("Some \r\nline\nbreaks\n\r\n\nin this one"));
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            string value = "";
            Assert.IsTrue(string.IsNullOrEmpty(value));
            value = null;
            Assert.IsTrue(string.IsNullOrEmpty(value));
            value = "value";
            Assert.IsFalse(string.IsNullOrEmpty(value));
            value = "1";
            Assert.IsFalse(string.IsNullOrEmpty(value));
        }

        [TestMethod]
        public void TestGetHintFromFilename()
        {
            const string filename = @"c:\blah\blah\blah\filename.sij";
            Assert.AreEqual("sij", StringUtils.GetExtensionFromFilename(filename));
        }

        [TestMethod]
        public void TestTrimNullValue()
        {
            const string value = null;
            Assert.IsNull(StringUtils.Trim(value));
        }

        [TestMethod]
        public void TestTrimStringValue()
        {
            const string value = " abcd .    ";
            const string expected = "abcd .";
            Assert.AreEqual(expected, StringUtils.Trim(value));
        }


        [TestMethod]
        public void TestStripEmailsAndPhoneNumbersVersion1()
        {
            const string textWithEmailsAndPhoneNumbers = "Please Call LinkMe recruitment Co on 0404123345 or email linkme3@test.linkme.net.au.";
            string strippedText = StringUtils.StripEmailsAndPhoneNumbers(textWithEmailsAndPhoneNumbers);
            Assert.AreEqual("Please Call LinkMe recruitment Co on [phone number removed] or email [email removed].", strippedText);
        }

        [TestMethod]
        public void TestStripEmailsAndPhoneNumbersVersion2()
        {
            const string textWithEmailsAndPhoneNumbers = "email@thestart.com. Please Call LinkMe recruitment Co on (03) 83452345";
            string strippedText = StringUtils.StripEmailsAndPhoneNumbers(textWithEmailsAndPhoneNumbers);
            Assert.AreEqual("[email removed]. Please Call LinkMe recruitment Co on [phone number removed]", strippedText);
        }


        [TestMethod]
        public void TestStripEmailsAndPhoneNumbersVersion3()
        {
            const string textWithEmailsAndPhoneNumbers = "0883452345. Please Email: recruiterdude@recruiting.net";
            string strippedText = StringUtils.StripEmailsAndPhoneNumbers(textWithEmailsAndPhoneNumbers);
            Assert.AreEqual("[phone number removed]. Please Email: [email removed]", strippedText);
        }

        [TestMethod]
        public void TestStripEmailsAndPhoneNumbersVersion4()
        {
            const string textWithEmailsAndPhoneNumbers = "Call 0883452345 or +614 1399 6299. ";
            string strippedText = StringUtils.StripEmailsAndPhoneNumbers(textWithEmailsAndPhoneNumbers);
            Assert.AreEqual("Call [phone number removed] or [phone number removed]. ", strippedText);
        }

        [TestMethod]
        public void TestStripEmailsAndPhoneNumbersVersion5()
        {
            const string textWithEmailsAndPhoneNumbers = "Call 0332982342 or (09)87632342";
            string strippedText = StringUtils.StripEmailsAndPhoneNumbers(textWithEmailsAndPhoneNumbers);
            Assert.AreEqual("Call [phone number removed] or [phone number removed]", strippedText);
        }

        [TestMethod]
        public void TestStripEmailsAndPhoneNumbersVersion6()
        {
            const string textWithEmailsAndPhoneNumbers = "Call 0414 687 674 or email blah@blah.co.uk";
            string strippedText = StringUtils.StripEmailsAndPhoneNumbers(textWithEmailsAndPhoneNumbers);
            Assert.AreEqual("Call [phone number removed] or email [email removed]", strippedText);
        }

        [TestMethod]
        public void TestStripEmailsAndPhoneNumbersVersion7()
        {
            const string textWithPhoneNumber = "Call [[0414 687 674]]";
            string strippedText = StringUtils.StripEmailsAndPhoneNumbers(textWithPhoneNumber);
            Assert.AreEqual("Call [[[phone number removed]]]", strippedText);
        }

        private const string AusMobPhoneNumberRegex = @"04\d\d\s*\d\d\d\s*\d\d\d|04(\d\D{0,2}){9}|\+61(\D){0,3}(\(0\)){0,1}(\D){0,3}4(\D){0,3}((\d\D{0,2}){8})|\(04\d\d\)(\D{0,1})(\d\D{0,2}){6}|\(04\)(\D{0,1})(\d\D{0,2}){8}";
		
        [TestMethod]
        public void TestWhichRegExIsRooted()
        {
            const string textWithSuspiciousPhoneNumber = "Call 0414 687 674 test";

            string[] regExes = AusMobPhoneNumberRegex.Split('|');
            bool match = false;
            foreach(string rex in regExes)
            {
                var testRegex = new Regex(rex);
				
                if(testRegex.IsMatch(textWithSuspiciousPhoneNumber))
                {
                    string result = "[" + rex + "] == " + testRegex.Replace(textWithSuspiciousPhoneNumber, "[phone number removed]");
                    Assert.AreEqual("[" + rex + "] == " + "Call [phone number removed] test", result);
                    match = true;
                }
            }
		
            Assert.IsTrue(match);
        }

        [TestMethod]
        public void TestSplitEmailAddresses()
        {
            Assert.IsTrue(new string[0].SequenceEqual(TextUtil.SplitEmailAddresses(null)));
            Assert.IsTrue(new[] { "one@test.com" }.SequenceEqual(TextUtil.SplitEmailAddresses("one@test.com")));
            Assert.IsTrue(new[] { "one@test.com" }.SequenceEqual(TextUtil.SplitEmailAddresses(" one@test.com ")));
            Assert.IsTrue(new[] { "one@test.com", "two.test.com", "three.test.com" }.SequenceEqual(TextUtil.SplitEmailAddresses("one@test.com, two.test.com;three.test.com")));
            Assert.IsTrue(new[] { "one@test.com", "two.test.com", "three.test.com" }.SequenceEqual(TextUtil.SplitEmailAddresses("one@test.com; two.test.com,three.test.com, ")));
            Assert.IsTrue(new[] { "one@test.com", "two.test.com", "three.test.com" }.SequenceEqual(TextUtil.SplitEmailAddresses("one@test.com; two.test.com,one@test.com;,three.test.com, ;")));
        }

        [TestMethod]
        public void TestSplitIntoWords()
        {
            // Simple

            Assert.IsTrue(new string[0].SequenceEqual(TextUtil.SplitIntoWords("", new string[0])));
            Assert.IsTrue(new[] { "One", "two", "three" }.SequenceEqual(TextUtil.SplitIntoWords("One\ttwo  three", null)));

            // Punctuation.

            Assert.IsTrue(new[] { "O-n.e", "t_wo", "three" }.SequenceEqual(TextUtil.SplitIntoWords("O-n.e\tt_wo  three", new string[0])));

            // Quotes and brackets.

            Assert.IsTrue(new[] { "One / still \\ one", "two", "three\tstill  three" }.SequenceEqual(TextUtil.SplitIntoWords("\"One / still \\ one\" two \"three\tstill  three\"", new string[0])));
            Assert.IsTrue(new[] { "One", "two", "three" }.SequenceEqual(TextUtil.SplitIntoWords("(One two) three", new string[0])));

            // Ignored words.

            Assert.IsTrue(new[] { "two" }.SequenceEqual(TextUtil.SplitIntoWords("One\ttwo  three", new[] { "one", "three" })));
            Assert.IsTrue(new[] { "One / still \\ one", "three\tstill  three" }.SequenceEqual(TextUtil.SplitIntoWords("\"One / still \\ one\" two \"three\tstill  three\"", new[] { "two" })));

            // Miscellaneous.

            Assert.IsTrue(new[] { "One", "two three" }.SequenceEqual(TextUtil.SplitIntoWords("(One) AND (\"two three\")", new[] { "and" })));
            Assert.IsTrue(new[] { "one two", "three" }.SequenceEqual(TextUtil.SplitIntoWords("\"one two\"and\"three\"", new[] { "and" })));
            Assert.IsTrue(new[] { "one two", "three" }.SequenceEqual(TextUtil.SplitIntoWords("\"one two\"\"three\"", null)));
            Assert.IsTrue(new[] { "one", "two", "three" }.SequenceEqual(TextUtil.SplitIntoWords("(one two)(three)", null)));
        }

        [TestMethod]
        public void TestWildcardToSqlLike()
        {
            Assert.AreEqual("", StringUtils.WildcardToSqlLike("", true));
            Assert.AreEqual("no wildcard", StringUtils.WildcardToSqlLike("no wildcard", true));
            Assert.AreEqual("some%wildcards_", StringUtils.WildcardToSqlLike("some*wildcards?", true));
            Assert.AreEqual("escape [%][[]th''is]!", StringUtils.WildcardToSqlLike("escape %[th'is]!", true));
            Assert.AreEqual("escape [%][[]with%wild_cards][_]",
                StringUtils.WildcardToSqlLike("escape %[with*wild?cards]_", true));
        }

        [TestMethod]
        public void TestWildcardToRegex()
        {
            Assert.AreEqual("", StringUtils.WildcardToRegex(""));
            Assert.AreEqual(@"no\ wildcard", StringUtils.WildcardToRegex("no wildcard"));
            Assert.AreEqual("some.*wildcards.", StringUtils.WildcardToRegex("some*wildcards?"));
            Assert.AreEqual(@"\(escape\ \[this]\)", StringUtils.WildcardToRegex("(escape [this])"));
            Assert.AreEqual(@"escape\.\(\[with.*wild.cards]",
                StringUtils.WildcardToRegex("escape.([with*wild?cards]"));
        }

        [TestMethod]
        public void TestIndexOfWholeWordIgnoreCase()
        {
            Assert.AreEqual(-1, TextUtil.IndexOfWholeWordIgnoreCase("", "test", 0));

            Assert.AreEqual(0, TextUtil.IndexOfWholeWordIgnoreCase("test", "test", 0));
            Assert.AreEqual(2, TextUtil.IndexOfWholeWordIgnoreCase("a test", "test", 0));
            Assert.AreEqual(2, TextUtil.IndexOfWholeWordIgnoreCase("a test", "test", 1));
            Assert.AreEqual(2, TextUtil.IndexOfWholeWordIgnoreCase("a test", "test", 2));
            Assert.AreEqual(-1, TextUtil.IndexOfWholeWordIgnoreCase("a test", "test", 3));

            Assert.AreEqual(0, TextUtil.IndexOfWholeWordIgnoreCase("test two", "test", 0));
            Assert.AreEqual(-1, TextUtil.IndexOfWholeWordIgnoreCase("test two", "test", 1));

            Assert.AreEqual(4, TextUtil.IndexOfWholeWordIgnoreCase("one test two", "test", 0));
            Assert.AreEqual(4, TextUtil.IndexOfWholeWordIgnoreCase("one test two", "test", 2));
            Assert.AreEqual(4, TextUtil.IndexOfWholeWordIgnoreCase("one test two", "test", 4));
            Assert.AreEqual(-1, TextUtil.IndexOfWholeWordIgnoreCase("one tester two", "test", 0));

            Assert.AreEqual(-1, TextUtil.IndexOfWholeWordIgnoreCase("test", "test", 1));
            Assert.AreEqual(-1, TextUtil.IndexOfWholeWordIgnoreCase("testing", "test", 0));
            Assert.AreEqual(-1, TextUtil.IndexOfWholeWordIgnoreCase("atest", "test", 0));
            Assert.AreEqual(-1, TextUtil.IndexOfWholeWordIgnoreCase("atest", "test", 1));

            Assert.AreEqual(4, TextUtil.IndexOfWholeWordIgnoreCase("one test two", "test two", 0));
            Assert.AreEqual(4, TextUtil.IndexOfWholeWordIgnoreCase("one test two three", "test two", 0));
            Assert.AreEqual(-1, TextUtil.IndexOfWholeWordIgnoreCase("one test twos three", "test two", 0));

            // Case 1500 - . is now considered a word boundary as well.

            Assert.AreEqual(1, TextUtil.IndexOfWholeWordIgnoreCase(".net", "net", 0));
            Assert.AreEqual(4, TextUtil.IndexOfWholeWordIgnoreCase("asp.net", "net", 0));
            Assert.AreEqual(0, TextUtil.IndexOfWholeWordIgnoreCase("asp.net", "asp", 0));
            Assert.AreEqual(-1, TextUtil.IndexOfWholeWordIgnoreCase("asp/net", "asp", 0));
        }

        [TestMethod]
        public void TestSplitString()
        {
            Assert.AreEqual(0, StringUtils.SplitString("x", null).Count);
            Assert.AreEqual(0, StringUtils.SplitString("x", "").Count);
            Assert.IsTrue(new[] { "blah" }.NullableSequenceEqual(StringUtils.SplitString("x", "blah")));
            Assert.IsTrue(new[] { "blah", "" }.NullableSequenceEqual(StringUtils.SplitString("x", "blahx")));
            Assert.IsTrue(new[] { "blah", "z" }.NullableSequenceEqual(StringUtils.SplitString("x", "blahxz")));
            Assert.IsTrue(new[] { "", "blah" }.NullableSequenceEqual(StringUtils.SplitString("x", "xblah")));
            Assert.IsTrue(new[] { "", "blah", "" }.NullableSequenceEqual(StringUtils.SplitString("x", "xblahx")));
            Assert.IsTrue(new[] { "blah" }.NullableSequenceEqual(StringUtils.SplitString("x", "xblahx", StringSplitOptions.RemoveEmptyEntries)));

            Assert.IsTrue(new[] { "one", "two", "", "three" }.NullableSequenceEqual(StringUtils.SplitString("<br>", "one<br>two<br><br>three", StringSplitOptions.None)));
            Assert.IsTrue(new[] { "one", "two", "three" }.NullableSequenceEqual(StringUtils.SplitString("<br>", "one<br>two<br><br>three", StringSplitOptions.RemoveEmptyEntries)));
            Assert.IsTrue(new[] { "one", "two", "three" }.NullableSequenceEqual(StringUtils.SplitString("<br>", "<br><br>one<br>two<br>three<br><br>", StringSplitOptions.RemoveEmptyEntries)));
        }

        [TestMethod]
        public void TestAreWordBondaries()
        {
            Assert.IsTrue(StringUtils.AreWordBoundaries("one two three", 0, 12));
            Assert.IsTrue(StringUtils.AreWordBoundaries("one two three", 0, 2));
            Assert.IsTrue(StringUtils.AreWordBoundaries("one two three", 4, 12));
            Assert.IsTrue(StringUtils.AreWordBoundaries("one two three", 4, 6));
            Assert.IsFalse(StringUtils.AreWordBoundaries("one two three", 3, 8));
            Assert.IsFalse(StringUtils.AreWordBoundaries("one two three", 5, 9));
            Assert.IsFalse(StringUtils.AreWordBoundaries("one two three", 0, 9));
            Assert.IsFalse(StringUtils.AreWordBoundaries("one two three", 5, 11));
        }

        [TestMethod]
        public void HexStringToByteArrayAndBack()
        {
            Assert.IsTrue(new byte[0].SequenceEqual(StringUtils.HexStringToByteArray("")));
            Assert.AreEqual("", StringUtils.ByteArrayToHexString(new byte[0]));

            var bytes = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 254, 255 };
            const string str = "000102030405060708090a0B0c0D0e0F1011FefF";
            Assert.IsTrue(bytes.SequenceEqual(StringUtils.HexStringToByteArray(str)));
            Assert.AreEqual(str.ToUpper(), StringUtils.ByteArrayToHexString(bytes));
        }

        [TestMethod]
        public void Join()
        {
            Assert.AreEqual("", StringUtils.Join(", ", null));
            Assert.AreEqual("", StringUtils.Join(null, null));

            var a = new ArrayList {"1", "1a", "1b"};
            Assert.AreEqual(String.Format("{0}\n{1}\n{2}", a[0], a[1], a[2]), StringUtils.Join("\n", a));
            Assert.AreEqual(String.Format("{0}{1}{2}", a[0], a[1], a[2]), StringUtils.Join("", a));
            Assert.AreEqual(String.Format("{0}{1}{2}", a[0], a[1], a[2]), StringUtils.Join(null, a));
        }

        [TestMethod]
        public void ToPascalCase()
        {
            Assert.AreEqual("The Cat Ran", StringUtils.ToPascalSentence("the Cat RAN"));
            Assert.AreEqual("The Cat Ran", StringUtils.ToPascalSentence("The Cat Ran"));
            Assert.AreEqual("Some Hy-phens", StringUtils.ToPascalSentence("Some hy-phens"));
            Assert.AreEqual(" Starts & Ends With Space ", StringUtils.ToPascalSentence(" starts & ends with SPACE "));
        }

        [TestMethod]
        public void CollapseSpaces()
        {
            Assert.AreEqual("", "".CollapseSpaces());
            Assert.AreEqual(" ", "  ".CollapseSpaces());
            Assert.AreEqual(" ", "   ".CollapseSpaces());
            Assert.AreEqual(" ", "    ".CollapseSpaces());
            Assert.AreEqual("one two", "one two".CollapseSpaces());
            Assert.AreEqual("one two", "one  two".CollapseSpaces());
            Assert.AreEqual("one two", "one   two".CollapseSpaces());
            Assert.AreEqual(" one two", " one    two".CollapseSpaces());
            Assert.AreEqual(" one two ", "  one   two          ".CollapseSpaces());
            Assert.AreEqual("one two three four ", "one two three four ".CollapseSpaces());
        }

        /* Manually run test

        [TestMethod]
        public void PerformanceTest()
        {
            for (int i = 0; i < 100000; i++)
            {
                IsWildcardMatch();
                IsWildcardMatchInPractice();
            }
        }
        */

        [TestMethod]
        public void IsWildcardMatch()
        {
            // No wildcard.

            Assert.IsFalse(StringUtils.IsWildcardMatch(null, "blah"));
            Assert.IsFalse(StringUtils.IsWildcardMatch(null, ""));
            Assert.IsTrue(StringUtils.IsWildcardMatch("", ""));
            Assert.IsFalse(StringUtils.IsWildcardMatch("", "blah"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("blah", "blah"));
            Assert.IsFalse(StringUtils.IsWildcardMatch("blah", "bla"));
            Assert.IsFalse(StringUtils.IsWildcardMatch("blah", "lah"));
            Assert.IsFalse(StringUtils.IsWildcardMatch("some thing", "thing"));

            // Wilcards.

            Assert.IsTrue(StringUtils.IsWildcardMatch("thing", "*thing*"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("thing", "*thing"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("thing", "thing*"));
            Assert.IsFalse(StringUtils.IsWildcardMatch("some thing", "thing*"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("some thing", "*thing*"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("some thing", "*some*"));
            Assert.IsFalse(StringUtils.IsWildcardMatch("some thing", "*some"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("some thing", "some*thing"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("some thing", "*some*thing"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("some thing", "some*thing*"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("some thing", "*some*thing*"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("some other thing", "some*thing"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("A fewwildcardshere", "A few*wild*cards*here"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("A few wildcards also here", "A few*wild*cards*here"));
            Assert.IsFalse(StringUtils.IsWildcardMatch("More than a few wildcards here", "A few*wild*cards*here"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("More than a few wildcards here", "*A few*wild*cards*here"));
            Assert.IsFalse(StringUtils.IsWildcardMatch("More than a few wildcards here", "*A few*wild*cards*here",
                StringComparison.CurrentCulture));
            Assert.IsFalse(StringUtils.IsWildcardMatch("A few Wildcards here", "*A few*wild*cards*here",
                StringComparison.CurrentCulture));
            Assert.IsTrue(StringUtils.IsWildcardMatch("A few Wildcards here", "*A few*wild*cards*here",
                StringComparison.InvariantCultureIgnoreCase));
            Assert.IsFalse(StringUtils.IsWildcardMatch("A few wildcards here too", "*A few*wild*cards*here"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("A few wildcards here too", "A few*wild*cards*here*"));
        }

        [TestMethod]
        public void IsWildcardMatchInPractice()
        {
            // And this is how it's really used.

            Assert.IsTrue(StringUtils.IsWildcardMatch("/something/", "/something/*"));
            Assert.IsTrue(StringUtils.IsWildcardMatch("/something/else", "/something/*"));
            Assert.IsFalse(StringUtils.IsWildcardMatch("/nothing/", "/something/*"));
            Assert.IsFalse(StringUtils.IsWildcardMatch("/another/something/", "/something/*"));
        }

        [TestMethod]
        public void TruncateForDisplay()
        {
            Assert.IsNull(TextUtil.TruncateForDisplay(null, 10, false));
            Assert.AreEqual("", TextUtil.TruncateForDisplay("", 10, false));
            Assert.AreEqual("Four", TextUtil.TruncateForDisplay("Four", 10, false));
            Assert.AreEqual("ExactlyTen", TextUtil.TruncateForDisplay("ExactlyTen", 10, false));
            Assert.AreEqual("Eleven ...", TextUtil.TruncateForDisplay("Eleven Chop", 10, false));
            Assert.AreEqual("Much lo...", TextUtil.TruncateForDisplay("Much longer than ten with\r\n some new lines", 10, false));
            Assert.AreEqual("Stop\r\nhere", TextUtil.TruncateForDisplay("Stop\r\nhere", 10, false));
            Assert.AreEqual("Stop...", TextUtil.TruncateForDisplay("Stop\r\nhere", 10, true));
            Assert.AreEqual("Stop...", TextUtil.TruncateForDisplay("Stop\rhere", 10, true));
            Assert.AreEqual("Stop...", TextUtil.TruncateForDisplay("Stop\nhere", 10, true));
            Assert.AreEqual("Alm...", TextUtil.TruncateForDisplay("Almost\n", 6, true));
        }

        [TestMethod]
        public void Repeat()
        {
            Assert.AreEqual(null, StringUtils.Repeat(null, 3));
            Assert.AreEqual("", StringUtils.Repeat("", 3));
            Assert.AreEqual("", StringUtils.Repeat("nothing", 0));
            Assert.AreEqual("something", StringUtils.Repeat("something", 1));
            Assert.AreEqual("somethingsomething", StringUtils.Repeat("something", 2));
            Assert.AreEqual("abcabcabc", StringUtils.Repeat("abc", 3));
        }
    }
}