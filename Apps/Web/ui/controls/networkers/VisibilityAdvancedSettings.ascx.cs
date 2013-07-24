using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Views;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class VisibilityAdvancedSettings : LinkMeUserControl
    {
        protected readonly PersonalContactDegree[] _levels = {
                                                       PersonalContactDegree.Self,
                                                       PersonalContactDegree.FirstDegree,
                                                       PersonalContactDegree.SecondDegree,
                                                       PersonalContactDegree.Public
                                                   };

        protected static readonly PersonalVisibility[] HighlyVisible = { PersonalVisibilitySettings.HighFirstDegree, PersonalVisibilitySettings.HighSecondDegree, PersonalVisibilitySettings.HighPublic };
        protected static readonly PersonalVisibility[] ModeratelyVisible = { PersonalVisibilitySettings.ModerateFirstDegree, PersonalVisibilitySettings.ModerateSecondDegree, PersonalVisibilitySettings.ModeratePublic };
        protected static readonly PersonalVisibility[] LessVisible = { PersonalVisibilitySettings.LessFirstDegree, PersonalVisibilitySettings.LessSecondDegree, PersonalVisibilitySettings.LessPublic };
        protected static readonly PersonalVisibility[] Invisible = { PersonalVisibilitySettings.InvisibleFirstDegree, PersonalVisibilitySettings.InvisibleSecondDegree, PersonalVisibilitySettings.InvisiblePublic };

        protected static readonly PersonalVisibility[][] AllVisibilities = { Invisible, LessVisible, ModeratelyVisible, HighlyVisible };

        private IList<VisibilityCheckBoxRow> _rows;

        #region VisibilityCheckBoxRows
        
        protected VisibilityCheckBoxRow rowName;
        protected VisibilityCheckBoxRow rowPhoto;
        protected VisibilityCheckBoxRow rowGenderAge;
        protected VisibilityCheckBoxRow rowResume;
        protected VisibilityCheckBoxRow rowCandidateStatus;
        protected VisibilityCheckBoxRow rowDesiredJob;
        protected VisibilityCheckBoxRow rowPhone;
        protected VisibilityCheckBoxRow rowEmail;
        protected VisibilityCheckBoxRow rowSuburb;
        protected VisibilityCheckBoxRow rowCountrySubdivision;
        protected VisibilityCheckBoxRow rowCountry;
        protected VisibilityCheckBoxRow rowSendInvites;
        protected VisibilityCheckBoxRow rowSendMessages;
        protected VisibilityCheckBoxRow rowFriendsList;
        protected VisibilityCheckBoxRow rowCurrentJob;
        protected VisibilityCheckBoxRow rowEducation;
        protected VisibilityCheckBoxRow rowInterestsAffiliations;
        
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InitialiseForm();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                LoadFormData();
            }
        }

        public IList<VisibilityCheckBoxRow> Rows
        {
            get { return _rows; }
        }

        private void InitialiseForm()
        {
            rowName.DisplayName = "Name";
            rowName.ContactAccessFlag = PersonalVisibility.Name;
            rowName.AlwaysFirstDegree = true;

            rowPhoto.DisplayName = "Photo";
            rowPhoto.ContactAccessFlag = PersonalVisibility.Photo;

            rowGender.DisplayName = "Gender";
            rowGender.ContactAccessFlag = PersonalVisibility.Gender;

            rowAge.DisplayName = "Age";
            rowAge.ContactAccessFlag = PersonalVisibility.Age;

            rowResume.DisplayName = "Resume";
            rowResume.ContactAccessFlag = PersonalVisibility.Resume;
            rowResume.NeverPublic = true;
            rowResume.NeverSecondDegree = true;

            rowCandidateStatus.DisplayName = "Work status";
            rowCandidateStatus.ContactAccessFlag = PersonalVisibility.CandidateStatus;

            rowDesiredJob.DisplayName = "Desired job";
            rowDesiredJob.ContactAccessFlag = PersonalVisibility.DesiredJob;

            rowPhone.DisplayName = "Phone numbers";
            rowPhone.NeverPublic = true;
            rowPhone.ContactAccessFlag = PersonalVisibility.PhoneNumbers;
            
            rowEmail.DisplayName = "Email address";
            rowEmail.NeverPublic = true;
            rowEmail.ContactAccessFlag = PersonalVisibility.EmailAddress;

            rowSuburb.DisplayName = "Suburb";
            rowSuburb.NeverPublic = true;
            rowSuburb.ContactAccessFlag = PersonalVisibility.Suburb;

            rowCountrySubdivision.DisplayName = "State";
            rowCountrySubdivision.ContactAccessFlag = PersonalVisibility.CountrySubdivision;

            rowCountry.DisplayName = "Country";
            rowCountry.ContactAccessFlag = PersonalVisibility.Country;

            rowSendInvites.DisplayName = "Can invite me to be a friend";
            rowSendInvites.NeverFirstDegree = true;
            rowSendInvites.ContactAccessFlag = PersonalVisibility.SendInvites;

            rowSendMessages.DisplayName = "Can send me messages";
            rowSendMessages.ContactAccessFlag = PersonalVisibility.SendMessages;

            rowFriendsList.DisplayName = "Can see my list of friends";
            rowFriendsList.ContactAccessFlag = PersonalVisibility.FriendsList;

            rowCurrentJob.DisplayName = "Current employer and job title";
            rowCurrentJob.ContactAccessFlag = PersonalVisibility.CurrentJobs;

            rowPreviousJob.DisplayName = "Previous employer and job title";
            rowPreviousJob.ContactAccessFlag = PersonalVisibility.PreviousJob;

            rowEducation.DisplayName = "Education history";
            rowEducation.ContactAccessFlag = PersonalVisibility.Education;

            rowInterests.DisplayName = "Interests";
            rowInterests.ContactAccessFlag = PersonalVisibility.Interests;

            rowAffiliations.DisplayName = "Affiliations";
            rowAffiliations.ContactAccessFlag = PersonalVisibility.Affiliations;

            _rows = GetRowsInDisplayOrder();
        }

        private IList<VisibilityCheckBoxRow> GetRowsInDisplayOrder()
        {
            var ordered = new List<VisibilityCheckBoxRow>();

            foreach (var control in Controls)
            {
                var row = control as VisibilityCheckBoxRow;
                if (row != null)
                {
                    ordered.Add(row);
                }
            }

            if (ordered.Count == 0)
                throw new ApplicationException("No VisibilityCheckBoxRow controls were found.");

            return ordered;
        }

        private void LoadFormData()
        {
            Member member = LoggedInMember;

            foreach (VisibilityCheckBoxRow row in Rows)
            {
                PersonalContactDegree level = PersonalContactDegree.Self;
                var viewer = new PersonalView(member, PersonalContactDegree.FirstDegree, PersonalContactDegree.FirstDegree);
                if (viewer.CanAccess(row.ContactAccessFlag))
                {
                    level++;

                    viewer = new PersonalView(member, PersonalContactDegree.SecondDegree, PersonalContactDegree.SecondDegree);
                    if (viewer.CanAccess(row.ContactAccessFlag))
                    {
                        level++;

                        viewer = new PersonalView(member, PersonalContactDegree.Public, PersonalContactDegree.Public);
                        if (viewer.CanAccess(row.ContactAccessFlag))
                        {
                            level++;
                        }
                    }
                }

                row.ContactDegree = level;
            }
        }

        public void SaveFormData()
        {
            foreach (VisibilityCheckBoxRow row in Rows)
            {
                LoggedInMember.VisibilitySettings.Personal.Set(row.ContactDegree, row.ContactAccessFlag);
            }
        }
    }
}