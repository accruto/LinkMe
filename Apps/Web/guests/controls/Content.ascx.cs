using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.UI;

namespace LinkMe.Web.Guests.Controls
{
    public partial class Content : LinkMeUserControl
    {
        private string heading;
        private Url imageUrl;

        public string Heading
        {
            get { return heading; }
            set { heading = value; }
        }

        public Url ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }
    }
}