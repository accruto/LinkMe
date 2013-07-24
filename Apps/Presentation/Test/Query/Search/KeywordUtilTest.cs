using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LinkMe.Apps.Presentation.Query.Search;
using LinkMe.Utility.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Presentation.Test.Query.Search
{
	[TestClass]
	public class KeywordUtilTest
	{
		[TestMethod, Ignore]
		public void TestKeywordsInContext()
		{
			const string text1 = "One two three four five six seven eight  nine ten eleven twelve"
					  + " thirteen fourteen fifteen sixteen seventeen eighteen nineteen twenty"
					  + " twentyOne twentyTwo.";
			const string text2 = "The keyword a b\t\tc  \t d e f g.\t The second sentence is quite a"
					  + " long one and ends with the keyword! And another sentence.";
			const string text3 = "One two three four five six seven a b c d e f eight  nine ten"
					  + " eleven twelve thirteen fourteen fifteen g h i j k l m n o p sixteen seventeen"
					  + " eighteen nineteen twenty twentyOne twentyTwo.";
			const string text4 = "One two three four five six seven a b c d e f g eight  nine ten"
					  + " eleven twelve thirteen fourteen fifteen h i j k l m n o p sixteen seventeen"
					  + " eighteen nineteen twenty twentyOne twentyTwo.";
			const string text5 = "\r\n\r\nOne two three four five six seven eight  nine ten eleven twelve"
					  + " thirteen fourteen fifteen sixteen seventeen\r\nEighteen nineteen twenty\r\n"
					  + "twentyOne twentyTwo.";

			var util = new KeywordUtil("\r\n", "<", ">", "... ", " ...");

			// No results.

			List<SnippetContext> results = util.GetKeywordsInContext("", "anything");
			Assert.AreEqual(0, results.Count);

			results = util.GetKeywordsInContext("nothing", new[] { "anything" });
            Assert.AreEqual(0, results.Count);

			// One snippet.

			results = util.GetKeywordsInContext("One", "One");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("<One> ...", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			results = util.GetKeywordsInContext("One of two.", "one");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("<One> of two.", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			results = util.GetKeywordsInContext(text1, "fifteen");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(
                "One two three four five six seven eight  nine ten eleven twelve thirteen fourteen"
                + " <fifteen> sixteen seventeen eighteen nineteen twenty twentyOne twentyTwo.",
                results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			// Two 14-word snippets (the first two keywords are just close enough to fit into
			// one 14-word snippet instead of two 7-word ones).

			results = util.GetKeywordsInContext(text3, "four", "eleven", "nineteen");
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("... <four> five six seven a b c d e f eight  nine ten <eleven> ...", 
                results[0].Text);
            Assert.AreEqual(1 + 2, results[0].KeywordSet);
            Assert.AreEqual("... j k l m n o p sixteen seventeen eighteen <nineteen> twenty twentyOne twentyTwo.",
                results[1].Text);
            Assert.AreEqual(4, results[1].KeywordSet);

			// One 28-word snippet with two keywords.

			results = util.GetKeywordsInContext(text4, new[] { "four", "eleven" });
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(
				"One two three <four> five six seven a b c d e f g eight  nine ten"
				+ " <eleven> twelve thirteen fourteen fifteen h i j k l m ...",
				results[0].Text);
            Assert.AreEqual(1 + 2, results[0].KeywordSet);

			// Three 7-word snippets.

			results = util.GetKeywordsInContext(text4, "four", "eleven", "nineteen");
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("One two three <four> five six seven ...", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);
            Assert.AreEqual("... eight  nine ten <eleven> twelve thirteen fourteen ...", results[1].Text);
            Assert.AreEqual(2, results[1].KeywordSet);
            Assert.AreEqual("... sixteen seventeen eighteen <nineteen> twenty twentyOne twentyTwo.", results[2].Text);
            Assert.AreEqual(4, results[2].KeywordSet);

			// A keyword at the end of the sentence, tabs, exclamation mark.

			results = util.GetKeywordsInContext(text2, "keyword");
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("The <keyword> a b\t\tc  \t d e f g.", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);
            Assert.AreEqual("The second sentence is quite a long one and ends with the <keyword>!", results[1].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			// Hyphen should be treated as part of the word.

			results = util.GetKeywordsInContext(text1, "two");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(
                "One <two> three four five six seven eight  nine ten eleven twelve thirteen fourteen"
                + " fifteen sixteen seventeen eighteen nineteen twenty twentyOne twentyTwo.",
                results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			// Two overlapping snippets combined into one.

			results = util.GetKeywordsInContext(text1, "two", "nineteen");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(
                "One <two> three four five six seven eight  nine ten eleven twelve thirteen fourteen"
                + " fifteen sixteen seventeen eighteen <nineteen> twenty twentyOne twentyTwo.",
                results[0].Text);
            Assert.AreEqual(1 + 2, results[0].KeywordSet);

			// Three overlapping snippets combined into one.

			results = util.GetKeywordsInContext(text1, "four", "eleven", "nineteen");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(
                "One two three <four> five six seven eight  nine ten <eleven> twelve thirteen"
                + " fourteen fifteen sixteen seventeen eighteen <nineteen> twenty twentyOne"
                + " twentyTwo.",
                results[0].Text);
            Assert.AreEqual(1 + 2 + 4, results[0].KeywordSet);

			// Keywords at the start and end of a 28-word sentence.

			results = util.GetKeywordsInContext(
				"Ignore this. Keyword a b c d e f g h i j k l m n o p q r s t u v w x y z keyword!",
				"keyword");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("<Keyword> a b c d e f g h i j k l m n o p q r s t u v w x y z <keyword>!", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			results = util.GetKeywordsInContext(
				"Keyword a b c d e f g h i j k l m n o p q r s t u v w x y z keyword! Ignore this.",
				"keyword");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("<Keyword> a b c d e f g h i j k l m n o p q r s t u v w x y z <keyword>!", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			results = util.GetKeywordsInContext("OneWordBefore Keyword"
				+ " a b c d e f g h i j k l m n o p q r s t u v w x y keyworD. Ignore this.",
				"keyword");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("OneWordBefore <Keyword> a b c d e f g h i j k l m n o p q r s t u v w x y <keyworD>.",
                results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			// New lines.

			results = util.GetKeywordsInContext("Ignore the first line\r\n"
				+ "Keyword a b c d e f g h i j k l m n o p q r s t u v w x y z keyword! Ignore this.",
				"keyword");
            Assert.AreEqual(1, results.Count);
		    Assert.AreEqual("<Keyword> a b c d e f g h i j k l m n o p q r s t u v w x y z <keyword>!", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			results = util.GetKeywordsInContext(text5, "fifteen", "nineteen", "twentyOne");
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("... eleven twelve thirteen fourteen <fifteen> sixteen seventeen ...", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);
            Assert.AreEqual("Eighteen <nineteen> twenty ...", results[1].Text);
            Assert.AreEqual(2, results[1].KeywordSet);
            Assert.AreEqual("<twentyOne> twentyTwo.", results[2].Text);
            Assert.AreEqual(4, results[2].KeywordSet);

			// A line consisting of the keyword only.

			results = util.GetKeywordsInContext("\r\nkeyword\r\n", "keyword");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("<keyword> ...", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			// Many keywords that fit into a 14-word snippet, but should be displayed in a 28-word
			// snippet, since it's the only one.

			results = util.GetKeywordsInContext("I am experienced in preparing test documentation"
				+ " including test plans, test strategies, test scripts and test matrices.",
				"test");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(
                "I am experienced in preparing <test> documentation"
                + " including <test> plans, <test> strategies, <test> scripts and <test> matrices.", 
                results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			// Bugs found from processing actual resumes.

			results = util.GetKeywordsInContext("She is a well accomplished test specialist"
				+ " with her experience crossing over roles such as Senior System Test Analyst, Test"
				+ " Lead and Test Manager in the telecommunications, media, retail and government"
				+ " industries.",
				"test");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(
                "She is a well accomplished <test> specialist with her experience crossing over roles"
                + " such as Senior System <Test> Analyst, <Test> Lead and <Test> Manager in the"
                + " telecommunications, media, ...",
                results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			results = util.GetKeywordsInContext("I have been a Test Analyst for 6 years and in"
				+ " mining solutions quickly and efficiently in any type of database system. \r\n\r\n"
				+ " As a Senior Test Analyst my responsibilities involve testing and creating test"
				+ " documentation.  I have done most types of Black Box testing, including Sanity"
				+ " testing, Functional testing, Systems testing, End to End system testing and"
				+ " Regression testing as well as User Acceptance Testing. I am experienced in preparing"
				+ " test documentation including test plans, test strategies, test scripts and test"
				+ " matrices.",
				"test");
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("... have been a <Test> Analyst for 6 ...", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);
            Assert.AreEqual("As a Senior <Test> Analyst my responsibilities involve testing and creating <test> documentation.",
                results[1].Text);
            Assert.AreEqual(1, results[1].KeywordSet);
            Assert.AreEqual("... preparing <test> documentation including <test> plans, <test> ...", results[2].Text);
            Assert.AreEqual(1, results[2].KeywordSet);

			// Punctuation in text.

			results = util.GetKeywordsInContext(
                "We have the keyword, an e-keyword, keyword-ing, a.keyword and keyword.net - what about this keyword?",
				"keyword");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(
                "We have the <keyword>, an e-<keyword>, <keyword>-ing, a.keyword and keyword.net - what about this <keyword>?",
                results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			results = util.GetKeywordsInContext("We have the keyword, an e-keyword another/keyword"
				+ " and keyword.net - what about this keyword ?",
				"keyword");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(
                "We have the <keyword>, an e-<keyword> another/<keyword>"
                + " and keyword.net - what about this <keyword> ?",
                results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			results = util.GetKeywordsInContext(
				"What about (brackets or brackets) or (brackets) or brackets(a) ?",
				"brackets");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("What about (<brackets> or <brackets>) or (<brackets>) or <brackets>(a) ?", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			// Punctuation in keywords.

			results = util.GetKeywordsInContext("We have the keyword, an e-keyword, a.keyword.",
				"e-keyword");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("We have the keyword, an <e-keyword>, a.keyword.", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			results = util.GetKeywordsInContext("We have the keyword, an e/keyword, e / keyword.",
				"e/keyword");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("We have the keyword, an <e/keyword>, e / keyword.", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			results = util.GetKeywordsInContext("We have the keyword, an e/keyword, e / keyword.",
				"e / keyword");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("We have the keyword, an e/keyword, <e / keyword>.", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

            results = util.GetKeywordsInContext("An &amp; and an amp and another amp.", "amp");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("An &amp; and an <amp> and another <amp>.", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			// If all keywords appear as an exact string (separated by whitespac) they should be
			// highlighted together. Ideally the "analyst" in "systems analyst" should be highlighted
			// as well, but this is not implemented yet.

			results = util.GetKeywordsInContext(
                "I have systems analyst, business analyst and business experience.",
				"business", "analyst");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("I have systems analyst, <business analyst> and <business> experience.", results[0].Text);
            Assert.IsTrue(((1 + 2) & results[0].KeywordSet) == (1 + 2));

			results = util.GetKeywordsInContext(
                "I have systems analyst, business \t analyst and business experience.",
				"business", "analyst");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("I have systems analyst, <business \t analyst> and <business> experience.", results[0].Text);
            Assert.IsTrue(((1 + 2) & results[0].KeywordSet) == (1 + 2));

			// .. and the exact match should be returned first. Ideally preceding partial matches
			// should be returned as well, but this is not implemented yet.

			results = util.GetKeywordsInContext(
                "I have systems analyst and business experience. I want to be a business analyst.",
				"business", "analyst");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("I want to be a <business analyst>.", results[0].Text);
            Assert.IsTrue(((1 + 2) & results[0].KeywordSet) == (1 + 2));

			// Start of the linebreak string should not be included in the snippets.

			util = new KeywordUtil(StringUtils.HTML_LINE_BREAK, "<", ">", "... ", " ...");

			results = util.GetKeywordsInContext("3+ years of experience as a QA Test engineer"
				+ " in testing client server environment and doing web testing. <br />Good Knowledge"
				+ " and Experience in Execution of Functional Test Cases on the basis of Usecase and"
				+ " Generated Test Report.<br />And Test Director, for Bug reporting.<br />",
				"test");
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("... as a QA <Test> engineer in testing ...", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);
            Assert.AreEqual("... Execution of Functional <Test> Cases on the basis of Usecase and Generated <Test> Report.", 
                results[1].Text);
            Assert.AreEqual(1, results[1].KeywordSet);
            Assert.AreEqual("And <Test> Director, for Bug reporting.", results[2].Text);
            Assert.AreEqual(1, results[2].KeywordSet);

			results = util.GetKeywordsInContext("<br />management.  KEYWORD was appointed Receiver"
				+ " & Manager and managed the transition of M>business to Connex trains and Yarra Trams"
				+ ".         <br />",
				"keyword");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(
                "<KEYWORD> was appointed Receiver & Manager and managed the transition of M>business to"
                + " Connex trains and Yarra Trams.", 
                results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);

			results = util.GetKeywordsInContext("One keyword. Successfully did something"
				+ " and something else by KEYWORD blah blah and blah blah.",
				"keyword");
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("One <keyword>.", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);
            Assert.AreEqual("Successfully did something and something else by <KEYWORD> blah blah and blah blah.", results[1].Text);
            Assert.AreEqual(1, results[1].KeywordSet);

			results = util.GetKeywordsInContext("At the end keyword.<br />Ignore this.",
				"keyword");
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("At the end <keyword>.", results[0].Text);
            Assert.AreEqual(1, results[0].KeywordSet);
		}

	    [TestMethod]
		public void TestOverlappingKeywords()
		{
		    string text = "2002-2003: Acting Personal Assistant, <br />"
		        + "2002: Acting Personal Assistant to Chief Executive Officer, Some Corporation";

		    var util = new KeywordUtil(StringUtils.HTML_LINE_BREAK,
		        "<b>", "</b>", "<b>...</b> ", " <b>...</b>");
		    var expected = new[]
		        {
		            "2002-2003: Acting <b>Personal Assistant</b>, <b>...</b>",
		            "2002: <b>Acting Personal Assistant to Chief Executive Officer</b>, Some Corporation <b>...</b>"
		        };

		    // Longer keyword string first.

		    var keywords = new[]
		        {
		            "acting personal assistant to Chief Executive Officer",
		            "personal assistant"
		        };

		    List<SnippetContext> results = util.GetKeywordsInContext(text, keywords);
            Assert.IsTrue(results.Select(sc => sc.Text).SequenceEqual(expected));
            Assert.AreEqual(2, results[0].KeywordSet);
            Assert.AreEqual(1 + 2, results[1].KeywordSet);

		    // Shorter keyword string first.

		    keywords = new[]
		        {
		            "personal assistant",
		            "acting personal assistant to Chief Executive Officer"
		        };

		    results = util.GetKeywordsInContext(text, keywords);
            Assert.IsTrue(results.Select(sc => sc.Text).SequenceEqual(expected));
            Assert.AreEqual(1, results[0].KeywordSet);
            Assert.AreEqual(1 + 2, results[1].KeywordSet);

		    // Longer keyword string first, start with the same text.

		    keywords = new[]
		        {
		            "personal assistant to Chief Executive Officer",
		            "personal assistant"
		        };
		    expected = new[]
		        {
		            "2002-2003: Acting <b>Personal Assistant</b>, <b>...</b>",
		            "2002: Acting <b>Personal Assistant to Chief Executive Officer</b>, Some Corporation <b>...</b>"
		        };

		    results = util.GetKeywordsInContext(text, keywords);
            Assert.IsTrue(results.Select(sc => sc.Text).SequenceEqual(expected));
            Assert.AreEqual(2, results[0].KeywordSet);
            Assert.AreEqual(1 + 2, results[1].KeywordSet);

		    // Two occurrances - one long match, one short match.

		    text = "Personal Assistant to Chief Executive Officer - Personal Assistant to Senior"
		        + " Management Staff";

		    keywords = new[]
	        {
	            "personal assistant",
	            "personal assistant to Chief Executive Officer"
	        };
		    expected = new[]
	        {
	            "<b>Personal Assistant to Chief Executive Officer</b> -"
	                + " <b>Personal Assistant</b> to Senior Management Staff <b>...</b>"
	        };

		    results = util.GetKeywordsInContext(text, keywords);
            Assert.IsTrue(results.Select(sc => sc.Text).SequenceEqual(expected));
            Assert.AreEqual(1 + 2, results[0].KeywordSet);
        }

        [TestMethod]
        public void TestKeywordRegex()
        {
            TryKeywordRegex("A simple sentence.", "A <b>simple</b> sentence.", "simple");
            TryKeywordRegex("Different Case", "Different <b>Case</b>", "caSE");
            TryKeywordRegex("A simple sentence, with punctuation.", "A simple <b>sentence</b>, with <b>punctuation</b>.",
                "sentence", "punctuation");

            // & character.

            TryKeywordRegex("An &amp; and an amp and another amp.", "An &amp; and an <b>amp</b> and another <b>amp</b>.", "amp");

            // Two occurrances using GetKeywordsRegex with the shorter keyword string first.

            TryKeywordRegex("Personal Assistant to Chief Executive Officer - Personal Assistant to Senior Management Staff",
                "<b>Personal Assistant to Chief Executive Officer</b> - <b>Personal Assistant</b> to Senior Management Staff",
	            "personal assistant", "personal assistant to Chief Executive Officer");
		}

        private static void TryKeywordRegex(string text, string expected, params string[] keywords)
        {
            Regex regex = KeywordUtil.GetKeywordRegex(keywords, StringUtils.HTML_LINE_BREAK);

            string replaced = regex.Replace(text, "<b>$1</b>");
            Assert.AreEqual(expected, replaced);
        }
	}
}
