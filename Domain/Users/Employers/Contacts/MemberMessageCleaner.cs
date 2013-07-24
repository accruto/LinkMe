using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace LinkMe.Domain.Users.Employers.Contacts
{
    public class MemberMessageCleaner
    {
        private static readonly Dictionary<string, string> Substitutions = new Dictionary<string, string>
        {
            {"first-name", "<%= To.FirstName %>"},
            {"last-name", "<%= To.LastName %>"},
            {"full-name", "<%= To.FullName %>"},
        };

        public string CleanSubject(string subject)
        {
           // Strip out anything resembling HTML from the subject.
           return string.IsNullOrEmpty(subject) ? string.Empty : Clean(subject, true);
        }

        public string CleanBody(string body)
        {
            // Only allow certain tags.
            return string.IsNullOrEmpty(body) ? string.Empty : Clean(body, false);
        }

        private static string Clean(string html, bool everything)
        {
            var document = new HtmlDocument { OptionWriteEmptyNodes = true };
            document.LoadHtml(html);

            var nodes = document.DocumentNode.Descendants().ToList();
            for (var index = nodes.Count - 1; index >= 0; index--)
            {
                var node = nodes[index];

                switch (node.Name)
                {
                    case "#text":
                        break;

                    case "div":
                    case "p":
                    case "b":
                    case "i":
                    case "u":
                    case "strong": 
                    case "em":
                    case "br":
                        if (everything)
                            node.Remove();
                        else
                            CleanAttributes(node);
                        break;

                    case "a":
                        if (everything)
                            node.Remove();
                        else
                            CleanAttributes(node, "href");
                        break;

                    case "span":

                        // Try to substitute first.

                        if (Substitute(document, node))
                        {
                            node.Remove();
                        }
                        else
                        {
                            if (everything)
                                node.Remove();
                            else
                                CleanAttributes(node, "href");
                        }
                        break;

                    case "img":
                        Substitute(document, node);
                        node.Remove();
                        break;

                    default:
                        // Remove everything else.

                        node.Remove();
                        break;
                }
            }

            return document.DocumentNode.WriteTo();
        }

        private static void CleanAttributes(HtmlNode node, params string[] allowedNames)
        {
            if (allowedNames.Length == 0)
                node.Attributes.RemoveAll();

            var toRemove = new List<HtmlAttribute>();
            for (var index = 0; index < node.Attributes.Count; ++index)
            {
                if (!allowedNames.Contains(node.Attributes[index].Name))
                    toRemove.Add(node.Attributes[index]);
            }

            foreach (var attribute in toRemove)
                attribute.Remove();
        }

        private static bool Substitute(HtmlDocument document, HtmlNode node)
        {
            var attributes = node.Attributes.AttributesWithName("class");
            if (attributes.Count() == 1)
            {
                string substitution;
                if (Substitutions.TryGetValue(attributes.First().Value, out substitution))
                {
                    node.ParentNode.InsertAfter(document.CreateTextNode(substitution), node);
                    return true;
                }
            }

            return false;
        }
    }
}
