using System;
using System.Globalization;
using System.Linq;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers
{
    public abstract class ViewCreditsUsageTests
        : EmployersTests
    {
        protected readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        protected readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        protected readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        protected readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        protected readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        protected readonly IEmployerCreditsCommand _employerCreditsCommand = Resolve<IEmployerCreditsCommand>();
        protected readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        protected abstract class UsedOn
        {
            public abstract string Name { get; }
        }

        private class UsedOnMember
            : UsedOn
        {
            private readonly Member _member;

            public UsedOnMember(Member member)
            {
                _member = member;
            }

            public override string Name
            {
                get { return _member.FullName; }
            }
        }

        protected class UsedOnJobAd
            : UsedOn
        {
            private readonly JobAdEntry _jobAd;

            public UsedOnJobAd(JobAdEntry jobAd)
            {
                _jobAd = jobAd;
            }

            public override string Name
            {
                get { return TextUtil.TruncateForDisplay(_jobAd.Title, 50); }
            }
        }

        protected void AssertCredits<T>(Administrator administrator, Employer employer, Member member, ICreditOwner owner, bool hasExercisedCredit, Allocation allocation, Allocation[] allAllocations)
            where T : Credit
        {
            AssertCredits<T>(administrator, employer, new UsedOnMember(member), owner, hasExercisedCredit, allocation, allAllocations);
        }

        protected void AssertCredits<T>(Administrator administrator, Employer employer, JobAdEntry jobAd, ICreditOwner owner, bool hasExercisedCredit, Allocation allocation, Allocation[] allAllocations)
            where T : Credit
        {
            AssertCredits<T>(administrator, employer, jobAd == null ? null : new UsedOnJobAd(jobAd), owner, hasExercisedCredit, allocation, allAllocations);
        }

        private void AssertCredits<T>(IUser administrator, IEmployer employer, UsedOn usedOn, ICreditOwner owner, bool hasExercisedCredit, Allocation allocation, Allocation[] allAllocations)
            where T : Credit
        {
            LogIn(administrator);

            // Check who the employer has exercised credits on.

            AssertEmployerCreditUsage<T>(employer, owner, hasExercisedCredit, usedOn);

            if (owner is Employer)
            {
                // Check their credits.

                AssertEmployerCredits(employer, allAllocations);

                // Check the allocation.

                if (allocation != null)
                    AssertEmployerCreditUsage(employer, allocation, hasExercisedCredit, usedOn);
            }
            else if (owner is Organisation)
            {
                var organisation = owner as Organisation;

                // Check their credits.

                AssertOrganisationCredits(organisation, allAllocations);

                // Check who has used the organisation's credits.

                AssertOrganisationCreditUsage<T>(organisation, employer, hasExercisedCredit, usedOn);

                // Check the allocation.

                if (allocation != null)
                    AssertOrganisationCreditUsage(organisation, employer, allocation, hasExercisedCredit, usedOn);
            }

            LogOut();
        }

        protected void AssertEmployerCredits(IEmployer employer, params Allocation[] allocations)
        {
            Get(GetCreditsUrl(employer));

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            if (allocations.Length > 0)
            {
                Assert.IsTrue(nodes != null && nodes.Count == allocations.Length);

                foreach (var node in nodes)
                {
                    var status = node.SelectSingleNode("td[position()=2]").InnerText;
                    var initialQuantity = node.SelectSingleNode("td[position()=3]").InnerText;
                    var remainingQuantity = node.SelectSingleNode("td[position()=4]").InnerText;

                    var found = allocations.Any(
                        allocation =>
                            GetStatus(allocation) == status
                            && GetQuantity(allocation.InitialQuantity) == initialQuantity
                            && GetQuantity(allocation.RemainingQuantity) == remainingQuantity);

                    Assert.IsTrue(found);
                }

                AssertPageDoesNotContain("There are no credits allocated to this employer.");
            }
            else
            {
                Assert.IsTrue(nodes == null || nodes.Count == 0);
                AssertPageContains("There are no credits allocated to this employer.");
            }
        }

        private static string GetQuantity(int? quantity)
        {
            return quantity == null ? "unlimited" : quantity.Value.ToString(CultureInfo.InvariantCulture);
        }

        private static string GetStatus(Allocation allocation)
        {
            if (allocation.IsDeallocated)
                return "Deallocated";

            if (allocation.HasExpired)
                return "Expired";

            if (allocation.IsUnlimited || (allocation.RemainingQuantity != null && allocation.RemainingQuantity.Value > 0))
                return "Active";

            return "Inactive";
        }

        protected void AssertEmployerCreditUsage<T>(IEmployer employer, ICreditOwner owner, bool hasExercisedCredit, params Member[] members)
            where T : Credit
        {
            AssertEmployerCreditUsage<T>(employer, owner, hasExercisedCredit, (from m in members select (UsedOn)new UsedOnMember(m)).ToArray());
        }

        protected void AssertEmployerCreditUsage<T>(IEmployer employer, ICreditOwner owner, bool hasExercisedCredit, params UsedOn[] usedOns)
            where T : Credit
        {
            Get(GetCreditsUsageUrl(employer));

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            if (hasExercisedCredit)
            {
                AssertPageDoesNotContain("This employer has not used any credits.");
                Assert.IsTrue(nodes != null && nodes.Count == usedOns.Length);
                foreach (var node in nodes)
                {
                    Assert.AreEqual(_creditsQuery.GetCredit<T>().ShortDescription, node.SelectSingleNode("td[position()=2]").InnerText);
                    if (owner != null)
                        Assert.AreEqual(owner.FullName, node.SelectSingleNode("td[position()=3]/a").InnerText);
                    Assert.IsTrue((from u in usedOns where u.Name == node.SelectSingleNode("td[position()=4]/a").InnerText select u).Any());
                }
            }
            else
            {
                Assert.IsTrue(nodes == null || nodes.Count == 0);
                AssertPageContains("This employer has not used any credits.");
            }
        }

        protected void AssertEmployerCreditUsage(IEmployer employer, Allocation allocation, bool hasExercisedCredit, params Member[] members)
        {
            AssertEmployerCreditUsage(employer, allocation, hasExercisedCredit, (from m in members select (UsedOn)new UsedOnMember(m)).ToArray());
        }

        protected void AssertEmployerCreditUsage(IEmployer employer, Allocation allocation, bool hasExercisedCredit, params UsedOn[] usedOns)
        {
            Get(GetCreditsUsageUrl(employer, allocation));

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            if (hasExercisedCredit)
            {
                Assert.IsTrue(nodes != null && nodes.Count == usedOns.Length);
                foreach (var node in nodes)
                    Assert.IsTrue((from u in usedOns where u.Name == node.SelectSingleNode("td[position()=2]/a").InnerText select u).Any());

                AssertPageDoesNotContain("This allocation has not had any credits used.");
            }
            else
            {
                Assert.IsTrue(nodes == null || nodes.Count == 0);
                AssertPageContains("This allocation has not had any credits used.");
            }
        }

        protected void AssertOrganisationCredits(Organisation organisation, params Allocation[] allocations)
        {
            Get(GetCreditsUrl(organisation));

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            if (allocations.Length > 0)
            {
                Assert.IsTrue(nodes != null && nodes.Count == allocations.Length);

                foreach (var node in nodes)
                {
                    var status = node.SelectSingleNode("td[position()=2]").InnerText;
                    var initialQuantity = node.SelectSingleNode("td[position()=3]").InnerText;
                    var remainingQuantity = node.SelectSingleNode("td[position()=4]").InnerText;

                    var found = allocations.Any(
                        allocation =>
                            GetStatus(allocation) == status
                            && GetQuantity(allocation.InitialQuantity) == initialQuantity
                            && GetQuantity(allocation.RemainingQuantity) == remainingQuantity);

                    Assert.IsTrue(found);
                }

                AssertPageDoesNotContain("There are no credits allocated to this organisation.");
            }
            else
            {
                Assert.IsTrue(nodes == null || nodes.Count == 0);
                AssertPageContains("There are no credits allocated to this organisation.");
            }
        }

        protected void AssertOrganisationCreditUsage<T>(Organisation organisation, IRegisteredUser employer, bool hasExercisedCredit, params Member[] members)
            where T : Credit
        {
            AssertOrganisationCreditUsage<T>(organisation, employer, hasExercisedCredit, (from m in members select (UsedOn)new UsedOnMember(m)).ToArray());
        }

        protected void AssertOrganisationCreditUsage<T>(Organisation organisation, IRegisteredUser employer, bool hasExercisedCredit, params UsedOn[] usedOns)
            where T : Credit
        {
            Get(GetCreditsUsageUrl(organisation));

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            if (hasExercisedCredit)
            {
                Assert.IsTrue(nodes != null && nodes.Count == usedOns.Length);

                foreach (var node in nodes)
                {
                    Assert.AreEqual(_creditsQuery.GetCredit<T>().ShortDescription, node.SelectSingleNode("td[position()=2]").InnerText);
                    Assert.AreEqual(employer.FullName, node.SelectSingleNode("td[position()=3]/a").InnerText);
                    Assert.IsTrue((from u in usedOns where u.Name == node.SelectSingleNode("td[position()=4]/a").InnerText select u).Any());
                }

                AssertPageDoesNotContain("This organisation has not had any credits used.");
            }
            else
            {
                Assert.IsTrue(nodes == null || nodes.Count == 0);
                AssertPageContains("This organisation has not had any credits used.");
            }
        }

        protected void AssertOrganisationCreditUsage(Organisation organisation, IEmployer employer, Allocation allocation, bool hasExercisedCredit, params Member[] members)
        {
            AssertOrganisationCreditUsage(organisation, employer, allocation, hasExercisedCredit, (from m in members select (UsedOn)new UsedOnMember(m)).ToArray());
        }

        protected void AssertOrganisationCreditUsage(Organisation organisation, IEmployer employer, Allocation allocation, bool hasExercisedCredit, params UsedOn[] usedOns)
        {
            Get(GetCreditsUsageUrl(organisation, allocation));

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//table[@class='list']/tbody/tr");
            if (hasExercisedCredit)
            {
                Assert.IsTrue(nodes != null && nodes.Count == usedOns.Length);

                for (var index = 0; index < usedOns.Length; ++index)
                {
                    var node = nodes[index];
                    Assert.AreEqual(employer.FullName, node.SelectSingleNode("td[position()=2]/a").InnerText);
                    Assert.IsTrue((from u in usedOns where u.Name == node.SelectSingleNode("td[position()=3]/a").InnerText select u).Any());
                }

                AssertPageDoesNotContain("This allocation has not had any credits used.");
            }
            else
            {
                Assert.IsTrue(nodes == null || nodes.Count == 0);
                AssertPageContains("This allocation has not had any credits used.");
            }
        }

        protected Allocation GetAllocation<TCredit>(Guid ownerId)
            where TCredit : Credit
        {
            var allocations = _allocationsQuery.GetAllocationsByOwnerId<TCredit>(ownerId);
            if (allocations.Count == 0)
                return null;

            if (allocations.Count > 1)
            {
                if (allocations.Any(a => a.RemainingQuantity != 0))
                    Assert.Fail("There are more than 1 allocations.");
            }

            return allocations[0];
        }

        protected Employer CreateEmployer(bool verified)
        {
            var organisation = verified
                ? _organisationsCommand.CreateTestVerifiedOrganisation(0, null, Guid.NewGuid())
                : _organisationsCommand.CreateTestOrganisation(0);
            return _employerAccountsCommand.CreateTestEmployer(1, organisation);
        }

        protected Member CreateMember(int index)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        protected Administrator CreateAdministrator()
        {
            return _administratorAccountsCommand.CreateTestAdministrator(3);
        }
    }
}
