using System;
using LinkMe.Apps.Agents.Context;

namespace Linkme.Web.UI.controls.common
{
    public partial class FeaturedPartnersAds : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected bool DisplayAds
        {
            get { return !Request.IsSecureConnection && !ActivityContext.Current.Community.IsSet; }
        }
    }
}