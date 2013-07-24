using System;
using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Query.Members;
using PublishedEvents=LinkMe.Apps.Agents.Users.Members.PublishedEvents;

namespace LinkMe.Apps.Agents.Query.Search.Members
{
    public class MemberSearchSubscriber
    {
        private readonly IChannelManager<IMemberSearchService> _serviceManager;
        private readonly IMemberSearchEngineCommand _memberSearchEngineCommand;

        public MemberSearchSubscriber(IChannelManager<IMemberSearchService> serviceManager, IMemberSearchEngineCommand memberSearchEngineCommand)
        {
            _serviceManager = serviceManager;
            _memberSearchEngineCommand = memberSearchEngineCommand;
        }

        [SubscribesTo(LinkMe.Domain.Users.Members.Affiliations.PublishedEvents.MemberChanged)]
        public void OnCommunityMemberChanged(object sender, MemberEventArgs e)
        {
            Update(e.MemberId);
        }

        [SubscribesTo(PublishedEvents.MemberCreated)]
        public void OnMemberCreated(object sender, MemberCreatedEventArgs e)
        {
            Update(e.MemberId);
        }

        [SubscribesTo(PublishedEvents.MemberUpdated)]
        public void OnMemberChanged(object sender, EventArgs<Member> e)
        {
            Update(e.Value.Id);
        }

        [SubscribesTo(LinkMe.Domain.Roles.Candidates.PublishedEvents.CandidateUpdated)]
        public void OnCandidateChanged(object sender, EventArgs<Guid> e)
        {
            Update(e.Value);
        }

        [SubscribesTo(LinkMe.Domain.Roles.Candidates.PublishedEvents.ResumeUpdated)]
        public void OnResumeUpdated(object sender, ResumeUpdatedEventArgs e)
        {
            Update(e.CandidateId);
        }

        [SubscribesTo(LinkMe.Domain.Roles.Candidates.PublishedEvents.PropertiesChanged)]
        public void OnCandidateStatusUpdated(object sender, PropertiesChangedEventArgs e)
        {
            Update(e.InstanceId);
        }

        [SubscribesTo(LinkMe.Domain.Accounts.PublishedEvents.UserActivated)]
        public void OnUserActivated(object sender, UserAccountEventArgs e)
        {
            if (e.UserType == UserType.Member)
                Update(e.UserId);
        }

        [SubscribesTo(LinkMe.Domain.Accounts.PublishedEvents.UserDeactivated)]
        public void OnUserDeactivated(object sender, UserDeactivatedEventArgs e)
        {
            if (e.UserType == UserType.Member)
                Update(e.UserId);
        }

        [SubscribesTo(LinkMe.Domain.Accounts.PublishedEvents.UserEnabled)]
        public void OnUserEnabled(object sender, UserAccountEventArgs e)
        {
            if (e.UserType == UserType.Member)
                Update(e.UserId);
        }

        [SubscribesTo(LinkMe.Domain.Accounts.PublishedEvents.UserDisabled)]
        public void OnUserDisabled(object sender, UserAccountEventArgs e)
        {
            if (e.UserType == UserType.Member)
                Update(e.UserId);
        }

        [SubscribesTo(LinkMe.Domain.Roles.Registration.PublishedEvents.EmailAddressUnverified)]
        public void OnEmailAddressUnverified(object sender, EmailAddressUnverifiedEventArgs e)
        {
            Update(e.UserId);
        }

        [SubscribesTo(LinkMe.Domain.Roles.Registration.PublishedEvents.EmailAddressVerified)]
        public void OnEmailAddressVerified(object sender, EmailAddressVerifiedEventArgs e)
        {
            Update(e.UserId);
        }

        private void Update(Guid memberId)
        {
            // Update all search engines.

            _memberSearchEngineCommand.SetModified(memberId);

            // Make sure the local one is updated now.

            var service = _serviceManager.Create();
            try
            {
                service.UpdateMember(memberId);
            }
            catch (Exception)
            {
                _serviceManager.Abort(service);
                throw;
            }
            _serviceManager.Close(service);
        }
    }
}