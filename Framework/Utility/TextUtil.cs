using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Utility
{
    /// <summary>
    /// Provides static methods for manipulating text.
    /// </summary>
    public static class TextUtil
    {
        private const string SplitIntoWordsPattern = @"(?<word>(?<=\(?)\""[^\""]+\""(?=\)?)|(?<=^|\s+|\(|\)|\"")[^\s\""\(\)]+(?=$|\s+|\(|\)|\""))";
        // A regular expression that matches \n when it does not follow \r.
        private static Regex _lineFeedRegEx;
        private static readonly Regex SplitIntoWordsRegex = new Regex(SplitIntoWordsPattern, RegexOptions.Compiled | RegexOptions.ExplicitCapture);
        private static Encoding _unicode;

        public static readonly char[] WhitespaceChars = new[]
        {
            '\t', '\n', '\v', '\f', '\r', ' ', '\x0085', '\x00a0', '\u1680', '\u2000', '\u2001', '\u2002',
            '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200a', '\u200b',
            '\u2028', '\u2029', '\u3000', '\ufeff'
        };

        public static readonly char[] WordBoundaryChars = MiscUtils.ConcatArrays(new[] { '.' }, WhitespaceChars);

        public static string ToHexString(this byte[] bytes)
        {
            var chars = new char[bytes.Length * 2];
            for (var i = 0; i < bytes.Length; ++i)
            {
                var b = ((byte)(bytes[i] >> 4));
                chars[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                b = ((byte)(bytes[i] & 0xF));
                chars[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }

            return new string(chars);
        }

        public static string EscapeSqlText(string text)
        {
            return (text == null ? null : text.Replace("'", "''"));
        }

        public static string[] EscapeSqlText(string[] text)
        {
            if (text == null || text.Length == 0)
                return text;

            var escaped = new string[text.Length];
            for (var index = 0; index < text.Length; index++)
            {
                escaped[index] = EscapeSqlText(text[index]);
            }

            return escaped;
        }

        public static string NullIfEmpty(this string value)
        {
            return (value != null && value.Length == 0 ? null : value);
        }

        public static string EmptyIfNull(this string value)
        {
            return (value ?? "");
        }

        public static bool TrimAndCheckEmpty(ref string text)
        {
            if (text == null)
                return true;
            text = text.Trim();
            return text.Length == 0;
        }

        public static void TrimAndReplaceCharInArray(char separator, string[] array)
        {
            if (array != null)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    if (array[i] != null)
                        array[i] = array[i].Trim(separator).Replace(separator, ' ');
                }
            }
        }

        public static string Truncate(string text, int maxLength)
        {
            if (maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException("maxLength", maxLength,
                    "The maximum length must be greater than 0.");
            }

            if (text == null || text.Length <= maxLength)
                return text;
            return text.Substring(0, maxLength);
        }

        public static string TruncateForDisplay(string text, int maxLength)
        {
            return TruncateForDisplay(text, maxLength, false);
        }

        public static string TruncateForDisplay(string text, int maxLength, bool stopAtNewline)
        {
            const string suffix = "...";

            if (maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException("maxLength", maxLength,
                    "The maximum length must be greater than 0.");
            }

            if (string.IsNullOrEmpty(text))
                return text;

            if (stopAtNewline)
            {

                var firstNewLine = text.IndexOfAny(System.Environment.NewLine.ToCharArray(), 0, Math.Min(maxLength, text.Length));
                if (firstNewLine != -1)
                {
                    text = text.Substring(0, firstNewLine) + suffix;
                }
            }

            if (text.Length <= maxLength)
                return text;
            return text.Substring(0, maxLength - suffix.Length) + suffix;
        }

        public static string TruncateAtWord(this string input, int length)
        {
            if (input == null || input.Length < length)
                return input;

            var iNextSpace = input.LastIndexOf(" ", length - 3, StringComparison.Ordinal);

            return string.Format("{0}...", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }

        /// <summary>
        /// Replaces any number of spaces with a single space (eg. "one  two    three" becomes "one two three").
        /// </summary>
        public static string CollapseSpaces(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var length = input.Length;
            var sb = new StringBuilder(input, length);

            int startRemove = -1, endRemove = -1;

            for (var i = 0; i < length; i++)
            {
                if (sb[i] == ' ')
                {
                    if (startRemove == -1)
                        startRemove = i + 1;
                    endRemove = i;
                }
                else if (startRemove != -1)
                {
                    var removeCount = endRemove - startRemove + 1;
                    sb.Remove(startRemove, removeCount);

                    i -= removeCount;
                    length -= removeCount;

                    startRemove = endRemove = -1;
                }
            }

            if (startRemove != -1)
                sb.Remove(startRemove, (endRemove - startRemove + 1));
            else if (length == input.Length)
                return input;

            return sb.ToString();
        }

        /// <summary>
        /// Finds a string in an array of strings, ignoring case.
        /// </summary>
        /// <param name="toSearch">The array of strings to search.</param>
        /// <param name="toFind">The string to find.</param>
        /// <param name="startIndex">The index inside <paramref name="toSearch" /> at which to start searching.</param>
        /// <returns>Index of the first occurrance of <paramref name="toFind" /> inside
        /// <paramref name="toSearch" /> or -1 if not found.</returns>
        public static int IndexOfStringIgnoreCase(string[] toSearch, string toFind, int startIndex)
        {
            if (toSearch == null || toSearch.Length == 0)
                return -1;

            if (startIndex < 0 || startIndex >= toSearch.Length)
            {
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "The start index, "
                    + startIndex + " must be greater than or equal to 0 and less than the length of"
                    + " the array.");
            }

            for (int index = startIndex; index < toSearch.Length; index++)
            {
                if (string.Compare(toSearch[index], toFind, true) == 0)
                    return index;
            }

            return -1;
        }

        /// <summary>
        /// Strips all characters, except letters, digits and whitespace from the input text.
        /// </summary>
        public static string StripToAlphaNumericAndWhiteSpace(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var sb = new StringBuilder(text);

            int i = 0;
            while (i < sb.Length)
            {
                char c = sb[i];

                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                {
                    i++;
                }
                else
                {
                    sb.Remove(i, 1);
                }
            }

            return (sb.Length == text.Length ? text : sb.ToString());
        }

        public static string StripExtraWhiteSpace(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Remove all space at the start and end.

            text = text.Trim();
            var sb = new StringBuilder(text);
            int i = 0;
            while (i < sb.Length)
            {
                char c = sb[i];

                if (i > 0 && char.IsWhiteSpace(c) && char.IsWhiteSpace(sb[i - 1]))
                {
                    sb.Remove(i, 1);
                }
                else
                {
                    i++;
                }
            }

            return (sb.Length == text.Length ? text : sb.ToString());
        }

        /// <summary>
        /// Replaces all characters, except letters, digits and whitespace from the input text with the new character.
        /// </summary>
        public static string ReduceToAlphaNumericAndWhiteSpace(string text, char newChar)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var sb = new StringBuilder(text);

            int i = 0;
            while (i < sb.Length)
            {
                char c = sb[i];

                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                {
                    i++;
                }
                else
                {
                    sb[i] = newChar;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Strips all characters, except letters, digits and spaces from the input text.  All other whitespace
        /// is replaced by the space character.
        /// </summary>
        public static string StripToAlphaNumeric(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var sb = new StringBuilder(text);
            int i = 0;
            while (i < sb.Length)
            {
                char c = sb[i];

                if (char.IsLetterOrDigit(c))
                {
                    i++;
                }
                else
                {
                    sb.Remove(i, 1);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Strips all characters, except letters, digits and spaces from the input text.  All other whitespace
        /// is replaced by the space character.
        /// </summary>
        public static string StripToAlphaNumericAndSpace(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var sb = new StringBuilder(text);
            int i = 0;
            while (i < sb.Length)
            {
                char c = sb[i];

                if (char.IsLetterOrDigit(c) || c == ' ')
                {
                    i++;
                }
                else if (char.IsWhiteSpace(c))
                {
                    sb[i] = ' ';
                }
                else
                {
                    sb.Remove(i, 1);
                }
            }

            return sb.ToString();
        }

        public static string GetEmailDomain(string address)
        {
            if (string.IsNullOrEmpty(address))
                return null;

            string[] arr = address.Split('@');
            if (arr.Length != 2)
                throw new ArgumentException("Invalid email address format");

            return arr[1];
        }

        public static string[] SplitEmailAddresses(string addresses)
        {
            if (string.IsNullOrEmpty(addresses))
                return new string[0];

            var dict = new ListDictionary();
            foreach (string address in addresses.Split(',', ';'))
            {
                if (address != null)
                {
                    string trimmed = address.Trim();
                    if (trimmed.Length > 0)
                    {
                        dict[trimmed] = null;
                    }
                }
            }

            var uniqueAddreses = new string[dict.Count];
            dict.Keys.CopyTo(uniqueAddreses, 0);

            return uniqueAddreses;
        }

        /// <summary>
        /// Splits a string that contains a name optionally followed by a number into the name and number parts.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="name">The name part (which may be the entire input string).</param>
        /// <param name="number">The number part if the input string ends in 1 to 4 digits, otherwise 0.</param>
        /// <returns>True if the input string contained a number of not more than 4 digits at the end,
        /// otherwise false. If the return value is false then the value of <paramref name="number"/> is 0.</returns>
        public static bool SplitNameIntoNameAndNumber(string input, out string name, out int number)
        {
            const string method = "SplitNameIntoNameAndNumber";

            if (input == null)
                throw new NullParameterException(typeof(TextUtil), method, "input");

            int firstDigitIndex = input.Length;
            while (firstDigitIndex > 0 && char.IsDigit(input, firstDigitIndex - 1))
            {
                firstDigitIndex--;
            }

            // Use a maximum of 4 digits of the existing number - if it's longer then just append a
            // digit, as if the existing key didn't contain a number at all.

            if (firstDigitIndex < input.Length && firstDigitIndex > input.Length - 5)
            {
                name = input.Substring(0, firstDigitIndex);
                number = int.Parse(input.Substring(firstDigitIndex));
                return true;
            }

            name = input;
            number = 0;
            return false;
        }

        /// <summary>
        /// Compares two strings. Returns true if they are equal, otherwise false.
        /// </summary>
        /// <remarks>If the return value is false, the <paramref name="diffStart"/> parameter contain the
        /// index of the first character that differs and the <paramref name="diffLengthOne"/> and
        /// <paramref name="diffLengthTwo"/> parameters contains the lengths within strings <paramref name="one"/>
        /// and <paramref name="two"/>, respectively, of the chracter range that differs. That is, the difference
        /// between the strings is from <paramref name="diffStart"/> to <paramref name="diffStart"/> +
        /// <paramref name="diffLengthOne"/> in string <paramref name="one"/> and from <paramref name="diffStart"/>
        /// to <paramref name="diffStart"/> + <paramref name="diffLengthTwo"/> in string <paramref name="two"/>.
        /// If the longer string begins with the shorter one then <paramref name="diffStart"/> is equal to the
        /// length of the shorter string and either <paramref name="diffLengthOne"/> or <paramref name="diffLengthTwo"/>
        /// (whichever is the shorter string) is 0. If the return value is true then <paramref name="diffStart"/>
        /// is -1 and both <paramref name="diffLengthOne"/> and <paramref name="diffLengthTwo"/> are 0.
        /// </remarks>
        /// <param name="one">The first string to compare.</param>
        /// <param name="two">The second string to compare</param>
        /// <param name="ignoreCase">True to perform a case-insensitive comparison, false to perform a
        /// case-sensitive comparison.</param>
        /// <param name="diffStart">Index of the first character that differs or -1 if the strings are equal.</param>
        /// <param name="diffLengthOne">The length within string <paramref name="one"/> of the character range that
        /// differs.</param>
        /// <param name="diffLengthTwo">The length within string <paramref name="two"/> of the character range that
        /// differs.</param>
        /// <returns>True if the two strings are equal, otherwise false.</returns>
        public static bool SimpleStringDiff(string one, string two, bool ignoreCase, out int diffStart,
            out int diffLengthOne, out int diffLengthTwo)
        {
            diffStart = -1;
            diffLengthOne = 0;
            diffLengthTwo = 0;

            // Determine which string is shorter.

            string shorter;
            string longer;
            if (one.Length < two.Length)
            {
                shorter = one;
                longer = two;
            }
            else
            {
                shorter = two;
                longer = one;
            }

            // Find the first character that differs.

            int step = Math.Max(shorter.Length / 10, 1);
            int offset = 0;

            while (true)
            {
                if (string.Compare(shorter, offset, longer, offset, step, ignoreCase) == 0)
                {
                    offset += step;
                    if (offset + step > shorter.Length)
                    {
                        if (step == 1)
                            break; // The longer string begins with the shorter one.

                        offset = shorter.Length - step;
                        step = Math.Max(step / 10, 1);
                    }
                }
                else if (step == 1)
                {
                    diffStart = offset;
                    break;
                }
                else
                {
                    step = Math.Max(step / 10, 1);
                }
            }

            if (diffStart == -1)
            {
                // If the strings are the same length then they're equal, otherwise the longer string is
                // the shorter string with a suffix, so the difference is the entire length past the shorter one.

                if (shorter.Length == longer.Length)
                    return true;

                diffStart = shorter.Length;
                if (one.Length < two.Length)
                {
                    diffLengthOne = 0;
                    diffLengthTwo = two.Length - diffStart;
                }
                else
                {
                    diffLengthOne = one.Length - diffStart;
                    diffLengthTwo = 0;
                }

                return false;
            }
            // Find the last character that differs. Same logic as before, but start from the end of the
            // longer string.

            int maxOffSet = shorter.Length - diffStart;
            step = Math.Min(maxOffSet, Math.Max(longer.Length / 10, 1));
            offset = step;

            while (true)
            {
                if (string.Compare(shorter, shorter.Length - offset, longer, longer.Length - offset, step, ignoreCase) == 0)
                {
                    offset += step;

                    if (offset > maxOffSet)
                    {
                        if (step == 1)
                        {
                            offset = Math.Min(offset, maxOffSet);
                            diffLengthOne = one.Length - offset - diffStart;
                            diffLengthTwo = two.Length - offset - diffStart;
                            break;
                        }

                        offset = Math.Min(offset - (int)Math.Ceiling(step * 0.9), maxOffSet);
                        step = Math.Max(step / 10, 1);
                    }
                }
                else if (step == 1)
                {
                    diffLengthOne = Math.Max(one.Length - offset - diffStart + 1, 0);
                    diffLengthTwo = Math.Max(two.Length - offset - diffStart + 1, 0);
                    break;
                }
                else
                {
                    offset -= (int)Math.Ceiling(step * 0.9);
                    step = Math.Max(step / 10, 1);
                }
            }

            Debug.Assert(diffLengthOne >= 0 && diffLengthTwo >= 0, "diffLengthOne >= 0 && diffLengthTwo >= 0");
            Debug.Assert(diffLengthOne > 0 || diffLengthTwo > 0, "Both diffLengths are 0.");
            Debug.Assert(diffStart + diffLengthOne <= one.Length, "diffStart + diffLengthOne <= one.Length");
            Debug.Assert(diffStart + diffLengthTwo <= two.Length, "diffStart + diffLengthTwo <= two.Length");

            return false;
        }

        /// <summary>
        /// Counts the number of occurrances of a character in a string.
        /// </summary>
        /// <param name="text">String to search.</param>
        /// <param name="value">The character to search for.</param>
        /// <returns>The number of occurances of <paramref name="value"/> found.</returns>
        public static int GetCharacterCount(string text, char value)
        {
            const string method = "GetCharacterCount";

            if (text == null)
                throw new NullParameterException(typeof(TextUtil), method, "text");

            return GetCharacterCount(text, value, 0, text.Length);
        }

        /// <summary>
        /// Counts the number of occurrances of a character in a string.
        /// </summary>
        /// <param name="text">String to search.</param>
        /// <param name="value">The character to search for.</param>
        /// <param name="startIndex">Index at which to start searching.</param>
        /// <param name="count">Number of character positions to examine.</param>
        /// <returns>The number of occurances of <paramref name="value"/> found.</returns>
        public static int GetCharacterCount(string text, char value, int startIndex, int count)
        {
            const string method = "GetCharacterCount";

            if (text == null)
                throw new NullParameterException(typeof(TextUtil), method, "text");

            int total = 0;
            int start = startIndex;
            int index = text.IndexOf(value, start, count);

            while (index != -1)
            {
                total++;
                count -= index - start + 1;
                start = index + 1;
                index = text.IndexOf(value, start, count);
            }

            return total;
        }

        /// <summary>
        /// Escapes a string for use as a format string by replacing "{" with "{{" and "}" with "}}".
        /// </summary>
        public static string EscapeFormatString(string text)
        {
            if (text == null)
                return null;

            var sb = new StringBuilder(text);
            sb.Replace("{", "{{");
            sb.Replace("}", "}}");

            return sb.ToString();
        }

        /// <summary>
        /// Replaces all linefeed (LF) characters in the supplied string with CRLF, except those that are
        /// already part of a CRLF combination (ie. does not replace CRLF with CRCRLF).
        /// </summary>
        public static string ReplaceLfWithCrlf(string text)
        {
            if (text == null)
                return null;

            if (_lineFeedRegEx == null)
            {
                // A regular expression that matches \n when it does not follow \r.
                _lineFeedRegEx = new Regex(@"(?<![\r])\n");
            }

            return _lineFeedRegEx.Replace(text, "\r\n");
        }

        /// <summary>
        /// Converts a pattern containing * and ? wildcards to a regular expression.
        /// </summary>
        public static string WildcardToRegex(string pattern)
        {
            if (pattern == null)
                return null;
            if (pattern.Length == 0)
                return pattern;

            var sb = new StringBuilder(Regex.Escape(pattern));

            sb.Replace("\\*", ".*");
            sb.Replace("\\?", ".?");

            sb.Insert(0, "^");
            sb.Append("$");

            return sb.ToString();
        }

        /// <summary>
        /// Enclose a string in quotes and escape special characters, as for a string literal in C#.
        /// </summary>
        public static string QuoteStringForCSharp(string value)
        {
            if (value == null)
                return null;
            if (value.Length < 256 || value.Length > 1500)
                return QuoteSnippetStringCStyle(value);
            return QuoteSnippetStringVerbatimStyle(value);
        }

        /// <summary>
        /// Remove quotes and unescape special characters, as the C# compiler would for a string literal.
        /// </summary>
        public static string ParseCSharpQuotedString(string value)
        {
            // Implemented from C sharp language specification:
            // ms-help://MS.VSCC.2003/MS.MSDNQTR.2003OCT.1033/csspec/html/vclrfcsharpspec_2_4.htm

            if (value == null)
                return null;
            return value.StartsWith("@") ? ParseVerbatimQuotedString(value) : ParseCStyleQuotedString(value);
        }

        /// <summary>
        /// Convert the specified value to a string and, if the value is a numeric type that has a type suffix
        /// in C#, append the suffix (eg. "D" for double, "UL" for unsigned long, etc).
        /// </summary>
        public static string QuoteNumericLiteralForCSharp(ValueType value)
        {
            if (value is uint)
                return value + "U";
            if (value is long)
                return value + "L";
            if (value is ulong)
                return value + "UL";
            if (value is float)
                return value + "F";
            if (value is double)
                return value + "D";
            if (value is decimal)
                return value + "M";
            return value.ToString();
        }

        /// <summary>
        /// Parse the specified string into a number as the C# compiler would parse a numeric literal. The type
        /// of the returned value depends on the suffix (eg. "5L" would return 5 as a long, "5M" would return
        /// 5.0 as a decimal).
        /// </summary>
        public static ValueType ParseCSharpNumericLiteral(string value)
        {
            // Implemented from C sharp language specification:
            // ms-help://MS.VSCC.2003/MS.MSDNQTR.2003OCT.1033/csspec/html/vclrfcsharpspec_2_4.htm

            if (value == null)
                throw new ArgumentNullException("value");

            string upper = value.ToUpper();

            if (upper.EndsWith("LU") || upper.EndsWith("UL"))
                return ulong.Parse(value.Substring(0, value.Length - 2));
            if (upper.EndsWith("L"))
            {
                value = value.Substring(0, value.Length - 1);

                try
                {
                    return long.Parse(value);
                }
                catch (OverflowException)
                {
                }

                return ulong.Parse(value);
            }

            if (upper.EndsWith("U"))
            {
                value = value.Substring(0, value.Length - 1);

                try
                {
                    return uint.Parse(value);
                }
                catch (OverflowException)
                {
                }

                return ulong.Parse(value);
            }
            if (upper.EndsWith("F"))
                return float.Parse(value.Substring(0, value.Length - 1));
            if (upper.EndsWith("D"))
                return double.Parse(value.Substring(0, value.Length - 1));
            if (upper.EndsWith("M"))
                return decimal.Parse(value.Substring(0, value.Length - 1));
            if (value.IndexOf('.') != -1)
                return double.Parse(value);

            // No integral type suffix and no decimal point - try int, uint, long and finally ulong.

            try
            {
                return int.Parse(value);
            }
            catch (OverflowException)
            {
            }

            try
            {
                return uint.Parse(value);
            }
            catch (OverflowException)
            {
            }

            try
            {
                return long.Parse(value);
            }
            catch (OverflowException)
            {
            }

            return ulong.Parse(value);
        }

        /// <summary>
        /// Returns a string array in which each element is the result of calling ToString() on the corresponding
        /// element in the source list. If an element in the source list is null that element is also null
        /// in the returned array.
        /// </summary>
        public static string[] ListToStringArray(IList list)
        {
            if (list == null)
                return null;

            var result = new string[list.Count];

            for (int index = 0; index < list.Count; index++)
            {
                object item = list[index];
                result[index] = (item == null ? null : item.ToString());
            }

            return result;
        }

        /// <summary>
        /// If the specified string is longer than maxLength truncate it and append "...", so that the total length
        /// is maxLength.
        /// </summary>
        public static string TruncateText(string text, int maxLength)
        {
            const string method = "TruncateText";

            if (maxLength < 4)
            {
                throw new ParameterOutOfRangeException(typeof(TextUtil), method, "maxLength", maxLength, 4, int.MaxValue);
            }
            if (text == null)
                return null;
            if (text.Length <= maxLength)
                return text;

            var sb = new StringBuilder(text, 0, maxLength - 3, maxLength);
            sb.Append("...");

            return sb.ToString();
        }

        /// <summary>
        /// Returns true if the specified string consists entirely of letters, otherwise false.
        /// </summary>
        public static bool IsLetters(string text)
        {
            if (text == null)
                return false;

            return text.All(char.IsLetter);
        }

        /// <summary>
        /// Returns true if the specified string consists entirely of digits, otherwise false.
        /// </summary>
        public static bool IsDigits(string text)
        {
            if (text == null)
                return false;

            return text.All(char.IsDigit);
        }

        /// <summary>
        /// Returns true if the specified string consists entirely of letters or digits or a mix of the two,
        /// otherwise false.
        /// </summary>
        public static bool IsLettersOrDigits(string text)
        {
            if (text == null)
                return false;

            return text.All(char.IsLetterOrDigit);
        }

        private static string QuoteSnippetStringVerbatimStyle(string value)
        {
            var sb = new StringBuilder(value.Length + 5);

            sb.Append("@\"");

            foreach (var t in value)
            {
                if (t == '\"')
                {
                    sb.Append("\"\"");
                }
                else
                {
                    sb.Append(t);
                }
            }

            sb.Append("\"");

            return sb.ToString();
        }

        private static string QuoteSnippetStringCStyle(string value)
        {
            const int maxLineLength = 80;

            var sb = new StringBuilder(value.Length + 5);

            sb.Append("\"");

            for (int i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case '\0':
                        sb.Append("\\0");
                        break;

                    case '\"':
                        sb.Append("\\\"");
                        break;

                    case '\'':
                        sb.Append("\\\'");
                        break;

                    case '\\':
                        sb.Append("\\\\");
                        break;

                    case '\t':
                        sb.Append("\\t");
                        break;

                    case '\r':
                        sb.Append("\\r");
                        break;

                    case '\n':
                        sb.Append("\\n");
                        break;

                    case '\u2028':
                        sb.Append("\u2028");
                        break;

                    case '\u2029':
                        sb.Append("\u2029");
                        break;

                    default:
                        sb.Append(value[i]);
                        break;
                }

                if (i > 0 && i % maxLineLength == 0)
                {
                    sb.Append("\" +" + System.Environment.NewLine + "\"");
                }
            }

            sb.Append("\"");

            return sb.ToString();
        }

        private static string ParseVerbatimQuotedString(string value)
        {
            if (value.Length < 3 || !value.StartsWith("@\"") || !value.EndsWith("\""))
            {
                throw new FormatException(@"The string is not a valid verbatim escaped string. It must begin with" + " '@\"' and end with '\"'.");
            }

            // Get the string between the quotes and unescape "" to ".

            var sb = new StringBuilder(value, 2, value.Length - 3, value.Length);
            sb.Replace("\"\"", "\"");

            return sb.ToString();
        }

        private static string ParseCStyleQuotedString(string value)
        {
            if (value.Length < 2 || !value.StartsWith("\"") || !value.EndsWith("\""))
            {
                throw new FormatException(@"The string is not a valid escaped string. It must begin and end" + " with '\"'.");
            }

            value = value.Substring(1, value.Length - 2);
            var sb = new StringBuilder(value.Length);

            for (int i = 0; i < value.Length; i++)
            {
                char ch = value[i];

                if (ch == '\\')
                {
                    // \ begins an escape sequence - look at the next character to see what it is.

                    if (i == value.Length - 1)
                        throw new FormatException("The '\\' character is invalid at the end of the string.");

                    switch (value[++i])
                    {
                        case '\'':
                            sb.Append('\'');
                            break;

                        case '\"':
                            sb.Append('\"');
                            break;

                        case '\\':
                            sb.Append('\\');
                            break;

                        case '0':
                            sb.Append('\0');
                            break;

                        case 'a':
                            sb.Append('\a');
                            break;

                        case 'b':
                            sb.Append('\b');
                            break;

                        case 'f':
                            sb.Append('\f');
                            break;

                        case 'n':
                            sb.Append('\n');
                            break;

                        case 'r':
                            sb.Append('\r');
                            break;

                        case 't':
                            sb.Append('\t');
                            break;

                        case 'v':
                            sb.Append('\v');
                            break;

                        case 'u':
                            // A 4 digit unicode escape sequence.

                            if (i + 5 > value.Length)
                            {
                                throw new FormatException(string.Format("Unicode escape sequence at position {0}"
                                    + " is too short. It must have 4 digits.", i));
                            }

                            try
                            {
                                sb.Append(ParseUnicodeEscapeSequence(value.Substring(i + 1, 4)));
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException(string.Format("Invalid unicode escape sequence '\\u{0}'"
                                    + " at position {1}.", value.Substring(i + 1, 4), i), ex);
                            }

                            i += 4;
                            break;

                        case 'x':
                            // A variable-length unicode escape sequence (could be from 1 to 4 digits).

                            if (i + 2 > value.Length)
                            {
                                throw new FormatException(string.Format("Hexadecimal escape sequence at position {0}"
                                    + " is empty. It must have 1 to 4 digits.", i));
                            }

                            int count = 1;
                            while (count < 4 && i + count + 1 < value.Length && Uri.IsHexDigit(value[i + count]))
                            {
                                count++;
                            }

                            try
                            {
                                sb.Append(ParseUnicodeEscapeSequence(value.Substring(i + 1, count).PadLeft(4, '0')));
                            }
                            catch (Exception ex)
                            {
                                throw new FormatException(string.Format("Invalid hexadecimal escape sequence '\\x{0}'"
                                    + " at position {1}.", value.Substring(i + 1, count), i), ex);
                            }

                            i += count;
                            break;

                        case 'U':
                            throw new NotSupportedException(string.Format("A '\\U' escape sequence, which is not"
                                + " supported, was found at position {0}.", i));

                        default:
                            {
                                throw new FormatException(string.Format("Unrecognized escape sequence '\\{0}' at position {1}.",
                                    value[i], i));
                            }
                    }
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        private static string ParseUnicodeEscapeSequence(string value)
        {
            Debug.Assert(value != null && value.Length == 4, "value != null && value.Length == 4");

            byte b1 = byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber);
            byte b2 = byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber);

            if (_unicode == null)
            {
                _unicode = new UnicodeEncoding(true, false);
            }

            return _unicode.GetString(new[] { b1, b2 });
        }

        public static string StringFromArrayForPersistence(char separator, string[] array)
        {
            if (array == null)
                return null;

            var i = FindCharInArray(array, separator);
            if (i != -1)
                throw new ApplicationException(string.Format("String array item '{0}' contains the separator character '{1}'.", array[i], separator));
            return string.Join(separator.ToString(), array);
        }

        public static string[] StringToArrayForPersistence(char separator, string value, bool removeEmptyEntries)
        {
            var options = removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
            return value == null ? null : value.Split(new[] { separator }, options);
        }

        public static int FindCharInArray(string[] strings, char toFind)
        {
            for (int i = 0; i < strings.Length; i++)
            {
                if (strings[i] != null && strings[i].IndexOf(toFind) != -1)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Splits a string of text into individual words and excludes specified words from the result.
        /// Words are separated by spaces, but a string in quotes (") is considered a single word.
        /// </summary>
        /// <param name="text">The text to split.</param>
        /// <param name="wordsToExclude">Words to exclude from the result (case-insensitive).</param>
        /// <returns>The array of words in the text.</returns>
        public static string[] SplitIntoWords(string text, string[] wordsToExclude)
        {
            if (string.IsNullOrEmpty(text))
                return new string[0];

            var matches = SplitIntoWordsRegex.Matches(text);
            if (matches.Count == 0)
            {
                Debug.Fail("The text is not empty, but no words were found. Text: '" + text + "'.");
                return new[] { text };
            }

            return matches.Cast<Match>().Select(match => match.Value.Trim('\"', '(', ')')).Where(word => wordsToExclude == null || IndexOfStringIgnoreCase(wordsToExclude, word, 0) == -1).ToArray();
        }

        private static bool IsWordBoundaryChar(char c)
        {
            return char.IsWhiteSpace(c) || Array.IndexOf(WordBoundaryChars, c) != -1;
        }

        /// <summary>
        /// Returns true if the character to the left of indexLeft is a word boundary, ie. whitespace or
        /// start/end of the string.
        /// </summary>
        public static bool IsLeftWordBoundary(string text, int indexLeft)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            if (indexLeft < 0)
                throw new ArgumentOutOfRangeException("indexLeft", indexLeft, "indexLeft must be >= 0");

            return (indexLeft == 0 || IsWordBoundaryChar(text[indexLeft - 1]));
        }

        /// <summary>
        /// Returns true if the character to the right of indexRight is a word boundary, ie. whitespace or
        /// start/end of the string.
        /// </summary>
        public static bool IsRightWordBoundary(string text, int indexRight)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            if (indexRight >= text.Length)
            {
                throw new ArgumentOutOfRangeException("indexRight", indexRight, "indexRight must be < "
                    + text.Length + " (the length of the text)");
            }

            return (indexRight == text.Length - 1 || IsWordBoundaryChar(text[indexRight + 1]));
        }

        /// <summary>
        /// Searches for a string inside another string, but only if the match is at word boundaries.
        /// </summary>
        /// <param name="source">The string to search.</param>
        /// <param name="value">The string to find.</param>
        /// <param name="startIndex">The index at which to start searching.</param>
        /// <returns>Index of <paramref name="value" /> inside <paramref name="source" /> if found,
        /// otherwise -1.</returns>
        public static int IndexOfWholeWordIgnoreCase(string source, string value, int startIndex)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("The value to find must be specified.", "value");

            int foundIndex = CultureInfo.CurrentCulture.CompareInfo.IndexOf(source, value, startIndex,
                CompareOptions.IgnoreCase);
            if (foundIndex == -1)
                return -1;

            // Check for word boundaries.

            if (!IsLeftWordBoundary(source, foundIndex))
                return -1;

            int endIndex = foundIndex + value.Length - 1;
            if (!IsRightWordBoundary(source, endIndex))
                return -1;

            return foundIndex;
        }

        /// <summary>
        /// Returns true if the specified character is a regular expression \w class (word) character, other false.
        /// </summary>
        public static bool IsWordChar(char c)
        {
            return (c == '_' || char.IsLetterOrDigit(c));
        }

        /// <summary>
        /// When a text representation of an expression starts with a opening bracket
        /// and ends with its matching closing bracket they are trimmed, e.g.:
        ///   (Developer OR Architect) => Developer OR Architect
        ///   (Developer OR (Technical AND Architect)) => Developer OR (Technical AND Architect)
        ///   (Java AND Developer) or (Technical AND Architect) not affected
        /// </summary>
        public static string TrimEndBracketsFromExpression(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length < 2 || text[0] != '(' || text[text.Length - 1] != ')')
                return text;

            // Keep a running tally.

            int open = 0;
            for (int index = 0; index < text.Length; ++index)
            {
                char c = text[index];
                if (c == '(')
                {
                    ++open;
                }
                else if (c == ')')
                {
                    --open;
                    if (open == 0)
                    {
                        // If not at the end then the opening bracket does not match the closing bracket.

                        if (index == text.Length - 1)
                            return text.Substring(1, text.Length - 2);
                        return text;
                    }
                }
            }

            return text;
        }

        public static string ToSentenceCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            int length = input.Length;

            var stringBuilder = new StringBuilder(textInfo.ToLower(input), length);
            stringBuilder[0] = textInfo.ToUpper(input[0]);

            return stringBuilder.ToString();
        }

        public static string EscapeRtfText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var sb = new StringBuilder(text);

            sb.Replace("\\", "\\\\");
            sb.Replace("{", "\\{");
            sb.Replace("}", "\\}");
            sb.Replace('\t', ' ');

            return sb.ToString();
        }
    }
}
