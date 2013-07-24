using System;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.ViewCredits
{
    [TestClass]
    public class UnverifiedNoCreditsTests
        : ViewCreditsTests
    {
        protected override Employer CreateEmployer(Guid creditId)
        {
            return CreateEmployer(false, creditId, null, 0);
        }

        protected override bool HasAllocation
        {
            get { return true; }
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