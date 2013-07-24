using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI.WebControls;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Utilities;
using LinkMe.Web.Applications.Facade;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Content;
using LinkMe.Web.Helper;
using LinkMe.Web.UI.Controls.Common;
using LinkMe.Apps.Asp.Navigation;
using Application=LinkMe.Domain.Roles.Contenders.Application;

namespace LinkMe.Web.UI.Registered.Networkers
{
	public partial class PreviousApplications : LinkMePage
	{
        private readonly IMemberJobAdViewsQuery _memberJobAdViewsQuery = Container.Current.Resolve<IMemberJobAdViewsQuery>();
        private readonly IEmployersQuery _employersQuery = Container.Current.Resolve<IEmployersQuery>();
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery = Container.Current.Resolve<IJobAdApplicantsQuery>();
        private readonly IMemberApplicationsQuery _memberApplicationsQuery = Container.Current.Resolve<IMemberApplicationsQuery>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Container.Current.Resolve<IJobAdApplicationSubmissionsCommand>();

        private IList<Application> _applications;
	    private IDictionary<Guid, MemberJobAdView> _jobAds;

	    #region Page Controls

        protected PlaceHolder phApplications;
		protected PagingBar ucPagingBar;
		
		#endregion

	    protected override UserType[] AuthorizedUserTypes
	    {
            get { return new[] { UserType.Member }; }
	    }

	    protected override bool GetRequiresActivation()
        {
            return true;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }

        private IList<Application> SubmittedJobApplications
		{
			get
			{
                if (_applications == null)
                {
                    var applicantId = LoggedInMember.Id;
                    _applications = _memberApplicationsQuery.GetApplications(applicantId).OrderByDescending(a => a.CreatedTime).ToList();
                }
			    return _applications;
			}
		}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Jobs);

            ucPagingBar.ResultsPerPage = ApplicationContext.Instance.GetIntProperty(
                ApplicationContext.JOB_APPLICATIONS_PER_PAGE);
            ucPagingBar.StartIndexParam = SearchHelper.RESULT_INDEX_PARAM;

