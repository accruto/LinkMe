using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Cms.ContentEditors
{
    public class HtmlContentEditor
        : ContentEditor
    {
        private readonly HtmlContentItem _item;
        private TextBox _textBox;

        internal HtmlContentEditor(HtmlContentItem item)
        {
            _item = item;
        }

        protected override void AddControls(Control container)
        {
            if (_item != null)
            {
                _textBox = new TextBox
                {
                    TextMode = TextBoxMode.MultiLine,
                    Height = new Unit("300px"),
                    Width = new Unit("450px"),
                    CssClass = "email-text-area",
                    ID = "edit_html"
                };
                container.Controls.Add(_textBox);
            }
        }

        protected override void UpdateEditor()
        {
            // Reset the text.

            if (_textBox != null)
            {
                if (_item != null && !string.IsNullOrEmpty(_item.Text))
                    _textBox.Text = _item.Text;
                else
                    _textBox.Text = string.Empty;
            }
        }


        protected override void UpdateItem()
        {
            if (_item != null)
            {
                if (_textBox != null)
                {
                    var document = new HtmlDocument();
                    document.LoadHtml(_textBox.Text);

                    ResolvePaths(document, "//img", "src");
                    ResolvePaths(document, "//link", "href");
                    ResolvePaths(document, "//a", "href");

                    var sb = new StringBuilder();
                    var writer = new StringWriter(sb);

                    document.OptionWriteEmptyNodes = true;
                    document.Save(writer);
                    _item.Text = sb.ToString();
                }
                else
                {
                    _item.Text = string.Empty;
                }
            }
        }

        private static void ResolvePaths(HtmlDocument document, string elements, string attributeName)
        {
            var nodes = document.DocumentNode.SelectNodes(elements);
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var attribute = node.Attributes[attributeName];
                    if (attribute != null)
                        ResolvePath(attribute);
                }
            }
        }

        private static void ResolvePath(HtmlAttribute attribute)
        {
            ReadOnlyApplicationUrl url;
            var rootUrl = new ApplicationUrl(false, "~/");

            try
            {
                var value = attribute.Value;

                // If the user has used the TinyMCE HTML editor directly then it may have changed '&' into '&amp;' in the URLs, e.g.
                //   /bs.nsf/filterSpectatorsc?openview&restricttocategory=Tony%20Boyd
                // will come in as
                //   /bs.nsf/filterSpectatorsc?openview&amp;restricttocategory=Tony%20Boyd
                // The logic below will then mess up the urls so convert first.

                value = HttpUtility.HtmlDecode(value);

                // Trying simply reading the value.

                url = new ReadOnlyApplicationUrl(false, value);
            }
            catch (Exception)
            {
                // This may have failed because the path is relative so try that.

                url = new ReadOnlyApplicationUrl(rootUrl, attribute.Value);
            }

            // Look for an absolute path.

            if (url.AbsoluteUri.StartsWith(rootUrl.AbsoluteUri, StringComparison.InvariantCultureIgnoreCase))
                attribute.Value = "~" + url.AppRelativePathAndQuery;
            else
                attribute.Value = url.AbsoluteUri;
        }

        protected override bool IsValid
        {
            get
            {
                // Sorts of tests could be done here and this will be fleshed out in the future.

                try
                {
                    if (string.IsNullOrEmpty(_textBox.Text.Trim()) /*&& (string.Compare(_item.Title, "Default") == 0)*/)
                    {
                        AddError("You must enter default LinkMe content.");
                        return false;
                    }

                    // For now just check that the text can be loaded.

                    if (_textBox != null)
                    {
                        var document = new HtmlDocument();
                        document.LoadHtml(_textBox.Text);
                    }

                    return true;
                }
                catch (Exception e)
                {
                    AddError("The HTML is not valid: " + e.Message);
                    return false;
                }
            }
        }
    }

    public class HtmlContentEditorFactory
        : IItemContentEditorFactory
    {
        IContentEditor IItemContentEditorFactory.CreateEditor(ContentItem item)
        {
            if (item is HtmlContentItem)
                return new HtmlContentEditor(item as HtmlContentItem);
            return null;
        }
    }
}
