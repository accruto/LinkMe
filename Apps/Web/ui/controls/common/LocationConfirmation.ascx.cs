using System;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class LocationConfirmation : LinkMeUserControl
    {
        private Location location;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            AddStyleSheetReference(StyleSheets.LocationConfirmation);

            hlinkYes.NavigateUrl = ConfirmUrl;
            hlinkNo.NavigateUrl = RejectUrl;
        }

        public string Text
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }

        public Location Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                location.SetLocationConfirmation(this);
            }
        }

        private string ConfirmUrl
        {
            get { return location == null ? string.Empty : Location.ConfirmUrl; }
        }

        private string RejectUrl
        {
            get { return location == null ? string.Empty : location.RejectUrl; }
        }

        internal string ElementId
        {
            get { return divConfirmation.ClientID; }
        }
    }
}