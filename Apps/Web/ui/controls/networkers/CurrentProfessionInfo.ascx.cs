using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class CurrentProfessionInfo : LinkMeUserControl
    {
        public bool ShowCurrentJobAndEmployer
        {
            set { phCurrentJobAndEmployer.Visible = value; }
            get { return phCurrentJobAndEmployer.Visible; }
        }

        public bool CurrentJobPopulated
        {
            get { return !string.IsNullOrEmpty(txtCurrentJob.Text) || !string.IsNullOrEmpty(txtCurrentEmployer.Text); }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ucCurrentIndustry.DisplayAllIndustries();
        }

        public void Display(Candidate candidate)
        {
            ucCurrentIndustry.SelectedIndustries = candidate.Industries;
        }

        public void Update(Candidate candidate)
        {
            candidate.Industries = ucCurrentIndustry.SelectedIndustries;
        }

        public void Update(Resume resume)
        {
            if (ShowCurrentJobAndEmployer && (!string.IsNullOrEmpty(txtCurrentJob.Text) || !string.IsNullOrEmpty(txtCurrentEmployer.Text)))
            {
                resume.Jobs = new List<Job>
                {
                    new Job
                    {
                        Dates = null,
                        Title = txtCurrentJob.Text,
                        Company = txtCurrentEmployer.Text,
                    }
                };
            }
        }
    }
}