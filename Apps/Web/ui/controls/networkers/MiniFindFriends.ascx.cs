using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.Applications.Facade;
using LinkMe.Web.Service;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class MiniFindFriends : LinkMeUserControl
    {
        private static readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private static readonly IMemberContactsQuery _memberContactsQuery = Container.Current.Resolve<IMemberContactsQuery>();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                if (LoggedInUserId != null)
                {
                    var ids = _memberContactsQuery.GetFirstDegreeContacts(LoggedInUserId.Value, PersonalVisibility.SendMessages);
                    var names = _membersQuery.GetFullNames(ids);

                    //Initialise friends collection for Ajax drop down
                    var sessWrapper = new HttpSessionWrapper(typeof(GetSuggestedContacts), Session);
                    sessWrapper.SetValue(GetSuggestedContacts.SessionKey, names);
                }
            }
        }
    }
}