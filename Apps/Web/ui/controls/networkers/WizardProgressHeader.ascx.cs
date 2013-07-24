using System;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class WizardProgressHeader : LinkMeUserControl
    {
        private int step;
        private string title;
        private string[] stepTitles;

        public string[] StepTitles
        {
            get { return stepTitles; }
            set { stepTitles = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public int Step
        {
            get { return step; }
            set { step = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            rptStepList.DataSource = StepTitles;
            rptStepList.DataBind();
        }

        protected string GetSelectedClass(string stepTitle)
        {
            if (stepTitle == stepTitles[step - 1])
            {
                return " class = \"selected\"";
            }

            return string.Empty;
        }
    }
}