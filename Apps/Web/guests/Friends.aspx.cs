using System;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Members.Friends;
using LinkMe.Web.UI;

namespace LinkMe.Web.Guests
{
    public partial class Friends : LinkMePage
    {
        protected override UserType[] AuthorizedUserTypes
        {
            get { return new UserType[0]; }
        }

        protected override ReadOnlyUrl GetAuthorizedUrl()
        {
            return NavigationManager.GetUrlForPage<ViewFriends>();
        }

        protected override bool GetRequiresActivation()
        {
            return false;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ucContent.ImageUrl = new ApplicationUrl("~/ui/images/anonymous-pages/friends.jpg");
        }
    }
}
