using LinkMe.Apps.Agents.Security.Commands;

namespace LinkMe.Apps.Mocks.Security
{
    public class NotSoRandomPasswordsCommand
        : IPasswordsCommand
    {
        public static readonly string TestPassword = "abc123";

        public string GenerateRandomPassword()
        {
            return TestPassword;
        }
    }
}