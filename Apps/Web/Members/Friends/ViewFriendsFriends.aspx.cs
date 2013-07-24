using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Content;
using LinkMe.Web.Domain.Users.Members;
using LinkMe.Web.Helper;
using LinkMe.Web.UI;

namespace LinkMe.Web.Members.Friends
{
    public partial class ViewFriendsFriends
        : LinkMePage
    {
        private readonly IMemberContactsQuery _memberContactsQuery = Container.Current.Resolve<IMemberContactsQuery>();
        private readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();
        private readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private readonly ICandidatesQuery _candidatesQuery = Container.Current.Resolve<ICandidatesQuery>();
        private readonly IResumesQuery _resumesQuery = Container.Current.Resolve<IResumesQuery>();

        private const string ResultParameter = "result";

        private int _ownersFriendCount;
        protected Member OwnerOfFriends { get; private set; }

        #region Page Actions

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Networking);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ucPagingBarTop.ResultsPerPage = ucPagingBarBottom.ResultsPerPage = ApplicationContext.Instance.GetIntProperty(ApplicationContext.FRIENDS_PER_PAGE);
            ucPagingBarTop.StartIndexParam = ucPagingBarBottom.StartIndexParam = ResultParameter;

            if (!Page.IsPostBack)
            {
                try
                {
                    // Get the owner of the contacts being viewed.

                    var memberId = ParseUtil.ParseUserInputGuid(Request.QueryString[SearchHelper.MemberIdParam], "member ID");
                    OwnerOfFriends = _membersQuery.GetMember(memberId);
                    if (OwnerOfFriends == null)
                        throw new UserException("There is no networker with the specified ID.");

                    IList<Guid> friendIds = _memberContactsQuery.GetFirstDegreeContacts(OwnerOfFriends.Id);
                    _ownersFriendCount = friendIds.Count;

                    // Get the list of friends to display on this page that the viewer can actually see.

                    friendIds = _memberContactsQuery.GetContacts(LoggedInUserId.Value, friendIds);
                    friendIds = friendIds.Skip(ucPagingBarTop.StartIndex).Take(ucPagingBarTop.ResultsPerPage).ToList();

                    // Get the members and networker.

                    var members = _membersQuery.GetMembers(friendIds);
                    var candidates = _candidatesQuery.GetCandidates(friendIds);
                    var resumes = _resumesQuery.GetResumes(from c in candidates where c.ResumeId != null select c.ResumeId.Value);
                    var views = _memberViewsQuery.GetPersonalViews(LoggedInUserId, members);

                    // Get the contact degree for the owner.

                    var view = _memberViewsQuery.GetPersonalView(LoggedInUserId.Value, OwnerOfFriends);
                    SetPageVisibility(view.EffectiveContactDegree, view.ActualContactDegree);

                    ucPagingBarTop.InitPagesList(GetResultUrl(), _ownersFriendCount, false);
                    ucPagingBarBottom.InitPagesList(GetResultUrl(), _ownersFriendCount, false);
                    contactsListControl.DisplayContacts(friendIds, views, members, candidates, resumes);
                }
                catch (UserException ex)
                {
                    AddError(ex.Message);
                }
            }
        }

        #endregion

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

        private void SetPageVisibility(PersonalContactDegree effectiveOwnerDegreeFromViewer, PersonalContactDegree actualOwnerDegreeFromViewer)
        {
            var view = new PersonalView(OwnerOfFriends, effectiveOwnerDegreeFromViewer, actualOwnerDegreeFromViewer);

            imgPhoto.Width = Constants.ThumbnailMaxSize.Width;
            imgPhoto.Height = Constants.ThumbnailMaxSize.Height;

            imgPhoto.ImageUrl = view.GetPhotoUrlOrDefault().ToString();

            var canSendInvites = view.CanAccess(PersonalVisibility.SendInvites);
            phAddToFriends.Visible = canSendInvites && actualOwnerDegreeFromViewer != PersonalContactDegree.Self
                && actualOwnerDegreeFromViewer != PersonalContactDegree.FirstDegree;
        }

        private Url GetResultUrl()
        {
            Url url = new ApplicationUrl(Request.Url.AbsolutePath);
            url.QueryString.Add(SearchHelper.MemberIdParam, OwnerOfFriends.Id.ToString());
            return url;
        }

        protected ReadOnlyUrl BuildViewProfileLink()
        {
            return GetUrlForPage<ViewFriend>(ViewFriend.FriendIdParameter, OwnerOfFriends.Id.ToString());
        }

        protected string DescribeFriendsFriendsCount()
        {
            return _ownersFriendCount.GetFriendsCountDisplayText(OwnerOfFriends);
        }
    }
}