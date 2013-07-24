using System;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Apps.Presentation.Query.Search;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.Helper;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    public partial class EmploymentSummary : LinkMeUserControl
    {
        private const int MaxJobsToDisplay = 6;

        private readonly IResumeHighlighterFactory _highlighterFactory = Container.Current.Resolve<IResumeHighlighterFactory>();
        private IResumeHighlighter _highlighter;
        private bool _hideRecentEmployers;
        private Job _previousJob;

        public void DisplayExperienceToEmployer(Member member, Resume resume, ProfessionalView view, IResumeHighlighter highlighter)
        {
            _highlighter = highlighter ?? _highlighterFactory.Create(ResumeHighlighterKind.Null, null, new HighlighterConfiguration());
            _hideRecentEmployers = !view.CanAccess(ProfessionalVisibility.RecentEmployers);

            DisplayExperienceInternal(member, resume);
        }

        public void DisplayExperienceToSelf(Member member, Resume resume)
        {
            _highlighter = _highlighterFactory.Create(ResumeHighlighterKind.Null, null, new HighlighterConfiguration());
            _hideRecentEmployers = false;

            DisplayExperienceInternal(member, resume);
        }

        public void DisplayExperienceToOtherMember(Member member, Resume resume)
        {
            _highlighter = _highlighterFactory.Create(ResumeHighlighterKind.Null, null, new HighlighterConfiguration());
            _hideRecentEmployers = false;
            DisplayExperienceInternal(member, resume);
        }

        protected string GetJobTitleAndEmployer(object dataItem)
        {
            return SearchHelper.GetJobTitleAndEmployerHtml((Job)dataItem, _highlighter, _hideRecentEmployers,
                _previousJob);
        }

        protected string GetJobTitle(object dataItem)
        {
            return SearchHelper.GetJobTitleHtml((Job)dataItem, _highlighter);
        }

        protected string GetEmployer(object dataItem)
        {
            return SearchHelper.GetEmployerHtml((Job)dataItem, _highlighter, _hideRecentEmployers, _previousJob);
        }

        protected static string GetDateRange(object dataItem)
        {
            return ((Job)dataItem).GetDateRangeDisplayText();
        }

        private void DisplayExperienceInternal(Member member, Resume resume)
        {
            if (member == null)
                throw new ArgumentNullException("member");
            if (resume == null)
                return;

            var jobs = resume.Jobs;
            if (jobs.IsNullOrEmpty())
                return;

            phEmploymentSummaryHeading.Visible = true;

            if (jobs.Count > MaxJobsToDisplay)
                jobs = MiscUtils.GetListSubset(jobs, 0, MaxJobsToDisplay);

            _previousJob = resume.PreviousJob;

            rptExperience.DataSource = jobs;
            rptExperience.DataBind();
        }
    }
}