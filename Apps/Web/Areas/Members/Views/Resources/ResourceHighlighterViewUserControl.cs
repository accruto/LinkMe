using System;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Presentation.Query.Search;
using LinkMe.Apps.Presentation.Query.Search.Resources;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Resources;
using Microsoft.Practices.Unity;

namespace LinkMe.Web.Areas.Members.Views.Resources
{
    public abstract class ResourceHighlighterViewUserControl<TModel>
        : ViewUserControl<TModel>
        where TModel : class
    {
        private IResourceHighlighter _resourceHighlighter;

        [Dependency]
        public IResourceHighlighterFactory ResourceHighlighterFactory { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _resourceHighlighter = ResourceHighlighterFactory.Create(ResourceHighlighterKind.Snippet, GetCriteria(), new HighlighterConfiguration());
        }

        protected abstract ResourceSearchCriteria GetCriteria();

        protected string SummarizeContent(string content)
        {
            content = HtmlUtil.StripHtmlTags(content);
            var summary = _resourceHighlighter.SummarizeContent(content, false);
            return string.IsNullOrEmpty(summary) ? HighlightContent(content) : summary;
        }

        protected string HighlightTitle(string title)
        {
            return _resourceHighlighter.HighlightTitle(title, false);
        }

        protected string HighlightContent(string content)
        {
            return _resourceHighlighter.HighlightContent(content, false);
        }
    }
}
