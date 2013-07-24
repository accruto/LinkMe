using System;
using System.Web.UI;
using LinkMe.Domain.Location;
using LinkMe.Web.Content;
using LinkMe.Web.UI.Controls.Common;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Web.UI.Controls.Fields
{
    public partial class CountryField
        : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        public string Label
        {
            get { return lblField.Text; }
            set { lblField.Text = value; }
        }

        public bool IncludeGlobal
        {
            get { return ucLocationCountry.IncludeGlobal; }
            set { ucLocationCountry.IncludeGlobal = value; }
        }

        public Country Country
        {
            get { return ucLocationCountry.SelectedValue; }
            set { ucLocationCountry.SelectedValue = value; }
        }

        internal LocationCountry LocationCountry
        {
            get { return ucLocationCountry; }
        }

        public string DropDownClientID
        {
            get { return ucLocationCountry.DropDownClientID; }
        }
    }
}