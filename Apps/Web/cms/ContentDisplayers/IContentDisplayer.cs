using System.Web.UI;
using LinkMe.Framework.Content;

namespace LinkMe.Web.Cms.ContentDisplayers
{
    public interface IContentDisplayer
    {
        void AddControls(Control container);
    }

    public interface IContentDisplayerFactory
    {
        IContentDisplayer CreateDisplayer(ContentItem item);
    }
}
