using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Test.Users.Employers
{
    public static class EmployerAccountsTestExtensions
    {
        private const string UserIdFormat = "employer{0}";
        private const string EmailAddressFormat = "{0}@test.linkme.net.au";
        private const string DefaultPassword = "password";
        private const string FirstNameFormat = "Monty{0}";
        private const string LastNameFormat = "Burns{0}";
        private const string DefaultPhoneNumber = "0410635666";

        public static string GetLoginId(this Employer employer)
        {
            return employer.EmailAddress.Address.Substring(0, employer.EmailAddress.Address.IndexOf("@"));
        }

        public static string GetPassword(this Employer employer)
        {
            return DefaultPassword;
        }

        public static Employer CreateTestEmployer(this IEmployerAccountsCommand employersCommand, int index, IOrganisation organisation)
        {
            return employersCommand.CreateTestEmployer(
                string.Format(UserIdFormat, index),
                string.Format(FirstNameFormat, index),
                string.Format(LastNameFormat, index),
                EmployerSubRole.Employer,
                organisation);
        }

        public static Employer CreateTestRecruiter(this IEmployerAccountsCommand employersCommand, int index, IOrganisation organisation)
        {
            return employersCommand.CreateTestEmployer(
                string.Format(UserIdFormat, index),
                string.Format(FirstNameFormat, index),
                string.Format(LastNameFormat, index),
                EmployerSubRole.Recruiter,
                organisation);
        }

        public static Employer CreateTestEmployer(this IEmployerAccountsCommand employersCommand, string loginId, IOrganisation organisation)
        {
            return employersCommand.CreateTestEmployer(
                loginId,
                string.Format(FirstNameFormat, 0),
                string.Format(LastNameFormat, 0),
                EmployerSubRole.Employer,
                organisation);
        }

        public static Employer CreateTestRecruiter(this IEmployerAccountsCommand employersCommand, string loginId, IOrganisation organisation)
        {
            return employersCommand.CreateTestEmployer(
                loginId,
                string.Format(FirstNameFormat, 0),
                string.Format(LastNameFormat, 0),
                EmployerSubRole.Recruiter,
                organisation);
        }

        public static Employer CreateTestEmployer(this IEmployerAccountsCommand employersCommand, string loginId, string firstName, string lastName, IOrganisation organisation)
        {
            return employersCommand.CreateTestEmployer(
                loginId,
                firstName,
                lastName,
                EmployerSubRole.Employer,
                organisation);
        }

        public static Employer CreateTestEmployer(this IEmployerAccountsCommand employersCommand, string loginId, string firstName, string lastName, string emailAddress, IOrganisation organisation)
        {
            return CreateTestEmployer(
                employersCommand,
                loginId,
                firstName,
                lastName,
                emailAddress,
                EmployerSubRole.Employer,
                organisation);
        }

        private static Employer CreateTestEmployer(this IEmployerAccountsCommand employersCommand, string loginId, string firstName, string lastName, EmployerSubRole subRole, IOrganisation organisation)
        {
            return CreateTestEmployer(
                employersCommand,
                loginId,
                firstName,
                lastName,
                string.Format(EmailAddressFormat, loginId),
                subRole,
                organisation);
        }

        private static Employer CreateTestEmployer(this IEmployerAccountsCommand employersCommand, string loginId, string firstName, string lastName, string emailAddress, EmployerSubRole subRole, IOrganisation organisation)
        {
            var employer = new Employer
            {
                EmailAddress = new EmailAddress { Address = emailAddress },
                IsActivated = true,
                IsEnabled = true,
                PhoneNumber = new PhoneNumber { Number = DefaultPhoneNumber, Type = PhoneNumberType.Mobile },
                FirstName = firstName,
                LastName = lastName,
                Organisation = organisation,
                SubRole = subRole,
            };

            employersCommand.CreateTestEmployer(employer, loginId);
            return employer;
        }

        private static void CreateTestEmployer(this IEmployerAccountsCommand employersCommand, Employer employer, string loginId)
        {
            employersCommand.CreateEmployer(employer, new LoginCredentials { LoginId = loginId, PasswordHash = LoginCredentials.HashToString(DefaultPassword) });
        }
    }
}