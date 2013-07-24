using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Xml;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility;
using LinkMe.Apps.Asp.Navigation;

namespace LinkMe.Web.Helper
{
    public static class JobBoardHelper
    {
        #region Constants

        public const string ResidencyRequiredText = "Only people with the right to work in Australia may apply.";
        public const string PreviewModeDisabledLinks = "This link is for preview only.";

        #endregion

        public static string GetCandidateStatusChangeMessage(ProfessionalView view, ApplicantStatus newStatus)
        {
            string verb;
            switch (newStatus)
            {
                case ApplicantStatus.Rejected:
                    verb = "rejected";
                    break;

                case ApplicantStatus.Shortlisted:
                    verb = "shortlisted";
                    break;

                default:
                    throw new ArgumentException("Unexpected value of newStatus: " + newStatus);
            }

            return "\"" + HtmlUtil.TextToHtml(view.GetFullNameDisplayText()) + "\" has been " + verb + ".";
        }

        public static string GetJobTypeText(JobTypes type)
        {
            if(type == JobTypes.All)
                return "Any job type";

            if(type == JobTypes.None)
                return "None";

            var strColl = new StringCollection();
			
            if((type & JobTypes.FullTime)	== JobTypes.FullTime)
                strColl.Add("Full Time");

            if((type & JobTypes.PartTime)	== JobTypes.PartTime)
                strColl.Add("Part Time");

            if((type & JobTypes.Contract)	== JobTypes.Contract)
                strColl.Add("Contract");

            if((type & JobTypes.Temp)		== JobTypes.Temp)
                strColl.Add("Temporary");

            if((type & JobTypes.JobShare)	== JobTypes.JobShare)
                strColl.Add("Job Share");

            if(strColl.Count == 0)
                throw new Exception("GetJobTypeText() receieved undefined value");

            var sb = new StringBuilder();
            for(int i=0; i<strColl.Count; i++)
            {
                if(i>0 && i<strColl.Count-1)
                    sb.Append(", ");
                else if(i == strColl.Count-1)
                {
                    if(i != 0)	// edge case
                        sb.Append(" or ");
                }
                sb.Append(strColl[i]);
            }

            return sb.ToString();
        }

        public static IEnumerable<SiteMapNode> GetBrowseIndustryJobsNodes()
        {
            const string xpath = "//*[@jobs='true' and @industry]";
            return NavigationSiteMap.RootNode.SelectNodes(xpath);
        }

        public static IEnumerable<SiteMapNode> GetBrowseLocationJobsNodes(bool areCountrySubdivisions)
        {
            var xpath = "//*[@jobs='true' and @location and @isCountrySubdivision='" + XmlConvert.ToString(areCountrySubdivisions) + "']";
            return NavigationSiteMap.RootNode.SelectNodes(xpath);
        }
    }
}