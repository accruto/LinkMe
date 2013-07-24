using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Presentation.Query.Search;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Employers.Models.Search;
using Microsoft.Practices.Unity;

namespace LinkMe.Web.Areas.Employers.Views.Shared
{
    public class CandidateListViewUserControl
        : ViewUserControl<CandidateListModel>
    {
        private IResumeHighlighter _resumeHighlighter;

        [Dependency]
        public IResumeHighlighterFactory ResumeHighlighterFactory { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Model is SuggestedCandidatesListModel)
            {
                _resumeHighlighter = ResumeHighlighterFactory.Create(ResumeHighlighterKind.Snippet, Model.Criteria, new HighlighterConfiguration());
            }
            else if (Model is SearchListModel)
            {
                _resumeHighlighter = ResumeHighlighterFactory.Create(ResumeHighlighterKind.Snippet, Model.Criteria, new HighlighterConfiguration());
            }
            else
            {
                _resumeHighlighter = ResumeHighlighterFactory.Create(ResumeHighlighterKind.Null, null, new HighlighterConfiguration());
            }
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

        protected IEnumerable<KeyValuePair<string, string>> SummarizeSections(EmployerMemberView view)
        {
            return _resumeHighlighter.SummarizeSections(view);
        }
    }
}
