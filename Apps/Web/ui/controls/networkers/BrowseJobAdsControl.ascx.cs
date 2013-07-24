using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinkMe.Web.Content;
using LinkMe.Web.Helper;
using LinkMe.Apps.Asp.Navigation;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public enum BrowseMode
    {
        Industry,
        Location
    }

	public partial class BrowseJobAdsControl
        : LinkMeUserControl
	{
        private BrowseMode _mode;
        private string _filter;
	    private int _itemCount;

        private class BrowseDataItem
        {
            private readonly string _url;
            private readonly string _displayName;
            private readonly string _title;

            public BrowseDataItem(string url, string displayName, string title)
            {
                _url = url;
                _displayName = displayName;
                _title = title;
            }

            public string Url
            {
                get { return _url; }
            }

            public string DisplayName
            {
                get { return _displayName; }
            }

            public string Title
            {
                get { return _title; }
            }
        }

        private static readonly IDictionary<string, SortedList<string, BrowseDataItem>> _industryItems = new Dictionary<string, SortedList<string, BrowseDataItem>>();
        private static readonly IDictionary<string, SortedList<string, BrowseDataItem>> _isSubdivisionItems = new Dictionary<string, SortedList<string, BrowseDataItem>>();
        private static readonly IDictionary<string, SortedList<string, BrowseDataItem>> _isNotSubdivisionItems = new Dictionary<string, SortedList<string, BrowseDataItem>>();

        static BrowseJobAdsControl()
        {
            // This control lives on some heavily hit pages so cache the details instead of accessing the site map each time.

            // Populate the industry items which are grouped by location.

            LoadIndustryItems(_industryItems, JobBoardHelper.GetBrowseIndustryJobsNodes());

            // Populate the location items which are grouped by industry, and whether or not they are country subdivisions.

            LoadLocationItems(_isSubdivisionItems, JobBoardHelper.GetBrowseLocationJobsNodes(true));
            LoadLocationItems(_isNotSubdivisionItems, JobBoardHelper.GetBrowseLocationJobsNodes(false));
        }

	    private static void LoadIndustryItems(IDictionary<string, SortedList<string, BrowseDataItem>> items, IEnumerable<SiteMapNode> nodes)
	    {
            foreach (NavigationSiteMapNode industryNode in nodes)
            {
                var location = industryNode["location"] ?? string.Empty;
                SortedList<string, BrowseDataItem> locationItems;
                if (!items.TryGetValue(location, out locationItems))
                {
                    locationItems = new SortedList<string, BrowseDataItem>();
                    items[location] = locationItems;
                }

                var displayName = location != string.Empty ? industryNode["industryText"] : industryNode.Text;
                var title = displayName + " Jobs";
                locationItems.Add(industryNode["industry"], new BrowseDataItem(industryNode.Url, displayName, title));
            }
	    }

        private static void LoadLocationItems(IDictionary<string, SortedList<string, BrowseDataItem>> items, IEnumerable<SiteMapNode> nodes)
        {
            foreach (NavigationSiteMapNode locationNode in nodes)
            {
                var industry = locationNode["industry"] ?? string.Empty;
                SortedList<string, BrowseDataItem> industryItems;
                if (!items.TryGetValue(industry, out industryItems))
                {
                    industryItems = new SortedList<string, BrowseDataItem>();
                    items[industry] = industryItems;
                }

                var displayName = industry != string.Empty ? locationNode["locationText"] : locationNode.Text;
                var title = displayName + " Jobs";
                industryItems.Add(locationNode["location"], new BrowseDataItem(locationNode.Url, displayName, title));
            }
        }

        public BrowseMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        public string Filter
        {
            get { return _filter; }
            set { _filter = value; }
        }

        protected bool TwoColumnLayout
        {
            get { return _mode == BrowseMode.Location; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            AddStyleSheetReference(StyleSheets.BrowseJobAds);

            switch (_mode)
            {
                case BrowseMode.Location:
                    rptCategories1.DataSource = _isSubdivisionItems[_filter ?? string.Empty].Values;
                    rptCategories2.DataSource = _isNotSubdivisionItems[_filter ?? string.Empty].Values;
                    rptCategories1.DataBind();
                    rptCategories2.DataBind();
                    break;

                case BrowseMode.Industry:
                    var items = _industryItems[_filter ?? string.Empty].Values;
                    _itemCount = items.Count;
                    rptCategories3.DataSource = items.Take(_itemCount / 2);
                    rptCategories3.DataBind();
                    rptCategories4.DataSource = items.Skip(_itemCount / 2);
                    rptCategories4.DataBind();
                    break;

                default:
                    throw new Exception(_mode + " is not a valid browse mode. Use BrowseMode.Location or BrowseMode.Industry instead.");
            }

		}

        // MF (2008-09-29) - functions to allow proper columnar layout of items.
        //
        protected int GetColumnIndex(int itemIndex, int columnCount)
        {
            if (_mode == BrowseMode.Location)
                throw new Exception("GetColumnIndex() is only implemented for browse jobs by industry (not by location).");

            return (itemIndex < (_itemCount / 2)) ? 0 : 1;
        }

        protected bool IsNewColumn(int itemIndex, int columnCount)
        {
            if (_mode == BrowseMode.Location)
                throw new Exception("IsNewColumn() is only implemented for browse jobs by industry (not by location).");
            
            // MF: Also dirty but works (warning: passes -1 to GetColumnIndex(), this is bad semantics)
            return itemIndex == 0 || (itemIndex == _itemCount / 2);
        }

        protected static string GetUrl(object dataItem)
        {
            var item = (BrowseDataItem)dataItem;
            return item.Url;
        }

		protected static string GetDisplayName(object dataItem)
		{
            var item = (BrowseDataItem)dataItem;
            return item.DisplayName;
		}

        protected static string GetTitle(object dataItem)
        {
            var item = (BrowseDataItem)dataItem;
            return item.Title;
        }
	}
}
