using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search
{
    [TestClass]
    public class HelpPromptTests
        : SearchTests
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        private const string Keywords = "Archeologist";

        [TestMethod]
        public void TestAnonymousSearches()
        {
            TestSearches(null, true, 5);
        }

        [TestMethod]
        public void TestLimitedSearches()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 10 });
            TestSearches(employer, true, 10);
        }

        [TestMethod]
        public void TestUnlimitedSearches()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = null });
            TestSearches(employer, false, 0);
        }

        [TestMethod]
        public void TestAnonymousViewings()
        {
            TestViewings(null, true, 2);
        }

        [TestMethod]
        public void TestLimitedViewings()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 10 });
            TestViewings(employer, true, 3);
        }

        [TestMethod]
        public void TestUnlimitedViewings()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = null });
            TestViewings(employer, false, 0);
        }

        private void TestSearches(IUser employer, bool shouldShowPrompt, int searchesPrompt)
        {
            if (employer != null)
                LogIn(employer);

            for (var search = 0; search < searchesPrompt - 1; ++search)
            {
                Get(GetSearchUrl(Keywords));
                AssertNoPrompt();
            }

            // Do one more search.

            Get(GetSearchUrl(Keywords));
            if (shouldShowPrompt)
                AssertPrompt();
            else
                AssertNoPrompt();

            // Do some more, it should only get shown once.

            for (var search = 0; search < 10; ++search)
            {
                Get(GetSearchUrl(Keywords));
                AssertNoPrompt();
            }
        }

        private void TestViewings(IUser employer, bool shouldShowPrompt, int viewingsPrompt)
        {
            if (employer != null)
                LogIn(employer);

            var member = CreateMember(0);

            // Do an initial search.

            Get(GetSearchUrl(Keywords));
            AssertNoPrompt();

            // Do some viewings.

            for (var viewing = 0; viewing < viewingsPrompt + 1; ++viewing)
                Get(GetCandidatesUrl(member.Id));

            // Do one more search.

            Get(GetSearchUrl(Keywords));
            if (shouldShowPrompt)
                AssertPrompt();
            else
                AssertNoPrompt();

            // Do some more, it should only get shown once.

            for (var search = 0; search < 10; ++search)
            {
                Get(GetSearchUrl(Keywords));
                AssertNoPrompt();
            }
        }

        private void AssertNoPrompt()
        {
            AssertPageDoesNotContain("$(\".needhelpfindingcandidates\").dialog");
        }

        private void AssertPrompt()
        {
            AssertPageContains("$(\".needhelpfindingcandidates\").dialog");
        }
    }
}