using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;

namespace LinkMe.Framework.Utility
{
    public static class HtmlUtil
    {
        #region Nested types

        private enum TagType
        {
            None,
            Open,
            Close,
            OpenAndClose
        }

        // Copied from System.Web.CrossSiteScriptingValidation
        private class CrossSiteScriptingValidation
        {
            public static readonly char[] startingChars = new[] { '<', '&', 'o', 'O', 's', 'S', 'e', 'E' };

            private CrossSiteScriptingValidation()
            {
            }

            private static bool IsDangerousExpressionString(string s, int index)
            {
                if ((index + 10) >= s.Length)
                {
                    return false;
                }
                if ((s[index + 1] != 'x') && (s[index + 1] != 'X'))
                {
                    return false;
                }
                return (string.Compare(s, index + 2, "pression(", 0, 9, true, CultureInfo.InvariantCulture) == 0);
            }

            private static bool IsDangerousOnString(string s, int index)
            {
                if ((s[index + 1] == 'n') || (s[index + 1] == 'N'))
                {
                    if ((index > 0) && IsAtoZ(s[index - 1]))
                    {
                        return false;
                    }
                    int num1 = s.Length;
                    index += 2;
                    while ((index < num1) && IsAtoZ(s[index]))
                    {
                        index++;
                    }
                    while ((index < num1) && char.IsWhiteSpace(s[index]))
                    {
                        index++;
                    }
                    if (index < num1)
                    {
                        return (s[index] == '=');
                    }
                }
                return false;
            }

            private static bool IsDangerousScriptString(string s, int index)
            {
                if (!NextCharsAreCript(s, index))
                    return false;

                int length = s.Length;
                index += 6;

                while ((index < length) && char.IsWhiteSpace(s[index]))
                {
                    index++;
                }

                if (index < length)
                    return (s[index] == ':');

                return false;
            }

            private static bool NextCharsAreCript(string s, int index)
            {
                if ((index + 6) >= s.Length)
                    return false;

                if ((((s[index + 1] != 'c') && (s[index + 1] != 'C')) || ((s[index + 2] != 'r') && (s[index + 2] != 'R'))) || ((((s[index + 3] != 'i') && (s[index + 3] != 'I')) || ((s[index + 4] != 'p') && (s[index + 4] != 'P'))) || ((s[index + 5] != 't') && (s[index + 5] != 'T'))))
                    return false;

                return true;
            }

            private static bool NextCharsAreScript(string s, int index)
            {
                if ((index + 6) >= s.Length)
                    return false;

                if (s[index] != 's' && s[index] != 'S')
                    return false;

                return NextCharsAreCript(s, index);
            }

            private static bool ContainsXss(string s, int index)
            {
                // Does it contain the string "XSS"? There can be /* comments */ anywhere. Rather than
                // trying to process them properly just consider the string dangerous if it contains EITHER
                // "XSS" or "/*" anywhere before a "<".

                int endIndex = s.IndexOf('>', index);
                int count = (endIndex == -1 ? s.Length - index : endIndex - index + 1);

                return (s.IndexOf("xss", index, count, StringComparison.OrdinalIgnoreCase) != -1
                    || s.IndexOf("/*", index, count, StringComparison.OrdinalIgnoreCase) != -1);
            }

            internal static bool IsDangerousString(string s, bool anyHtmlIsDangerous, out int matchIndex)
            {
                matchIndex = 0;
                int startIndex = 0;

                s = HttpUtility.HtmlDecode(s);

                while (true)
                {
                    int index = s.IndexOfAny(startingChars, startIndex);
                    if (index < 0)
                    {
                        return false;
                    }
                    if (index == (s.Length - 1))
                    {
                        return false;
                    }
                    matchIndex = index;
                    switch (s[index])
                    {
                        case 'o':
                        case 'O':
                            if (IsDangerousOnString(s, index))
                            {
                                return true;
                            }
                            break;

                        case 's':
                        case 'S':
                            if (IsDangerousScriptString(s, index))
                            {
                                return true;
                            }
                            break;

                        case 'e':
                        case 'E':
                            if (IsDangerousExpressionString(s, index))
                            {
                                return true;
                            }
                            break;

                        case '<':
                            if (anyHtmlIsDangerous)
                            {
                                if (IsAtoZ(s[index + 1]) || (s[index + 1] == '!'))
                                    return true;
                            }
                            else
                            {
                                if (NextCharsAreScript(s, index + 1))
                                    return true;
                            }

                            if (ContainsXss(s, index + 1))
                                return true;

                            break;
                    }
                    startIndex = index + 1;
                }
            }

