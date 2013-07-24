using System;
using System.Globalization;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Utility.Utilities;

namespace LinkMe.Apps.Services.JobAds
{
    public static class JobAdsExtensions
    {
        // These start the "irrelevant" text to be stripped from job ads.

        private static string[] IrrelevantTextStart
        {
            get
            {
                return new[]
                {
                    "<font size=\"80%\">Not the job for you?",
                    "Not the job for you?",
                    "---NOW RECRUITING---",
                    "---REVIEW TIME OVER",
                };
            }
        }

        private const string IrrelevantStyleStart = "<span class='extraJobAdText'>";
        private const string IrrelevantStyleEnd = "</span>";
        private const int MinAdLengthToKeep = 200;

        /// <summary>
        /// Removes or de-emphasizes some "irrelevant" text that can be found in some job ads.
        /// </summary>
        /// <param name="text">The text to fix.</param>
        /// <param name="returnEmptyOnError">True to return an empty string if an error occurs,
        /// false to throw an exception.</param>
        /// <param name="setStyle">True to encose the irrelevant text in the "extraJobAdText" style,
        /// false to remove the irrelevant text entirely.</param>
        /// <returns>The fixed text.</returns>
        public static string FixExtraTextAndTrim(this string text, bool returnEmptyOnError, bool setStyle)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Some job ads actually have more than one marquee - remove them all.

            while (FixExtraTextInternal(ref text, returnEmptyOnError, setStyle))
            {
            }

            // Do an extra check - no marquees must survive!

            if (text.IndexOf("<marquee") != -1)
                throw new ApplicationException("Text still contains 'marquee'.");
            if (text.IndexOf("text-decoration: blink") != -1)
                throw new ApplicationException("Text still contains 'text-decoration: blink'.");

            return text;
        }

        private static bool FixExtraTextInternal(ref string text, bool returnEmptyOnError, bool setStyle)
        {
            text = text.Trim('\'').Trim();

            // Try <marquee>.

            if (StripMarqueeHtml(ref text, returnEmptyOnError))
                return true;

            // Try "Not the job for you?", etc.

            if (FixIrrelevantPlainText(ref text, returnEmptyOnError, setStyle))
                return true;

            // Try invisble text if stripping. No point if styling - it's already invisible.

            if (!setStyle)
            {
                if (StripHiddenHtml(ref text))
                    return true;
            }

            return false;
        }

        public static string GetPhoneNumber(this string value)
        {
            // Some phone numbers were coming in the form "A or B".  Just take the first.

            if (string.IsNullOrEmpty(value))
                return null;
            var index = value.IndexOf(" or ", StringComparison.InvariantCultureIgnoreCase);
            value = index == -1 ? value : value.Substring(0, index);

            // Validate it is in fact a phone number, e.g. not something like "Please apply via the link".

            var validator = (IValidator)new PhoneNumberValidator();
            return validator.IsValid(value) ? value : null;
        }

        private static bool StripMarqueeHtml(ref string text, bool returnEmptyOnError)
        {
            var marqueeStartIndex = CultureInfo.CurrentCulture.CompareInfo.IndexOf(text, "<marquee", CompareOptions.IgnoreCase);
            if (marqueeStartIndex == -1)
                return false;

            // The marquee is usually at the end of the text and often (but not always) start with <html>.

            var htmlStartIndex = CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(text, "<html", marqueeStartIndex, CompareOptions.IgnoreCase);
            if (htmlStartIndex == -1)
                htmlStartIndex = CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(text, "<center", marqueeStartIndex, CompareOptions.IgnoreCase);

            if (htmlStartIndex == -1)
            {
                // No <html> - look for other strings that indicate the start of the irrelevant text to remove.

                foreach (var irrelevant in IrrelevantTextStart)
                {
                    var tempIndex = CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(text, irrelevant, marqueeStartIndex, CompareOptions.IgnoreCase);
                    if (tempIndex != -1 && (htmlStartIndex == -1 || tempIndex < htmlStartIndex))
                        htmlStartIndex = tempIndex;
                }

                if (htmlStartIndex == -1)
                    htmlStartIndex = marqueeStartIndex;
            }

            if (htmlStartIndex < MinAdLengthToKeep)
                return RemoveMarqueeFromStartOfText(ref text, marqueeStartIndex, htmlStartIndex, returnEmptyOnError);

            text = text.Substring(0, htmlStartIndex).Trim();
            return true;
        }

