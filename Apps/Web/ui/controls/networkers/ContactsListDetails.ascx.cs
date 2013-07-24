using System;
using System.Web.UI;
using LinkMe.Apps.Presentation.Domain.Roles.Representatives;
using LinkMe.Apps.Presentation.Domain.Roles.Resumes;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Applications.Facade;
using LinkMe.Web.Domain.Users.Members;
using LinkMe.Web.Helper;
using LinkMe.Web.Members.Friends;
using LinkMe.Web.Service;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public interface IExtraContactDetailsControl
    {
        void DisplayContact(PersonalView view, string itemSuffix);
    }

    public interface IExtraContactDetailsFactory
    {
        Control GetExtraActions(TemplateControl parent);
        Control GetExtraDescription(TemplateControl parent);
    }

    public partial class ContactsListDetails
        : LinkMeUserControl
    {
        public ContactsListDetails()
        {
            CanAccessFriends = true;
            CanAddToFriends = true;
        }

        public bool AlwaysDisplayNames { get; set; }
        public bool CanAccessFriends { get; set; }
        public bool CanAddToFriends { get; set; }

        public void DisplayContact(PersonalView view, Member member, Candidate candidate, Resume resume, string itemSuffix)
        {
            // Image.

            lnkPhoto.NavigateUrl = BuildViewProfileLink(view.Id).ToString();
            imgPhoto.ImageUrl = view.GetPhotoUrlOrDefault().ToString();

            // Full Name.

            lnkFullName.NavigateUrl = BuildViewProfileLink(view.Id).ToString();
            lnkFullName.Text = AlwaysDisplayNames ? HtmlUtil.TextToHtml(((ICommunicationRecipient)view).FullName) : HtmlUtil.TextToHtml(view.GetFullNameDisplayText());

            // Representative.

            switch (view.EffectiveContactDegree)
            {
                case PersonalContactDegree.Representative:
                    phRepresentative.Visible = true;
                    lblRepresentative.Text = view.EffectiveContactDegree.GetRepresentativeDescriptionText();
                    break;

                case PersonalContactDegree.Representee:
                    phRepresentative.Visible = true;
                    lblRepresentative.Text = view.EffectiveContactDegree.GetRepresentativeDescriptionText();
                    break;

                default:
                    phRepresentative.Visible = false;
                    break;
            }

            // Current Title.

            var currentJobsText = resume == null ? null : resume.GetCurrentJobsDisplayHtml();
            if (string.IsNullOrEmpty(currentJobsText))
            {
                phCurrentJobs.Visible = false;
            }
            else
            {
                lblCurrentJobsCaption.Text = GetCurrentJobsCaption(resume);
                ltlCurrentJobs.Text = currentJobsText;
                SetPlaceHolderVisibility(phCurrentJobs, view, PersonalVisibility.CurrentJobs);
            }

            // Candidate Status.

            if (candidate.Status == CandidateStatus.Unspecified)
            {
                phCandidateStatus.Visible = false;
            }
            else
            {
                ltlCandidateStatus.Text = NetworkerFacade.GetCandidateStatusText(candidate.Status);
                SetPlaceHolderVisibility(phCandidateStatus, view, PersonalVisibility.CandidateStatus);
            }

            // Location.

            var locationText = GetDisplayText(member.Address.Location, view);
            if (string.IsNullOrEmpty(locationText))
            {
                phLocation.Visible = false;
            }
            else
            {
                ltlLocation.Text = locationText;
                phLocation.Visible = true;
            }

            // Full Profile.

            lnkFullProfile.NavigateUrl = BuildViewProfileLink(view.Id).ToString();

            // Send Message.

            if (LoggedInUserId == view.Id)
                phThisIsYou.Visible = true;

            // View Friends.

            lnkViewFriends.NavigateUrl = BuildViewFriendsLink(view.Id).ToString();
            SetPlaceHolderVisibility(phViewFriends, view, PersonalVisibility.FriendsList);

            if (!CanAccessFriends)
                phViewFriends.Visible = false;

            // Add To Friends.

            lnkAddToFriends.NavigateUrl = "javascript:populateOverlayPopup('"
                + GetUrlForPage<InvitationPopupContents>() + "', '"
                + InvitationPopupContents.InviteeIdParameter + "=" + view.Id + "');";
            phAddToFriends.Visible = (view.ActualContactDegree != PersonalContactDegree.Self && view.ActualContactDegree != PersonalContactDegree.FirstDegree
                && view.CanAccess(PersonalVisibility.SendInvites));

            if (!CanAddToFriends)
                phAddToFriends.Visible = false;

            // Extra details, if any.

            PopulateDynamicControls(phExtraDescription, view, itemSuffix);
            PopulateDynamicControls(phExtraActions, view, itemSuffix);
        }

        private static void PopulateDynamicControls(Control placeHolder, PersonalView view, string itemSuffix)
        {
            if (placeHolder.Controls.Count == 0)
                return;

            var iExtraActions = placeHolder.Controls[0] as IExtraContactDetailsControl;
            if (iExtraActions != null)
            {
                iExtraActions.DisplayContact(view, itemSuffix);
            }
        }

        public void CreateDynamicControls(IExtraContactDetailsFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            var extraDescription = factory.GetExtraDescription(this);
            if (extraDescription != null)
            {
                phExtraDescription.Controls.Add(extraDescription);
            }

            var extraActions = factory.GetExtraActions(this);
            if (extraActions != null)
            {
                phExtraActions.Controls.Add(extraActions);
            }
        }

        private static void SetPlaceHolderVisibility(Control placeHolder, PersonalView viewer, PersonalVisibility visibility)
        {
            if (placeHolder == null)
                throw new ArgumentNullException("placeHolder");

            placeHolder.Visible = viewer.CanAccess(visibility);
        }

        private static string GetCurrentJobsCaption(Resume resume)
        {
            var currentJobs = resume == null ? null : resume.CurrentJobs;
            return currentJobs != null && currentJobs.Count > 1 ? "Jobs" : "Job";
        }

        protected static string GetDisplayText(LocationReference location, PersonalView viewer)
        {
            return HtmlUtil.TextToHtml(location.ToString(viewer.CanAccess(PersonalVisibility.CountrySubdivision), viewer.CanAccess(PersonalVisibility.Suburb)));
        }

        protected static ReadOnlyUrl BuildViewFriendsLink(Guid contactId)
        {
            return GetUrlForPage<ViewFriendsFriends>(SearchHelper.MemberIdParam, contactId.ToString("n"));
        }

        protected static ReadOnlyUrl BuildViewProfileLink(Guid contactId)
        {
            return GetUrlForPage<ViewFriend>(ViewFriend.FriendIdParameter, contactId.ToString("n"));
        }
    }
}
