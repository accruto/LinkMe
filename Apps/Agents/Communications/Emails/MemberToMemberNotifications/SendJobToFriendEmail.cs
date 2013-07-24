using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications
{
    public class SendJobToFriendEmail
        : MemberToMemberNotification
    {
        private readonly JobAdEntry _jobAd;
        private readonly string _messageText;
        private readonly string _fromDisplayName;
        private readonly string _toDisplayName;

        public SendJobToFriendEmail(string toEmailAddress, string toDisplayName, string fromEmailAddress, string fromDisplayName, JobAdEntry ad, string messageText)
            : base(GetUnregisteredMember(toEmailAddress, toDisplayName), GetUnregisteredMember(fromEmailAddress, fromDisplayName))
        {
            _jobAd = ad;
            _messageText = messageText;
            _fromDisplayName = fromDisplayName;
            _toDisplayName = toDisplayName;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("ToDisplayName", _toDisplayName);
            properties.Add("FromDisplayName", _fromDisplayName);
            properties.Add("MessageText", _messageText);
            properties.Add("JobAd", _jobAd);
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}