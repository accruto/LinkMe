using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Candidates.PhoneNumbers
{
    [TestClass]
    public class NoneHasNotAccessedTests
        : CreditsTests
    {
        protected override Employer CreateEmployer(Member member)
        {
            return CreateEmployer();
        }

        protected override bool HasBeenAccessed
        {
            get { return false; }
        }

        protected override CanContactStatus CanContact
        {
            get { return CanContactStatus.YesIfHadCredit; }
        }
    }
}
