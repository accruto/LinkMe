using LinkMe.Apps.Asp.Content;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.UI
{
    public interface IMasterPage
    {
        ReadOnlyUrl GetClientUrl();
        void SetTitle(string title);
        void AddTitleValue(string value);
        void AddMetaTag(string name, string content);

        string MainContentContainerId { get; set; }
        string MainContentContainerCssClass { get; set; }

        bool UseStandardStyleSheetReferences { get; set; }
        void AddStyleSheetReference(StyleSheetReference reference);
        void AddRssFeedReference(RssFeedReference reference);
        void AddJavaScriptReference(JavaScriptReference reference);
        void SetFaviconReference(FaviconReference reference);
        void InsertJavaScriptReferenceBeforeAll(JavaScriptReference reference);
    }
}
