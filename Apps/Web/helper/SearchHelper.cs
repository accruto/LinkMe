using System;
using System.Security.Principal;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Search.Members;
using LinkMe.Utility.Validation;

namespace LinkMe.Web.Helper
{
	public static class SearchHelper
	{
        internal const string MemberIdParam = "memberId";
        internal const string ListIdParam = "listId";
        internal const string ResumeIdParam = "resumeId";
        internal const string ShowPrevNextParam = "showPrevNext";
        internal const bool ShowPrevNextDefault = true;
        internal const string RESULT_INDEX_PARAM = "resultIndex";

        public static void EnsureEmployerIsJobPoster(Guid loggedInUserId, MemberSearch savedSearch)
        {
            if (savedSearch == null)
                throw new ArgumentNullException("savedSearch");

            if (savedSearch.OwnerId != loggedInUserId)
                throw new UserException(ValidationErrorMessages.NO_ACCESS_TO_SAVED_SEARCH);
        }

        internal static string GetJobTitleAndEmployerHtml(IJob job, IResumeHighlighter highlighter, bool hideRecentEmployers, IJob previousJobForResume)
        {
            // Do not show the company names for the "current" and "previous" jobs (as defined in
            // RequirementsForMyVisibilityBasicSettingsPage) if the candidate has chosen
            // to hide these or the employer hasn't paid, but tell the employer whether these are available.
            // Note that the experience is no longer hidden as of 3.0.

            string jobHtml = "";
            jobHtml += GetJobTitleHtml(job, highlighter);
            jobHtml += !string.IsNullOrEmpty(job.Company) ? ", " : "";
            jobHtml += GetEmployerHtml(job, highlighter, hideRecentEmployers, previousJobForResume);
            return jobHtml;
        }
        
        internal static string GetJobTitleHtml(IJob job, IResumeHighlighter highlighter)
        {            
            string jobHtml = "";
            if (!string.IsNullOrEmpty(job.Title))
            {
                jobHtml = highlighter.HighlightJobTitle(job.Title);
            }

            return jobHtml;
        }
        
        internal static string GetEmployerHtml(IJob job, IResumeHighlighter highlighter, bool hideRecentEmployers, IJob previousJobForResume)
        {
            // Do not show the company names for the "current" and "previous" jobs (as defined in
            // RequirementsForMyVisibilityBasicSettingsPage) if the candidate has chosen
            // to hide these or the employer hasn't paid, but tell the employer whether these are available.
            // Note that the experience is no longer hidden as of 3.0.
            string jobHtml = "";

            if (!string.IsNullOrEmpty(job.Company))
            {
                bool hideEmployer = hideRecentEmployers && (job.Dates == null || job.Dates.End == null || job == previousJobForResume);
            
                jobHtml += (hideEmployer ? "&lt;Employer hidden&gt;" :
                            highlighter.HighlightJobTitle(job.Company));
            }
            return jobHtml;
        }

        internal static Guid? GetSearcherId(IPrincipal user, UserType requiredRoles)
        {
            // Do not load the whole RegisteredUser object for the logged in user - but make sure it is an
            // employer or member, as required.

            return user.UserType() == requiredRoles ? user.Id() : null;
        }
	}
}
