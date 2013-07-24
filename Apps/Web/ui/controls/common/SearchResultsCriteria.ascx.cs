using System;

namespace LinkMe.Web.UI.Controls.Common
{
	public partial class SearchResultsCriteria : LinkMeUserControl
	{
	    protected string CriteriaHtml { get; private set; }
        protected string ItemsFoundHtml { get; private set; }

        public void SetDisplay(string criteriaHtml, string itemsFoundHtml)
        {
            CriteriaHtml = criteriaHtml;
            ItemsFoundHtml = itemsFoundHtml;
        }
	}
}
