using System;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Content;
using LinkMe.Web.UI;

namespace LinkMe.Web.Members.Friends
{
    public partial class Invitations
        : LinkMePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Networking);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ucSentInvites.Member = LoggedInMember;
        }

        protected override UserType[] AuthorizedUserTypes
        {
            get { return new[] { UserType.Member }; }
        }

        protected override bool GetRequiresActivation()
        {
            return true;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }
    }
}