using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Credits
{
    [TestClass]
    public class VerifiedUnlimitedCreditsTests
        : CreditsTests
    {
        protected override Employer CreateEmployer(Member member)
        {
            return CreateEmployer(true, null);
        }

        protected override bool CanContact
        {
            get { return true; }
        }

        protected override bool HasUsedCredit
        {
            get
            {
                // Unlimited always means you get access.

                return true;
            }
        }

        protected override bool ShouldUseCredit
        {
            get { return false; }
        }
    }
}