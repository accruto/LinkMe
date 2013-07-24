using System;
using System.Web.UI.WebControls;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using LinkMe.Web.Applications.Ajax;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    public partial class DiaryEntryRecord : LinkMeUserControl
    {
        private bool _allowEditing;
        private DiaryEntry _entry;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (AllowEditing)
            {
                lblTitle.Attributes["for"] = txtTitle.ClientID;

                lblStartYear.Attributes["for"] = selectStartYear.ClientID;
                lblStartMonth.Attributes["for"] = selectStartMonth.ClientID;
                lblEndYear.Attributes["for"] = selectEndYear.ClientID;
                lblEndMonth.Attributes["for"] = selectEndMonth.ClientID;
                lblDescription.Attributes["for"] = txtDescription.ClientID;
                lblTotalHours.Attributes["for"] = txtTotalHours.ClientID;

                chkCurrentEntry.Attributes["onclick"] = "HandleCurrentClick(this);";

                bool isCurrent = IsCurrent;
                chkCurrentEntryDisplay.Checked = isCurrent;
                chkCurrentEntry.Checked = isCurrent;
                selectEndMonth.Disabled = isCurrent;
                selectEndYear.Enabled = !isCurrent;

                ListItem[] yearItems = GetYearListItems();
                selectStartYear.Items.AddRange(yearItems);
                selectEndYear.Items.AddRange(yearItems);
            }
        }

        public DiaryEntry Entry
        {
            get { return _entry; }
            set { _entry = value; }
        }

        public bool AllowEditing
        {
            get { return _allowEditing; }
            set { _allowEditing = value; }
        }

        protected string Title
        {
            get { return Entry != null ? Entry.Title : string.Empty; }
        }

        protected string Start
        {
            get { return Entry != null ? GetMonthYear(Entry.StartTime) : string.Empty; }
        }

        protected string End
        {
            get { return Entry != null ? GetMonthYear(Entry.EndTime) : string.Empty; }
        }

        protected string StartMonth
        {
            get { return Entry == null ? string.Empty : GetMonth(Entry.StartTime); }
        }

        protected string StartYear
        {
            get { return Entry == null ? string.Empty : GetYear(Entry.StartTime); }
        }

        protected string EndMonth
        {
            get { return Entry == null ? string.Empty : GetMonth(Entry.EndTime); }
        }

        protected string EndYear
        {
            get { return Entry == null ? string.Empty : GetYear(Entry.EndTime); }
        }

        protected string Description
        {
            get { return Entry != null ? Entry.Description : string.Empty; }
        }

        protected double TotalHours
        {
            get { return Entry != null && Entry.TotalHours != null ? Entry.TotalHours.Value : 0; }
        }

        protected string EntryId
        {
            get { return Entry != null ? Entry.Id.ToString() :  AjaxEditorBase.NoRecordId; }
        }

        protected bool IsCurrent
        {
            get { return Entry != null && Entry.StartTime != null && Entry.EndTime == null; }
        }

        protected string GetTitleHtml()
        {
            return HtmlUtil.TextToHtml(Entry.Title);
        }

        protected string GetDescriptionHtml()
        {
            return HtmlUtil.TextToHtml(Entry.Description);
        }

        protected string GetStartDateValidationMessage()
        {
            return GetDateValidationMessage(Entry.StartTime);
        }

        protected string GetEndDateValidationMessage()
        {
            if (IsCurrent)
                return string.Empty;
            return GetDateValidationMessage(Entry.EndTime);
        }

        private static string GetMonthYear(DateTime? dt)
        {
            return dt == null ? string.Empty : dt.Value.ToString("MMM yyyy");
        }

        private static string GetMonth(DateTime? dt)
        {
            return dt == null ? string.Empty : dt.Value.ToString("MMM");
        }

        private static string GetYear(DateTime? dt)
        {
            return dt == null ? string.Empty : dt.Value.ToString("yyyy");
        }

        protected static string GetDateValidationMessage(DateTime? dt)
        {
            if (dt == null)
                return "This date is required.";

            if (dt.Value == DateTime.MinValue)
                return "This date is required.";

            return string.Empty;
        }

        private static ListItem[] GetYearListItems()
        {
            const int numYearsForResume = 20;
            var items = new ListItem[numYearsForResume + 1];
            items[0] = new ListItem("Year", "");

            var currentYear = DateTime.Now.Year;
            for (var i = 0; i < numYearsForResume; i++)
            {
                var yearCounter = (currentYear - i).ToString();
                items[i + 1] = new ListItem(yearCounter, yearCounter);
            }

            return items;
        }
    }
}
