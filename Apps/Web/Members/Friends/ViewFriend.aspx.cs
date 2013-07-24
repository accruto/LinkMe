using System;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.Representatives;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Members.Friends.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Validation;
using LinkMe.Web.Content;
using LinkMe.Web.Domain.Roles.Networking;
using LinkMe.Web.Domain.Users.Members;
using LinkMe.Web.Helper;
using LinkMe.Web.UI;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Web.UI.Registered.Networkers;
using InvalidOperationException=System.InvalidOperationException;

namespace LinkMe.Web.Members.Friends
{
    public partial class ViewFriend
        : LinkMePage
    {
        private static readonly IMemberFriendsCommand _memberFriendsCommand = Container.Current.Resolve<IMemberFriendsCommand>();
        private static readonly IMemberFriendsQuery _memberFriendsQuery = Container.Current.Resolve<IMemberFriendsQuery>();
        private static readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();
        private static readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private static readonly ICandidatesQuery _candidatesQuery = Container.Current.Resolve<ICandidatesQuery>();
        private static readonly IResumesQuery _resumesQuery = Container.Current.Resolve<IResumesQuery>();

        public const string FriendIdParameter = "friendId";

        private PersonalView _view;

        protected string Degree { get; private set; }
        protected string Institution { get; private set; }
        protected string CompletedDate { get; private set; }
        protected string PreviousEmployer { get; private set; }
        protected string PreviousTitle { get; private set; }
        protected Member ViewedMember { get; private set; }
        protected Candidate ViewedCandidate { get; private set; }
        protected Resume ViewedResume { get; private set; }

        protected override void  OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Networking);

            btnRemove.Click += btnRemove_Click;
            lnkAcceptInvitation.Click += lnkAcceptInvitation_Click;
            lnkIgnoreInvitation.Click += lnkIgnoreInvitation_Click;
            btnRemoveRepresentative.Click += btnRemoveRepresentative_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Guid friendId;
            try
            {
                friendId = ParseUtil.ParseUserInputGuid(Request.QueryString[FriendIdParameter], "friend ID");
            }
            catch (UserException ex)
            {
                AddError(ex.Message);
                return;
            }

            ViewedMember = _membersQuery.GetMember(friendId);
            if (ViewedMember == null)
            {
                AddError("There is no member with ID " + friendId);
                return;
            }

            ViewedCandidate = _candidatesQuery.GetCandidate(friendId);
            ViewedResume = ViewedCandidate.ResumeId == null ? null : _resumesQuery.GetResume(ViewedCandidate.ResumeId.Value);

            DisplayMember();
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

        private void DisplayMember()
        {
            _view = _memberViewsQuery.GetPersonalView(LoggedInUserId, ViewedMember);

            AddTitleValue(ViewedMember.FirstName.MakeNamePossessive());

            DisplayDetails();
            DisplayFriends();
            DisplayContactDetails();
            DisplayWorkStatus();
            DisplayCareer();
            DisplayOccupation();
            DisplayEducation();
            DisplayWorkHistory();
            DisplayInterestsAffiliations();
        }

        private void DisplayDetails()
        {
            // We want to display an error message if this person cannot view details.

            var canSeeName = _view.CanAccess(PersonalVisibility.Name);
            mainContactDetails.Visible = canSeeName;
            if (!canSeeName)
                AddError(ValidationErrorMessages.ATTEMPTED_TO_VIEW_NETWORKER_WITHOUT_ACCESS_TO_NAME);

            imgPhoto.ImageUrl = _view.GetPhotoUrlOrDefault().ToString();

            // Invitation.

            var invitation = _memberFriendsQuery.GetFriendInvitation(ViewedMember.Id, LoggedInMember.Id);

            phInvite.Visible = invitation == null
                && _view.CanAccess(PersonalVisibility.SendInvites)
                    && _view.ActualContactDegree != PersonalContactDegree.Self && _view.ActualContactDegree != PersonalContactDegree.FirstDegree;

            phRemoveFriend.Visible = _view.ActualContactDegree == PersonalContactDegree.FirstDegree;
            phInvitation.Visible = invitation != null && invitation.Status == RequestStatus.Pending;

            // Representative.

            phRepresentatives.Visible = false;
            switch (_view.EffectiveContactDegree)
            {
                case PersonalContactDegree.Representative:
                case PersonalContactDegree.Representee:
                    phRepresentatives.Visible = true;
                    break;
            }

            phAge.Visible = _view.CanAccess(PersonalVisibility.Age)
                && string.Compare(0.ToString(), GetAgeText()) != 0;
            phGender.Visible = _view.CanAccess(PersonalVisibility.Gender)
                && ViewedMember.Gender != Gender.Unspecified;
            phCountry.Visible = _view.CanAccess(PersonalVisibility.Country)
                && ViewedMember.Address.Location.CountrySubdivision.Country != null;

            var location = ViewedMember.Address.Location.ToString(_view.CanAccess(PersonalVisibility.CountrySubdivision), _view.CanAccess(PersonalVisibility.Suburb));
            if (string.IsNullOrEmpty(location))
            {
                phLocation.Visible = false;
            }
            else 
            {
                phLocation.Visible = true;
                ltlLocation.Text = HtmlUtil.TextToHtml(location);
            }
        }

