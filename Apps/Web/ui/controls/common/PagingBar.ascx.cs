using System;
using System.Collections.Generic;
using System.Web.UI;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Urls;
using InvalidOperationException=System.InvalidOperationException;

namespace LinkMe.Web.UI.Controls.Common
{
	public partial class PagingBar : UserControl
	{
        private int _numOfPages;
        private const int SpecialPageIndex = 5;	// the first/last x pages
		private const int StartEndPagesView	 = 10;	// how many next/previous pages to display on start/end pages
		private const int SpecialMidPagesView= 15;	// how many next/previous pages to display on middle pages

		// Parameters set by the parent page.
        private ReadOnlyUrl _baseUrl;
        private string _startIndexParam;
        private int _startIndex = -1;
		private int _totalResults;
        private int _resultsPerPage = 1;
        private bool _browse;

        public int ResultsPerPage
        {
            get { return _resultsPerPage; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", value, "ResultsPerPage value must be greater than 0.");

                _resultsPerPage = value;
            }
        }

	    public string StartIndexParam
	    {
	        get { return _startIndexParam; }
	        set { _startIndexParam = value; }
	    }

	    public int StartIndex
        {
            get
            {
                if (_startIndex == -1)
                {
                    if (string.IsNullOrEmpty(_startIndexParam))
                        throw new InvalidOperationException("StartIndexParam must be set before calling GetStartIndex().");

                    // The index in the request can be anything, but the one we return should always be on a page
                    // boundary, eg. if 10 results per page are shown and the index is 21 return 20 instead.

                    int requestedIndex = ParseUtil.ParseUserInputInt32Optional(Request.QueryString[_startIndexParam],
                        "start index", 0);
                    _startIndex = requestedIndex - requestedIndex % ResultsPerPage;
                }

                return _startIndex;
            }
            set { _startIndex = value; }
        }

	    protected int NumberOfPages
		{
			get { return _numOfPages; }
		}

        public void InitPagesList(Url baseUrl, int totalResults)
        {
            InitPagesList(baseUrl, totalResults, false);
        }

        public void InitPagesList(ReadOnlyUrl baseUrl, int totalResults, bool browse)
		{
            if (ResultsPerPage == 0)
                throw new InvalidOperationException("ResultsPerPage must be set before calling InitPagesList().");
            if (totalResults < 0)
            {
                throw new ArgumentOutOfRangeException("totalResults", totalResults,
                    "The total number of results must not be less than 0.");
            }

			_baseUrl = (baseUrl ?? new ApplicationUrl(Request.Url.AbsoluteUri));
			_totalResults = totalResults;
            _browse = browse;
            
            int currentPageIndex = GetCurrentPageNumber();
			_numOfPages = CalcNumberOfPages();
			if (_numOfPages <= 1)
			{
				repeaterPages.DataSource = null;
				repeaterPages.DataBind();
				return;
			}

			int pageStartIndex, pageCount;

			if (currentPageIndex <= SpecialPageIndex)
			{
                //1st page scrolling case
                pageStartIndex = 1;
				pageCount = Math.Min(_numOfPages, StartEndPagesView);
			}
			else if (currentPageIndex > _numOfPages - SpecialPageIndex)
			{
                //last page scrolling case
                pageCount = Math.Min(_numOfPages, StartEndPagesView);
				pageStartIndex = _numOfPages - Math.Max(SpecialPageIndex, pageCount) + 1;
			}
			else
			{
                //mid page scrolling case
                pageStartIndex = currentPageIndex - Math.Min(SpecialMidPagesView / 2, currentPageIndex - 1);
				pageCount = Math.Min(SpecialMidPagesView, _numOfPages);

				// the gap between SpecialMidPagesView and StartEndPagesView
				if (pageStartIndex + pageCount > _numOfPages)
					pageCount = _numOfPages - pageStartIndex +1;
			}

			var pageNumbers = new int[pageCount];

			for (int i = 0; i < pageCount; i++)
			{
				pageNumbers[i] = pageStartIndex + i;
			}

			repeaterPages.DataSource = pageNumbers;
			repeaterPages.DataBind();
		}

