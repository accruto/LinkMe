using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility.Validation;
using LinkMe.Web.Areas.Public.Routes;
using LinkMe.Web.UI;

namespace LinkMe.Web.Landing.Controls
{
    public partial class Join
        : LinkMeUserControl
    {
        private readonly ILocationQuery _locationQuery = Container.Current.Resolve<ILocationQuery>();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ucPasswordHashSaver.PasswordTextBoxes = txtPassword.ID;
            SetupValidators();
        }

        public Member GetMember()
        {
            var member = new Member
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = txtEmail.Text.Trim(), IsVerified = false } },
                Address = new Address { Location = new LocationReference() },
                VisibilitySettings = new VisibilitySettings(),
                IsActivated = false,
            };

            var country = ActivityContext.Current.Location.Country;
            _locationQuery.ResolveLocation(member.Address.Location, country);

            return member;
        }

        public LoginCredentials GetUserCredentials()
        {
            return new LoginCredentials
            {
                LoginId = txtEmail.Text.Trim(),
                PasswordHash = ucPasswordHashSaver.PasswordHash,
            };
        }

        private void SetupValidators()
        {
            reqPass.ErrorMessage = ValidationErrorMessages.REQUIRED_FIELD_NEW_PASSWORD + "<br />";

            reqTerms.ErrorMessage = ValidationErrorMessages.TERMS_OF_USE_NOT_AGREED_TO + "<br />";

            reqEmail.ErrorMessage = ValidationErrorMessages.REQUIRED_FIELD_EMAIL_ADDRESS + "<br />";
            valEmail.ErrorMessage = ValidationErrorMessages.INVALID_EMAIL_FORMAT + "<br />";

            reqFirst.ErrorMessage = ValidationErrorMessages.REQUIRED_FIELD_FIRST_NAME + "<br />";
            valFirst.ErrorMessage = ValidationErrorMessages.INVALID_FIRST_NAME + "<br />";
            valFirst.ValidationExpression = LinkMe.Domain.RegularExpressions.CompleteFirstNamePattern;

            reqLast.ErrorMessage = ValidationErrorMessages.REQUIRED_FIELD_LAST_NAME + "<br />";
            valLast.ErrorMessage = ValidationErrorMessages.INVALID_LAST_NAME + "<br />";
            valLast.ValidationExpression = LinkMe.Domain.RegularExpressions.CompleteLastNamePattern;

            reqTerms.ErrorMessage = ValidationErrorMessages.TERMS_OF_USE_NOT_AGREED_TO + "<br />";
        }

        public void Populate(string email, string password, string firstName, string lastName, bool acceptTermsAndConditions)
        {
            txtEmail.Text = email;
            txtPassword.Text = password;
            ucPasswordHashSaver.UpdatePasswordHash();
            txtFirstName.Text = firstName;
            txtLastName.Text = lastName;
            chkAcceptTermsAndConditions.Checked = acceptTermsAndConditions;
        }

        protected static string TermsAndConditionsPopup
        {
            get { return LinkMePage.GetPopupATag(SupportRoutes.Terms.GenerateUrl(), "Terms and conditions", "terms and conditions"); }
        }
    }
}