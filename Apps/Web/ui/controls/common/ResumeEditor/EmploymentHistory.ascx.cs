using System;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    public partial class EmploymentHistory : LinkMeUserControl
    {
        private bool _allowEditing;
        private bool _haveContent;
        private IResumeHighlighter _highlighter;
        private Job _previousJob;
        private bool _hideRecentEmployers;

        public bool StartEditingOnLoad { get; set; }

        protected bool AllowEditing
        {
            get { return _allowEditing; }
        }

        protected bool HaveContent
        {
            get { return _haveContent; }
        }

        protected IResumeHighlighter Highlighter
        {
            get { return _highlighter; }
        }

        protected Job PreviousJob
        {
            get { return _previousJob; }
        }

        protected bool HideRecentEmployers
        {
            get { return _hideRecentEmployers; }
        }

        public void DisplayJobs(Resume resume, bool allowEditing, IResumeHighlighter highlighter, bool hideRecentEmployers)
        {
            if (highlighter == null)
                throw new ArgumentNullException("highlighter");

            _allowEditing = allowEditing;
            _highlighter = highlighter;
            _hideRecentEmployers = hideRecentEmployers;

            if (resume == null)
                return;

            _previousJob = resume.PreviousJob;
            _haveContent = !resume.Jobs.IsNullOrEmpty();

            repRecords.DataSource = resume.Jobs;
            repRecords.DataBind();
        }
    }
}