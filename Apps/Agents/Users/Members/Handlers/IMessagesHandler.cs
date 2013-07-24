using System;
using LinkMe.Domain.Users.Employers.Contacts;

namespace LinkMe.Apps.Agents.Users.Members.Handlers
{
    public interface IMessagesHandler
    {
        void OnMessageSent(Guid fromId, Guid toId, Guid? representativeId, MemberMessage message);
    }
}
