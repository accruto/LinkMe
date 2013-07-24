using System;
using System.Web.UI.WebControls;
using LinkMe.Domain.Contacts;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class VisibilityBasicSettings : LinkMeUserControl
    {
        protected const string TEXT_HIGHLY_VISIBLE_EXPLANATION =
            "Selecting highly visible will allow all members to see most of your details. Only your friends and their friends will be able to see your resume, phone numbers, email address and suburb.";

        protected const string TEXT_MODERATELY_VISIBLE_EXPLANATION =
            "Selecting moderately visible will allow all members to see your name, photo, country and your list of friends. They will also be able to send you messages and friend invitations. Your friends and their friends will be able to see most of your other details. Your resume will be hidden from everyone.";

        protected const string TEXT_LESS_VISIBLE_EXPLANATION =
            "Selecting less visible will allow your friends and their friends to see your name. Your friends alone will be able to see most of your other details. Your resume, phone numbers, email address and suburb will be hidden from everyone.";

        protected const string TEXT_INVISIBLE_EXPLANATION =
            "Selecting invisible will mean that only your friends can see your name. All of your other details will be hidden from everyone.";

        protected const string TEXT_CUSTOM_VISIBILITY_EXPLANATION =
            "You have customised your visibility settings using the advanced settings page. Visit the advanced settings page to review your settings.";

        protected const string MESSAGE_ABOUT_TO_LOSE_CUSTOM_SETTINGS =
            "Warning - Selecting this option will override the advanced settings that you previously applied. Do you want to continue?";

        public bool ShowAdvancedSettingsLink = true;

        public bool HasASelectedOption
        {
            get 
            {
                return (rdoHighlyVisible.Checked || rdoModeratelyVisible.Checked || rdoLessVisible.Checked || rdoInvisible.Checked);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                LoadFormData();
            }

            SetUpRadioButtonClickJavascript();
        }

        private void SetUpRadioButtonClickJavascript()
        {
            rdoHighlyVisible.Attributes["onclick"] = "BasicVisibilityRadioButtonClicked(rdoHiVis);";
            rdoModeratelyVisible.Attributes["onclick"] = "BasicVisibilityRadioButtonClicked(rdoMidVis);";
            rdoLessVisible.Attributes["onclick"] = "BasicVisibilityRadioButtonClicked(rdoLoVis);";
            rdoInvisible.Attributes["onclick"] = "BasicVisibilityRadioButtonClicked(rdoInVis);";
        }

        private void LoadFormData()
        {
            RadioButton selectedButton = null;

            if (LoggedInMember.VisibilitySettings.Personal.PublicVisibility == PersonalVisibilitySettings.HighPublic &&
                LoggedInMember.VisibilitySettings.Personal.SecondDegreeVisibility == PersonalVisibilitySettings.HighSecondDegree &&
                LoggedInMember.VisibilitySettings.Personal.FirstDegreeVisibility == PersonalVisibilitySettings.HighFirstDegree)
            {
                selectedButton = rdoHighlyVisible;
            }
            else if (LoggedInMember.VisibilitySettings.Personal.PublicVisibility == PersonalVisibilitySettings.ModeratePublic &&
                LoggedInMember.VisibilitySettings.Personal.SecondDegreeVisibility == PersonalVisibilitySettings.ModerateSecondDegree &&
                LoggedInMember.VisibilitySettings.Personal.FirstDegreeVisibility == PersonalVisibilitySettings.ModerateFirstDegree)
            {
                selectedButton = rdoModeratelyVisible;
            }
            else if (LoggedInMember.VisibilitySettings.Personal.PublicVisibility == PersonalVisibilitySettings.LessPublic &&
                LoggedInMember.VisibilitySettings.Personal.SecondDegreeVisibility == PersonalVisibilitySettings.LessSecondDegree &&
                LoggedInMember.VisibilitySettings.Personal.FirstDegreeVisibility == PersonalVisibilitySettings.LessFirstDegree)
            {
                selectedButton = rdoLessVisible;
            }
            else if (LoggedInMember.VisibilitySettings.Personal.PublicVisibility == PersonalVisibilitySettings.InvisiblePublic &&
                LoggedInMember.VisibilitySettings.Personal.SecondDegreeVisibility == PersonalVisibilitySettings.InvisibleSecondDegree &&
                LoggedInMember.VisibilitySettings.Personal.FirstDegreeVisibility == PersonalVisibilitySettings.InvisibleFirstDegree)
            {
                selectedButton = rdoInvisible;
            }

            if (selectedButton != null)
            {
                selectedButton.Checked = true;
            }
        }

        public void SaveFormData()
        {
            if (rdoHighlyVisible.Checked)
            {
                LoggedInMember.VisibilitySettings.Personal.PublicVisibility = PersonalVisibilitySettings.HighPublic;
                LoggedInMember.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibilitySettings.HighFirstDegree;
                LoggedInMember.VisibilitySettings.Personal.SecondDegreeVisibility = PersonalVisibilitySettings.HighSecondDegree;
            }
            else if (rdoModeratelyVisible.Checked)
            {
                LoggedInMember.VisibilitySettings.Personal.PublicVisibility = PersonalVisibilitySettings.ModeratePublic;
                LoggedInMember.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibilitySettings.ModerateFirstDegree;
                LoggedInMember.VisibilitySettings.Personal.SecondDegreeVisibility = PersonalVisibilitySettings.ModerateSecondDegree;
            }
            else if (rdoLessVisible.Checked)
            {
                LoggedInMember.VisibilitySettings.Personal.PublicVisibility = PersonalVisibilitySettings.LessPublic;
                LoggedInMember.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibilitySettings.LessFirstDegree;
                LoggedInMember.VisibilitySettings.Personal.SecondDegreeVisibility = PersonalVisibilitySettings.LessSecondDegree;
            }
            else if (rdoInvisible.Checked)
            {
                LoggedInMember.VisibilitySettings.Personal.PublicVisibility = PersonalVisibilitySettings.InvisiblePublic;
                LoggedInMember.VisibilitySettings.Personal.FirstDegreeVisibility = PersonalVisibilitySettings.InvisibleFirstDegree;
                LoggedInMember.VisibilitySettings.Personal.SecondDegreeVisibility = PersonalVisibilitySettings.InvisibleSecondDegree;
            }
        }
    }
}