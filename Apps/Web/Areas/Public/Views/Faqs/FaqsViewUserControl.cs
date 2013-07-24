using System;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Presentation.Query.Search;
using LinkMe.Apps.Presentation.Query.Search.Resources;
using LinkMe.Framework.Utility;
using LinkMe.Web.Areas.Public.Models.Faqs;
using Microsoft.Practices.Unity;

namespace LinkMe.Web.Areas.Public.Views.Faqs
{
    public class FaqsViewUserControl
        : ViewUserControl<FaqListModel>
    {
        private IResourceHighlighter _faqHighlighter;

        [Dependency]
        public IFaqHighlighterFactory FaqHighlighterFactory { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _faqHighlighter = FaqHighlighterFactory.Create(ResourceHighlighterKind.Snippet, Model.Criteria, new HighlighterConfiguration { FragmentSize = 100 });
        }

        protected string SummarizeContent(string content)
        {
            content = HtmlUtil.StripHtmlTags(content);
            var summary = _faqHighlighter.SummarizeContent(content, false);
            return string.IsNullOrEmpty(summary) ? HighlightContent(content) : summary;
        }

        protected string HighlightTitle(string title)
        {
            return _faqHighlighter.HighlightTitle(title, false);
        }

        protected string HighlightContent(string content)
        {
            return _faqHighlighter.HighlightContent(content, false);
        }
    }
}
