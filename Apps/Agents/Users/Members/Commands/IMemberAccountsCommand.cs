using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Users.Members.Commands
{
    public interface IMemberAccountsCommand
    {
        void CreateMember(Member member, LoginCredentials credentials, Guid? affiliateId);
        void CreateMember(Member member, ExternalCredentials credentials, Guid? affiliateId);
        void UpdateMember(Member member);
        void CreateCredentials(Member member, LoginCredentials credentials);
    }
}
