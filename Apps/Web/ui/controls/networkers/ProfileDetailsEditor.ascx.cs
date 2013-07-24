using System;
using System.Linq;
using System.Web.UI.WebControls;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Users.Members.Affiliations.Affiliates;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Affiliations.Affiliates;
using LinkMe.Domain.Users.Members.Affiliations.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.Applications.Facade;
using LinkMe.Web.Helper;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class ProfileDetailsEditor
        : LinkMeUserControl
    {
        private readonly IMemberAffiliationsQuery _memberAffiliationsQuery = Container.Current.Resolve<IMemberAffiliationsQuery>();

        private Member _member;
        private bool _canEditContactDetails;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ucLocationCountry.Location = ucLocation;
            ucLocationConfirmation.Location = ucLocation;
        }

        public void Display(Member member, bool canEditContactDetails)
        {
            _member = member;
            _canEditContactDetails = canEditContactDetails;

            ucLocationCountry.SelectedValue = member.Address.Location.CountrySubdivision.Country;

            rdoMale.Checked = member.Gender == Gender.Male;
            rdoFemale.Checked = member.Gender == Gender.Female;

            if (member.DateOfBirth.HasValue)
            {
                txtYear.Value = member.DateOfBirth.Value.Year.ToString();
                ddlMonth.SelectedIndex = member.DateOfBirth.Value.Month ?? 1;
                ddlDay.SelectedIndex = member.DateOfBirth.Value.Day ?? 1;
            }

            ucLocation.Text = member.Address.Location.ToString();

            chkListEthnicStatus.Items.AddRange(
                FieldInputHelper.CreateListItems(EthnicStatusDisplay.Values, EthnicStatusDisplay.GetDisplayText, false)
                .ToArray());

            foreach (ListItem item in chkListEthnicStatus.Items)
            {
                var itemFlag = (EthnicStatus)int.Parse(item.Value);
                item.Selected = ((member.EthnicStatus & itemFlag) != 0);
                item.Attributes.Add("intValue", item.Value); // used by the client JavaScript 
            }
        }

        protected bool CanEditContactDetails
        {
            get { return _canEditContactDetails; }
        }

        protected Member Member
        {
            get { return _member; }
        }

        protected string GetFullNameText()
        {
            return  HtmlUtil.TextToHtml(_member.FullName);
        }

        protected string GetAgeText()
        {
            return _member.DateOfBirth.GetAgeDisplayText();
        }

        protected string GetEthnicStatusText()
        {
            return NetworkerFacade.GetEthnicStatusText(_member, true);
        }

        protected string GetAimeStatusText()
        {
            var communityId = ActivityContext.Current.Community.Id;
            if (communityId == null)
                return null;

            var items = _memberAffiliationsQuery.GetItems(_member.Id, communityId.Value) as AimeAffiliationItems;
            if (items == null)
                return null;

            if (items.Status == null)
                return null;
            return items.Status.GetDisplayText();
        }
    }
}