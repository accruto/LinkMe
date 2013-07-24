using System.Web;
using LinkMe.Apps.Asp.Content;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.UI
{
    public class EmptyMasterPage
        : System.Web.UI.MasterPage, IMasterPage
    {
        public ReadOnlyUrl GetClientUrl()
        {
            return HttpContext.Current.GetClientUrl();
        }

        public void SetTitle(string title)
        {
        }

        public void AddTitleValue(string value)
        {
        }

        public void AddMetaTag(string name, string content)
        {
        }

        public string MainContentContainerId
        {
            get { return null; }
            set { }
        }

        public string MainContentContainerCssClass
        {
            get { return null; }
            set { }
        }

        public bool UseStandardStyleSheetReferences
        {
            get { return false; }
            set { }
        }

        public void AddStyleSheetReference(StyleSheetReference reference)
        {
        }

        public void AddRssFeedReference(RssFeedReference reference)
        {
        }

        public void AddJavaScriptReference(JavaScriptReference reference)
        {
        }

        public void SetFaviconReference(FaviconReference reference)
        {
        }

        public void InsertJavaScriptReferenceBeforeAll(JavaScriptReference reference)
        {
        }
    }
}
