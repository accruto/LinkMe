using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;

namespace LinkMe.Web.Cms.ContentDisplayers
{
    public class TextContentDisplayer
        : IContentDisplayer
    {
        private readonly TextContentItem _item;

        public TextContentDisplayer(TextContentItem item)
        {
            _item = item;
        }

        void IContentDisplayer.AddControls(Control container)
        {
            if (_item != null)
            {
                if (!string.IsNullOrEmpty(_item.Text))
                {
                    var literal = new Literal {Text = _item.Text};
                    container.Controls.Add(literal);
                }
            }
        }
    }

    public class TextContentDisplayerFactory
        : IContentDisplayerFactory
    {
        IContentDisplayer IContentDisplayerFactory.CreateDisplayer(ContentItem item)
        {
            if (item is TextContentItem)
                return new TextContentDisplayer(item as TextContentItem);
            return null;
        }
    }
}
