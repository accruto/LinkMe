using System;
using System.Web.UI.WebControls;
using LinkMe.Apps.Agents.Context;
using LinkMe.Domain.Location;
using LinkMe.Utility.Validation;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class LocationCountry : LinkMeUserControl
    {
        private Location location;
        private bool includeGlobal = false;

        public bool IncludeGlobal
        {
            get { return includeGlobal; }
            set { includeGlobal = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            ErrorMessage = ValidationErrorMessages.REQUIRED_FIELD_COUNTRY;

            ddlCountry.DataSource = LocationQuery.GetCountries();
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "Id";

            DataBind();

            if (includeGlobal)
                ddlCountry.Items.Insert(0, new ListItem("Global", "255"));

            // Set the default selected country based on the current request.

            SelectedValue = ActivityContext.Current.Location.Country;

            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Add the script for when the country is changed.

            ddlCountry.Attributes.Add("onblur", SetCountryScript);
        }

        public DropDownList DropDownList
        {
            get { return ddlCountry; }
        }

        public Country SelectedValue
        {
            get
            {
                // Return null if "Global" is included and selected, as well as if nothing is selected.

                var minIndex = (includeGlobal ? 1 : 0);
                return ddlCountry.SelectedIndex < minIndex ? null : LocationQuery.GetCountry(int.Parse(ddlCountry.SelectedValue));
            }
            set
            {
                ddlCountry.SelectedValue = (value == null ? null : value.Id.ToString());
            }
        }

        public short TabIndex
        {
            get { return ddlCountry.TabIndex; }
            set { ddlCountry.TabIndex = value; }
        }

        public string ErrorMessage
        {
            get { return reqCountry.ErrorMessage; }
            set { reqCountry.ErrorMessage = value; }
        }

        public string CssClass
        {
            get { return ddlCountry.CssClass; }
            set { ddlCountry.CssClass = value; }
        }

        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                location.SetLocationCountry(this);
            }
        }

        private string SetCountryScript
        {
            get { return location == null ? string.Empty : location.SetCountryScript; }
        }

        public override void DataBind()
        {
            ddlCountry.DataBind();
        }

        public string DropDownClientID
        {
            get { return ddlCountry.ClientID;  }
        }
    }
}
