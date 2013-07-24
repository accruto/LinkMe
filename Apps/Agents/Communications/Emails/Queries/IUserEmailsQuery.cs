using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Settings;

namespace LinkMe.Apps.Agents.Communications.Emails.Queries
{
    public interface IUserEmailsQuery
    {
        bool ShouldSend(ICommunicationUser to, Definition definition, Category category, bool requiresActivation, bool ignoreChecks, DateTime? notIfLastSentLaterThanThis);
    }
}