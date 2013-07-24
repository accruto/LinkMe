using System.Web.UI;

namespace LinkMe.Framework.Content.UI
{
    public class UserControl<TItem>
        : UserControl, IContentItemControl
        where TItem : ContentItem
    {
        private TItem _item;

        ContentItem IContentItemControl.Item
        {
            get { return _item; }
            set { _item = value as TItem; }
        }

        public TItem Item
        {
            get { return _item; }
        }
    }
}
