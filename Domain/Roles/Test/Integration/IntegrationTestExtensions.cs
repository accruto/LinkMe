using System;
using System.Security.Cryptography;
using System.Text;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;

namespace LinkMe.Domain.Roles.Test.Integration
{
    public static class IntegrationTestExtensions
    {
        private const string UserName = "test-integrator";
        private const string Password = "password";

        public static IntegratorUser CreateTestIntegratorUser(this IIntegrationCommand integrationCommand)
        {
            return integrationCommand.CreateTestIntegratorUser(UserName, Password, IntegratorPermissions.GetJobAds | IntegratorPermissions.GetJobApplication | IntegratorPermissions.PostJobAds);
        }

        public static IntegratorUser CreateTestIntegratorUser(this IIntegrationCommand integrationCommand, string loginId, string password, IntegratorPermissions permissions)
        {
            var system = new Ats { Name = loginId + " Integration System" };
            integrationCommand.CreateIntegrationSystem(system);

            var user = new IntegratorUser { LoginId = loginId, PasswordHash = HashToString(password), Permissions = permissions, IntegrationSystemId = system.Id };
            integrationCommand.CreateIntegratorUser(user);

            return user;
        }

        private static byte[] HashToBytes(string password)
        {
            if (string.IsNullOrEmpty(password))
                return null;
            var data = Encoding.UTF8.GetBytes(password);
            var md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(data);
        }

        private static string HashToString(string password)
        {
            return string.IsNullOrEmpty(password) ? password : Convert.ToBase64String(HashToBytes(password));
        }
    }
}
