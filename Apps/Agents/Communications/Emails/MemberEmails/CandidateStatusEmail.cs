using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberEmails
{
    public class CandidateLookingConfirmationEmail
        : MemberEmail
    {
        public CandidateLookingConfirmationEmail(ICommunicationUser to)
            : base(to)
        {
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }
    }

    public class CandidatePassiveNotificationEmail
        : MemberEmail
    {
        public CandidatePassiveNotificationEmail(ICommunicationUser to)
            : base(to)
        {
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }
    }

    public class CandidateAvailableConfirmationEmail
        : MemberEmail
    {
        public CandidateAvailableConfirmationEmail(ICommunicationUser to)
            : base(to)
        {
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }
    }

    public class CandidateLookingNotificationEmail
        : MemberEmail
    {
        public CandidateLookingNotificationEmail(ICommunicationUser to)
            : base(to)
        {
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }
    }
}
