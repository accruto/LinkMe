using System;

namespace LinkMe.Apps.Agents.Security.Commands
{
    public class PasswordsCommand
        : IPasswordsCommand
    {
        string IPasswordsCommand.GenerateRandomPassword()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
        }
    }
}