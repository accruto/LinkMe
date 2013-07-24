using System;
using LinkMe.Utility.Configuration;
using LinkMe.Web.Helper;

namespace LinkMe.Web.UI.Controls.Common
{
	public partial class SearchResultsCaption : LinkMeUserControl
	{
	    private int _totalMatches;
	    private int _availableMatches;
		private TimeSpan _searchDuration;

		protected bool ShowTotalSearchResults
		{
			get
			{
                if (_totalMatches <= _availableMatches)
					return false;

                return ApplicationContext.Instance.GetBoolProperty(ApplicationContext.SHOW_TOTAL_SEARCH_RESULTS);
			}
		}

		protected bool ShowSearchDuration
		{
			get
			{
			    return (false);
			}
		}

		protected string SearchDurationSeconds
		{
			get { return string.Format("{0}s", _searchDuration.TotalSeconds.ToString("n3")); }
		}

		protected string TotalSearchResults
		{
			get { return _totalMatches.ToString(); }
		}

        protected string SearchResultsCaptionHtml { get; private set; }

        public void SetDisplay(int totalMatches, int availableMatches, bool cachedResults, TimeSpan? duration, string searchResultsCaptionHtml)
		{
            _totalMatches = totalMatches;
            _availableMatches = availableMatches;
            SearchResultsCaptionHtml = searchResultsCaptionHtml;

            // Don't show the search duration if the results were cached.

            _searchDuration = cachedResults || duration == null ? TimeSpan.Zero : duration.Value;
		}
	}
}