        /// <summary>
        /// If removing the specified item from the list (specified using a relative index, relative to the
        /// current page) would result in having to display a different page of results, returns the url of that
        /// page, otherwise null. Call this method whenever an item is removed from the list. If it returns null
        /// then refresh the results on the same page, otherwise redirect to the returned URL.
        /// </summary>
        public Url GetRedirectUrlAfterRemovingItem(int relativeIndex)
        {
            if (string.IsNullOrEmpty(StartIndexParam))
            {
                throw new InvalidOperationException("StartIndexParam must be set before calling"
                     + "GetRedirectUrlAfterRemovingItem().");
            }

            int currentPageIndex = GetCurrentPageNumber();

            // We only need to redirect if this was the first result on the page, the last result over and there
            // are still results left.

            if (relativeIndex != 0)
                return null;

            int absoluteIndex = GetAbsoluteResultIndex(relativeIndex, currentPageIndex);
            if (absoluteIndex == 0 || absoluteIndex != _totalResults - 1)
                return null;

            Url url = new ApplicationUrl(Request.Url.AbsoluteUri);
            url.QueryString[StartIndexParam] = GetAbsoluteResultIndex(0, currentPageIndex - 1).ToString();

            return url;
        }

        public IList<T> GetResultSubset<T>(IList<T> results)
        {
            if (ResultsPerPage == 0)
                throw new InvalidOperationException("ResultsPerPage has not been initialised.");
            if (results == null)
                throw new ArgumentNullException("results");

            int index = StartIndex;
            if (results.Count == 0 && index == 0)
                return results; // Empty result set.

            if (index < 0 || index >= results.Count)
            {
                throw new ArgumentOutOfRangeException(string.Format("Start index {0} (page {1}) is invalid."
                    + " It must be between 0 and {2}.", index, GetCurrentPageNumber(), results.Count - 1));
            }

            int count = Math.Min(ResultsPerPage, results.Count - index);

            var subset = new T[count];
            for (int i = 0; i < count; i++)
            {
                subset[i] = results[index + i];
            }

            return subset;
        }

        public int GetCurrentPageNumber()
        {
            return GetPageNumberForResultIndex(StartIndex);
        }

		protected string GetPreviousLink()
		{
            int currentPageIndex = GetCurrentPageNumber();
			if (currentPageIndex <= 1)
				return string.Empty;

			return string.Format("<a href=\"{0}\"><b>&laquo;&nbsp;Previous</b></a>&nbsp;",
				GetPageUrl(currentPageIndex - 1));
		}

		protected string GetNextLink()
		{
            int currentPageIndex = GetCurrentPageNumber();
            if (currentPageIndex >= NumberOfPages)
				return string.Empty;

			return string.Format("&nbsp;<a href=\"{0}\"><b>Next&nbsp;&raquo;</b></a>",
				GetPageUrl(currentPageIndex + 1));
		}

		protected string GetPageLink(int pageIndex)
		{
		    if (pageIndex == GetCurrentPageNumber())
			{
				return string.Format("<a disabled=\"disabled\">{0}</a>", pageIndex);
			}
		    
            return string.Format("<a href=\"{0}\">{1}</a>",
		        GetPageUrl(pageIndex), pageIndex);
		}

	    protected override object SaveViewState()
        {
            try
            {
                return new Triplet(base.SaveViewState(), StartIndex, _totalResults);
            }
            catch (UserException)
            {
                // Ignore errors from invalid URLs (spam bots, etc.)
                return base.SaveViewState();
            }
        }

        protected override void LoadViewState(object savedState)
        {
            var triplet = (Triplet)savedState;
            base.LoadViewState(triplet.First);
            _startIndex = (int)triplet.Second;
            _totalResults = (int)triplet.Third;
        }

        private ReadOnlyUrl GetPageUrl(int pageIndex)
		{
            int absolutePageIndex = GetAbsoluteResultIndex(0, pageIndex);
            return _browse ? new ReadOnlyApplicationUrl(_baseUrl, absolutePageIndex.ToString()) : _baseUrl.Clone(new QueryString(_startIndexParam, absolutePageIndex.ToString()));
		}

		private int CalcNumberOfPages()
		{
		    if (_totalResults == 0)
				return 0;
		    return (_totalResults - 1) / ResultsPerPage + 1;
		}

	    private int GetPageNumberForResultIndex(int absoluteIndex)
        {
            if (ResultsPerPage == 0)
                throw new InvalidOperationException("ResultsPerPage has not been initialised.");

            return absoluteIndex / ResultsPerPage + 1; // The page number is 1-based.
        }

        private int GetAbsoluteResultIndex(int relativeIndex, int pageNumber)
        {
            if (ResultsPerPage == 0)
                throw new InvalidOperationException("ResultsPerPage has not been initialised.");

            return (pageNumber - 1) * ResultsPerPage + relativeIndex;
        }
    }
}