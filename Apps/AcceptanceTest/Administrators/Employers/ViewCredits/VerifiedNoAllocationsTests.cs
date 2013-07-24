using System;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.ViewCredits
{
    [TestClass]
    public class VerifiedNoAllocationsTests
        : ViewCreditsTests
    {
        protected override Employer CreateEmployer(Guid creditId)
        {
            return CreateEmployer(true);
        }

        protected override bool HasAllocation
        {
            get { return false; }
        }

        protected override int? ExpectedCredits
        {
            get { return 0; }
        }

        protected override DateTime? ExpectedExpiryDate
        {
            get { return null; }
        }
    }
}