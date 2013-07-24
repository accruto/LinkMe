using System;
using System.Collections.Specialized;
using System.Web;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Query.Search.JobAds;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Apps.Asp.Navigation;

namespace LinkMe.Web.Helper
{
	public static class JobSearchHelper
	{
		public const string JOB_AD_ID_PARAM		    = "jobAdId";
        public const string SEARCH_URL_PARAM        = "searchUrl";
        public const string EXTERNAL_URL_PARAM      = "externalUrl";
        public const string JUST_REGISTERED_PARAM   = "justRegistered";
        public const string EMAIL_PARAM             = "email";

		internal const int CRITERIA_TEXT_MAX_LENGTH = 200;

		internal const string AD_TITLE_PARAM 		= "adTitle";
		internal const string KEYWORDS_PARAM 		= "keywords";
		internal const string LOCALITY_PARAM 		= "locality";
		internal const string POSTCODE_PARAM 		= "postcode";
		internal const string STATE_PARAM			= "state";
		internal const string SEARCH_TYPE_SIMPLE	= "simpleJobSearch";
		internal const string SEARCH_TYPE_ADVANCED	= "advancedJobSearch";
        internal const string SEARCH_TYPE_BROWSE    = "browseJobSearch";
        internal const string SEARCH_TYPE_SUGGESTED = "suggestedJobSearch";
        internal const string MIN_SALARY_PARAM = "minSalary";
		internal const string MAX_SALARY_PARAM 		= "maxSalary";
		internal const string DISTANCE_PARAM		= "distance";

	    internal static int ResultsPerPage
		{
			get
			{
				return ApplicationContext.Instance.GetIntProperty(ApplicationContext.SEARCH_RESULTS_PER_PAGE);
			}
		}
	}
}
