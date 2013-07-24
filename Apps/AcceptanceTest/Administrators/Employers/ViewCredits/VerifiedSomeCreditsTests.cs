using System;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.ViewCredits
{
    [TestClass]
    public class VerifiedSomeCreditsTests
        : ViewCreditsTests
    {
        protected override Employer CreateEmployer(Guid creditId)
        {
            return CreateEmployer(true, creditId, null, 10);
        }

        protected override bool HasAllocation
        {
            get { return true; }
        }

        protected override int? ExpectedCredits
        {
            get { return 10; }
        }

        protected override DateTime? ExpectedExpiryDate
        {
            get { return null; }
        }
    }
}