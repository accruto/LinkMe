using System;
using System.Web.UI.WebControls;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class EmployerPrivacy : LinkMeUserControl
    {
        private readonly ICommunitiesQuery _communitiesQuery = Container.Current.Resolve<ICommunitiesQuery>();

        private bool _enableShowResume = true;
        private bool _showPrivacyText = true;
        private bool _showPhotoCheckBox = true;
        private bool _renderFieldsetElement = true;
        private bool _labelsOnLeft;

        public bool EnableShowResume
        {
            set { _enableShowResume = value; }
        }

        public CheckBox FullResume
        {
            get { return chkRecentEmployers; }
        }

        public CheckBox AnonymousResume
        {
            get { return chkAnonResume; }
        }

        /// <summary>
        /// We don't want to show this on the join process
        /// </summary>
        public bool ShowPhotoCheckBox
        {
            get { return _showPhotoCheckBox; }
            set { _showPhotoCheckBox = value; }
        }

        public bool ShowPrivacyText
        {
            get { return _showPrivacyText; }
            set { _showPrivacyText = value; }
        }

        public bool RenderFieldsetElement
        {
            get { return _renderFieldsetElement; }
            set
            {
                _renderFieldsetElement = value;
                phFieldsetOpeningTag.Visible = _renderFieldsetElement;
                phFieldsetClosingTag.Visible = _renderFieldsetElement;
            }
        }

        public bool LabelsOnLeft
        {
            get { return _labelsOnLeft; }
            set { _labelsOnLeft = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            LoadFormData();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (_enableShowResume)
            {
                chkAnonResume.Attributes.Add("onclick", "SetEmployerPrivacyCheckBoxesEnabled();SetEmployerPrivacyCheckBoxesValues();SetRecentEmployersMessageVisibility();");
                chkRecentEmployers.Attributes.Add("onclick", "SetRecentEmployersMessageVisibility();");
            }
            else
            {
                chkRecentEmployers.Checked = chkRecentEmployers.Enabled = false;
                chkAnonResume.Checked = chkAnonResume.Enabled = false;
            }

            phPrivacyBlurb.Visible = ShowPrivacyText;
            phShowPhotoOption.Visible = ShowPhotoCheckBox;

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "SetEmployerPrivacyCheckBoxEnabled", "SetEmployerPrivacyCheckBoxesEnabled();", true);
        }

        public void SaveFormData()
        {
            ProfessionalVisibility access = ProfessionalVisibility.None;

            if (chkAnonResume.Checked)
            {
                access = ProfessionalVisibility.Resume;

                if (chkName.Checked)
                    access |= ProfessionalVisibility.Name;

                if (chkPhone.Checked)
                    access |= ProfessionalVisibility.PhoneNumbers;

                if (chkPhoto.Checked)
                    access |= ProfessionalVisibility.ProfilePhoto;

                if (chkRecentEmployers.Checked)
                    access |= ProfessionalVisibility.RecentEmployers;

                if (chkCommunity.Checked)
                    access |= ProfessionalVisibility.Communities;
            }

            LoggedInMember.VisibilitySettings.Professional.EmploymentVisibility = access;
        }

        private void LoadFormData()
        {
            // Set up the community related controls first.

            var affiliateId = LoggedInMember.AffiliateId;
            if (affiliateId != null)
            {
                var community = _communitiesQuery.GetCommunity(affiliateId.Value);
                if (community != null)
                {
                    phCommunity.Visible = true;
                    chkCommunity.Text = community.Name + " logo";
                    litCommunity.Text = community.Name;
                    ucCommunityCandidateImage.Initialise(community.Id);
                }
                else
                {
                    phCommunity.Visible = false;
                }
            }
            else
            {
                phCommunity.Visible = false;
            }

            ProfessionalVisibility ea = LoggedInMember.VisibilitySettings.Professional.EmploymentVisibility;
            bool accessResume = (ea & ProfessionalVisibility.Resume) != 0;
            chkAnonResume.Checked = accessResume;
            if (accessResume)
            {
                chkName.Checked = (ea & ProfessionalVisibility.Name) != 0;
                chkPhone.Checked = (ea & ProfessionalVisibility.PhoneNumbers) != 0;
                chkPhoto.Checked = (ea & ProfessionalVisibility.ProfilePhoto) != 0;
                chkRecentEmployers.Checked = (ea & ProfessionalVisibility.RecentEmployers) != 0;
                chkCommunity.Checked = (ea & ProfessionalVisibility.Communities) != 0;

                pnlRecentEmployersMessage.Style["display"] =
                    (chkAnonResume.Checked && !chkRecentEmployers.Checked) ? "block" : "none";
            }
        }
    }
}
