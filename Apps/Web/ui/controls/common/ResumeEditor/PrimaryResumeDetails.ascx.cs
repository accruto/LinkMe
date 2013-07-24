using System;
using System.Web.UI.WebControls;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using LinkMe.Utility.Utilities;

namespace LinkMe.Web.UI.Controls.Common.ResumeEditor
{
    public partial class PrimaryResumeDetails
        : LinkMeUserControl
    {
        private Member _member;
        private Candidate _candidate;
        private bool _canEdit;
        private bool _canEditContactDetails;
        private bool _displayPhone;

        public bool StartEditingOnLoad { get; set; }

        protected bool CanEdit
        {
            get { return _canEdit; }
        }

        protected bool CanEditContactDetails
        {
            get { return _canEditContactDetails; }
        }

        protected bool DisplayPhone
        {
            get { return _displayPhone; }
        }

        #region Static methods

        protected static string HideIf(bool hide)
        {
            return "display: " + (hide ? "none;" : "block;");
        }

        #endregion

        public void DisplayMemberToSelf(Member member, Candidate candidate, bool canEdit, bool canEditContactDetails)
        {
            _displayPhone = true;
            DisplayMemberInternal(member, candidate, canEdit, canEditContactDetails);
        }

        public void DisplayMemberToOther(Member member, Candidate candidate)
        {
            DisplayMemberInternal(member, candidate, false, false);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ucIndustryList.SelectionMode = ListSelectionMode.Multiple;
            ucIndustryList.DisplayAllIndustries();

            ucLocationCountry.Location = ucLocation;
        }

        protected override void OnLoad(EventArgs e)
        {
            ucLocation.Text = GetLocation();
            base.OnLoad(e);
        }

        protected string GetFirstName()
        {
            return HtmlUtil.TextToHtml(_member.FirstName);
        }

        protected string GetLastName()
        {
            return HtmlUtil.TextToHtml(_member.LastName);
        }

        protected string GetLastNameSuffix()
        {
            return _member.LastName.GetNamePossessiveSuffix();
        }

        protected string GetMobilePhone()
        {
            var phoneNumber = _member.GetPhoneNumber(PhoneNumberType.Mobile);
            return (DisplayPhone && phoneNumber != null ? HtmlUtil.TextToHtml(phoneNumber.Number) : null);
        }

        protected string GetWorkPhone()
        {
            var phoneNumber = _member.GetPhoneNumber(PhoneNumberType.Work);
            return (DisplayPhone && phoneNumber != null ? HtmlUtil.TextToHtml(phoneNumber.Number) : null);
        }

        protected string GetHomePhone()
        {
            var phoneNumber = _member.GetPhoneNumber(PhoneNumberType.Home);
            return (DisplayPhone && phoneNumber != null ? HtmlUtil.TextToHtml(phoneNumber.Number) : null);
        }

        protected string GetEmail()
        {
            return HtmlUtil.TextToHtml(_member.GetBestEmailAddress().Address);
        }

        protected string GetIndustryDisplayNames()
        {
            // The separator here must be the same as the LIST_ITEM_DISPLAY_SEPARATOR constant in ResumeEditor.js.
            return _candidate.GetIndustriesDisplayText(StringUtils.HTML_LINE_BREAK);
        }

        protected bool HaveNoIndustries()
        {
            return _candidate.Industries.IsNullOrEmpty();
        }

        private void DisplayMemberInternal(Member member, Candidate candidate, bool canEdit, bool canEditContactDetails)
        {
            if (member == null)
                throw new ArgumentNullException("member");
            if (candidate == null)
                throw new ArgumentNullException("candidate");

            _member = member;
            _candidate = candidate;
            _canEdit = canEdit;
            _canEditContactDetails = canEditContactDetails;

            if (CanEdit)
            {
                ucLocationCountry.SelectedValue = member.Address.Location.CountrySubdivision.Country;
            }
        }

        protected string GetLocation()
        {
            string location = String.Empty;

            if (_member != null)
                location = _member.Address.Location.ToString();

            return HtmlUtil.TextToHtml(location);
        }

        protected string GetCountry()
        {
            return _member.Address.Location.CountrySubdivision.Country.Name;
        }
    }
}
