using System;
using System.Web.UI.WebControls;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Web.Applications.Ajax;
using LinkMe.Web.Helper;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    public partial class EmploymentHistoryRecord : LinkMeUserControl
    {
        private IResumeHighlighter _highlighter;
        private bool _hideRecentEmployers = true; // Err on the safe side, if not set.
        private Job _previousJob;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (AllowEditing)
            {
                lblJobTitle.Attributes["for"] = txtJobTitle.ClientID;
                lblCompanyName.Attributes["for"] = txtCompanyName.ClientID;

                lblStartYear.Attributes["for"] = selectStartYear.ClientID;
                lblStartMonth.Attributes["for"] = selectStartMonth.ClientID;
                lblEndYear.Attributes["for"] = selectEndYear.ClientID;
                lblEndMonth.Attributes["for"] = selectEndMonth.ClientID;
                lblDescription.Attributes["for"] = txtDescription.ClientID;

                chkCurrentJob.Attributes["onclick"] = "HandleCurrentClick(this);";

                bool isCurrent = JobIsCurrent;
                chkCurrentJobDisplay.Checked = isCurrent;
                chkCurrentJob.Checked = isCurrent;
                selectEndMonth.Disabled = isCurrent;
                selectEndYear.Enabled = !isCurrent;

                ListItem[] yearItems = GetYearListItems();
                selectStartYear.Items.AddRange(yearItems);
                selectEndYear.Items.AddRange(yearItems);
            }
        }

        public Job Job { get; set; }
        public bool AllowEditing { get; set; }

        public IResumeHighlighter Highlighter
        {
            get { return _highlighter; }
            set { _highlighter = value; }
        }

        public bool HideRecentEmployers
        {
            get { return _hideRecentEmployers; }
            set { _hideRecentEmployers = value; }
        }

        public Job PreviousJob
        {
            get { return _previousJob; }
            set { _previousJob = value; }
        }

        protected string JobTitle
        {
            get { return Job != null ? Job.Title : string.Empty; }
        }

        protected string JobEmployer
        {
            get { return Job != null ? Job.Company : string.Empty; }
        }

        protected string JobStart
        {
            get { return Job != null ? Job.GetStartDisplayText() : string.Empty; }
        }

        protected string JobEnd
        {
            get { return Job != null ? Job.GetEndDisplayText() : string.Empty; }
        }

        protected string JobStartMonth
        {
            get { return Job != null ? Job.GetStartMonthDisplayText() : string.Empty; }
        }

        protected string JobStartYear
        {
            get { return Job != null ? Job.GetStartYearDisplayText() : string.Empty; }
        }

        protected string JobEndMonth
        {
            get { return Job != null ? Job.GetEndMonthDisplayText() : string.Empty; }
        }

        protected string JobEndYear
        {
            get { return Job != null ? Job.GetEndYearDisplayText() : string.Empty; }
        }

        protected string JobDescription
        {
            get { return Job != null ? Job.Description : string.Empty; }
        }

        protected string JobId
        {
            get { return Job != null ? Job.Id.ToString() : AjaxEditorBase.NoRecordId; }
        }

        protected bool JobIsCurrent
        {
            get { return Job != null && Job.IsCurrent; }
        }

        protected string GetJobDatesText()
        {
            return (Job == null ? "" : Job.GetDateRangeDisplayText());
        }

        protected string GetJobTitleHtml()
        {
            return SearchHelper.GetJobTitleHtml(Job, _highlighter);
        }

        protected string GetCompanyNameHtml()
        {
            return SearchHelper.GetEmployerHtml(Job, _highlighter, _hideRecentEmployers, _previousJob);
        }

        protected string GetJobTitleAndCompanyNameHtml()
        {
            return SearchHelper.GetJobTitleAndEmployerHtml(Job, _highlighter, _hideRecentEmployers, _previousJob);
        }

        protected string GetJobDescriptionHtml()
        {
            return _highlighter.HighlightContent(Job.Description);
        }

        private static ListItem[] GetYearListItems()
        {
            const int numYearsForResume = 55;

            var items = new ListItem[numYearsForResume + 1];
            items[0] = new ListItem("Year", "");

            int currentYear = DateTime.Now.Year;

            for (int i = 0; i < numYearsForResume; i++)
            {
                string yearCounter = (currentYear - i).ToString();
                items[i + 1] = new ListItem(yearCounter, yearCounter);
            }

            return items;
        }
    }
}
