using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Donations;
using LinkMe.Domain.Roles.Networking;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications
{
    public class ContactInvitationEmail
        : MemberToMemberNotification
    {
        private readonly NetworkingInvitation _invitation;
        private readonly DonationRequest _donationRequest;
        private readonly DonationRecipient _donationRecipient;
        private readonly IList<Job> _currentJobs;
        private readonly int _contactCount;

        public ContactInvitationEmail(ICommunicationUser inviter, NetworkingInvitation invitation, DonationRequest donationRequest, DonationRecipient donationRecipient, IList<Job> currentJobs, int contactCount)
            : base(GetUnregisteredMember(invitation.InviteeEmailAddress), inviter)
        {
            _invitation = invitation;
            _donationRequest = donationRequest;
            _donationRecipient = donationRecipient;
            _currentJobs = currentJobs;
            _contactCount = contactCount;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Invitation", _invitation);
            properties.Add("DonationAmount", _donationRequest == null ? string.Empty : _donationRequest.Amount.ToString("C"));
            properties.Add("DonationRecipient", _donationRecipient == null ? string.Empty : _donationRecipient.Name);
            properties.Add("MessageText", _invitation.Text);

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