            public static bool ContainsHtml(string s, out int matchIndex)
            {
                matchIndex = 0;
                int startIndex = 0;
                while (true)
                {
                    int num2 = s.IndexOfAny(startingChars, startIndex);
                    if (num2 < 0)
                    {
                        return false;
                    }
                    if (num2 == (s.Length - 1))
                    {
                        return false;
                    }
                    matchIndex = num2;
                    char ch = s[num2];
                    if (ch != '&')
                    {
                        if ((ch == '<') && ((IsAtoZ(s[num2 + 1]) || (s[num2 + 1] == '!')) || (s[num2 + 1] == '/')))
                        {
                            return true;
                        }
                    }
                    else if (s[num2 + 1] == '#')
                    {
                        return true;
                    }
                    startIndex = num2 + 1;
                }
            }
        }

        #endregion

        // List of tags taken from http://www.htmlcodetutorial.com/quicklist.html
        private static readonly HashSet<string> _tagsNotToClose = new HashSet<string>(
            new[]
		    {
			    "app", "applet", "area", "base", "basefont", "bgsound", "br", "col", "dd", "dl", "dt", "embed",
			    "frame", "hr", "iframe", "img", "input", "isindex", "li", "link", "meta", "option", "param",
			    "script", "sound", "spacer", "wbr"
		    },
            StringComparer.CurrentCultureIgnoreCase);

