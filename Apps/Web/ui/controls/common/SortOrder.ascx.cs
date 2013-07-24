using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Common
{
	public partial class SortOrder : LinkMeUserControl
    {
	    private const string CssClassThis = "sortorder_ascx";
	    private const string CssClassItems = "items";
	    private const string ListOpener = " (";
        private const string ListSeparator = ", ";
	    private const string ListCloser = ")";

	    private HtmlGenericControl _divTop;
        private Label _lblSortOrder;
	    private HtmlGenericControl _spnItems;
        private int[] _values;
        private string _sortOrderParam;
	    private int _sortOrder;
        private string[] _presentTenseDescriptions;

#if DEBUG
        protected override void OnPreRender(EventArgs e)
        {
            if (_values.IsNullOrEmpty())
            {
                throw new ApplicationException("The '" + ClientID + "' " + GetType().Name
                    + " has not been initialiased. You must call Initialise() in OnInit() or OnLoad().");
            }

            base.OnPreRender(e);
        }
#endif

        public void Initialise(Url baseUrl, string sortOrderParam, int[] values, string[] descriptions, string[] presentTenseDescriptions)
        {
            AddStyleSheetReference(StyleSheets.SortOrder);

            if (baseUrl == null)
                throw new ArgumentNullException("baseUrl");
            if (string.IsNullOrEmpty(sortOrderParam))
                throw new ArgumentException("The sort order parameter must be specified.", "sortOrderParam");
            if (values == null)
                throw new ArgumentNullException("values");
            if (descriptions == null)
                throw new ArgumentNullException("descriptions");
            if (presentTenseDescriptions == null)
                throw new ArgumentNullException("presentTenseDescriptions");
            if (values.Length != descriptions.Length)
                throw new ArgumentException("The number of descriptions must match the number of values.", "values");
            if (values.Length != presentTenseDescriptions.Length)
                throw new ArgumentException("The number of present-tense descriptions must match the number of values.", "values");
 
            _values = values;
            _sortOrderParam = sortOrderParam;
            _presentTenseDescriptions = presentTenseDescriptions;

            Url url = baseUrl.Clone();

            // Div: top-level rendering of this class
            //
            _divTop = new HtmlGenericControl("div");
            _divTop.Attributes["class"] = CssClassThis;
            Controls.Add(_divTop);

            // Label: holds the name of the seleced sort order
            //
            _lblSortOrder = new Label();
            _divTop.Controls.Add(_lblSortOrder);

            // Span: holds all list items and separators.
            //
            _spnItems = new HtmlGenericControl("span");
            _spnItems.Attributes["class"] = CssClassItems;
            _divTop.Controls.Add(_spnItems);
            _spnItems.Controls.Add(new LiteralControl(ListOpener));

            for (int i = 0; i < values.Length; i++)
            {
                url.QueryString[sortOrderParam] = values[i].ToString();

                var hyperLink = new HyperLink {Text = descriptions[i], NavigateUrl = url.ToString()};
                _spnItems.Controls.Add(hyperLink);

                _spnItems.Controls.Add(new LiteralControl(ListSeparator));
            }

            _spnItems.Controls.Add(new LiteralControl(ListCloser));
        }

        public void SetSelectedOption(int order)
        {
            _sortOrder = order;

            LiteralControl litFinalSeparator = null;

            foreach (Control control in _spnItems.Controls)
            {
                var hyperLink = control as HyperLink;

                if (hyperLink != null)
                {
                    var url = new Url(hyperLink.NavigateUrl);
                    bool blnSelectedLink = (url.QueryString[_sortOrderParam] == _sortOrder.ToString());
                    hyperLink.Visible = !blnSelectedLink;

                    var litSeparator = (LiteralControl)_spnItems.Controls[_spnItems.Controls.IndexOf(hyperLink) + 1];
                    if (litSeparator.Text == ListSeparator)
                    {
                        litSeparator.Visible = !blnSelectedLink;
                        if (!blnSelectedLink) litFinalSeparator = litSeparator;
                    }
                    if (blnSelectedLink)
                    {
                        _lblSortOrder.Text = _presentTenseDescriptions[Array.IndexOf(_values, _sortOrder)];
                    } 
                }
            }

            if (litFinalSeparator != null)
            {
                litFinalSeparator.Visible = false;
            }
        }

        public int GetSelectedOption()
        {
            return _sortOrder;
        }
    }
}
