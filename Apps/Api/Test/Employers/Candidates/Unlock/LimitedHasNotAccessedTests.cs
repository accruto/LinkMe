using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Candidates.Unlock
{
    [TestClass]
    public class LimitedHasNotAccessedTests
        : CreditTests
    {
        protected override Employer CreateEmployer(Member member)
        {
            var employer = CreateEmployer();
            Allocate(employer.Id, 10);
            return employer;
        }

        protected override bool HasBeenAccessed
        {
            get { return false; }
        }

        protected override CanContactStatus CanContact
        {
            get { return CanContactStatus.YesWithCredit; }
        }
    }
}