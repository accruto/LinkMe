using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.Contacts
{
    [TestClass]
    public class AnonymousContactTests
        : ContactTests
    {
        protected override IEmployer CreateEmployer()
        {
            // Anonymous means no employer.

            return null;
        }

        protected override void AllocateCredits<T>(IEmployer employer, int? quantity)
        {
        }
    }
}