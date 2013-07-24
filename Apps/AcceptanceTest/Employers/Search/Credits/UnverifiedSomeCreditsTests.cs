using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Credits
{
    [TestClass]
    public class UnverifiedSomeCreditsTests
        : CreditsTests
    {
        protected override Employer CreateEmployer(Member member)
        {
            return CreateEmployer(false, 10);
        }

        protected override bool CanContact
        {
            get { return true; }
        }

        protected override bool HasUsedCredit
        {
            get { return false; }
        }

        protected override bool ShouldUseCredit
        {
            get { return true; }
        }
    }
}