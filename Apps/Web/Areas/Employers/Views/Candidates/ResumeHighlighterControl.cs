using System;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Presentation.Query.Search;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using Microsoft.Practices.Unity;

namespace LinkMe.Web.Areas.Employers.Views.Candidates
{
    public class ResumeHighlighterControl
        : ViewUserControl<ViewCandidateModel>
    {
        private IResumeHighlighter _resumeHighlighter;

        [Dependency]
        public IResumeHighlighterFactory ResumeHighlighterFactory { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _resumeHighlighter = (Model.CurrentSearch != null)
                ? ResumeHighlighterFactory.Create(ResumeHighlighterKind.Full, Model.CurrentSearch.Criteria, new HighlighterConfiguration())
                : ResumeHighlighterFactory.Create(ResumeHighlighterKind.Null, null, new HighlighterConfiguration());
        }

        protected string HighlightJobTitle(string jobTitle)
        {
            return _resumeHighlighter.HighlightJobTitle(jobTitle);
        }

        protected string HighlightDesiredJobTitle(string desiredJobTitle)
        {
            return _resumeHighlighter.HighlightDesiredJobTitle(desiredJobTitle);
        }

        protected string HighlightEmployer(string companyName)
        {
            return _resumeHighlighter.HighlightEmployer(companyName);
        }

        protected string HighlightKeywords(string text)
        {
            return _resumeHighlighter.HighlightContent(text);
        }

        protected string HighlightEducation(string text)
        {
            return _resumeHighlighter.HighlightEducation(text);
        }

    }
}
