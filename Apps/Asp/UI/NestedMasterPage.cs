using System.Web;
using LinkMe.Apps.Asp.Content;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.UI
{
    public class NestedMasterPage
        : System.Web.UI.MasterPage, IMasterPage
    {
        protected new IMasterPage Master
        {
            get { return base.Master as IMasterPage; }
        }

        ReadOnlyUrl IMasterPage.GetClientUrl()
        {
            var masterPage = Master;
            return masterPage != null ? masterPage.GetClientUrl() : HttpContext.Current.GetClientUrl();
        }

        void IMasterPage.SetTitle(string title)
        {
            var masterPage = Master;
            if (masterPage != null)
                masterPage.SetTitle(title);
        }

        void IMasterPage.AddTitleValue(string value)
        {
            var masterPage = Master;
            if (masterPage != null)
                masterPage.AddTitleValue(value);
        }

        void IMasterPage.AddMetaTag(string name, string content)
        {
            var masterPage = Master;
            if (masterPage != null)
                masterPage.AddMetaTag(name, content);
        }

        string IMasterPage.MainContentContainerId
        {
            get
            {
                var masterPage = Master;
                return masterPage != null ? masterPage.MainContentContainerId : null;
            }
            set
            {
                var masterPage = Master;
                if (masterPage != null)
                    masterPage.MainContentContainerId = value;
            }
        }

        string IMasterPage.MainContentContainerCssClass
        {
            get
            {
                var masterPage = Master;
                return masterPage != null ? masterPage.MainContentContainerCssClass : null;
            }
            set
            {
                var masterPage = Master;
                if (masterPage != null)
                    masterPage.MainContentContainerCssClass = value;
            }
        }

        void IMasterPage.AddRssFeedReference(RssFeedReference reference)
        {
            var masterPage = Master;
            if (masterPage != null)
                masterPage.AddRssFeedReference(reference);
        }

        bool IMasterPage.UseStandardStyleSheetReferences
        {
            get
            {
                var masterPage = Master;
                return masterPage == null || masterPage.UseStandardStyleSheetReferences;
            }
            set
            {
                var masterPage = Master;
                if (masterPage != null)
                    masterPage.UseStandardStyleSheetReferences = value;
            }
        }

        void IMasterPage.AddStyleSheetReference(StyleSheetReference reference)
        {
            var masterPage = Master;
            if (masterPage != null)
                masterPage.AddStyleSheetReference(reference);
        }

        void IMasterPage.AddJavaScriptReference(JavaScriptReference reference)
        {
            var masterPage = Master;
            if (masterPage != null)
                masterPage.AddJavaScriptReference(reference);
        }

        void IMasterPage.SetFaviconReference(FaviconReference reference)
        {
            var masterPage = Master;
            if (masterPage != null)
                masterPage.SetFaviconReference(reference);
        }

        public void InsertJavaScriptReferenceBeforeAll(JavaScriptReference reference)
        {
            var masterPage = Master;
            if (masterPage != null)
                masterPage.InsertJavaScriptReferenceBeforeAll(reference);
        }
    }
}
