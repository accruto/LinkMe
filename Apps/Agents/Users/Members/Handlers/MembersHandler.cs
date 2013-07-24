using System;
using LinkMe.Apps.Agents.Users.Accounts.Commands;
using LinkMe.Domain.Users.Members.Queries;

namespace LinkMe.Apps.Agents.Users.Members.Handlers
{
    public class MembersHandler
        : IMembersHandler
    {
        private readonly IMembersQuery _membersQuery;
        private readonly IAccountVerificationsCommand _accountVerificationsCommand;

        public MembersHandler(IMembersQuery membersQuery, IAccountVerificationsCommand accountVerificationsCommand)
        {
            _membersQuery = membersQuery;
            _accountVerificationsCommand = accountVerificationsCommand;
        }

        void IMembersHandler.OnMemberCreated(Guid memberId)
        {
            var member = _membersQuery.GetMember(memberId);
            if (member == null)
                return;

            // No activation email etc may have been sent at this stage but start the work flow to remind.

            _accountVerificationsCommand.StartActivationWorkflow(member);
        }

        void IMembersHandler.OnMemberDisabled(Guid memberId)
        {
            var member = _membersQuery.GetMember(memberId);
            if (member == null)
                return;

            _accountVerificationsCommand.StopActivationWorkflow(member);
        }

        void IMembersHandler.OnMemberActivated(Guid memberId)
        {
            var member = _membersQuery.GetMember(memberId);
            if (member == null)
                return;

            _accountVerificationsCommand.StopActivationWorkflow(member);
        }

        void IMembersHandler.OnMemberDeactivated(Guid memberId)
        {
            var member = _membersQuery.GetMember(memberId);
            if (member == null)
                return;

            _accountVerificationsCommand.StopActivationWorkflow(member);
        }
    }
}
