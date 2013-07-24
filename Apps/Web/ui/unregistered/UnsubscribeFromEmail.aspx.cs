using System;
using System.Web;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Applications.Facade;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.UI.Registered.Employers;
using LinkMe.Apps.Asp.Navigation;

namespace LinkMe.Web.UI.Unregistered
{
    public partial class UnsubscribeFromEmail : LinkMePage
    {
        protected override UserType[] AuthorizedUserTypes
        {
            get { return null; }
        }

        protected override bool GetRequiresActivation()
        {
            return false;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }

        public const string EMAIL_ADDRESS_PARAM = "emailAddress";

        private string emailAddress;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            emailAddress = Request.QueryString[EMAIL_ADDRESS_PARAM];
            if (string.IsNullOrEmpty(emailAddress))
            {
                AddError("The email address to unsubscribe must be specified.");
            }

            bool enableUnsubscribe = true;

            if (!IsPostBack)
            {
                enableUnsubscribe = !CheckIfAlreadyUnsubscribed();
            }

            if (enableUnsubscribe)
            {
                litTitle.Text = "Unsubscribe From " + EmailNameHtml + " Emails";
                litMessage.Text = "You are about to unsubscribe <strong>" + EmailAddressHtml
                    + "</strong> from <strong>" + EmailNameHtml + "</strong> emails.";
            }
            else
            {
                DisableUnsubscribe();
            }

            btnUnsubscribe.Enabled = enableUnsubscribe && !HasError();
        }

        protected string EmailNameHtml
        {
            get { return HttpUtility.HtmlEncode("Suggested Candidates"); }
        }

        protected string EmailAddressHtml
        {
            get { return HttpUtility.HtmlEncode(emailAddress); }
        }

        protected void btnUnsubscribe_Click(object sender, EventArgs e)
        {
            int registeredCount;
            bool success = EmailSubscriptionFacade.UnusbscribeFromSuggestedCandidatesWithCheck(emailAddress, out registeredCount);
            ShowMessage(registeredCount, !success);

            DisableUnsubscribe();
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            // Take this as a parameter if networkers use the same form.
            NavigationManager.Redirect(HttpContext.Current.GetHomeUrl());
        }

        private void DisableUnsubscribe()
        {
            btnUnsubscribe.Visible = false;
            btnHome.Text = "OK";
        }

        private bool CheckIfAlreadyUnsubscribed()
        {
            int registeredCount;
            bool already = EmailSubscriptionFacade.IsUnsubscribedFromSuggestedCandidates(emailAddress, out registeredCount);

            if (already)
            {
                litTitle.Text = "You have already unsubscribed";
                ShowMessage(registeredCount, true);
            }

            return already;
        }

        private void ShowMessage(int registeredCount, bool already)
        {
            string unsubscribed = (already ? "already" : "successfully");

            if (registeredCount > 0)
            {
                litMessage.Text = string.Format("You have {0} unsubscribed <strong>{1}</strong> from"
                    + "  <strong>Suggested Candidates</strong> emails. If you wish to receive these emails again, please go to"
                    + " your <a href=\"{2}\">Account</a> page to change your preferences.",
                    unsubscribed, emailAddress, SettingsRoutes.Settings.GenerateUrl());

                if (registeredCount > 1)
                {
                    litMessage.Text += " Note that there are multiple employer accounts with this email"
                        + " address and any one of them can be used to change this setting.";
                }
            }
            else
            {
                litMessage.Text = string.Format("You have {0} unsubscribed <strong>{1}</strong> from"
                    + "  <strong>Suggested Candidates</strong> emails. If you wish to receive these emails again, you will first"
                    + " need to <a href=\"{2}\"> create an account</a> with the same email address, then go to your"
                    + " <a href=\"{3}\">Account</a> page to change your preferences.",
                    unsubscribed, emailAddress, AccountsRoutes.Join.GenerateUrl(), SettingsRoutes.Settings.GenerateUrl());
            }
        }
    }
}