        private void DisplayFriends()
        {
            phFriends.Visible = _view.CanAccess(PersonalVisibility.FriendsList);
            ucMiniFriendsList.FriendsOwner = ViewedMember;
        }

        private void DisplayContactDetails()
        {
            if (_view.CanAccess(PersonalVisibility.PhoneNumbers))
            {
                phPhone.Visible = ViewedMember.PhoneNumbers != null && ViewedMember.PhoneNumbers.Count > 0;
            }
            else
            {
                phPhone.Visible = false;
            }

            phEmailAddress.Visible = _view.CanAccess(PersonalVisibility.EmailAddress);
            phContactDetails.Visible = phPhone.Visible || phEmailAddress.Visible;
        }

        private void DisplayWorkStatus()
        {
            phWorkStatus.Visible = _view.CanAccess(PersonalVisibility.CandidateStatus)
                && ViewedCandidate.Status != CandidateStatus.Unspecified;
            phDesiredJob.Visible = _view.CanAccess(PersonalVisibility.DesiredJob)
                && !string.IsNullOrEmpty(ViewedCandidate.DesiredJobTitle);

            phWorkStatusSection.Visible = phWorkStatus.Visible || phDesiredJob.Visible;
        }

        private void DisplayCareer()
        {
            ahrefViewResume.Visible = _view.CanAccess(PersonalVisibility.Resume);
            ahrefViewResume.HRef = GetUrlForPage<ResumePreview>(ResumePreview.MemberIdParam, ViewedMember.Id.ToString()).ToString();
        }

        private void DisplayOccupation()
        {
            if (_view.CanAccess(PersonalVisibility.CurrentJobs))
                phOccupation.Visible = phCurrentTitles.Visible = ViewedResume != null && !ViewedResume.CurrentJobs.IsNullOrEmpty();
            else
                phOccupation.Visible = false;
        }

        private void DisplayEducation()
        {
            if (!ViewedResume.HasSchools())
            {
                phEducation.Visible = false;
                return;
            }

            var university = ViewedResume.Schools[0];
            if (university != null && _view.CanAccess(PersonalVisibility.Education))
            {
                Degree = university.Degree;
                phDegree.Visible = !string.IsNullOrEmpty(Degree);

                Institution = university.Institution;
                phInstitution.Visible = !string.IsNullOrEmpty(Institution);

                CompletedDate = university.GetCompletionDateDisplayText();
                phDegreeCompletedDate.Visible = !string.IsNullOrEmpty(CompletedDate);

                if (phDegree.Visible || phInstitution.Visible || phDegreeCompletedDate.Visible)
                    phEducation.Visible = true;
                else
                    phEducation.Visible = false;
            }
            else
            {
                phEducation.Visible = false;
            }
        }
        
        private void DisplayWorkHistory()
        {
            var oldJob = ViewedResume != null ? ViewedResume.PreviousJob : null;

            if (oldJob != null && _view.CanAccess(PersonalVisibility.PreviousJob))
            {
                PreviousEmployer = oldJob.Company;
                phPreviousEmployer.Visible = !string.IsNullOrEmpty(PreviousEmployer);

                PreviousTitle = oldJob.Title;
                phPreviousJob.Visible = !string.IsNullOrEmpty(PreviousTitle);

                if (phPreviousEmployer.Visible || phPreviousJob.Visible)
                    phWorkHistory.Visible = true;
                else
                    phWorkHistory.Visible = false;
            }
            else
            {
                phWorkHistory.Visible = false;
            }
        }

