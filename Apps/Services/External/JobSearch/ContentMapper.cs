﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
﻿using HtmlAgilityPack;
﻿using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Services.External.JobSearch
{
    public class ContentMapper
    {
        private readonly int _maxTitleLength;
        private readonly int _maxDescriptionLength;

        public ContentMapper(int maxTitleLength, int maxDescriptionLength)
        {
            _maxDescriptionLength = maxDescriptionLength;
            _maxTitleLength = maxTitleLength;
        }

        public string MapTitle(string title)
        {
            return title.TruncateAtWord(_maxTitleLength);
        }

        public string MapBody(Guid jobAdId, IList<string> bulletPoints, string contentHtml)
        {
            // Job ad link.

            var applyLink = new TagBuilder("a") { InnerHtml = "Please click here to apply." };
            var applyUrl = new ReadOnlyApplicationUrl("~/jobs/" + jobAdId.ToString());
            applyLink.MergeAttribute("href", applyUrl.ToString());

            var applyLinkBold = new TagBuilder("b") { InnerHtml = applyLink.ToString() };
            var applyHtmlBottom = "<br/><br/>" + applyLinkBold;

            // Map bullet points.

            var bulletPointsHtml = string.Empty;

            if (bulletPoints != null && bulletPoints.Count != 0)
            {
                var listItems = bulletPoints.Aggregate(new StringBuilder(), (agg, bulletPoint) =>
                {
                    // Make sure that is looks good on Search Results page
                    var lastChar = bulletPoint[bulletPoint.Length - 1];
                    if (lastChar != '.' && lastChar != ',' && lastChar != ';' && lastChar != '!')
                        bulletPoint += ". ";
                    else
                        bulletPoint += " ";

                    var listItem = new TagBuilder("li");
                    listItem.SetInnerText(bulletPoint);
                    return agg.Append(listItem.ToString());
                });

                var list = new TagBuilder("ul") { InnerHtml = listItems.ToString() };
                bulletPointsHtml = list.ToString();
            }

            // Map content.

            var contentText = HtmlUtil.StripHtmlTags(contentHtml, "p", "li");

            // Remove any attributes.

            contentText = RemoveAttributes(contentText);

            // Process some things manually.

            contentText = contentText.Replace("<p>", "");
            contentText = contentText.Replace("</p>", "\n\n");
            contentText = contentText.Replace("<li>", "");
            contentText = contentText.Replace("</li>", "\n\n");
            if (contentText.EndsWith("\n\n"))
                contentText = contentText.Substring(0, contentText.Length - 2);

            contentText = contentText.Replace('\u0095', '\u2022'); // replace Windows Western bullet with Unicode bullet
            contentText = contentText.Replace("&nbsp;", new string('\u00A0', 1));
            contentText = contentText.Replace("&amp;", "&");
            contentText = contentText.Replace("&quot;", "\"");

            var contentSimpleHtml = BuildContentSimpleHtml(contentText, _maxDescriptionLength - (bulletPointsHtml.Length + applyHtmlBottom.Length));
            return bulletPointsHtml + contentSimpleHtml + applyHtmlBottom;
        }

        private string RemoveAttributes(string contentText)
        {
            var document = new HtmlDocument();
            document.LoadHtml(contentText);

            RemoveAttributes(document.DocumentNode);
            return document.DocumentNode.InnerHtml;
        }

        private void RemoveAttributes(HtmlNode node)
        {
            if (node.Attributes.Count > 0)
                node.Attributes.RemoveAll();
            foreach (var childNode in node.ChildNodes)
                RemoveAttributes(childNode);
        }

        private static string BuildContentSimpleHtml(string contentText, int maxLength)
        {
            if (maxLength <= 0)
                return string.Empty;

            var contentSimpleHtml = TextToHtml(contentText);
            if (contentSimpleHtml.Length <= maxLength)
                return contentSimpleHtml;

            contentSimpleHtml = TextToHtml(contentText.TruncateAtWord(maxLength));
            var length = maxLength;
            while (contentSimpleHtml.Length > maxLength)
            {
                length--;
                contentSimpleHtml = TextToHtml(contentText.TruncateAtWord(length));
            }

            return contentSimpleHtml;
        }

        private static string TextToHtml(string text)
        {
            var encodedText = HttpUtility.HtmlEncode(text);
            var html = HtmlUtil.LineBreaksToHtml(encodedText);

            html = html.Replace("<br />", "<br/>");

            var length = 0;
            do
            {
                length = html.Length;
                html = html.Replace("<br/><br/> ", "<br/><br/>");
            } while (html.Length != length);

            do
            {
                length = html.Length;
                html = html.Replace(" <br/><br/>", "<br/><br/>");
            } while (html.Length != length);

            return html;
        }
    }
}
