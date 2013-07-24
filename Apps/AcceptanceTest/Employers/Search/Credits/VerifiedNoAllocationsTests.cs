using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Credits
{
    [TestClass]
    public class VerifiedNoAllocationsTests
        : CreditsTests
    {
        protected override Employer CreateEmployer(Member member)
        {
            return CreateEmployer(true);
        }

        protected override bool CanContact
        {
            get { return false; }
        }

        protected override bool HasUsedCredit
        {
            get { return false; }
        }

        protected override bool ShouldUseCredit
        {
            get { return false; }
        }
    }
}