        private void DisplayInterestsAffiliations()
        {
            wcInterests.Content = ViewedResume.HasInterests() ? ViewedResume.Interests : string.Empty;

            phAffiliations.Visible = ViewedResume.HasAffiliations() && _view.CanAccess(PersonalVisibility.Affiliations);
            phInterests.Visible = ViewedResume.HasInterests() && _view.CanAccess(PersonalVisibility.Interests);

            phInterestsAffiliations.Visible = phAffiliations.Visible || phInterests.Visible;
        }

        protected string GetAgeText()
        {
            return ViewedMember.DateOfBirth.GetAgeDisplayText();
        }

        protected string GetRepresentativeText()
        {
            return HtmlUtil.TextToHtml(_view.EffectiveContactDegree.GetRepresentativeDescriptionText(ViewedMember.FirstName));
        }

        protected string GetRepresentativeRemoveText()
        {
            switch (_view.EffectiveContactDegree)
            {
                case PersonalContactDegree.Representative:
                    return "Remove representative";

                case PersonalContactDegree.Representee:
                    return "Remove acting as representative";

                default:
                    return string.Empty;
            }
        }

        protected string GetRepresentativeConfirmText()
        {
            switch (_view.EffectiveContactDegree)
            {
                case PersonalContactDegree.Representative:
                    return "Do you really want to stop "
                        + HtmlUtil.TextToHtml(ViewedMember.FirstName)
                            + " from acting as your representative?";

                case PersonalContactDegree.Representee:
                    return "Do you really want to stop representing "
                        + HtmlUtil.TextToHtml(ViewedMember.FirstName) + "?";

                default:
                    return string.Empty;
            }
        }

        protected ReadOnlyUrl GetViewFriendsUrl()
        {
            return GetUrlForPage<ViewFriendsFriends>(SearchHelper.MemberIdParam, ViewedMember.Id.ToString());
        }

        protected string FilthyHackToDisplayEmailAddress()
        {
            /* BF 19/9/2007: This is the filthiest of hacks to get around long email addresses breaking IE.
             * It's probably the worst hack I've had to do at LinkMe.. and that's saying something.
             * */
            var email = ViewedMember.GetBestEmailAddress().Address;
            if (email.Length > 30)
            {
                email = email.Replace("@", "@ ");
            }

            return HtmlUtil.TextToHtml(email);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            _memberFriendsCommand.DeleteFriend(LoggedInMember.Id, ViewedMember.Id);
            NavigationManager.Redirect<ViewFriends>();
        }

        private void lnkAcceptInvitation_Click(object sender, EventArgs e)
        {
            try
            {
                var invitation = _memberFriendsQuery.GetFriendInvitation(ViewedMember.Id, LoggedInMember.Id);
                _memberFriendsCommand.AcceptInvitation(LoggedInMember.Id, invitation);

                var acceptMsg = invitation.GetInvitationAcceptedHtml(ViewedMember);

                phInvitation.Visible = false;
                phRemoveFriend.Visible = true;

                DisplayMember(); // Redisplay the details now that the viewer has 1st-degree access.

                AddConfirm(acceptMsg, false);
            }
            catch (UserException ex)
            {
                AddError(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                AddError(ex.Message);
            }
        }

        private void lnkIgnoreInvitation_Click(object sender, EventArgs e)
        {
            try
            {
                var invitation = _memberFriendsQuery.GetFriendInvitation(ViewedMember.Id, LoggedInMember.Id);
                if (invitation != null)
                    _memberFriendsCommand.RejectInvitation(invitation);

                NavigationManager.Redirect<ViewFriends>();
            }
            catch (UserException ex)
            {
                AddError(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                AddError(ex.Message);
            }
        }

        private void btnRemoveRepresentative_Click(object sender, EventArgs e)
        {
            switch (_view.EffectiveContactDegree)
            {
                case PersonalContactDegree.Representative:
                    _memberFriendsCommand.DeleteRepresentative(LoggedInUser.Id, ViewedMember.Id);
                    break;

                case PersonalContactDegree.Representee:
                    _memberFriendsCommand.DeleteRepresentative(ViewedMember.Id, LoggedInUser.Id);
                    break;
            }

            NavigationManager.Redirect<ViewFriend>(FriendIdParameter, ViewedMember.Id.ToString());
        }
    }
}