            InitialiseApplications();
		}

	    private void InitialiseApplications()
	    {
            ucPagingBar.InitPagesList(null, SubmittedJobApplications.Count, false);
            
            // init repeater according to paging's current page

            var appsSubset = ucPagingBar.GetResultSubset(SubmittedJobApplications);

            if (appsSubset.IsNullOrEmpty())
            {
                phNoApplications.Visible = true;
                phApplications.Visible = false;
                return;
            }

            _jobAds = _memberJobAdViewsQuery.GetMemberJobAdViews(LoggedInMember, appsSubset.Select(a => a.PositionId).Distinct()).ToDictionary(j => j.Id, j => j);

            rptApplications.DataSource = appsSubset;
            rptApplications.DataBind();
            phApplications.Visible = true;
		}

        private void ReInitialiseApplications()
        {
            _applications = null;
            InitialiseApplications();
        }

		protected void rptApplications_ItemCreated(object obj, RepeaterItemEventArgs e)
		{
            var application = (Application)e.Item.DataItem;
			if(application == null)
				return;

            var jobAd = _jobAds[application.PositionId];

			var lit = (Literal)e.Item.FindControl("jobAdTitleLiteral");
			if(lit != null)
			{
			    lit.Text = string.Format("{0}", jobAd.Title);
			    if (jobAd.Description.Salary != null)
			    {
			        var displayText = jobAd.Description.Salary.GetDisplayText();
                    if (!string.IsNullOrEmpty(displayText))
                        lit.Text += string.Format(", {0}", displayText);
			    }
			}

            lit = (Literal)e.Item.FindControl("postedDateLiteral");
            if (lit != null)
            {
                lit.Text = string.Format("Posted {0} ({1})",
                    jobAd.CreatedTime.ToString("dd MMMM yyyy"),
                    jobAd.GetPostedDisplayText());
            }
		}
		
		protected void rptApplications_ItemCommand(object obj, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName) 
			{ 
				case "deleteCmd":
                    var jobAppId = new Guid((string)e.CommandArgument);

                    var jobApplication = _memberApplicationsQuery.GetInternalApplication(jobAppId);
                    if (jobApplication == null)
                        throw new ApplicationException("Failed to find job application with ID " + jobAppId);

                    var jobAd = _jobAds[jobApplication.PositionId];
                    var jobTitle = jobAd.Title;

                    _jobAdApplicationSubmissionsCommand.RevokeApplication(jobAd, LoggedInMember.Id);

					AddConfirm("Your application for \"" + jobTitle + "\" was deleted successfully.");

					ReInitialiseApplications();
					break;

				default: throw new Exception("rptApplications_ItemCommand was called with undefined command name!");
			}

            var redirectUrl = ucPagingBar.GetRedirectUrlAfterRemovingItem(e.Item.ItemIndex);
            if (redirectUrl == null)
            {
                // Refresh the list of job applications now that one has been removed.

                InitialiseApplications();
            }
            else
            {
                NavigationManager.Redirect(redirectUrl);
            }
		}

	    protected string GetJobLocality(object dataItem)
		{
			if (dataItem == null)
				return "";

            var jobAd = _jobAds[((Application)dataItem).PositionId];
            return jobAd.GetLocationDisplayText();
		}

        protected string GetStatusText(Application application)
		{
            if (application == null)
                return string.Empty;

            if (application is ExternalApplication)
                return "Managed externally";

            var internalApplication = (InternalApplication) application;
            var status = _jobAdApplicantsQuery.GetApplicantStatus(application.Id);

            switch (status)
            {
                case ApplicantStatus.New:

                    var jobAd = _jobAds[application.PositionId];
                    if (jobAd.Processing != JobAdProcessing.ManagedInternally)
                    {
                        if (internalApplication.IsPending && !string.IsNullOrEmpty(jobAd.Integration.ExternalApplyUrl))
                        {
                            var applyLink = GetPopupATag(jobAd.GetExternalApplyUrl(internalApplication.Id), JobAdFacade.GetExternalApplyPopupWindowName(jobAd), "Complete application");
                            return "Pending (external site)" + StringUtils.HTML_LINE_BREAK + applyLink;
                        }
                        return "Managed externally";
                    }

                    return "New";

                case ApplicantStatus.Shortlisted:
                    return "Shortlisted";
                case ApplicantStatus.Rejected:
                    return "Declined";
                default:
                    return "Managed externally";
            }
		}

        protected ReadOnlyUrl GetJobAdUrl(object dataItem)
		{
			if (dataItem == null)
				return null;

            var jobAd = _jobAds[((Application)dataItem).PositionId];
            return jobAd.GenerateJobAdUrl();
		}

		protected string GetContactDetailsText(object dataItem)
		{
			if (dataItem == null)
				return "";

            var jobApp = (Application)dataItem;
            var jobAd = _jobAds[jobApp.PositionId];
			Debug.Assert(jobAd != null, "jobAd != null");

            var employer = _employersQuery.GetEmployer(jobAd.PosterId);
            var text = "From " + employer.Organisation.FullName;

            if (jobAd.ContactDetails != null
                && jobApp is InternalApplication
                && jobAd.Processing == JobAdProcessing.ManagedInternally
                && !string.IsNullOrEmpty(jobAd.ContactDetails.EmailAddress))
            {
                text += " (<a href=\"mailto:" + jobAd.ContactDetails.EmailAddress + "\">email</a>)";
			}

			return text;
		}

        protected bool EnableDelete(Application application)
	    {
            if (application is InternalApplication)
            {
                var jobAd = _jobAds[application.PositionId];
                return jobAd.Processing == JobAdProcessing.ManagedInternally;
            }
            return false;
	    }
	}
}
