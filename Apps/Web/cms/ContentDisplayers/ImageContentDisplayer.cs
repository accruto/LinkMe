using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Cms.ContentDisplayers
{
    public class ImageContentDisplayer
        : IContentDisplayer
    {
        private readonly ImageContentItem _item;

        public ImageContentDisplayer(ImageContentItem item)
        {
            _item = item;
        }

        void IContentDisplayer.AddControls(Control container)
        {
            if (_item != null)
            {
                if (!string.IsNullOrEmpty(_item.RootFolder) && !string.IsNullOrEmpty(_item.RelativePath))
                {
                    var image = new Image();
                    var rootFolder = _item.RootFolder;
                    if (!rootFolder.EndsWith("/"))
                        rootFolder += "/";
                    image.ImageUrl = new ApplicationUrl(new ApplicationUrl(rootFolder), _item.RelativePath).ToString();
                    container.Controls.Add(image);
                }
            }
        }
    }

    public class ImageContentDisplayerFactory
        : IContentDisplayerFactory
    {
        IContentDisplayer IContentDisplayerFactory.CreateDisplayer(ContentItem item)
        {
            if (item is ImageContentItem)
                return new ImageContentDisplayer(item as ImageContentItem);
            return null;
        }
    }
}
