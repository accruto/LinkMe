using System;
using System.Collections.Generic;
using System.Globalization;

namespace LinkMe.Framework.Text
{
    public static class EditDistance
    {
        public struct Match
        {
            public string Term;
            public int Distance;

            public Match(string term, int distance)
            {
                Term = term;
                Distance = distance;
            }

            public override string ToString()
            {
                return Term + " (" + Distance + ")";
            }
        }

        /// <summary>
        /// Gets the closest matches (by edit distance) from a dictionary of terms.
        /// </summary>
        public static IList<Match> GetClosestMatches(IEnumerable<string> dictionary, string term,
                                                     int maxEditDistance, bool caseSensitive, bool firstLetterMustMatch)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");
            if (string.IsNullOrEmpty(term))
                throw new ArgumentException("The term must be specified.", "term");

            var matches = new List<Match>();

            TextInfo textInfo = (caseSensitive ? null : CultureInfo.InvariantCulture.TextInfo);

            foreach (string candidate in dictionary)
            {
                // Check the first letter, if requested.

                if (firstLetterMustMatch)
                {
                    bool firstLetterMatches;
                    if (caseSensitive)
                    {
                        firstLetterMatches = (candidate[0] == term[0]);
                    }
                    else
                    {
                        firstLetterMatches = (textInfo.ToUpper(candidate[0]) == textInfo.ToUpper(term[0]));
                    }

                    if (!firstLetterMatches)
                        continue;
                }

                // Check the distance.

                int distance;
                if (caseSensitive)
                {
                    distance = GetEditDistanceCaseSensitive(term, candidate);
                }
                else
                {
                    distance = GetEditDistance(term, candidate, textInfo);
                }

                if (distance <= maxEditDistance)
                {
                    matches.Add(new Match(candidate, distance));
                }
            }

            matches.Sort(CompareMatches);

            return matches;
        }

        /// <summary>
        /// Computes the Damerau-Levenshtein edit distance.
        /// </summary>
        /// <remarks>
        /// Original algorithm from http://en.wikipedia.org/wiki/Damerau-Levenshtein_distance,
        /// vector algorithm from http://www.codeproject.com/KB/recipes/Levenshtein.aspx
        /// </remarks>
        public static int GetEditDistance(string one, string two)
        {
            if (one == null)
                throw new ArgumentNullException("one");
            if (two == null)
                throw new ArgumentNullException("two");

            return GetEditDistance(one, two, CultureInfo.InvariantCulture.TextInfo);
        }

        private static int GetEditDistance(string one, string two, TextInfo textInfo)
        {
            return GetEditDistanceCaseSensitive(textInfo.ToUpper(one), textInfo.ToUpper(two));
        }

        // Uses a simple == comparison for characters, which makes it case-sensitive.
        private static unsafe int GetEditDistanceCaseSensitive(string one, string two)
        {
            int length1 = one.Length;
            int length2 = two.Length;

            if (length1 == 0)
                return length2;
            if (length2 == 0)
                return length1;

            fixed (char* chars1 = one)
            fixed (char* chars2 = two)
            {
                // Create the three vectors

                int* vI = stackalloc int[length1 + 1]; // d[i]
                int* vIminus1 = stackalloc int[length1 + 1]; // d[i - 1]
                int* vIminus2 = stackalloc int[length1 + 1]; // d[i - 2]

                // Initialize the first vector

                for (int i = 1; i <= length1; ++i)
                {
                    vIminus1[i] = i;
                }

                // For each column...

                for (int i = 1; i <= length2; ++i)
                {
                    vI[0] = i;

                    for (int j = 1; j <= length1; ++j)
                    {
                        int cost = (chars1[j - 1] == chars2[i - 1]) ? 0 : 1;

                        int delCost = vIminus1[j] + 1; // Deletion cost
                        int insCost = vI[j - 1] + 1; // Insertion cost
                        int substCost = vIminus1[j - 1] + cost; // Substitution cost

                        int temp = (insCost < delCost ? insCost : delCost);
                        vI[j] = (substCost < temp ? substCost : temp);

                        // Now the "Damerau" part of "Damerau-Levenshtein"

                        if (j > 1 && i > 1 && chars1[j - 1] == chars2[i - 2] && chars1[j - 2] == chars2[i - 1])
                        {
                            int transCost = vIminus2[j - 2] + cost;
                            if (transCost < vI[j])
                            {
                                vI[j] = transCost;
                            }
                        }
                    }

                    // Rotate through the vectors

                    int* vectorTemp = vIminus1;
                    vIminus1 = vI;
                    vI = vIminus2;
                    vIminus2 = vectorTemp;
                }

                return vIminus1[length1];
            }
        }

        private static int CompareMatches(Match a, Match b)
        {
            int result = a.Distance.CompareTo(b.Distance);
            if (result != 0)
                return result;

            return a.Term.CompareTo(b.Term);
        }
    }
}