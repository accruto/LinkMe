using System;
using LinkMe.Domain.Contacts;
using LinkMe.Web.UI.Controls.Networkers;

namespace LinkMe.Web.UI.Registered.Networkers
{
    public partial class JustJoinedApplication : LinkMePage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ucJoinToApplySteps.Step = JoinToApplySteps.ActiveStep.Two;
        }

        protected override UserType[] AuthorizedUserTypes
        {
            get { return new[] { UserType.Member }; }
        }

        protected override bool GetRequiresActivation()
        {
            return false;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }
    }
}
