using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Candidates.PhoneNumbers
{
    [TestClass]
    public class UnlimitedHasNotAccessedTests
        : CreditsTests
    {
        protected override Employer CreateEmployer(Member member)
        {
            var employer = CreateEmployer();
            Allocate(employer.Id, null);
            return employer;
        }

        protected override bool HasBeenAccessed
        {
            get { return false; }
        }

        protected override CanContactStatus CanContact
        {
            get { return CanContactStatus.YesWithoutCredit; }
        }
    }
}
