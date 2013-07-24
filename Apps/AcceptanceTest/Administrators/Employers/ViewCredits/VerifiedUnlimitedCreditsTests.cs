using System;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.ViewCredits
{
    [TestClass]
    public class VerifiedUnlimitedCreditsTests
        : ViewCreditsTests
    {
        private DateTime? _expiryDate;

        protected override Employer CreateEmployer(Guid creditId)
        {
            _expiryDate = DateTime.Now.AddDays(100);
            return CreateEmployer(true, creditId, _expiryDate, null);
        }

        protected override bool HasAllocation
        {
            get { return true; }
        }

        protected override int? ExpectedCredits
        {
            get { return null; }
        }

        protected override DateTime? ExpectedExpiryDate
        {
            get { return _expiryDate; }
        }
    }
}