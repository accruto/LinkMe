using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;

namespace LinkMe.AcceptanceTest.Employers.Candidates
{
    public abstract class ApiActionCreditLimitsTests
        : ActionCreditLimitsTests
    {
        protected abstract JsonResponseModel CallAction(Member member);

        protected override void PerformAction(Employer employer, Member member)
        {
            var model = CallAction(member);
            AssertJsonError(model, null, "Please call LinkMe on 1800 546-563 to contact additional candidates.");
        }
    }
}
