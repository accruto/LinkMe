using System.Web.UI;
using LinkMe.Web.Content;
using LinkMe.Web.UI.Controls.Common;

namespace LinkMe.Web.UI.Controls.Fields
{
    public partial class LocationField
        : UserControl
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            ucLocationConfirmation.Location = ucLocation;
        }

        public string Label
        {
            get { return litLabel.Text; }
            set { litLabel.Text = value; }
        }

        public string Text
        {
            get { return ucLocation.Text; }
            set { ucLocation.Text = value; }
        }

        public string Example
        {
            get { return ucLocation.ExampleText; }
            set { ucLocation.ExampleText = value; }
        }

        public Common.Location.ResolutionMethod Method
        {
            get { return ucLocation.Method; }
            set { ucLocation.Method = value; }
        }

        public CountryField CountryField
        {
            set { ucLocation.SetLocationCountry(value.LocationCountry); }
        }

        public string TextBoxClientID
        {
            get { return ucLocation.TextBoxClientID; }
        }
    }
}