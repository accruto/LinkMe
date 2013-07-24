using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace LinkMe.Apps.Agents.Communications.Emails
{
    public static class EmailsExtensions
    {
        public static string ConvertHtmlToPlainText(this string html)
        {
            var document = new HtmlDocument { OptionWriteEmptyNodes = true };
            document.LoadHtml(html);

            var sb = new StringBuilder();
            ConvertHtmlToPlainText(sb, document.DocumentNode.ChildNodes);
            return sb.ToString();
        }

        private static void ConvertHtmlToPlainText(StringBuilder sb, HtmlNode node)
        {
            switch (node.Name)
            {
                case "#text":
                    sb.Append(node.InnerText.Trim().Replace(" &nbsp;", " ").Replace("&nbsp; ", " ").Replace("&nbsp;", " "));
                    break;

                case "div":
                case "p":
                    sb.AppendLine();
                    sb.AppendLine();
                    ConvertHtmlToPlainText(sb, node.ChildNodes);
                    break;

                case "br":
                    sb.AppendLine();
                    ConvertHtmlToPlainText(sb, node.ChildNodes);
                    break;

                case "span":
                case "b":
                case "i":
                case "u":
                case "strong":
                case "em":
                case "a":
                    ConvertHtmlToPlainText(sb, node.ChildNodes);
                    break;

                case "img":
        //            Substitute(document, node);
                    break;
            }
        }

        private static void ConvertHtmlToPlainText(StringBuilder sb, IEnumerable<HtmlNode> nodes)
        {
            foreach (var node in nodes)
                ConvertHtmlToPlainText(sb, node);
        }
    }
}