        private static readonly Regex _containsJsEvent = new Regex(@"\s{0,1}on[a-z]+?=(("".*?[^\\]"")|('.*?[^\\]'))",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex _containsScript = new Regex(@"<[\s\n]*?script.*?>.*?<[\s\n]*?/[\s\n]*?script[\s\n]*?>",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex _containsHtml = new Regex("</*?[a-z]+?.*?>",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static string LineBreaksToHtml(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var doc = new HtmlDocument();
            doc.LoadHtml(text);

            //merely loading corrects most errors (including removing \r\n and \n from within tags (eg. <span \r\n >)

            doc.DocumentNode.InnerHtml = doc.DocumentNode.InnerHtml.Replace("\r\n", "<br />");
            doc.DocumentNode.InnerHtml = doc.DocumentNode.InnerHtml.Replace("\n", "<br />");

            var returnString = doc.DocumentNode.InnerHtml;

            //ensure there are no unclosed br tags
            return returnString.Replace("<br>", "<br />");
        }

        public static string HtmlLineBreakToText(string text)
        {
            return string.IsNullOrEmpty(text) ? text : text.Replace("<br />", "\r\n").Replace("<br/>", "\r\n");
        }

        public static string TextToHtml(string text)
        {
            return LineBreaksToHtml(HttpUtility.HtmlEncode(text));
        }

        public static string[] TextToHtml(string[] text)
        {
            if (text == null)
                return new string[0];

            string[] html = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                html[i] = TextToHtml(text[i]);
            }

            return html;
        }

        public static string HtmlToText(string value)
        {
            return HttpUtility.HtmlDecode(HtmlLineBreakToText(value));
        }

        public static string AposToHtml(string text)
        {
            return text.Replace("&apos;", "&#39;"); // &apos; is not part of HTML
        }

        /// <summary>
        /// Check user-supplied text and returns it if it doesn't contains any HTML, otherwise returns null.
        /// </summary>
        public static string NullIfContainsHtml(string unsafeString)
        {
            return (ContainsHtml(unsafeString) ? null : unsafeString);
        }

        public static string StrippedIfContainsHtml(string unsafeString)
        {
            return (ContainsHtml(unsafeString) ? "(HTML stripped)" : unsafeString);
        }

        /// <summary>
        /// Check user-supplied text and return true if it contains any HTML, otherwise false.
        /// </summary>
        public static bool ContainsHtml(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            int dummy;
            return CrossSiteScriptingValidation.IsDangerousString(s, true, out dummy);
        }

        /// <summary>
        /// Check user-supplied text and return true if it contains any HTML, otherwise false.
        /// </summary>
        public static bool ContainsHtml(string s, out int matchIndex)
        {
            if (string.IsNullOrEmpty(s))
            {
                matchIndex = -1;
                return false;
            }

            return CrossSiteScriptingValidation.IsDangerousString(s, true, out matchIndex);
        }

        /// <summary>
        /// Check user-supplied text and return true if it contains any script, otherwise false. The check
        /// is not perfect and errs on the safe side, so it may sometimes return true when there is no
        /// actual script.
        /// </summary>
        public static bool ContainsScript(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            int dummy;
            return CrossSiteScriptingValidation.IsDangerousString(s, false, out dummy);
        }

        public static string CloseHtmlTags(string html)
        {
            if (string.IsNullOrEmpty(html))
                return html;

            IList tagsToClose = GetUnclosedTags(html);

            if (tagsToClose.Count == 0)
                return html;

            StringBuilder sb = new StringBuilder(html);
            AddClosingTags(sb, tagsToClose);

            return sb.ToString();
        }

        public static string StripHtmlComments(string html)
        {
            if (string.IsNullOrEmpty(html))
                return html;

            const string openComment = "<!--";
            const string closeComment = "-->";

            var startIndex = html.IndexOf(openComment);

            while (startIndex != -1)
            {
                var endIndex = html.IndexOf(closeComment, startIndex);

                html = endIndex == -1 ? html.Remove(startIndex) : html.Remove(startIndex, endIndex - startIndex + closeComment.Length);

                startIndex = html.IndexOf(openComment);
            }

            return html;
        }

        public static string StripHtmlTags(string html, params string[] tagsToLeave)
        {
            if (string.IsNullOrEmpty(html))
                return html;

            StringBuilder sb = new StringBuilder(html);

            int startIndex = 0;
            int endIndex;
            string tagName;
            TagType tagType;

            while (FindNextTagOpenOrClose(html, startIndex, out tagName, out tagType,
                out startIndex, out endIndex))
            {
                Debug.Assert(tagName != null, "tagName != null");

                if (tagName.Length > 0 && TextUtil.IndexOfStringIgnoreCase(tagsToLeave, tagName, 0) != -1)
                {
                    startIndex = endIndex + 1; // Ignored tag.
                }
                else
                {
                    if (endIndex > sb.Length - 1)
                    {
                        sb.Remove(startIndex, endIndex - startIndex);
                    }
                    else
                    {
                        sb.Remove(startIndex, endIndex - startIndex + 1);
                    }

                    // If removing the tag results in spaces at the end or the start remove them.

                    if (startIndex == 0)
                    {
                        while (sb.Length > 0 && char.IsWhiteSpace(sb[0]))
                        {
                            sb.Remove(0, 1);
                        }
                    }
                    else if (startIndex == sb.Length)
                    {
                        while (sb.Length > 0 && char.IsWhiteSpace(sb[sb.Length - 1]))
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                    }
                    else if (startIndex < sb.Length - 1 && !char.IsWhiteSpace(sb[startIndex - 1]) && !char.IsWhiteSpace(sb[startIndex]))
                    {
                        // But if it results in two words being joined into one (ie. they were only separated by the
                        // HTML tags) then insert a space.

                        sb.Insert(startIndex, ' ');
                    }

                    html = sb.ToString();
                }
            }

            return sb.ToString();
        }


        /// <summary>
        /// Strips out script tags and Javascript event attributes and the associated JS from a string.
        /// </summary>
        /// <param name="text">String to clean</param>
        /// <returns>string</returns>
        public static string CleanScriptAndEventTags(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            string noEvent = _containsJsEvent.Replace(text, "");
            string noScript = _containsScript.Replace(noEvent, "");

            if (noScript != text)
                return CleanScriptAndEventTags(noScript);

            return text;
        }

        /// <summary>
        /// Strips out html tags and javascript (by calling CleanScriptAndEventTags)
        /// </summary>
        /// <param name="text">Text to be cleaned</param>
        /// <returns>Text with all html and script removed</returns>
        public static string CleanScriptEventHtmlTags(string text)
        {
            if (String.IsNullOrEmpty(text))
                return text;

            // Need to call the script cleanser first, so the script doesn't still remain
            // after tags are removed
            string noScript = CleanScriptAndEventTags(text);
            string noHtml = _containsHtml.Replace(noScript, "");

            if (noHtml != text)
                return CleanScriptEventHtmlTags(noHtml);

            return text;
        }

        private static void AddClosingTags(StringBuilder sb, IList tagsToClose)
        {
            for (int i = tagsToClose.Count - 1; i >= 0; i--)
            {
                string tag = (string)tagsToClose[i];
                sb.Append("</");
                sb.Append(tag);
                sb.Append(">");
            }
        }

        private static IList GetUnclosedTags(string html)
        {
            var unclosedTags = new List<string>();

            int startSearchIndex = 0;
            TagType tagType;
            int startIndex, endIndex;
            string tagText;

            while (FindNextTagOpenOrClose(html, startSearchIndex, out tagText, out tagType, out startIndex,
                out endIndex))
            {
                startSearchIndex = endIndex + 1;

                switch (tagType)
                {
                    case TagType.OpenAndClose:
                        continue;

                    case TagType.Close:
                        // Remove the last matching opening tag and everything after it - can't just "pop", since
                        // in HTML not all tags are closed, eg. "<span>blah<br></span>".

                        for (int i = unclosedTags.Count - 1; i >= 0; i--)
                        {
                            if (string.Compare(tagText, unclosedTags[i], true) == 0)
                            {
                                unclosedTags.RemoveRange(i, unclosedTags.Count - i);
                                break;
                            }
                        }
                        break;

                    case TagType.Open:
                        if (!_tagsNotToClose.Contains(tagText))
                        {
                            unclosedTags.Add(tagText);
                        }
                        break;

                    default:
                        Debug.Fail("Unexpected tag type: " + tagType);
                        break;
                }
            }

            return unclosedTags;
        }

        private static bool IsAtoZ(char c)
        {
            return ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
        }

        /// <summary>
        /// Finds the next start or end HTML tag.
        /// </summary>
        /// <param name="html">The HTML string to search.</param>
        /// <param name="startSearchIndex">The index at which to start searching.</param>
        /// <param name="tagName">The text of the found tag. Null if no tag was found or the found tag
        /// was both opened and closed (ie. there is not tag body).</param>
        /// <param name="tagType">Type of the tag that was found - open, close or both (ie. no tag body).</param>
        /// <param name="startIndex">Index of the start of the tag in the string.</param>
        /// <param name="endIndex">Index of the end of the tag in the string.</param>
        /// <returns>True if a tag was found at all (whether closed or not), otherwise false.</returns>
        private static bool FindNextTagOpenOrClose(string html, int startSearchIndex, out string tagName,
            out TagType tagType, out int startIndex, out int endIndex)
        {
            tagType = TagType.None;
            startIndex = -1;
            endIndex = -1;
            tagName = null;

            if (startSearchIndex >= html.Length)
                return false;

            int tagStart = html.IndexOf('<', startSearchIndex);

            while (FindNextTagOpenOrCloseFromStart(html, tagStart, ref tagName, ref tagType, ref startIndex, ref endIndex))
            {
                if (tagName != null)
                    return true;

                // Haven't found a tag yet, but continue searching.

                tagStart = html.IndexOf('<', endIndex);
            }

            return false;
        }

        // Returns true to continue searching, false to stop. Note that tagName may be null even if true is returned,
        // meaning that no tag was found, but there may still be tags later in the text (after endIndex).
        private static bool FindNextTagOpenOrCloseFromStart(string html, int tagStart, ref string tagName,
            ref TagType tagType, ref int startIndex, ref int endIndex)
        {
            if (tagStart == -1 || tagStart == html.Length - 1)
                return false;

            startIndex = tagStart++;

            if (html[tagStart] == '/')
            {
                tagType = TagType.Close;
                tagStart++;
            }
            else
            {
                tagType = TagType.Open;
            }

            int tagEnd = tagStart;
            char startQuote = '\0';

            while (tagEnd < html.Length)
            {
                char c = html[tagEnd];
                if (c == '>')
                {
                    // End of the tag.

                    if (tagName == null)
                    {
                        tagName = html.Substring(tagStart, tagEnd - tagStart);
                    }

                    endIndex = tagEnd;
                    return true;
                }
                else if (c == '<')
                {
                    // Start of another tag - remove the previous (unclosed) tag.

                    endIndex = tagEnd - 1; // Don't remove this '<', but remove everything up to it.
                    tagName = (endIndex > tagStart ? html.Substring(tagStart, endIndex - tagStart) : "");

                    return true;
                }
                else if (c == '\'' || c == '\"')
                {
                    if (startQuote == c)
                    {
                        startQuote = '\0'; // Closing quotes.
                    }
                    else
                    {
                        startQuote = c; // Opening quotes.
                    }
                }
                else if (char.IsWhiteSpace(c))
                {
                    // End of the tag name, but not the end of the tag - it may still be closed later.

                    if (tagName == null)
                    {
                        if (tagEnd == tagStart)
                        {
                            endIndex = tagEnd;
                            return true; // This handles "< "
                        }

                        tagName = html.Substring(tagStart, tagEnd - tagStart);
                    }
                }
                else if (tagType == TagType.Open && c == '/' && startQuote == '\0'
                    && (tagEnd == html.Length - 1 || html[tagEnd + 1] == '>'))
                {
                    // If '>' (or end of string) follows then the tag was opened and closed again.

                    tagType = TagType.OpenAndClose;

                    if (tagName == null)
                    {
                        tagName = html.Substring(tagStart, tagEnd - tagStart);
                    }

                    endIndex = tagEnd;
                    if (endIndex < html.Length - 1)
                    {
                        endIndex++;
                    }

                    return true;
                }
                else if (tagName == null && c != ':' && !char.IsLetterOrDigit(c))
                {
                    // The tag name contains a punctuation character - looks like it's not an HTML tag after all,
                    // just some text starting with <, but continue searching.

                    endIndex = tagEnd;
                    return true;
                }

                tagEnd++;
            }

            if (tagName == null && tagEnd > tagStart)
            {
                tagName = html.Substring(tagStart, tagEnd - tagStart);
            }
            endIndex = tagEnd;

            return true;
        }
    }
}
