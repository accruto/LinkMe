using System;
using System.Web.UI.WebControls;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Validation;
using LinkMe.Web.Content;
using Constants = LinkMe.Apps.Asp.Constants;

namespace LinkMe.Web.UI.Registered.Networkers
{
    public partial class VisibilitySettingsAdvanced : LinkMePage
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Container.Current.Resolve<IMemberAccountsCommand>();

        protected Button btnSaveTop;
        protected Button btnCancelTop;
        protected Button btnSaveBottom;
        protected Button btnCancelBottom;

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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStyleSheetReference(StyleSheets.Networking);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            btnSaveTop.Click += btnSave_Click;
            btnSaveBottom.Click += btnSave_Click;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            SaveFormData();
            AddConfirm(ValidationInfoMessages.VISIBILITY_SETTINGS_SAVED);
        }

        protected ReadOnlyUrl VisibilitySettingsUrl
        {
            get
            {
                return GetUrlForPage<VisibilitySettingsBasic>(Constants.ReturnUrlParameter, Request.Url.PathAndQuery);
            }
        }

        private void SaveFormData()
        {
            if (ucVisibilityBasicSettings.HasASelectedOption)
                ucVisibilityBasicSettings.SaveFormData();
            else
                ucVisibilityAdvancedSettings.SaveFormData();

            _memberAccountsCommand.UpdateMember(LoggedInMember);
        }
    }
}
