using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Users.Accounts.Commands
{
    public interface IAccountVerificationsCommand
    {
        void StartActivationWorkflow(IRegisteredUser user);
        void StopActivationWorkflow(IRegisteredUser user);

        Guid? Activate(string verificationCode);
        Guid? Verify(string verificationCode);
        void Verify(IRegisteredUser user, string emailAddress);

        void SendActivation(IRegisteredUser user, string emailAddress);
        void SendVerification(IRegisteredUser user, string emailAddress);

        void ResendActivation(IRegisteredUser user);
        void ResendVerification(IRegisteredUser user, string emailAddress);

        void SendReactivation(IRegisteredUser user);
    }
}
