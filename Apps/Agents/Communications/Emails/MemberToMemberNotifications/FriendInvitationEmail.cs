using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications
{
    public class FriendInvitationEmail
        : MemberToMemberNotification
    {
        private readonly FriendInvitation _invitation;
        private readonly EmailVerification _emailVerification;
        private readonly IList<Job> _currentJobs;
        private readonly int _contactCount;

        public FriendInvitationEmail(ICommunicationUser to, ICommunicationUser from, FriendInvitation invitation, EmailVerification emailVerification, IList<Job> currentJobs, int contactCount)
            : base(to, from)
        {
            _invitation = invitation;
            _emailVerification = emailVerification;
            _currentJobs = currentJobs;
            _contactCount = contactCount;
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

            // Current Job

            if (!_currentJobs.IsNullOrEmpty() && _currentJobs.Count > 0)
            {
                // Choose the one with the most recent start date.

                var currentJob = (from j in _currentJobs
                                  where !string.IsNullOrEmpty(j.Title)
                                  && j.Dates.Start != null
                                  orderby j.Dates.Start.Value descending
                                  select j.Title).FirstOrDefault();

                properties.Add("CurrentJob", currentJob);
            }
            else
            {
                properties.Add("CurrentJob", string.Empty);
            }

            // ContactCount

            properties.Add("ContactCount", _contactCount > 0 ? _contactCount : 0);
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}