using System.Collections.Generic;
using LinkMe.Framework.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test.Utilities
{
    [TestClass]
    public class EditDistanceTest
        : TestClass
    {
        /*
        private string[] _profilingDictionary =
            new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        [TestMethod]
        [Ignore("Run manually under dotTrace")]
        public void ProfileCaseSensitiveFirstLetter()
        {
            TryGetClosestMatches(true, true);
        }

        [TestMethod]
        [Ignore("Run manually under dotTrace")]
        public void ProfileCaseInsensitiveFirstLetter()
        {
            TryGetClosestMatches(false, true);
        }

        [TestMethod]
        [Ignore("Run manually under dotTrace")]
        public void ProfileCaseSensitiveNoFirstLetter()
        {
            TryGetClosestMatches(true, false);
        }

        [TestMethod]
        [Ignore("Run manually under dotTrace")]
        public void ProfileCaseInsensitiveNoFirstLetter()
        {
            TryGetClosestMatches(false, false);
        }

        private void TryGetClosestMatches(bool caseSensitive, bool firstLetterMustMatch)
        {
            for (int i = 0; i < 40000; i++)
            {
                EditDistance.GetClosestMatches(_profilingDictionary, "sunday", 3, caseSensitive, firstLetterMustMatch);
                EditDistance.GetClosestMatches(_profilingDictionary, "dewnesday", 1, caseSensitive, firstLetterMustMatch);
                EditDistance.GetClosestMatches(_profilingDictionary, "dewnesday", 2, caseSensitive, firstLetterMustMatch);
            }
        }
*/
        [TestMethod]
        public void GetEditDistance()
        {
            AssertEditDistance(0, "", "");
            AssertEditDistance(4, "Four", "");
            AssertEditDistance(0, "same", "same");
            AssertEditDistance(0, "Same", "samE"); // case-insensitive

            AssertEditDistance(3, "kitten", "sitting"); // The example from http://en.wikipedia.org/wiki/Levenshtein_distance

            AssertEditDistance(1, "word", "dword"); // insert at the front
            AssertEditDistance(1, "word", "wo-rd"); // insert in the middle
            AssertEditDistance(1, "word", "words"); // insert at the end
            AssertEditDistance(1, "word", "ord"); // delete at the front
            AssertEditDistance(1, "word", "wod"); // delete in the middle
            AssertEditDistance(1, "word", "wor"); // delete at the end
            AssertEditDistance(1, "word", "cord"); // substitute at the start
            AssertEditDistance(1, "word", "ward"); // substitute in the middle
            AssertEditDistance(1, "word", "work"); // substitute at the end
            AssertEditDistance(1, "word", "owrd"); // transpose at the start
            AssertEditDistance(1, "word", "wrod"); // transpose in the middle
            AssertEditDistance(1, "word", "wodr"); // transpose at the end
            AssertEditDistance(2, "word", "orwd"); // 2 transpositions

            AssertEditDistance(1, "two words", "twow ords");
            AssertEditDistance(1, "two words", "twowords");
            AssertEditDistance(2, "two words", "tw wrds");
            AssertEditDistance(3, "two words", "wt worlds");
            AssertEditDistance(1, "Two Words", "two Worlds");
            AssertEditDistance(5, "Two Words", "one world");
            AssertEditDistance(9, "totally", "different");
            AssertEditDistance(7, "totally", "d");
            AssertEditDistance(1, "ColdFusion", "cold fusion");
            AssertEditDistance(5, "supermn", "then");
        }

        [TestMethod]
        public void GetClosestMatches()
        {
            var dictionary = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            AssertClosestMatches(dictionary, "sunday", 3, false, false,
                new EditDistance.Match("Sunday", 0),
                new EditDistance.Match("Monday", 2),
                new EditDistance.Match("Friday", 3),
                new EditDistance.Match("Saturday", 3),
                new EditDistance.Match("Tuesday", 3));
            AssertClosestMatches(dictionary, "sunday", 3, false, true,
                new EditDistance.Match("Sunday", 0),
                new EditDistance.Match("Saturday", 3));
            AssertClosestMatches(dictionary, "sunday", 3, true, false,
                new EditDistance.Match("Sunday", 1),
                new EditDistance.Match("Monday", 2),
                new EditDistance.Match("Friday", 3),
                new EditDistance.Match("Tuesday", 3)); // Saturday now has a distance of 4 due to s -> S
            AssertClosestMatches(dictionary, "Sunday", 3, true, true,
                new EditDistance.Match("Sunday", 0),
                new EditDistance.Match("Saturday", 3));

            AssertClosestMatches(dictionary, "dewnesday", 1, false, false);
            AssertClosestMatches(dictionary, "dewnesday", 2, false, false,
                new EditDistance.Match("Wednesday", 2));
            AssertClosestMatches(dictionary, "dewnesday", 3, false, false,
                new EditDistance.Match("Wednesday", 2));
            AssertClosestMatches(dictionary, "dewnesday", 4, false, false,
                new EditDistance.Match("Wednesday", 2),
                new EditDistance.Match("Tuesday", 4));
        }

        private static void AssertClosestMatches(IEnumerable<string> dictionary, string term,
            int maxEditDistance, bool caseSensitive, bool firstLetterMustMatch,
            params EditDistance.Match[] expected)
        {
            IList<EditDistance.Match> matches = EditDistance.GetClosestMatches(dictionary, term, maxEditDistance,
                caseSensitive, firstLetterMustMatch);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(i < matches.Count, "Expected match " + expected[i] + " was not returned.");
                Assert.AreEqual(expected[i].Term, matches[i].Term, "Match " + i + " is different.");
                Assert.AreEqual(expected[i].Distance, matches[i].Distance, "Match " + i + " has a different distance.");
            }

            if (expected.Length != matches.Count)
            {
                Assert.Fail(string.Format(
                    "Expected {0} matches, but {1} were returned. The first unexpected match is {2}.",
                    expected.Length, matches.Count, matches[expected.Length]));
            }
        }

        private static void AssertEditDistance(int expected, string one, string two)
        {
            Assert.AreEqual(expected, EditDistance.GetEditDistance(one, two));
            Assert.AreEqual(expected, EditDistance.GetEditDistance(two, one));
        }
    }
}
