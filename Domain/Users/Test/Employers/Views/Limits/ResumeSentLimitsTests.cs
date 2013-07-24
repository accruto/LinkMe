using LinkMe.Domain.Users.Employers.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.Limits
{
    [TestClass]
    public class ResumeSentLimitsTests
        : LimitsTests
    {
        protected override MemberAccessReason GetAccessReason()
        {
            return MemberAccessReason.ResumeSent;
        }

        protected override int DailyLimit
        {
            get { return 300; }
        }

        protected override int BulkDailyLimit
        {
            get { return 80; }
        }
    }
}
