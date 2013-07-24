using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Cms.ContentDisplayers
{
    public class HtmlContentDisplayer
        : IContentDisplayer
    {
        private static readonly ReadOnlyApplicationUrl ApplicationUrl = new ReadOnlyApplicationUrl("~/");
        private readonly HtmlContentItem _item;

        public HtmlContentDisplayer(HtmlContentItem item)
        {
            _item = item;
        }

        void IContentDisplayer.AddControls(Control container)
        {
            if (_item != null)
            {
                string html = _item.Text;

                try
                {
                    // Need to fix up all paths that may be included in the html.

                    if (!string.IsNullOrEmpty(html))
                    {
                        // Replace application paths.

                        var applicationPath = ApplicationUrl.Path;
                        html = Fix(html, "src=\"~/", "src=\"" + applicationPath);
                        html = Fix(html, "href=\"~/", "href=\"" + applicationPath);
                        html = Fix(html, "url(~/", "url(" + applicationPath);
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidContentItemException(string.Format(
                        "The HTML for CMS item {0} ('{1}') is not valid.", _item.Id, _item.Name), e);
                }

                var literal = new Literal {Text = html};
                container.Controls.Add(literal);
            }
        }

        private static string Fix(string html, string currentPath, string newPath)
        {
            return html.Replace(currentPath, newPath);
        }
    }

    public class HtmlContentDisplayerFactory
        : IContentDisplayerFactory
    {
        IContentDisplayer IContentDisplayerFactory.CreateDisplayer(ContentItem item)
        {
            if (item is HtmlContentItem)
                return new HtmlContentDisplayer(item as HtmlContentItem);
            return null;
        }
    }
}
