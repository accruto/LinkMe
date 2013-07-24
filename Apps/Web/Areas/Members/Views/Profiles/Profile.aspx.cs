using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Web.Areas.Members.Models.Profiles;

namespace LinkMe.Web.Areas.Members.Views.Profiles
{
    public class ProfileTabDetails
    {
        public string TabName { get; set; }
        public string TabDesc { get; set; }
        public string TabUrl { get; set; }
    }

    public class Profile
        : ViewPage<MemberModel>
    {
        private IList<ProfileTabDetails> Tabs { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Tabs = new List<ProfileTabDetails>
                       {
                           new ProfileTabDetails {TabName = "About me", TabDesc = "Contact details", TabUrl = "contactdetails"},
                           new ProfileTabDetails {TabName = "Desired job", TabDesc = "Job title, job type, salary", TabUrl = "desiredjob"},
                           new ProfileTabDetails {TabName = "Career objectives", TabDesc = "Objectives, summary, skills", TabUrl = "careerobjectives"},
                           new ProfileTabDetails {TabName = "Employment history", TabDesc = "Past and current jobs", TabUrl = "employmenthistory"},
                           new ProfileTabDetails {TabName = "Education/Qualification", TabDesc = "Degrees, qualification", TabUrl = "education"},
                           new ProfileTabDetails {TabName = "Other information", TabDesc = "Certifications and interests", TabUrl = "other"},
                       };
        }

        protected ProfileTabDetails GetTabs(int index)
        {
            return Tabs[index];
        }

        protected string SuccInfoType
        {
            get { return ViewData.GetClientUrl().QueryString["succInfoType"]; }
        }
    }
}