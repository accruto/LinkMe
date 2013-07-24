using System;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Domain.Users.Queries;

namespace LinkMe.Apps.Agents.Security.Handlers
{
    public class SecurityHandler
        : ISecurityHandler
    {
        private readonly IEmailsCommand _emailsCommand;
        private readonly IUsersQuery _usersQuery;

        public SecurityHandler(IEmailsCommand emailsCommand, IUsersQuery usersQuery)
        {
            _emailsCommand = emailsCommand;
            _usersQuery = usersQuery;
        }

        void ISecurityHandler.OnPasswordReset(bool isGenerated, Guid userId, string loginId, string password)
        {
            if (!isGenerated)
                return;

            var contact = _usersQuery.GetUser(userId);
            if (contact == null)
                return;

            var reminderEmail = new PasswordReminderEmail(contact, loginId, password);
            _emailsCommand.TrySend(reminderEmail, null);
        }
    }
}
