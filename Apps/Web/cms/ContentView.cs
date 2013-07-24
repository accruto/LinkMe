using System.Web.UI;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.UI;

namespace LinkMe.Web.Cms
{
    public abstract class ContentView
        : Control, INamingContainer
    {
        protected ContentItem GetParentContentItem()
        {
            // Look for a containing control that has content as indicated by the IItemContainer interface.

            var itemContainer = GetContainingControl<IContentItemControl>(Parent);
            return itemContainer != null ? itemContainer.Item : null;
        }

        private static T GetContainingControl<T>(Control parentControl) where T : class
        {
            // Either the top has been reached or it is found, or need to keep going up.

            if (parentControl == null || parentControl is T)
                return parentControl as T;
            return GetContainingControl<T>(parentControl.Parent);
        }
    }
}
