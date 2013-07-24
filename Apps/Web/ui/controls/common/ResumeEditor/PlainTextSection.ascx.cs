using System;
using System.Web.UI;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Framework.Utility;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    [ParseChildren(false)]
    public partial class PlainTextSection : LinkMeUserControl
    {
        private string sectionName;
        private string sectionDisplayName;
        private string noContentText;
        private string content;
        private bool allowEditing = false;
        private IResumeHighlighter highlighter;
        private bool startEditingOnLoad = false;

        public bool StartEditingOnLoad
        {
            get { return startEditingOnLoad; }
            set { startEditingOnLoad = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Resume);
            AddStyleSheetReference(StyleSheets.ResumeEditor);
        }

        /// <summary>
        /// The section name must be unique within the page and must not conflict with any other control names,
        /// eg. there should be no other txt<SectionName> or lbl<SectionName> controls.
        /// </summary>
        public string SectionName
        {
            get { return sectionName; }
            set { sectionName = value; }
        }

        /// <summary>
        /// This is the value displayed to the user as a section heading.
        /// </summary>
        public string SectionDisplayName
        {
            get { return sectionDisplayName ?? sectionName; }
            set { sectionDisplayName = value; }
        }

        protected string NoContentText
        {
            get { return noContentText; }
        }

        protected string Content
        {
            get { return content; }
        }

        protected bool HaveContent
        {
            get { return !string.IsNullOrEmpty(Content); }
        }

        protected bool AllowEditing
        {
            get { return allowEditing; }
        }

        public void DisplayContent(string content, bool allowEditing, IResumeHighlighter highlighter)
        {
            if (highlighter == null)
                throw new ArgumentNullException("highlighter");

            this.content = content;
            this.allowEditing = allowEditing;
            this.highlighter = highlighter;
        }

        protected override void AddParsedSubObject(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            LiteralControl literal = obj as LiteralControl;
            if (literal == null)
                throw new ArgumentException("Only literal content is allowed inside a plain text section.");

            noContentText = literal.Text;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (string.IsNullOrEmpty(sectionName))
                throw new ApplicationException("The section name has not been set for " + ID);
            if (allowEditing && string.IsNullOrEmpty(noContentText))
            {
                throw new ApplicationException("The text to display when there is no content must be specified"
                    + " inside the body of the <PlainTextSection> element.");
            }
        }

        protected string GetContentForDisplay()
        {
            return highlighter.HighlightContent(Content);
        }
    }
}