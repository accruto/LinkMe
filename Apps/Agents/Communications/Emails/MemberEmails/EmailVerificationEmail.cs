using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberEmails
{
    public abstract class EmailVerificationEmail
        : MemberEmail
    {
        private readonly EmailVerification _emailVerification;

        protected EmailVerificationEmail(IMember to, EmailVerification emailVerification)
            : base(GetMember(to, new EmailAddress { Address = emailVerification.EmailAddress, IsVerified = true }))
        {
            if (emailVerification == null)
                throw new ArgumentNullException("emailVerification");
            _emailVerification = emailVerification;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("VerificationCode", _emailVerification.VerificationCode);
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}