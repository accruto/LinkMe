using System;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Integration.Routes;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.Applications.Facade
{
	public static class JobAdFacade
	{
        private static readonly IEmployerJobAdsCommand _employerJobAdsCommand = Container.Current.Resolve<IEmployerJobAdsCommand>();
        private static readonly IJobAdsCommand _jobAdsCommand = Container.Current.Resolve<IJobAdsCommand>();

        /// <summary>
        /// Name of the query string parameter that is added to JobAd.ExternalApplyUrl when the user is
        /// transferred to the external job application page. This is documented in the interface spec with
        /// ATS providers and should not be changed.
        /// </summary>
        private const string ExternalApplyUrlLinkmeApplicationIDParam = "linkmeApplicationId";
        /// <summary>
        /// Name of the query string parameter that is added to JobAd.ExternalApplyUrl when the user is
        /// transferred to the external job application page. This is documented in the interface spec with
        /// ATS providers and should not be changed.
        /// </summary>
        private const string ExternalApplyUrlLinkmeApplicationUriParam = "linkmeApplicationUri";

        public static Url GetFullExternalApplyUrl(InternalApplication jobApplication, JobAdView jobAd)
        {
            if (jobApplication == null)
                throw new ArgumentNullException("jobApplication");

            var jobAppUrl = JobAdsRoutes.Application.GenerateUrl(new { applicationId = jobApplication.Id });
            var jobExternalApplyUrl = new ApplicationUrl(jobAd.Integration.ExternalApplyUrl);
            
            jobExternalApplyUrl.QueryString.Add(ExternalApplyUrlLinkmeApplicationIDParam, jobApplication.Id.ToString("n"));
            jobExternalApplyUrl.QueryString.Add(ExternalApplyUrlLinkmeApplicationUriParam, jobAppUrl.AbsoluteUri);

            return jobExternalApplyUrl;
        }

        public static string GetExternalApplyPopupWindowName(JobAdView jobAd)
        {
            return "ExternalAppForm_" + (jobAd == null ? "" : jobAd.Id.ToString("n"));
        }

        public static string ReopenJobAd(Employer employer, JobAd jobAd)
	    {
            _employerJobAdsCommand.OpenJobAd(employer, jobAd, true);
            return string.Format("{0} has been reopened.", jobAd.GetTitleAndReferenceDisplayText());
        }

        public static void SendJobAdEmailToFriend(string senderName, string senderEmail, string recipientName, string recipientEmail, string userMessage, string adId)
        {
            var ad = _jobAdsCommand.GetJobAd<JobAdEntry>(new Guid(adId));
            var email = new SendJobToFriendEmail(recipientEmail, recipientName, senderEmail, senderName, ad, userMessage);
            Container.Current.Resolve<IEmailsCommand>().TrySend(email);
        }
	}
}
