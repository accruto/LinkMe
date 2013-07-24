using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LinkMe.Apps.Asp.Notifications;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Framework.Utility;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Applications.Facade;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Helper;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Registered.Employers
{
	public partial class EmployerJobAdsControl
        : LinkMeUserControl
	{
        private readonly IEmployerJobAdsCommand _employerJobAdsCommand = Container.Current.Resolve<IEmployerJobAdsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Container.Current.Resolve<IJobAdsQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Container.Current.Resolve<IJobAdsCommand>();
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery = Container.Current.Resolve<IJobAdApplicantsQuery>();
        private readonly IEmployerCreditsQuery _employerCreditsQuery = Container.Current.Resolve<IEmployerCreditsQuery>();
        public const string QryStrSwitchedMode = "mode";
	    
        protected const string DeleteDraftCommand = "DeleteDraft";
        protected const string OpenAdCommand = "OpenAd";
        protected const string CloseAdCommand = "CloseAd";

        private IList<Guid> _currentJobAdsIds;
        private IDictionary<Guid, IDictionary<ApplicantStatus, int>> _counts;

        public bool ViewOnly { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ucPagingBar.ResultsPerPage = ApplicationContext.Instance.GetIntProperty(ApplicationContext.JOB_ADS_PER_PAGE);
            ucPagingBar.StartIndexParam = SearchHelper.RESULT_INDEX_PARAM;

			InitialiseJobAds(LoggedInEmployer);
		}

	    private void InitialiseJobAds(IEmployer employer)
		{
			InitModeRelatedControls();
            ucPagingBar.InitPagesList(GetResultUrl(), _currentJobAdsIds.Count, false);
			InitJobAdsRepeater(employer);
		}		
		
		private void InitModeRelatedControls()
		{
			var currentMode = GetCurrentMode();
            _currentJobAdsIds = _jobAdsQuery.GetJobAdIds(LoggedInUserId.Value, currentMode);
		}

		public JobAdStatus GetCurrentMode()
		{
			var currentMode = Request.QueryString[QryStrSwitchedMode];
			if (currentMode == null || currentMode == JobAdStatus.Open.ToString()) // open mode is the default
				return JobAdStatus.Open;
            return (JobAdStatus)Enum.Parse(typeof(JobAdStatus), currentMode);
		}

        private ReadOnlyUrl GetCandidatesUrl(Employer employer, JobAd ad, ApplicantStatus status)
        {
            return HaveCandidates(employer, ad) ? GetApplicantsUrl(ad, status) : new ReadOnlyUrl(GetSuggestionsUrl(ad).ToString());
        }

	    private static ReadOnlyUrl GetApplicantsUrl(IJobAd ad, ApplicantStatus status)
	    {
	        return JobAdsRoutes.ManageJobAdCandidates.GenerateUrl(new { jobAdId = ad.Id, status });
	    }

	    private static ReadOnlyUrl GetSuggestionsUrl(IJobAd ad)
        {
            return JobAdsRoutes.SuggestedCandidates.GenerateUrl(new {jobAdId = ad.Id});
        }

        protected string GetSuggestionsActionItem(JobAd ad)
        {
            var hypertext = new TagBuilder("a");
            hypertext.AddCssClass("execute-job-ad-action");
            hypertext.MergeAttribute("href", GetSuggestionsUrl(ad).ToString());
            hypertext.MergeAttribute("title", "See suggested candidates for this job ad");
            hypertext.SetInnerText("View suggested candidates");
            return string.Concat("<li>", hypertext.ToString(), "</li>");
        }

        protected string GetApplicantsActionItem(JobAd ad)
        {
            if (!HaveCandidates(LoggedInEmployer, ad))
                return string.Empty;

            var hypertext = new TagBuilder("a");
            hypertext.AddCssClass("execute-job-ad-action");
            var status = ApplicantStatus.New;
            if (GetCountsForJobAd(LoggedInEmployer, ad)[ApplicantStatus.New] == 0)
            {
                if (GetCountsForJobAd(LoggedInEmployer, ad)[ApplicantStatus.Shortlisted] == 0)
                    status = GetCountsForJobAd(LoggedInEmployer, ad)[ApplicantStatus.Rejected] == 0 ? ApplicantStatus.New : ApplicantStatus.Rejected;
                else
                    status = ApplicantStatus.Shortlisted;
            }

            hypertext.MergeAttribute("href", GetApplicantsUrl(ad, status).ToString());
            hypertext.MergeAttribute("title", "See the applicants for this job ad");
            hypertext.SetInnerText("Manage applicants");
            return string.Concat("<li>", hypertext.ToString(), "</li>");
        }

        private bool HaveCandidates(Employer employer, JobAd ad)
        {
            return GetCountsForJobAd(employer, ad).Values.Sum() > 0;
        }

        protected string GetCandidatesLink(ApplicantStatus status, JobAd ad)
		{
            var counts = GetCountsForJobAd(LoggedInEmployer, ad);

            return counts[status] == 0
                ? status.ToString()
                : string.Format("<a href=\"{0}\">{1}</a>", GetCandidatesUrl(LoggedInEmployer, ad, status), status);

		}

        protected string GetShortlistedNumber(JobAd ad)
		{
			var counts = GetCountsForJobAd(LoggedInEmployer, ad);
            // Include manually added candidates with shortlisted ones, since that's how they're displayed
            // on JobAdCandidates.aspx.
            return (counts[ApplicantStatus.Shortlisted]).ToString(CultureInfo.InvariantCulture);
		}

        protected string GetNewCandidatesNumber(JobAd ad)
		{
            return GetCountsForJobAd(LoggedInEmployer, ad)[ApplicantStatus.New].ToString(CultureInfo.InvariantCulture);
		}

        protected string GetRejectedCandidatesNumber(JobAd ad)
        {
            return GetCountsForJobAd(LoggedInEmployer, ad)[ApplicantStatus.Rejected].ToString(CultureInfo.InvariantCulture);
        }

        protected string GetTotalCandidatesNumber(Employer employer, JobAd ad)
		{
            return GetCountsForJobAd(employer, ad).Values.Sum().ToString(CultureInfo.InvariantCulture);
        }

        protected static string GetContactEmail(JobAd jobAd)
        {
            return (jobAd.ContactDetails == null ? null : jobAd.ContactDetails.EmailAddress);
        }

		protected void AdsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			var jobAdId = new Guid((string)e.CommandArgument);
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
			if (jobAd == null)
				throw new ArgumentException("No job ad with ID " + jobAdId + " was found.");

		    string message;
            switch (e.CommandName)
            {
                case OpenAdCommand:
                    if (_jobAdsCommand.CanBeOpened(jobAd))
                    {
                        message = JobAdFacade.ReopenJobAd(LoggedInEmployer, jobAd);
                        OnJobAdRemoved(e.Item.ItemIndex, message);
                    }
                    else
                    {
                        // Reposting - simply redirect to the Purchase Preview page and take the reposting workflow from there.

                        message = "Please review the ad before publishing it.";
                        var quantity = _employerCreditsQuery.GetEffectiveActiveAllocation<JobAdCredit>(LoggedInEmployer).RemainingQuantity;
                        if (quantity != null && quantity > 0)
                        {
                            message = "Reposting this ad will use one credit. " + message;
                        }

                        var previewUrl = JobAdsRoutes.Preview.GenerateUrl(new { jobAdId = jobAd.Id });
                        RedirectWithNotification(previewUrl, NotificationType.Information, message);
                    } 
                    break;
                case CloseAdCommand:
                    message = CloseJobAd(jobAd);
                    OnJobAdRemoved(e.Item.ItemIndex, message);
                    break;
                case DeleteDraftCommand:
                    message = DeleteDraftAd(jobAd);
                    OnJobAdRemoved(e.Item.ItemIndex, message);
                    break;
                default:
                    throw new ApplicationException("Unexpected command");
            }
		}

        private string CloseJobAd(JobAdEntry jobAd)
        {
            _employerJobAdsCommand.CloseJobAd(LoggedInEmployer, jobAd);
            return jobAd.GetTitleAndReferenceDisplayText() + " has been closed.";
        }

        private string DeleteDraftAd(JobAdEntry jobAd)
        {
            _employerJobAdsCommand.DeleteJobAd(LoggedInEmployer, jobAd);
            return String.Format("Your draft ad '{0}' has been deleted.", jobAd.Title);
        }

        private void OnJobAdRemoved(int itemIndex, string message)
	    {
	        var redirectUrl = ucPagingBar.GetRedirectUrlAfterRemovingItem(itemIndex);
	        if (redirectUrl == null)
	        {
	            // Refresh the list of job ads now that one has been removed.

	            LinkMePage.AddConfirm(message);
                InitialiseJobAds(LoggedInEmployer);
	        }
	        else
	        {
	            RedirectWithNotification(redirectUrl, NotificationType.Confirmation, HttpUtility.HtmlEncode(message));
	        }
	    }

	    protected static string GetJobAdHeader(JobAd ad)
        {
            return ad.Title;
		}
        
        protected static string GetExternalReference(JobAd ad)
        {
            return string.IsNullOrEmpty(ad.Integration.ExternalReferenceId) ? "" : "#" + ad.Integration.ExternalReferenceId;
        }

        protected ReadOnlyUrl GetEditJobAdUrl(JobAd ad)
        {
            return JobAdsRoutes.JobAd.GenerateUrl(new { jobAdId = ad.Id });
        }

        protected ReadOnlyUrl GetViewJobAdUrl(JobAd ad)
        {
            return JobAdsRoutes.Preview.GenerateUrl(new { jobAdId = ad.Id });
        }

		private Url GetResultUrl()
		{
            Url url = new ApplicationUrl(Request.Url.AbsolutePath);
            url.QueryString.Add(QryStrSwitchedMode, GetCurrentMode().ToString());
            return url;
		}

		private void InitJobAdsRepeater(IEmployer employer)
		{
            if (_currentJobAdsIds.IsNullOrEmpty())
            {
                phNoJobAds.Visible = true;
                phJobAds.Visible = false;
                return;
            }

            var currentPageIds = ucPagingBar.GetResultSubset(_currentJobAdsIds);
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(currentPageIds);

            // Get the counts for each job ad through each applicant list.

            var applicantLists = (from a in jobAds select _jobAdApplicantsQuery.GetApplicantList(employer, a)).ToArray();
            _counts = _jobAdApplicantsQuery.GetApplicantCounts(employer, from a in applicantLists where a != null select a);

			AdsRepeater.DataSource = jobAds;
			AdsRepeater.DataBind();

            phJobAds.Visible = true;
        }

	    protected string GetExecutionString(JobAd ad)
		{
		    switch (GetCurrentMode())
		    {
		        case JobAdStatus.Open:
		            return "Close";
		        case JobAdStatus.Closed:
                    return _jobAdsCommand.CanBeOpened(ad) ? "Reopen" : "Repost";
                case JobAdStatus.Draft:
		            return "Delete";
		        default:
		            return string.Empty;
		    }
		}

        protected string GetExecutionCssClass()
        {
            switch (GetCurrentMode())
            {
                case JobAdStatus.Open:
                    return "close-action";
                case JobAdStatus.Closed:
                    return "repost-action";
                case JobAdStatus.Draft:
                    return "close-action";
                default:
                    return "invisible-action";
            }
        }

        protected string GetCommandName()
        {
            switch (GetCurrentMode())
            {
                case JobAdStatus.Open:
                    return CloseAdCommand;
                case JobAdStatus.Closed:
                    return OpenAdCommand;
                case JobAdStatus.Draft:
                    return DeleteDraftCommand;
                default:
                    return string.Empty;
            }
        }

        protected string GetClientClickScript()
        {
            switch (GetCurrentMode())
            {
                case JobAdStatus.Draft:
                    return "confirm(\"This will delete this draft. You will not be able to access it again. Are you sure you want to delete this job ad draft?\");";

                default:
                    return String.Empty;
            }
        }

        protected IDictionary<ApplicantStatus, int> GetCountsForJobAd(Employer employer, JobAd jobAd)
        {
            IDictionary<ApplicantStatus, int> listCounts;
            if (!_counts.TryGetValue(jobAd.Id, out listCounts))
                return (from s in new[] { ApplicantStatus.New, ApplicantStatus.Rejected, ApplicantStatus.Shortlisted }
                        select s).ToDictionary(s => s, s => 0);

            return listCounts;
        }


        protected int GetDaysUntilExpiry(JobAd jobAd)
        {
            return jobAd.ExpiryTime != null && jobAd.ExpiryTime.Value > DateTime.Now ? (jobAd.ExpiryTime.Value - DateTime.Now).Days : 0;
        }
	}
}