        private static bool FixIrrelevantPlainText(ref string text, bool returnEmptyOnError, bool setStyle)
        {
            var irrelevantStart = -1;
            foreach (var irrelevant in IrrelevantTextStart)
            {
                var tempIndex = CultureInfo.CurrentCulture.CompareInfo.IndexOf(text, irrelevant, CompareOptions.IgnoreCase);
                if (tempIndex != -1 && (irrelevantStart == -1 || tempIndex < irrelevantStart))
                    irrelevantStart = tempIndex;
            }

            // If the "irrelevant" text is found within the first 200 characters don't strip it,
            // so we don't risk removing the whole job ad. Otherwise assume the irrelevant text comes at
            // the end, so remove up to the end of the ad.

            if (irrelevantStart == -1)
                return false;
            if (irrelevantStart < MinAdLengthToKeep)
            {
                if (returnEmptyOnError)
                {
                    text = "";
                    return true;
                }

                throw new ApplicationException("Found irrelevant text at position " + irrelevantStart + " in the text.");
            }

            if (setStyle)
            {
                // Has the style already been set?

                var testStartIndex = irrelevantStart - IrrelevantStyleStart.Length;
                if (testStartIndex > 0 && text.Substring(testStartIndex, IrrelevantStyleStart.Length) == IrrelevantStyleStart)
                    return false;

                text = text.Insert(irrelevantStart, IrrelevantStyleStart) + IrrelevantStyleEnd;
            }
            else
            {
                text = text.Substring(0, irrelevantStart);
            }

            return true;
        }

        private static bool StripHiddenHtml(ref string text)
        {
            const string displayStyleStart = "<span style=\"display:";
            const string displayStyleEnd = "</span>";

            var startIndex = CultureInfo.CurrentCulture.CompareInfo.IndexOf(text, displayStyleStart, CompareOptions.IgnoreCase);
            if (startIndex == -1)
                return false;

            var index = startIndex + displayStyleStart.Length;
            StringUtils.SkipChars(text, true, ref index, ' ');

            if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(text, "none", index, 4, CompareOptions.IgnoreCase) == -1)
                return false;

            var endIndex = CultureInfo.CurrentCulture.CompareInfo.IndexOf(text, displayStyleEnd, index, CompareOptions.IgnoreCase);
            text = endIndex == -1
                ? text.Substring(0, startIndex)
                : text.Remove(startIndex, endIndex + displayStyleEnd.Length - startIndex);

            return true;
        }

        private static bool RemoveMarqueeFromStartOfText(ref string text, int marqueeStartIndex,
            int htmlStartIndex, bool returnEmptyOnError)
        {
            const string endMarqueeText = "</marquee>";
            const string endCenterText = "</center>";
            const string endHtmlText = "</html>";
            const string endBoldText = "</b>";

            // Find </marquee>, </html>, </center> or </b> whichever comes first.

            var endMarquee = CultureInfo.CurrentCulture.CompareInfo.IndexOf(text, endMarqueeText, marqueeStartIndex, CompareOptions.IgnoreCase);
            if (endMarquee < marqueeStartIndex)
                endMarquee = -1;

            var endHtml = FindEndOfAny(text, htmlStartIndex, endHtmlText, endCenterText, endBoldText);
            if (endHtml == -1)
            {
                if (endMarquee != -1)
                {
                    // No </html>, so at least remove up to </marquee>.
                    endHtml = endMarquee + endMarqueeText.Length;
                }
                else
                {
                    if (returnEmptyOnError)
                    {
                        text = "";
                        return true;
                    }

                    throw new ApplicationException("Failed to find the end of the marquee. Text: " + text);
                }
            }

            if (endHtml > text.Length - 100)
            {
                text = ""; // Looks like the whole thing is a marquee!
                return false;
            }

            var endText = text.Substring(endHtml).Trim();

            if (htmlStartIndex == 0)
                text = endText;
            else
                text = HtmlUtil.CloseHtmlTags(text.Substring(0, htmlStartIndex).Trim()) + Environment.NewLine + endText;

            return true;
        }

        private static int FindEndOfAny(string text, int startIndex, params string[] toFind)
        {
            foreach (var strToFind in toFind)
            {
                var index = CultureInfo.CurrentCulture.CompareInfo.IndexOf(text, strToFind, startIndex, CompareOptions.IgnoreCase);
                if (index != -1)
                    return index + strToFind.Length;
            }

            return -1;
        }
    }
}
