using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Commands;

namespace LinkMe.Domain.Users.Test.Employers
{
    public static class EmployersTestExtensions
    {
        private const string EmailAddressFormat = "employer{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Monty{0}";
        private const string LastNameFormat = "Burns{0}";
        private const string DefaultPhoneNumber = "0410635666";

        public static Employer CreateTestEmployer(this IEmployersCommand employersCommand, int index, IOrganisation organisation)
        {
            return employersCommand.CreateTestEmployer(
                string.Format(EmailAddressFormat, index),
                string.Format(FirstNameFormat, index),
                string.Format(LastNameFormat, index),
                EmployerSubRole.Employer,
                organisation);
        }

        public static Employer CreateTestRecruiter(this IEmployersCommand employersCommand, int index, IOrganisation organisation)
        {
            return employersCommand.CreateTestEmployer(
                string.Format(EmailAddressFormat, index),
                string.Format(FirstNameFormat, index),
                string.Format(LastNameFormat, index),
                EmployerSubRole.Recruiter,
                organisation);
        }

        public static Employer CreateTestEmployer(this IEmployersCommand employersCommand, IOrganisation organisation)
        {
            return employersCommand.CreateTestEmployer(
                string.Format(EmailAddressFormat, 0),
                string.Format(FirstNameFormat, 0),
                string.Format(LastNameFormat, 0),
                EmployerSubRole.Employer,
                organisation);
        }

        public static Employer CreateTestRecruiter(this IEmployersCommand employersCommand, IOrganisation organisation)
        {
            return employersCommand.CreateTestEmployer(
                string.Format(EmailAddressFormat, 0),
                string.Format(FirstNameFormat, 0),
                string.Format(LastNameFormat, 0),
                EmployerSubRole.Recruiter,
                organisation);
        }

        public static Employer CreateTestEmployer(this IEmployersCommand employersCommand, string emailAddress, string firstName, string lastName, IOrganisation organisation)
        {
            return employersCommand.CreateTestEmployer(
                emailAddress,
                firstName,
                lastName,
                EmployerSubRole.Employer,
                organisation);
        }

        private static Employer CreateTestEmployer(this IEmployersCommand employersCommand, string emailAddress, string firstName, string lastName, EmployerSubRole subRole, IOrganisation organisation)
        {
            var employer = new Employer
            {
                IsEnabled = true,
                IsActivated = true,
                EmailAddress = new EmailAddress { Address = emailAddress, IsVerified = true },
                PhoneNumber = new PhoneNumber { Number = DefaultPhoneNumber, Type = PhoneNumberType.Work },
                FirstName = firstName,
                LastName = lastName,
                Organisation = organisation,
                SubRole = subRole,
            };

            employersCommand.CreateEmployer(employer);
            return employer;
        }
    }
}