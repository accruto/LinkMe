using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Security.Commands
{
    public class LoginCredentialsCommand
        : ILoginCredentialsCommand
    {
        private readonly ISecurityRepository _repository;
        private readonly IPasswordsCommand _passwordsCommand;

        public LoginCredentialsCommand(ISecurityRepository repository, IPasswordsCommand passwordsCommand)
        {
            _repository = repository;
            _passwordsCommand = passwordsCommand;
        }

        void ILoginCredentialsCommand.CreateCredentials(Guid userId, LoginCredentials credentials)
        {
            credentials.Prepare();
            credentials.Validate();
            _repository.UpdateCredentials(userId, credentials);
        }

        void ILoginCredentialsCommand.UpdateCredentials(Guid userId, LoginCredentials credentials, Guid updatedById)
        {
            credentials.Validate();
            _repository.UpdateCredentials(userId, credentials);
        }

        void ILoginCredentialsCommand.ResetPassword(Guid userId, LoginCredentials credentials)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");

            var password = _passwordsCommand.GenerateRandomPassword();
            UpdatePassword(userId, credentials, password, true);
        }

        public void ChangePassword(Guid userId, LoginCredentials credentials, string newPassword)
        {
            if (credentials == null)
                throw new ArgumentNullException("credentials");

            UpdatePassword(userId, credentials, newPassword, false);
        }

        [Publishes(PublishedEvents.PasswordReset)]
        public event EventHandler<PasswordResetEventArgs> PasswordReset;

        private void UpdatePassword(Guid userId, LoginCredentials credentials, string password, bool isGenerated)
        {
            // Change the credentials.

            credentials.PasswordHash = LoginCredentials.HashToString(password);
            credentials.MustChangePassword = isGenerated;
            _repository.UpdateCredentials(userId, credentials);

            // Fire events.

            var handlers = PasswordReset;
            if (handlers != null)
                handlers(this, new PasswordResetEventArgs(userId, credentials.LoginId, password, isGenerated));
        }
    }
}