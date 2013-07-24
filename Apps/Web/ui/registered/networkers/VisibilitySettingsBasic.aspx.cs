using System;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Validation;
using LinkMe.Web.Areas.Members.Routes;
using LinkMe.Web.Content;

namespace LinkMe.Web.UI.Registered.Networkers
{
	public partial class VisibilitySettingsBasic
        : LinkMePage
	{
        private static readonly IMemberAccountsCommand _memberAccountsCommand = Container.Current.Resolve<IMemberAccountsCommand>();
        private static readonly ICandidatesCommand _candidatesCommand = Container.Current.Resolve<ICandidatesCommand>();

	    private Candidate _candidate;

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
            AddJavaScriptReference(JavaScripts.VisibilitySettings);

            btnSave.Attributes["onclick"] = "buttonClicked=true; return true;";
            btnSave.Click += btnSave_Click;

            _candidate = _candidatesCommand.GetCandidate(LoggedInMember.Id);
            ucWorkStatusSettings.Display(_candidate);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            ucVisibilityBasicSettings.SaveFormData();
            ucEmployerPrivacy.SaveFormData();

            _memberAccountsCommand.UpdateMember(LoggedInMember);

            ucWorkStatusSettings.Update(_candidate);
            _candidatesCommand.UpdateCandidate(_candidate);

            AddConfirm(ValidationInfoMessages.VISIBILITY_SETTINGS_SAVED);
        }

        protected ReadOnlyUrl GetRedirectUrl()
        {
            return Context.GetReturnUrl(ProfilesRoutes.Profile.GenerateUrl());
        }
	}
}
