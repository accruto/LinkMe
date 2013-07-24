using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Web.Applications.Ajax;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    public partial class EducationRecord
        : LinkMeUserControl
    {
        private IResumeHighlighter _highlighter;

        public School School { get; set; }
        public bool AllowEditing { get; set; }

        public IResumeHighlighter Highlighter
        {
            get { return _highlighter; }
            set { _highlighter = value; }
        }

        protected string Id
        {
            get { return School == null ? AjaxEditorBase.NoRecordId : School.Id.ToString(); }
        }

        protected string Institution
        {
            get { return School == null ? string.Empty : ToHtml(School.Institution); }
        }

        protected string City
        {
            get { return School == null ? string.Empty : ToHtml(School.City); }
        }

        protected string Country
        {
            get { return School == null ? string.Empty : ToHtml(School.Country); }
        }

        protected string Qualification
        {
            get { return School == null ? string.Empty : ToHtml(School.Degree); }
        }

        protected string Major
        {
            get { return School == null ? string.Empty : ToHtml(School.Major); }
        }

        protected string Completed
        {
            get { return School == null ? string.Empty : ToHtml(School.GetCompletionDateDisplayText()); }
        }

        protected string Description
        {
            get { return School == null ? string.Empty : ToHtml(School.Description); }
        }

        protected static string HideIf(bool hide)
        {
            return "display: " + (hide ? "none;" : "block;");
        }

        private string ToHtml(string text)
        {
            if (text == null)
                return null;
            // This should work even in edit mode, because highlighter.AddHighlighting() will just return
            // the original text in that case.
            return _highlighter.HighlightContent(text);
        }
    }
}