using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications
{
    public class RepresentativeInvitationEmail
        : MemberToMemberNotification
    {
        private readonly RepresentativeInvitation _invitation;
        private readonly EmailVerification _emailVerification;

        public RepresentativeInvitationEmail(ICommunicationUser to, ICommunicationUser from, RepresentativeInvitation invitation, EmailVerification emailVerification)
            : base(to, from)
        {
            _invitation = invitation;
            _emailVerification = emailVerification;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("ToIsActivated", To.IsActivated);
            properties.Add("Invitation", _invitation);
            properties.Add("MessageText", _invitation.Text);

            if (_emailVerification != null)
            {
                properties.Add("InviteeVerificationCode", _emailVerification.VerificationCode);
                properties.Add("InvitationId", string.Empty);
            }
            else
            {
                properties.Add("InviteeVerificationCode", string.Empty);
                properties.Add("InvitationId", string.Empty);
            }
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}