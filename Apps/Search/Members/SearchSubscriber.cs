using LinkMe.Apps.Agents.Users.Members.Messages;
using LinkMe.Domain.Accounts.Messages;
using LinkMe.Domain.Roles.Candidates.Messages;
using LinkMe.Query.Members;
using NServiceBus;
using MemberChanged=LinkMe.Domain.Users.Members.Affiliations.Messages.MemberChanged;

namespace LinkMe.Apps.Search.Members
{
    public class SearchSubscriber : 
        IHandleMessages<ResumeUpdated>,
        IHandleMessages<CandidateChanged>,
        IHandleMessages<CandidateUpdated>,
        IHandleMessages<MemberCreated>,
        IHandleMessages<MemberUpdated>,
        IHandleMessages<UserActivated>,
        IHandleMessages<UserEnabled>,
        IHandleMessages<UserDisabled>,
        IHandleMessages<UserDeactivated>,
        IHandleMessages<MemberChanged>
    {
        private readonly IMemberSearchService _service;

        public SearchSubscriber(IMemberSearchService service)
        {
            _service = service;
        }

        public void Handle(ResumeUpdated message)
        {
            _service.UpdateMember(message.CandidateId);
        }

        public void Handle(CandidateChanged message)
        {
            _service.UpdateMember(message.CandidateId);
        }

        public void Handle(CandidateUpdated message)
        {
            _service.UpdateMember(message.CandidateId);
        }

        public void Handle(MemberCreated message)
        {
            _service.UpdateMember(message.MemberId);
        }

        public void Handle(MemberUpdated message)
        {
            _service.UpdateMember(message.Member.Id);
        }

        public void Handle(UserActivated message)
        {
            _service.UpdateMember(message.UserId);
        }

        public void Handle(UserEnabled message)
        {
            _service.UpdateMember(message.UserId);
        }

        public void Handle(UserDisabled message)
        {
            _service.UpdateMember(message.UserId);
        }

        public void Handle(UserDeactivated message)
        {
            _service.UpdateMember(message.UserId);
        }

        public void Handle(MemberChanged message)
        {
            _service.UpdateMember(message.MemberId);
        }
    }
}