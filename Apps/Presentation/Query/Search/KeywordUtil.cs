using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LinkMe.Apps.Presentation.Query.Search
{
    public class SnippetContext
    {
        public string Text;
        public int KeywordSet;
    }

	public class KeywordUtil
	{
		#region Nested Types

        private class MatchContext
        {
            public Match Match;
            public int KeywordSet;
        }

		private class MatchComparer : IComparer<MatchContext>
		{
			#region IComparer Members

            public int Compare(MatchContext a, MatchContext b)
			{
				int result = (a.Match.Index - b.Match.Index);
				if (result != 0)
					return result;

				return b.Match.Length - a.Match.Length;
			}

			#endregion
		}

		private class KeywordSubsetComparer : IComparer<string>
		{
            public int Compare(string a, string b)
			{
				if (a == b)
					return 0;
				else if (a.IndexOf(b) != -1)
					return -1;
				else if (b.IndexOf(a) != -1)
					return 1;
				else
					return 0;
			}
		}

		#endregion

		// Each "word" must be preceded and followed by by a non-word character other than '-' or '.',
		// except that ". " is allowed at the end of the word (end of the sentence). This means that
		// "mail" would not be found in the string "e-mail" and ".NET" would not be found in "ASP.NET",
		// for example.

		public const string KEYWORD_CAPTURE_NAME = "keyword";

        private const string _wordSeparatorCharRegex = @"[^\w\.&]";
		private const string KEYWORD_REGEX_FORMAT =
			@"(?<=^|{1}|" + _wordSeparatorCharRegex + ")(?<" + KEYWORD_CAPTURE_NAME + @">{0})(?=($|\.(\s|$|{1})|"
            + _wordSeparatorCharRegex + "))";

		private const string END_OF_SENTENCE_CHARS = ".!?";
		private const int SHORT_SNIPPET_LENGTH = 7;
		private const int MEDIUM_SNIPPET_LENGTH = 14;
		private const int LONG_SNIPPET_LENGTH = 28;

		private readonly string _newLine;
		private readonly string _keywordPrefix;
		private readonly string _keywordSuffix;
		private readonly string _startMarker;
		private readonly string _endMarker;

		public KeywordUtil(string newLine, string keywordPrefix, string keywordSuffix, string startMarker, string endMarker)
		{
			if (string.IsNullOrEmpty(newLine))
				throw new ArgumentException("The new line string must be specified.", "newLine");

			_newLine = newLine;
			_keywordPrefix = keywordPrefix;
			_keywordSuffix = keywordSuffix;
			_startMarker = startMarker;
			_endMarker = endMarker;
		}

        public List<SnippetContext> GetKeywordsInContext(string text, params string[] keywords)
		{
			// Check input.

			if (string.IsNullOrEmpty(text))
                return new List<SnippetContext>();

			if (keywords == null || keywords.Length == 0)
				throw new ArgumentException("The keywords must be specified.", "keywords");

			foreach (string keyword in keywords)
			{
				if (keyword.IndexOf(_newLine) != -1)
				{
					throw new ArgumentException(string.Format("A keyword, '{0}', contains the new line"
						+ " string, '{1}'.", keyword, _newLine));
				}
			}

			// Find all the keyword occurrences. Use multiple regexes - one for each keyword. For
			// some (very strange!) reason this is an order of magnitude faster than simply ORing
			// all the keywords in one regex.

			Regex findExactMatch;
			Regex[] findKeywords = GetKeywordRegexes(keywords, out findExactMatch);

			IList<MatchContext> matches = GetAllMatches(text, findKeywords, findExactMatch);
			if (matches.Count == 0)
                return new List<SnippetContext>();

			// Generate the snippets.

            List<SnippetContext> inContext = new List<SnippetContext>();
			int totalWords = 0;

			for (int firstMatchIndex = 0; firstMatchIndex < matches.Count; firstMatchIndex++)
			{
				// Work out the boundaries of the current line - the snippet must be within these.

				int startIndex = matches[firstMatchIndex].Match.Index;

				int leftBound, rightBound;
				GetSnippetBoundaries(text, startIndex, out leftBound, out rightBound);

				int lastMatchIndex = firstMatchIndex;
				int spannedWords = 1;
				int snippetTotalWordCount = 0;

				if (matches.Count > 1)
				{
					// Work out the possible snippet lengths for this match.

					int[] wordCounts = GetAvailableSnippetLengths(totalWords);
					if (wordCounts == null)
						break; // Enough words found, stop.

					// Find the best snippet (one that includes the most matches) that fits into the
					// available number of words.

					FindBestWordSpan(text, rightBound, matches, wordCounts, firstMatchIndex,
						out lastMatchIndex, out spannedWords, out snippetTotalWordCount);
				}

				MatchContext lastMatch = matches[lastMatchIndex];
				int endIndex = lastMatch.Match.Index + lastMatch.Match.Length - 1;

				int availableWords;
				if (snippetTotalWordCount == 0)
				{
					Debug.Assert(firstMatchIndex == lastMatchIndex, "firstMatchIndex == lastMatchIndex");
					availableWords = GetNextSnippetLength(totalWords, matches.Count - firstMatchIndex,
						inContext.Count) - 1;
					Debug.Assert(availableWords > 0, "GetNextSnippetLength() returned 0 - how?");
				}
				else
				{
					if (firstMatchIndex == 0 && lastMatchIndex == matches.Count - 1)
					{
						// All the keywords are in one snippet, might as well use all the avaialble words.

						snippetTotalWordCount = LONG_SNIPPET_LENGTH;
					}

					availableWords = snippetTotalWordCount - spannedWords;
				}

				// Pad it with words on both sides to reach the required number of words.

				bool atSentenceStart, atSentenceEnd;
				int addedWords = 0;

				if (availableWords == 0)
				{
					bool dummy;
					atSentenceStart = (IsAtSentenceBoundary(text, startIndex, leftBound, out dummy) != -1);

					// Only set atSentenceEnd to true if there is a full stop (exclamation mark, etc.)
					// at the end of it, not just if the end of the line is reached.

					int tempIndex = IsAtSentenceBoundary(text, endIndex, rightBound,
						out atSentenceEnd);
					if (tempIndex != -1 && atSentenceEnd)
					{
						endIndex = tempIndex; // Include the end-of-sentence character in the snippet.
					}
				}
				else
				{
					addedWords = ExpandSnippet(text, availableWords, leftBound, rightBound,
						ref startIndex, ref endIndex, out atSentenceStart, out atSentenceEnd);
				}
				Debug.Assert(startIndex >= 0 && endIndex > 0, "startIndex >= 0 && endIndex > 0");

				// Highlight the keywords and add sentence start and end markers, if needed.

				SnippetContext snippet = AddKeywordHighlighting(text, matches, startIndex, endIndex,
					firstMatchIndex, lastMatchIndex, atSentenceStart, atSentenceEnd);
				inContext.Add(snippet);

				totalWords += spannedWords + addedWords;
				firstMatchIndex = lastMatchIndex;
			}

			return inContext;
		}

		public static Regex GetKeywordRegex(string[] keywords, string newLine)
		{
			if (string.IsNullOrEmpty(newLine))
				throw new ArgumentException("The new line string must be specified.", "newLine");
            if (keywords == null || keywords.Length == 0)
                return null;

			StringBuilder sb = new StringBuilder();

			// If there are multiple keywords look for an exact string match first.

			if (keywords.Length > 1)
			{
				sb.Append(Regex.Escape(string.Join(" ", keywords)));
				sb.Append('|');

				// Rearrange the keywords, so that if one is a subset of another then the longer keyword
				// comes first. This results in the longer match being hihglighted.

				string[] reodered = new string[keywords.Length];
				keywords.CopyTo(reodered, 0);
				Array.Sort(reodered, new KeywordSubsetComparer());
				keywords = reodered;
			}

			foreach (string keyword in keywords)
			{
				if (string.IsNullOrEmpty(keyword))
					throw new ArgumentException("A keyword must not be null or empty string.", "keywords");

				sb.Append(Regex.Escape(keyword));
				sb.Append('|');
			}

			string regex = string.Format(KEYWORD_REGEX_FORMAT, sb.ToString(0, sb.Length - 1), newLine);

			return new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
		}

		private Regex[] GetKeywordRegexes(string[] keywords, out Regex exactMatchRegex)
		{
			if (keywords == null || keywords.Length == 0)
				throw new ArgumentException("No keywords were specified.");
			if (string.IsNullOrEmpty(_newLine))
				throw new ArgumentException("The new line string must be specified.", "newLine");

			// If there are multiple keywords look for an exact string match first.

			exactMatchRegex = null;
			if (keywords.Length > 1)
			{
				StringBuilder sb = new StringBuilder();

				sb.Append(Regex.Escape(keywords[0]));

				for (int index = 1; index < keywords.Length; index++)
				{
					sb.Append("\\s+");
					sb.Append(Regex.Escape(keywords[index]));
				}

				string regex = string.Format(KEYWORD_REGEX_FORMAT, sb, _newLine);
				exactMatchRegex = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
			}

			Regex[] regexes = new Regex[keywords.Length];

			for (int index = 0; index < keywords.Length; index++)
			{
				string keyword = keywords[index];
				if (string.IsNullOrEmpty(keyword))
					throw new ArgumentException("A keyword must not be null or empty string.", "keywords");

				string regex = string.Format(KEYWORD_REGEX_FORMAT, Regex.Escape(keyword), _newLine);
				regexes[index] = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
			}

			return regexes;
		}

		private static int GetNextSnippetLength(int wordsUsed, int snippetsRemaining, int snippetsUsed)
		{
			Debug.Assert(snippetsRemaining > 0, "snippetCount > 0");
            return MEDIUM_SNIPPET_LENGTH;

/*
			int wordsRemaining = LONG_SNIPPET_LENGTH - wordsUsed;

			if (wordsRemaining == LONG_SNIPPET_LENGTH)
			{
				if (snippetsRemaining == 1)
					return LONG_SNIPPET_LENGTH;
				else if (snippetsRemaining == 2)
					return MEDIUM_SNIPPET_LENGTH;
				else
					return SHORT_SNIPPET_LENGTH;
			}
			else if (wordsRemaining >= MEDIUM_SNIPPET_LENGTH)
			{
				if (snippetsRemaining == 1 && snippetsUsed == 1)
					return MEDIUM_SNIPPET_LENGTH;
				else
					return SHORT_SNIPPET_LENGTH;
			}
			else if (wordsRemaining >= SHORT_SNIPPET_LENGTH)
				return SHORT_SNIPPET_LENGTH;
			else
				return 0;
*/
		}

		private static int[] GetAvailableSnippetLengths(int totalWords)
		{
            return new int[] { /*SHORT_SNIPPET_LENGTH,*/ MEDIUM_SNIPPET_LENGTH/*, LONG_SNIPPET_LENGTH*/ };

/*
            if (totalWords == 0)
				return new int[] { SHORT_SNIPPET_LENGTH, MEDIUM_SNIPPET_LENGTH, LONG_SNIPPET_LENGTH };
			else if (LONG_SNIPPET_LENGTH - totalWords >= MEDIUM_SNIPPET_LENGTH)
				return new int[] { SHORT_SNIPPET_LENGTH, MEDIUM_SNIPPET_LENGTH };
			else if (LONG_SNIPPET_LENGTH - totalWords >= SHORT_SNIPPET_LENGTH)
				return new int[] { SHORT_SNIPPET_LENGTH };
			else
				return null;
*/
		}

		private SnippetContext AddKeywordHighlighting(string text, IList<MatchContext> matches,
			int startIndex, int endIndex, int firstMatchIndex, int lastMatchIndex,
			bool atSentenceStart, bool atSentenceEnd)
		{
			Debug.Assert(startIndex >= 0 && endIndex > 0 && firstMatchIndex >= 0 && lastMatchIndex >= 0,
				"startIndex >= 0 && endIndex > 0 && firstMatchIndex >= 0 && lastMatchIndex >= 0");

			string currentStartMarker = _startMarker;
			if (atSentenceStart || currentStartMarker == null)
			{
				currentStartMarker = "";
			}

			string currentEndMarker = _endMarker;
			if (atSentenceEnd || currentEndMarker == null)
			{
				currentEndMarker = "";
			}

			int length = endIndex - startIndex + 1;
			int capacity = length + currentStartMarker.Length + currentEndMarker.Length
				+ (lastMatchIndex - firstMatchIndex + 1) * (_keywordPrefix.Length + _keywordSuffix.Length);

			StringBuilder sb = new StringBuilder(text, startIndex, length, capacity);

			sb.Insert(0, currentStartMarker);
			int offset = startIndex - currentStartMarker.Length;
		    int keywordSet = 0;

			for (int matchIndex = firstMatchIndex; matchIndex <= lastMatchIndex; matchIndex++)
			{
				MatchContext match = matches[matchIndex];

				sb.Insert(match.Match.Index - offset, _keywordPrefix);
				offset -= _keywordPrefix.Length;
				sb.Insert(match.Match.Index + match.Match.Length - offset, _keywordSuffix);
				offset -= _keywordSuffix.Length;

			    keywordSet |= match.KeywordSet;
			}

			sb.Append(currentEndMarker);
			Debug.Assert(sb.Capacity > sb.Length, "sb.Capacity == sb.Length");

            return new SnippetContext { Text = sb.ToString(), KeywordSet = keywordSet };
		}

		private static void FindBestWordSpan(string text, int rightBound, IList<MatchContext> matches,
			int[] wordCounts, int firstMatchIndex, out int lastMatchIndex, out int spannedWordCount,
			out int totalWordCount)
		{
			MatchContext firstMatch = matches[firstMatchIndex];
			int lastWordIndex = firstMatch.Match.Index + firstMatch.Match.Length;

			int currentMatchIndex = firstMatchIndex;
			int currentWordCount;
			int currentSpannedWordCount = 1; // Including the matched word itself.

			lastMatchIndex = firstMatchIndex;
			totalWordCount = 0;
			spannedWordCount = 1;

			for (int wordCountIndex = 0; wordCountIndex < wordCounts.Length; wordCountIndex++)
			{
				currentWordCount = wordCounts[wordCountIndex];

				// Find the start index of the last word for the current snippet length (word count).

				while (currentSpannedWordCount < currentWordCount && currentMatchIndex + 1 < matches.Count)
				{
					int tempIndex = FindStartOfNextWord(text, lastWordIndex, rightBound);
					if (tempIndex == -1)
						return; // No more words - stop.

					lastWordIndex = tempIndex;
					currentSpannedWordCount++;

					// Check if more matches "fit into" the snippet now than before.

					if (matches[currentMatchIndex + 1].Match.Index <= lastWordIndex)
					{
						currentMatchIndex++;

						// Spanned more keywords than in the last iteration - use this length UNLESS:
						// 1) This would use up all the words we have;
						// 2) Only two matches were included; and
						// 3) There are still more matches.
						// In that case don't decide to use the new length yet, but keep looking in
						// case more matches can fit into it.

						if (currentWordCount != LONG_SNIPPET_LENGTH
							|| (currentMatchIndex - firstMatchIndex) > 1
							|| currentMatchIndex >= matches.Count - 1)
						{
							totalWordCount = currentWordCount;
							spannedWordCount = currentSpannedWordCount;
							lastMatchIndex = currentMatchIndex;
						}
					}
				}
			}
		}

		private void GetSnippetBoundaries(string text, int index, out int leftBound, out int rightBound)
		{
			leftBound = text.LastIndexOf(_newLine, index);
			if (leftBound == -1)
			{
				leftBound = 0;
			}
			else
			{
				leftBound += _newLine.Length;
			}

			rightBound = text.IndexOf(_newLine, index);
			if (rightBound == -1)
			{
				rightBound = text.Length - 1;
			}
			else
			{
				Debug.Assert(rightBound > 0, "rightBound > 0");
				rightBound--;
			}
		}

		private static int ExpandSnippet(string text, int maxWords, int leftBound, int rightBound,
			ref int startIndex, ref int endIndex, out bool atSentenceStart, out bool atSentenceEnd)
		{
			Debug.Assert(maxWords > 0, "maxWords > 0");
			Debug.Assert(startIndex >= 0 && endIndex > 0, "startIndex >= 0 && endIndex > 0");

			// Get words before the keyword.

			int maxWordsBefore = maxWords / 2 + maxWords % 2;

			int wordsBefore = 0;
			atSentenceStart = true;

			if (startIndex != 0)
			{
				int tempIndex = FindSnippetBoundary(text, startIndex - 1, maxWordsBefore, leftBound,
					true, out wordsBefore, out atSentenceStart);
				if (tempIndex != -1)
				{
					startIndex = tempIndex;
				}
			}

			// Get words after the keyword.

			int maxWordsAfter = maxWords - wordsBefore;
			int wordsAfter = 0;

			if (endIndex < rightBound)
			{
				int tempIndex = FindSnippetBoundary(text, endIndex + 1, maxWordsAfter, rightBound,
					false, out wordsAfter, out atSentenceEnd);
				if (tempIndex != -1)
				{
					endIndex = tempIndex;
				}
			}
			else
			{
				atSentenceEnd = (endIndex != rightBound);
			}

			if (!atSentenceStart && wordsAfter < maxWordsAfter)
			{
				// Reached the end of the sentence without the required number of words. Try to get
				// more words before the keyword.

				maxWordsBefore = maxWords - wordsBefore - wordsAfter;

				int extraWordsBefore;
				int tempIndex = FindSnippetBoundary(text, startIndex - 1, maxWordsBefore, leftBound,
					true, out extraWordsBefore, out atSentenceStart);

				if (tempIndex != -1)
				{
					startIndex = tempIndex;
					wordsBefore += extraWordsBefore;
				}
			}

			return wordsBefore + wordsAfter;
		}

		// If the word at "index" is at the sentence boundary return the character index of the sentence
		// boundary, otherwise -1.
		private static int IsAtSentenceBoundary(string text, int index, int boundary,
			out bool haveEndSentenceMarker)
		{
			haveEndSentenceMarker = false;

			if (index == boundary)
				return boundary;

			int sign = (index < boundary ? 1 : -1);
			index += sign;

			while (index * sign <= boundary * sign)
			{
				char c = text[index];

				if (END_OF_SENTENCE_CHARS.IndexOf(c) != -1)
				{
					if (c != '.' || index == text.Length - 1 || !char.IsLetterOrDigit(text[index + 1]))
					{
						haveEndSentenceMarker = true;
						return index; // Found end of sentence.
					}
				}
				else if (!char.IsWhiteSpace(c))
					return -1; // Found another word.

				index += sign;
			}

			return boundary; // Reached the boundary.
		}

		private static int FindSnippetBoundary(string text, int startIndex, int maxWords,
			int boundary, bool lineBoundaryIsSentenceBoundary, out int words, out bool atSentenceBoundary)
		{
			words = 0;
			atSentenceBoundary = false;

			if (boundary == startIndex)
			{
				atSentenceBoundary = lineBoundaryIsSentenceBoundary ||
					END_OF_SENTENCE_CHARS.IndexOf(text[boundary]) != -1;
				return startIndex;
			}

			int lastWordChar = -1;
			int index = startIndex;
			int sign = (boundary < startIndex ? -1 : 1); // Are we seeking backwards of forwards?

			while (index * sign <= boundary * sign && words <= maxWords)
			{
				char c = text[index];

				if (END_OF_SENTENCE_CHARS.IndexOf(c) != -1)
				{
					if (sign == -1)
					{
						if (lastWordChar != index + 1)
						{
							// Found the start of the sentence - stop.

							atSentenceBoundary = true;
							break;
						}
					}
					else if (c != '.' || index == text.Length - 1 || !char.IsLetterOrDigit(text[index + 1]))
					{
						// Found the end of the sentence - stop and include the end of sentence
						// character itself in the snippet.

						lastWordChar = index;
						atSentenceBoundary = true;
						break;
					}
				}
				else if (!char.IsWhiteSpace(c))
				{
					if (lastWordChar != index - sign)
					{
						words++; // Found another word.
					}

					lastWordChar = index;
				}
				else if (words == maxWords)
				{
					if (lineBoundaryIsSentenceBoundary)
					{
						// Found the required number of words, but is this the line boundary?

						while (index * sign < boundary * sign && char.IsWhiteSpace(text[index]))
						{
							index += sign;
						}

						if (index == boundary && char.IsWhiteSpace(text[index]))
						{
							atSentenceBoundary = true;
						}
					}

					return lastWordChar;
				}

				index += sign;
			}

			if (index == boundary + sign)
			{
				atSentenceBoundary = lineBoundaryIsSentenceBoundary;
			}

			return lastWordChar;
		}

		private static int FindStartOfNextWord(string text, int startIndex, int boundary)
		{
			bool inWhitespace = false;

			int sign = (startIndex < boundary ? 1 : -1);

			for (int index = startIndex; index * sign <= boundary * sign; index += sign)
			{
				char c = text[index];

				if (char.IsWhiteSpace(c))
				{
					inWhitespace = true;
					continue;
				}

				if (END_OF_SENTENCE_CHARS.IndexOf(c) != -1)
				{
					// Note that a '.' is not considered the end of the sentence if followed an
					// alphanumeric characters, eg. "ASP.NET".

					if (c != '.' || index == text.Length - 1 || !char.IsLetterOrDigit(text[index + 1]))
						return (sign == 1 ? -1 : index); // End of sentence - no next word if searching forward.
				}
				else if (inWhitespace)
					return index; // Found the next word (non-whitespace character).
			}

			return -1; // No more words.
		}

		private static IList<MatchContext> GetAllMatches(string text, Regex[] findKeywords, Regex findExactMatch)
		{
            List<MatchContext> matches = new List<MatchContext>();
			for (int i = 0; i < findKeywords.Length; i++)
			{
			    MatchCollection keywordMatches = findKeywords[i].Matches(text);

			    foreach (Match match in keywordMatches)
			    {
			        MatchContext matchContext = new MatchContext();
			        matchContext.Match = match;
			        matchContext.KeywordSet = 1 << i;

                    matches.Add(matchContext);
			    }
			}

			matches.Sort(new MatchComparer());
			RemoveMatchesForOverlappedKeywords(matches);

			if (findExactMatch == null)
				return matches;

			IEnumerable<Match> exactMatches = findExactMatch.Matches(text).Cast<Match>();
			if (!exactMatches.Any())
				return matches;

			// If there are multiple keywords and an exact match is found start from there, so that
			// the exact match appears first. Ideally preceding partial matches should be returned
			// as well, but this is not implemented yet, so just remove all matches before the exact match.
			// The proper logic would need to avoid overlapping matches, if implemented.

			foreach (Match exactMatch in exactMatches)
			{
				int index = 0;
				while (index < matches.Count)
				{
					MatchContext match = matches[index];

					if (match.Match.Index < exactMatch.Index ||
						match.Match.Index + match.Match.Length <= exactMatch.Index + exactMatch.Length)
					{
						matches.RemoveAt(index);
					}
					else
					{
						index++;
					}
				}
			}

            matches.InsertRange(0, exactMatches.Select(
                m => new MatchContext { Match = m, KeywordSet = int.MaxValue }
                ));

			return matches;
		}

		private static void RemoveMatchesForOverlappedKeywords(IList<MatchContext> matches)
		{
			// Assume the matches are sorted by index (start to end), then by length (longest first),
			// which is what MatchComparer does.

			int index = 1;
			while (index < matches.Count)
			{
                MatchContext lastMatch = matches[index - 1];
                MatchContext thisMatch = matches[index];
				Debug.Assert(lastMatch.Match.Index <= thisMatch.Match.Index, "lastMatch.Index <= thisMatch.Index");

				if (lastMatch.Match.Index + lastMatch.Match.Length >= thisMatch.Match.Index + thisMatch.Match.Length)
				{
					// The last match already includes all of this match - remove this one.
                    lastMatch.KeywordSet |= thisMatch.KeywordSet;
					matches.RemoveAt(index);
				}
				else
				{
					Debug.Assert(lastMatch.Match.Index + lastMatch.Match.Length - 1 < thisMatch.Match.Index,
						string.Format("Match '{0}' overlaps with match '{1}', but is not a subset of it.",
						thisMatch.Match.Value, lastMatch.Match.Value));
					index++;
				}
			}
		}
	}
}
