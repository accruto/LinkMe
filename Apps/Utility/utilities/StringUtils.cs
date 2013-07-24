using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Utility.Utilities
{
	public static class StringUtils
	{
		public const string HTML_LINE_BREAK = "<br />";

        private const string EMAIL_REMOVED_TEXT = "[email removed]";
		private const string PHONE_NUMBER_REMOVED_TEXT = "[phone number removed]";

		// We should monitor the performance of these regex's as they are publicly available ones.
		private const string AUS_INT_PHONE_NUMBER_REGEX = @"\+61[2-9][[:space:]][0-9]{4}-[0-9]{4}";
		private const string AUS_EIGHT_DIGIT_PHONE_NUMBER_REGEX = @"[0-9]{4}(-| )*[0-9]{4}";
		private const string AUS_MOB_PHONE_NUMBER_REGEX = @"04\d\d\s*\d\d\d\s*\d\d\d|04(\d\D{0,2}){7}\d|\+61(\D){0,3}(\(0\))?(\D){0,3}4(\D){0,3}((\d\D{0,2}){7}\d)|\(04\d\d\)(\D?)(\d\D{0,2}){5}\d|\(04\)(\D?)(\d\D{0,2}){7}\d";
		private const string AUS_PHONE_VARIOUS = @"((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,3})|(\(?\d{2,3}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}";

		public static readonly char[] WhitespaceChars = new char[]
		{
			'\t', '\n', '\v', '\f', '\r', ' ', '\x0085', '\x00a0', '\u1680', '\u2000', '\u2001', '\u2002',
			'\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', '\u2009', '\u200a', '\u200b',
			'\u2028', '\u2029', '\u3000', '\ufeff'
		};

		// Static and Compiled so we dont need to keep recompiling. Will Incur a slight startup cost the first time they are run, as they are compiled.
		private static readonly Regex ausIntPhoneRegex = new Regex(AUS_INT_PHONE_NUMBER_REGEX, RegexOptions.Compiled);
        private static readonly Regex ausVarPhoneRegex = new Regex(AUS_PHONE_VARIOUS, RegexOptions.Compiled);
        private static readonly Regex ausmobPhoneRegex = new Regex(AUS_MOB_PHONE_NUMBER_REGEX, RegexOptions.Compiled);
        private static readonly Regex ausotherPhoneRegex = new Regex(AUS_EIGHT_DIGIT_PHONE_NUMBER_REGEX, RegexOptions.Compiled);

        private static readonly char[] hexCharValues = new char[]
            { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private static byte[] hexByteValues;

		public static bool StringArraysEqual(string[] one, string[] two)
		{
			if (ReferenceEquals(one, two))
				return true;
			if (one == null || two == null)
				return false;
			if (one.Length != two.Length)
				return false;

			for (int index = 0; index < one.Length; index++)
			{
				if (one[index] != two[index])
					return false;
			}

			return true;
		}

		public static string Trim(string value)
		{
			return value != null ? value.Trim() : null;
		}

		/// <summary>
		/// Returns null if the input string is an empty string, otherwise the input string.
		/// </summary>
		public static string DeleteWhiteSpace(string value)
		{
			return Regex.Replace(value, @"\s+", "");
		}

		/// <summary>
		/// Performs the conversion of "the Cat RAN" into "The Cat Ran" - as quickly as possible.
		/// </summary>
		public static string ToPascalSentence(string input)
		{
            if (string.IsNullOrEmpty(input))
                return input;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            int length = input.Length;

            StringBuilder stringBuilder = new StringBuilder(textInfo.ToLower(input), length);
            stringBuilder[0] = textInfo.ToUpper(input[0]);

            int index = input.IndexOf(' ', 0, length - 1);
            while (index != -1)
            {
                int nextCharIndex = index + 1;
                stringBuilder[nextCharIndex] = textInfo.ToUpper(input[nextCharIndex]);
                index = input.IndexOf(' ', nextCharIndex, length - index - 2);
            }

			return stringBuilder.ToString();
		}

		public static string GetExtensionFromFilename(string filename)
		{
			return Path.GetExtension(filename).Trim('.');
		}

		public static bool IsDigits(string s)
		{
			if (string.IsNullOrEmpty(s))
				return false;

			for (int index = 0; index < s.Length; index++)
			{
				if (!char.IsDigit(s[index]))
					return false;
			}

			return true;
		}

		public static bool IsLetters(string s)
		{
			if (string.IsNullOrEmpty(s))
				return false;

			for (int index = 0; index < s.Length; index++)
			{
				if (!char.IsLetter(s[index]))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Returns true if most (over 90%) of the letters in the input string are capitals, otherwise false.
		/// </summary>
		public static bool IsMostlyCapitals(string text)
		{
			const float threshold = 0.9F;

			if (string.IsNullOrEmpty(text))
				return false;

			int upper = 0;
			int letters = 0;

			for (int index = 0; index < text.Length; index++)
			{
				char c = text[index];

				if (char.IsLetter(c))
				{
					letters++;

					if (char.IsUpper(c))
					{
						upper++;
					}
				}
			}

            // The (float) cast is NOT redundant.
// ReSharper disable RedundantCast
			float ratio = (float)upper / (float)letters;
// ReSharper restore RedundantCast

			return (ratio > threshold);
		}

		public static string MostlyCapsToPascalCase(string text)
		{
			return (IsMostlyCapitals(text) ? ToPascalSentence(text) : text);
		}

		/// <summary>
		/// Finds any one of an array of strings in another array of strings, ignoring case.
		/// </summary>
		/// <param name="toSearch">The array of strings to search.</param>
		/// <param name="toFind">The array of string to find.</param>
		/// <param name="startIndex">The index inside <paramref name="toSearch" /> at which to start searching.</param>
		/// <returns>Index of the first occurrance of any string in <paramref name="toFind" /> inside
		/// <paramref name="toSearch" /> or -1 if not found.</returns>
		public static int IndexOfAnyStringIgnoreCase(string[] toSearch, string[] toFind, int startIndex)
		{
			if (toSearch == null)
				throw new ArgumentNullException("toSearch");
			if (toFind == null)
				throw new ArgumentNullException("toFind");

			if (toSearch.Length == 0)
				return -1;

			if (startIndex < 0 || startIndex >= toSearch.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "The start index, "
					+ startIndex + " must be greater than or equal to 0 and less than the length of"
					+ " the array.");
			}

			for (int index = startIndex; index < toSearch.Length; index++)
			{
				for (int f = 0; f < toFind.Length; f++)
				{
					if (string.Compare(toSearch[index], toFind[f], true) == 0)
						return index;
				}
			}

			return -1;
		}

		public static string StripEmailsAndPhoneNumbers(string inputText)
		{
			if (string.IsNullOrEmpty(inputText))
				return inputText;

			inputText = ausVarPhoneRegex.Replace(inputText, PHONE_NUMBER_REMOVED_TEXT);
			inputText = ausIntPhoneRegex.Replace(inputText, PHONE_NUMBER_REMOVED_TEXT);
			inputText = ausmobPhoneRegex.Replace(inputText, PHONE_NUMBER_REMOVED_TEXT);
			inputText = RegularExpressions.EmailAddress.Replace(inputText, EMAIL_REMOVED_TEXT);
			inputText = ausotherPhoneRegex.Replace(inputText, PHONE_NUMBER_REMOVED_TEXT);

			return inputText;
		}

        /// <summary>
        /// Strips the protocol from a given URL. (MF 2008-09-24)
        /// For displaying web addresses as succinctly as possible.
        /// </summary>

        public static string StripUrlProtocol(string text)
        {
            int pos = text.IndexOf("://");
            if (pos + 3 == text.Length) return "";  /* pathological case */
            return pos > -1 ? text.Substring(pos + 3) : text;
        }

        /// <summary>
        /// Strips all characters, except letters and whitespace from the input text.
        /// </summary>
        public static string StripToAlphaAndWhiteSpace(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder sb = new StringBuilder(text);

            int i = 0;
            while (i < sb.Length)
            {
                char c = sb[i];

                if (char.IsLetter(c) || char.IsWhiteSpace(c))
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

        /// <summary>
        /// Strips all characters, except letters, digits and whitespace from the input text,
        /// except for that one which has been identified to ignore.
        /// </summary>
        public static string StripToAlphaNumericAndWhiteSpace(string text, char ignore)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder sb = new StringBuilder(text);

            int i = 0;
            while (i < sb.Length)
            {
                char c = sb[i];

                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == ignore)
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

	    /// <summary>
        /// Escapes a string to be used literally with the SQL LIKE operator.
        /// </summary>
        /// <param name="likePattern">The string to escape.</param>
        /// <param name="escapeQuotes">True to escape singe quotes, false to leave them. Set to true if value will be
        /// used inline, eg. "name LIKE 'pattern'", but false if it will be passed in a parameter, eg. "name LIKE @name".
        /// If unsure, set to true! Incorrectly using false will allow SQL injection!</param>
        /// <returns>The escaped value.</returns>
		public static string EscapeSqlLike(string likePattern, bool escapeQuotes)
		{
			if (string.IsNullOrEmpty(likePattern))
				return likePattern;

			StringBuilder sb = new StringBuilder(likePattern);
			EscapeSqlLikeInternal(sb, escapeQuotes);
			return sb.ToString();
		}

		/// <summary>
		/// Converts a wildcard pattern using * (zero or more characters) and ? (one character) to an SQL LIKE pattern.
		/// </summary>
        public static string WildcardToSqlLike(string wildcard, bool escapeQuotes)
		{
			if (string.IsNullOrEmpty(wildcard))
				return wildcard;

			StringBuilder sb = new StringBuilder(wildcard);
			EscapeSqlLikeInternal(sb, escapeQuotes);

			sb.Replace('*', '%');
			sb.Replace('?', '_');

			return sb.ToString();
		}

        /// <summary>
        /// A very simple wildcard matching function. Only supports * at the moment (not ?) - extend as needed.
        /// Performs an ordinal case-insensitive comparison.
        /// </summary>
        /// <param name="input">The text to match.</param>
        /// <param name="pattern">The wildcard pattern to match against.</param>
        /// <returns>True if the input matches the pattern, otherwise false.</returns>
        public static bool IsWildcardMatch(string input, string pattern)
        {
            return IsWildcardMatch(input, pattern, StringComparison.OrdinalIgnoreCase);
        }

	    /// <summary>
        /// A very simple wildcard matching function. Only supports * at the moment (not ?) - extend as needed.
        /// </summary>
        /// <param name="input">The text to match.</param>
        /// <param name="pattern">The wildcard pattern to match against.</param>
        /// <param name="comparisonType">The string comparision type to use</param>
        /// <returns>True if the input matches the pattern, otherwise false.</returns>
        public static bool IsWildcardMatch(string input, string pattern, StringComparison comparisonType)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");
            if (pattern.IndexOf('?') != -1)
                throw new NotSupportedException("The pattern contains a '?' character, which is not yet supported.");

            if (input == null)
                return false;
            if (pattern.Length == 0)
                return (input.Length == 0);

            // In practice most patterns are "something" or "something*" - try shortcuts for those case.

            int starIndex = pattern.IndexOf('*');
            if (starIndex == -1)
                return string.Equals(input, pattern, comparisonType); // No wildcards.
            else if (starIndex == pattern.Length - 1)
                return (string.Compare(input, 0, pattern, 0, pattern.Length - 1, comparisonType) == 0);

	        string[] parts = pattern.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

            int inputStartIndex = 0;
            int iPatternPart = 0;

            if (pattern[0] != '*')
            {
                // Doesn't start with a *, so the first pattern part must be right at the start.

                string firstPart = parts[0];
                if (!input.StartsWith(firstPart, comparisonType))
                    return false;

                inputStartIndex = firstPart.Length;
                iPatternPart = 1;
            }

            while (iPatternPart < parts.Length)
            {
                string patternPart = parts[iPatternPart];
                Debug.Assert(patternPart.Length > 0, "patternPart.Length > 0");

                int iMatch = input.IndexOf(patternPart, inputStartIndex, comparisonType);
                if (iMatch == -1)
                    return false;

                inputStartIndex = iMatch + patternPart.Length;
                iPatternPart++;
            }

            if (pattern[pattern.Length - 1] != '*')
            {
                // Doesn't end with a *, so the last pattern part must be right at the end.
                return (inputStartIndex == input.Length);
            }

            return true;
        }

        public static bool IsWildcardMatchAny(string input, IEnumerable<string> patterns,
            StringComparison comparisonType)
        {
            if (patterns == null)
                throw new ArgumentNullException("patterns");

            foreach (string pattern in patterns)
            {
                if (IsWildcardMatch(input, pattern, comparisonType))
                    return true;
            }

            return false;
        }

	    public static string WildcardToRegex(string wildcard)
		{
			if (string.IsNullOrEmpty(wildcard))
				return wildcard;

			string regex = Regex.Escape(wildcard);
			regex = regex.Replace("\\*", ".*").Replace("\\?", ".");

			return regex;
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

        private static void EscapeSqlLikeInternal(StringBuilder sb, bool escapeQuotes)
		{
            if (escapeQuotes)
            {
                sb.Replace("'", "''");
            }

            sb.Replace("[", "[[]");
			sb.Replace("%", "[%]");
			sb.Replace("_", "[_]");
		}

        public static IList<string> SplitString(string separator, string text)
        {
            return SplitString(separator, text, StringSplitOptions.None);
        }

	    public static IList<string> SplitString(string separator, string text, StringSplitOptions options)
	    {
            if (string.IsNullOrEmpty(separator))
                throw new ArgumentException("The separator must be specified.");

            if (string.IsNullOrEmpty(text))
                return new List<string>(0);

            bool includeEmpty = ((options & StringSplitOptions.RemoveEmptyEntries) != StringSplitOptions.RemoveEmptyEntries);
            List<string> split = new List<string>();

            int startIndex = 0;
            int endIndex = text.IndexOf(separator);

            while (endIndex != -1 && startIndex < text.Length)
            {
                if (includeEmpty || endIndex > startIndex + 1)
                {
                    split.Add(text.Substring(startIndex, endIndex - startIndex));
                }

                startIndex = endIndex + separator.Length;
                if (startIndex >= text.Length)
                    break;

                endIndex = text.IndexOf(separator, startIndex);
            }

            if (startIndex < text.Length)
            {
                split.Add(text.Substring(startIndex));
            }
            else if (includeEmpty)
            {
                Debug.Assert(startIndex == text.Length, "startIndex == text.Length + 1");
                split.Add("");
            }

	        return split;
	    }

	    /// <summary>
        /// Returns true if the character to the left of indexLeft and the character to the right of indexRight
        /// are both word boundaries, ie. whitespace or start/end of the string.
        /// </summary>
	    public static bool AreWordBoundaries(string text, int indexLeft, int indexRight)
	    {
            return TextUtil.IsLeftWordBoundary(text, indexLeft) && TextUtil.IsRightWordBoundary(text, indexRight);
	    }

        public static void SkipChars(string text, bool forward, ref int index, params char[] toSkip)
        {
            while (index >= 0 && index < text.Length && Array.IndexOf(toSkip, text[index]) != -1)
            {
                index += (forward ? 1 : -1);
            }
        }

        public static string ByteArrayToHexString(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");

            char[] chars = new char[bytes.Length * 2];
            int charIndex = 0;

            for (int byteIndex = 0; byteIndex < bytes.Length; byteIndex++)
            {
                int hexChar = (bytes[byteIndex] & 240) >> 4;
                chars[charIndex++] = hexCharValues[hexChar];

                hexChar = bytes[byteIndex] & 15;
                chars[charIndex++] = hexCharValues[hexChar];
            }

            return new string(chars);
        }

        public static byte[] HexStringToByteArray(string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");
            if ((str.Length & 1) == 1)
                throw new ArgumentException("The hex string is of odd length.", "str");

            byte[] hexValues = GetHexValues();

            byte[] bytes = new byte[str.Length / 2];
            int strIndex = 0;
            int bytesIndex = 0;
            int length = bytes.Length;

            while (--length >= 0)
            {
                int highDigit = ParseHexDigit(hexValues, str, ref strIndex);
                int lowDigit = ParseHexDigit(hexValues, str, ref strIndex);

                bytes[bytesIndex++] = (byte)((highDigit << 4) + lowDigit);
            }

            return bytes;
        }

	    private static int ParseHexDigit(byte[] hexValues, string str, ref int index)
	    {
	        try
	        {
	            return hexValues[str[index++]];
	        }
	        catch (ArgumentException ex)
	        {
	            throw new ArgumentException("The hex string is invalid at index " + index + ".", ex);
	        }
	        catch (IndexOutOfRangeException ex)
	        {
                throw new ArgumentException("The hex string is invalid at index " + index + ".", ex);
	        }
	    }

	    private static byte[] GetHexValues()
        {
            byte[] hexValues = hexByteValues;

            if (hexValues == null)
            {
                hexValues = new byte[0x67];

                int index = hexValues.Length;
                while (--index >= 0)
                {
                    if ((0x30 <= index) && (index <= 0x39))
                    {
                        hexValues[index] = (byte)(index - 0x30);
                    }
                    else
                    {
                        if ((0x61 <= index) && (index <= 0x66))
                        {
                            hexValues[index] = (byte)((index - 0x61) + 10);
                            continue;
                        }
                        if ((0x41 <= index) && (index <= 70))
                        {
                            hexValues[index] = (byte)((index - 0x41) + 10);
                        }
                    }
                }

                hexByteValues = hexValues;
            }

            return hexValues;
        }

	    public static string Join(string separator, IEnumerable values)
	    {
            if (values == null)
                return "";

            IEnumerator enumerator = values.GetEnumerator();
            if (!enumerator.MoveNext())
                return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(enumerator.Current);

            while (enumerator.MoveNext())
            {
                sb.Append(separator);
                sb.Append(enumerator.Current);
            }

            return sb.ToString();
	    }

        public static T[] ParseArrayFromString<T>(string separator, string array)
            where T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(array))
                return null;

            string[] strings = array.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            T[] values = new T[strings.Length];

            for (int i = 0; i < strings.Length; i++)
            {
                values[i] = (T)((IConvertible)strings[i]).ToType(typeof(T), null);
            }

            return values;
        }

        public static bool IsTrimmedStringEmpty(string str)
        {
            return String.IsNullOrEmpty(str) || String.IsNullOrEmpty(str.Trim());
        }

        public static bool StartsWithAVowel(string word)
        {
            if (string.IsNullOrEmpty(word))
                return false;

            // EP 10/12/07: This is much faster than ToLower().

            char c = word[0];
            return (c == 'a' || c == 'A' || c == 'e' || c == 'E' || c == 'i' || c == 'I' || c == 'o' || c == 'O'
                || c == 'u' || c == 'U');
        }

        public static string AllowWordBreaks(string text, char[] allowedWordBreakChars)
        {
            foreach (char c in allowedWordBreakChars)
            {
                text = text.Replace(c.ToString(), string.Format("{0}<wbr /><span class=\"wbr\"></span>", c));
            }

            return text;
        }

        public static string Repeat(string str, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", count, "The count must be >= 0.");

            if (string.IsNullOrEmpty(str) || count == 1)
                return str;
            if (count == 0)
                return "";

            int length = str.Length;
            char[] repeated = new char[length * count];

            for (int i = 0; i < count; i++)
            {
                str.CopyTo(0, repeated, i * length, length);
            }

            return new string(repeated);
        }
    }
}
