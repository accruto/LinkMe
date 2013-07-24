using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications
{
    public class RepresentativeInvitationConfirmationEmail
        : MemberToMemberNotification
    {
        private readonly IUser _invitee;

        public RepresentativeInvitationConfirmationEmail(ICommunicationUser to, IUser invitee)
            : base(to)
        {
            _invitee = invitee;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Invitee", _invitee);
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}