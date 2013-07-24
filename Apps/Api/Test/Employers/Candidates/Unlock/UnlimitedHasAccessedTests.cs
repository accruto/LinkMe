using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Candidates.Unlock
{
    [TestClass]
    public class UnlimitedHasAccessedTests
        : CreditTests
    {
        protected override Employer CreateEmployer(Member member)
        {
            var employer = CreateEmployer();
            Allocate(employer.Id, null);
            AccessMember(employer, member);
            return employer;
        }

        protected override bool HasBeenAccessed
        {
            get { return true; }
        }

        protected override CanContactStatus CanContact
        {
            get { return CanContactStatus.YesWithoutCredit; }
        }
    }
}