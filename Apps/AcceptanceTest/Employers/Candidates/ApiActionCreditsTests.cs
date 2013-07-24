using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Views;

namespace LinkMe.AcceptanceTest.Employers.Candidates
{
    public abstract class ApiActionCreditsTests
        : ActionCreditsTests
    {
        protected abstract JsonResponseModel CallAction(Member[] members);
        protected abstract MemberAccessReason? AssertModel(JsonResponseModel model, Employer employer, Member[] members);

        protected override MemberAccessReason? PerformAction(bool isApplicant, CreditInfo creditInfo, bool isLoggedIn, Employer employer, Member[] members)
        {
            var model = CallAction(members);
            if (isLoggedIn)
            {
                if (isApplicant || ((creditInfo.CanContact || creditInfo.HasUsedCredit) && !creditInfo.HasExpired))
                    return AssertModel(model, employer, members);

                AssertJsonError(model, null, "You need " + members.Length + " credit" + (members.Length > 1 ? "s" : "") + " to perform this action but you have none available.");
                return null;
            }

            AssertJsonError(model, null, "100", "The user is not logged in.");
            return null;
        }
    }
}
