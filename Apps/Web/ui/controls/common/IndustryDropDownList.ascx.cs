using System;
using System.Web.UI.WebControls;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class IndustryDropDownList : LinkMeUserControl
    {
        private readonly IIndustriesQuery _industriesQuery = Container.Current.Resolve<IIndustriesQuery>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ddlIndustry.Items.Add(new ListItem("--- any industry ---", string.Empty));

            foreach (Industry industry in _industriesQuery.GetIndustries())
                ddlIndustry.Items.Add(new ListItem(industry.Name, industry.UrlName));
        }

        #region Properties

        public DropDownList DropDownList
        {
            get { return ddlIndustry; }
        }

        public short TabIndex
        {
            get { return ddlIndustry.TabIndex; }
            set { ddlIndustry.TabIndex = value; }
        }

        public Unit Height
        {
            get { return ddlIndustry.Height; }
            set { ddlIndustry.Height = value; }
        }

        public Unit Width
        { 
            get { return ddlIndustry.Width; }
            set { ddlIndustry.Width = value; }
        }

        public string CssClass
        {
            get { return ddlIndustry.CssClass; }
            set { ddlIndustry.CssClass = value; }
        }

        public Industry SelectedIndustry
        {
            get 
            {
                string selectedUrl = ddlIndustry.SelectedValue;
                return (selectedUrl != string.Empty) ? _industriesQuery.GetIndustryByUrlName(selectedUrl) : null;
            }

            set { ddlIndustry.SelectedValue = (value != null) ? value.UrlName : string.Empty; }
        }

        #endregion
    }
}