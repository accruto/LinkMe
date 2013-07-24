using System;
using System.Collections.Generic;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    public partial class Education : LinkMeUserControl
    {
        private bool _allowEditing;
        private bool _haveContent;
        private IResumeHighlighter _highlighter;
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

        public void DisplayContent(IList<School> schools, bool allowEditing, IResumeHighlighter highlighter)
        {
            if (highlighter == null)
                throw new ArgumentNullException("highlighter");

            _allowEditing = allowEditing;
            _highlighter = highlighter;
            _haveContent = !schools.IsNullOrEmpty();

            repEducationRecords.DataSource = schools;
            repEducationRecords.DataBind();
        }

        protected static string GetYearRegexPart()
        {
            // Allow at least 5 years into the future.
            int yearMax = DateTime.Now.Year + (5 + 1);
            return RegexUtil.RegexForRange(1900, yearMax);
        }
    }
}