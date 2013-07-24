using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Domain.Users.Members;
using LinkMe.Web.Helpers;
using LinkMe.Web.Members.Friends;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class MiniFriendsList
        : LinkMeUserControl
    {
        private static readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private static readonly IMemberContactsQuery _memberContactsQuery = Container.Current.Resolve<IMemberContactsQuery>();
        private static readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();

        public static int MaxFriendsToShow = 8;
        private Member _friendsOwner;
        private PersonalViews _views;

        public Member FriendsOwner
        {
            get { return _friendsOwner; }
            set { _friendsOwner = value; }
        }

        #region Page Actions

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Choose a random set of friends to show, favouring those with photos.

            IList<Guid> friends = null;
            if (_friendsOwner != null)
            {
                friends = _memberContactsQuery.GetFirstDegreeContacts(_friendsOwner.Id, true).Randomise().Take(MaxFriendsToShow).ToList();
                if (friends.Count < MaxFriendsToShow)
                    friends = friends.Concat(_memberContactsQuery.GetFirstDegreeContacts(_friendsOwner.Id, false).Randomise().Take(MaxFriendsToShow - friends.Count)).ToList();
            }

            if (friends != null && friends.Count > 0)
            {
                _views = _memberViewsQuery.GetPersonalViews(LoggedInMemberId, _membersQuery.GetMembers(friends));
                rptMiniFriends.DataSource = friends;
                rptMiniFriends.DataBind();
            }
            else
            {
                rptMiniFriends.Visible = false;
            }
        }

        #endregion

        protected ReadOnlyUrl BuildImageUrl(Guid friendId)
        {
            return _views[friendId].GetPhotoUrlOrDefault();
        }

        protected static ReadOnlyUrl BuildViewProfileLink(Guid friendId)
        {
            return GetUrlForPage<ViewFriend>(ViewFriend.FriendIdParameter, friendId.ToString());
        }

        protected string GetFirstName(Guid friendId)
        {
            return HtmlUtil.TextToHtml(((ICommunicationRecipient)_views[friendId]).FirstName);
        }
    }
}