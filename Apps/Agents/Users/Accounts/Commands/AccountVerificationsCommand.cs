using System;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Workflow.ActivationEmailWorkflow;

namespace LinkMe.Apps.Agents.Users.Accounts.Commands
{
    public class AccountVerificationsCommand
        : IAccountVerificationsCommand
    {
        private readonly IMembersQuery _membersQuery;
        private readonly IUserAccountsCommand _userAccountsCommand;
        private readonly IEmailVerificationsCommand _emailVerificationsCommand;
        private readonly IEmailVerificationsQuery _emailVerificationsQuery;
        private readonly IEmailsCommand _emailsCommand;
        private readonly IChannelManager<IService> _activationEmailManager;

        public AccountVerificationsCommand(IMembersQuery membersQuery, IUserAccountsCommand userAccountsCommand, IEmailVerificationsCommand emailVerificationsCommand, IEmailVerificationsQuery emailVerificationsQuery, IEmailsCommand emailsCommand, IChannelManager<IService> activationEmailManager)
        {
            _membersQuery = membersQuery;
            _userAccountsCommand = userAccountsCommand;
            _emailVerificationsCommand = emailVerificationsCommand;
            _emailVerificationsQuery = emailVerificationsQuery;
            _emailsCommand = emailsCommand;
            _activationEmailManager = activationEmailManager;
        }

        void IAccountVerificationsCommand.StartActivationWorkflow(IRegisteredUser user)
        {
            // Only supported for members at the moment.

            var member = user as IMember;
            if (member == null)
                return;

            var service = _activationEmailManager.Create();
            try
            {
                service.StartSending(member.Id);
            }
            catch (Exception)
            {
                _activationEmailManager.Abort(service);
                throw;
            }

            _activationEmailManager.Close(service);
        }

        void IAccountVerificationsCommand.StopActivationWorkflow(IRegisteredUser user)
        {
            // Only supported for members at the moment.

            var member = user as IMember;
            if (member == null)
                return;

            var service = _activationEmailManager.Create();

            try
            {
                service.StopSending(member.Id);
            }
            catch (Exception)
            {
                _activationEmailManager.Abort(service);
                throw;
            }

            _activationEmailManager.Close(service);
        }

        Guid? IAccountVerificationsCommand.Activate(string verificationCode)
        {
            return Activate(verificationCode);
        }

        Guid? IAccountVerificationsCommand.Verify(string verificationCode)
        {
            return Activate(verificationCode);
        }

        void IAccountVerificationsCommand.Verify(IRegisteredUser user, string emailAddress)
        {
            Activate(user.Id, emailAddress);
        }

        void IAccountVerificationsCommand.SendActivation(IRegisteredUser user, string emailAddress)
        {
            Send(user, emailAddress, (m, v) => new ActivationEmail(m, v));
        }

        void IAccountVerificationsCommand.SendVerification(IRegisteredUser user, string emailAddress)
        {
            Send(user, emailAddress, (m, v) => new VerificationEmail(m, v));
        }

        void IAccountVerificationsCommand.ResendActivation(IRegisteredUser user)
        {
            // Only supported for members at the moment.

            var member = user as IMember;
            if (member == null)
                return;

            // Find an existing verification or create a new one.

            var emailVerification = GetEmailVerification(member);

            // Send the email.

            var email = new ActivationEmail(member, emailVerification);
            _emailsCommand.TrySend(email);
        }

        void IAccountVerificationsCommand.ResendVerification(IRegisteredUser user, string emailAddress)
        {
            // Only supported for members at the moment.

            var member = user as IMember;
            if (member == null)
                return;

            // Find an existing verification or create a new one.

            var emailVerification = GetEmailVerification(member.Id, emailAddress);

            // Send the email.

            var email = new VerificationEmail(member, emailVerification);
            _emailsCommand.TrySend(email);
        }

        void IAccountVerificationsCommand.SendReactivation(IRegisteredUser user)
        {
            // Only supported for members at the moment.

            var member = user as IMember;
            if (member == null)
                return;

            // Find an existing activation or create a new one.

            var emailVerification = GetEmailVerification(member);

            // Send the email.

            var email = new ReactivationEmail(member, emailVerification);
            _emailsCommand.TrySend(email);
        }

        private Guid? Activate(string verificationCode)
        {
            // Try to find the corresponding email verification.

            var emailVerification = _emailVerificationsQuery.GetEmailVerification(verificationCode);
            if (emailVerification == null)
                return null;

            // Make sure the email verification still corresponds to this user.

            var member = _membersQuery.GetMember(emailVerification.UserId);
            if (member == null)
                return null;

            return Activate(member, emailVerification);
        }

        private void Activate(Guid userId, string emailAddress)
        {
            // Try to find the corresponding email verification.

            var emailVerification = _emailVerificationsQuery.GetEmailVerification(userId, emailAddress);
            if (emailVerification == null)
                return;

            // Make sure the email verification still corresponds to this user.

            var member = _membersQuery.GetMember(emailVerification.UserId);
            if (member == null)
                return;

            Activate(member, emailVerification);
        }

        private Guid? Activate(Member member, EmailVerification emailVerification)
        {
            var emailAddress = (from a in member.EmailAddresses where string.Equals(a.Address, emailVerification.EmailAddress, StringComparison.InvariantCultureIgnoreCase) select a).SingleOrDefault();
            if (emailAddress == null)
                return null;

            // Activate the account.

            _userAccountsCommand.ActivateUserAccount(member, member.Id);

            // Verify the email address.

            _emailVerificationsCommand.VerifyEmailAddress(member.Id, emailAddress.Address);

            return member.Id;
        }

        private void Send(IUser user, string emailAddress, Func<IMember, EmailVerification, TemplateEmail> createEmail)
        {
            // Only supported for members at the moment.

            var member = user as IMember;
            if (member == null)
                return;

            // Don't send if there is already one out there.

            var emailVerification = _emailVerificationsQuery.GetEmailVerification(member.Id, emailAddress);
            if (emailVerification != null)
                return;

            // Create a new verification and send an email.

            emailVerification = new EmailVerification { EmailAddress = emailAddress, UserId = member.Id };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);

            _emailsCommand.TrySend(createEmail(member, emailVerification));
        }

        private EmailVerification GetEmailVerification(IMember member)
        {
            return GetEmailVerification(member.Id, member.GetBestEmailAddress().Address);
        }

        private EmailVerification GetEmailVerification(Guid memberId, string emailAddress)
        {
            var emailVerification = _emailVerificationsQuery.GetEmailVerification(memberId, emailAddress);
            if (emailVerification != null)
                return emailVerification;

            emailVerification = new EmailVerification { UserId = memberId, EmailAddress = emailAddress };
            _emailVerificationsCommand.CreateEmailVerification(emailVerification);
            return emailVerification;
        }
    }
}
