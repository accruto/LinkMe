using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LinkMe.Framework.Utility
{
    public class RegexUtil
    {
        // GPL3
        // C# port of Python code from http://code.activestate.com/recipes/534137/
        /// <summary>
        /// A recursive function to generate a regular expression that matches
        /// any number in the range between min and max inclusive.
        /// 
        ///    Usage / doctests:
        ///    >>> regex_for_range(13,57)
        ///    '4[0-9]|3[0-9]|2[0-9]|1[3-9]|5[0-7]'
        ///    >>> regex_for_range(1983,2011)
        ///    '200[0-9]|199[0-9]|198[3-9]|201[0-1]'
        ///    >>> regex_for_range(99,112)
        ///    '99|10[0-9]|11[0-2]'
        /// 
        /// </summary>
        public static string RegexForRange(int min, int max)
        {
            Debug.Assert((max >= min) && (min >= 0));

            string minStr = min.ToString();
            string maxStr = max.ToString();

            // calculations
            if (min == max)
                return max.ToString();

            if (maxStr.Length > minStr.Length)
            {
                // more digits in max than min, so we pair it down into sub ranges
                // that are the same number of digits.  If applicable we also create a pattern to
                // cover the cases of values with number of digits in between that of
                // max and min.
                string reMiddleRange = string.Empty;

                if (maxStr.Length > minStr.Length + 2)
                {
                    // digits more than 2 off, create mid range
                    reMiddleRange = string.Format("[0-9]{{{0},{1}}}", minStr.Length + 1, maxStr.Length - 1);
                }
                else if (maxStr.Length > minStr.Length + 1)
                {
                    // digits more than 1 off, create mid range
                    reMiddleRange = string.Format("[0-9]{{{0}}}", minStr.Length + 1);
                }

                // pair off into sub ranges
                var maxBig = max;
                var minBig = int.Parse("1" + new string('0', maxStr.Length - 1));
                var reBig = RegexForRange(minBig, maxBig);

                var maxSmall = int.Parse(new string('9', minStr.Length));
                var minSmall = min;
                var reSmall = RegexForRange(minSmall, maxSmall);

                if (reMiddleRange.Length > 0)
                    return string.Join("|", new[] { reSmall, reMiddleRange, reBig });
                else
                    return string.Join("|", new[] { reSmall, reBig });
            }
            else if (maxStr.Length == minStr.Length)
            {
                var patterns = new List<string>();

                if (maxStr.Length == 1)
                {
                    patterns.Add(NaiveRange(min, max));
                }
                else
                {
                    // this is probably the trickiest part so we'll follow the example of
                    // 1336 to 1821 through this section
                    var distance = (max - min).ToString(); // e.g., distance = 1821-1336 = 485
                    var increment = int.Parse("1" + new string('0', distance.Length - 1)); // e.g., 100 when distance is 485
                    if (increment == 1)
                    {
                        // it's safe to do a naive_range see, see def since 10's place is the same for min and max
                        patterns.Add(NaiveRange(min, max));
                    }
                    else
                    {
                        // create a function to return a floor to the correct digit position
                        // e.g., floor_digit_n(1336) => 1300 when increment is 100
                        Func<int, int> FloorDigit = (x => ((x / increment) * increment));

                        // capture a safe middle range
                        // e.g., create regex patterns to cover range between 1400 to 1800 inclusive
                        // so in example we should get: 14[0-9]{2}|15[0-9]{2}|16[0-9]{2}|17[0-9]{2}
                        for (int i = FloorDigit(max) - increment; i > FloorDigit(min); i -= increment)
                        {
                            var lenEndToReplace = increment.ToString().Length - 1;
                            var istr = i.ToString();

                            string pattern = (lenEndToReplace == 1)
                                                 ? string.Format("{0}[0-9]", istr.Substring(0, istr.Length - lenEndToReplace)) 
                                                 : string.Format("{0}[0-9]{{{1}}}", istr.Substring(0, istr.Length - lenEndToReplace), lenEndToReplace);

                            patterns.Add(pattern);
                        }

                        // split off ranges outside of increment digits, i.e., what isn't covered in last step.
                        // low side: e.g., 1336 -> min=1336, max=1300+(100-1) = 1399
                        patterns.Add(RegexForRange(min, FloorDigit(min) + (increment - 1)));
                        // high side: e.g., 1821 -> min=1800 max=1821
                        patterns.Add(RegexForRange(FloorDigit(max), max));
                    }
                }
                return string.Join("|", patterns.ToArray());
            }
            else
            {
                throw new ArgumentException("max value must have more or the same num digits as min");
            }
        }

        /// <summary>
        /// Simply matches min, to max digits by position.  Should create a
        /// valid regex when min and max have same num digits and has same 10s
        /// place digit.
        /// </summary>
        private static string NaiveRange(int min, int max)
        {
            var minStr = min.ToString();
            var maxStr = max.ToString();
            var pattern = new StringBuilder();

            for (int i = 0; i < minStr.Length; i++)
            {
                if (minStr[i] == maxStr[i])
                    pattern.Append(minStr[i]);
                else
                    pattern.AppendFormat("[{0}-{1}]", minStr[i], maxStr[i]);
            }

            return pattern.ToString();
        }
    }
}