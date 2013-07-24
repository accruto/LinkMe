using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views
{
    [TestClass]
    public class HierarchyTests
        : ViewsTests
    {
        [TestMethod]
        public void TestEmployerOrganisation()
        {
            //                          Organisation (credits)
            //                        /           |           \
            //      employer0 (credits)       employer1    employer2

            var credit = _creditsQuery.GetCredit<ContactCredit>();

            // Create employers and allocate credits.

            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());
            var employers = (from i in Enumerable.Range(0, 3) select _employersCommand.CreateTestEmployer(i, organisation)).ToArray();

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employers[0].Id, CreditId = credit.Id, InitialQuantity = 20 });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = organisation.Id, CreditId = credit.Id, InitialQuantity = 20 });

            // Create members to contact.

            var members = (from i in Enumerable.Range(0, 2) select CreateMember(i)).ToArray();
            var canContacts = (from i in Enumerable.Range(0, employers.Length) select new List<int>()).ToArray();

            // Check first.

            AssertViews(employers, members, canContacts);

            // employer0 => member0, personal credits => employer0 owns.

            _employerCreditsCommand.ExerciseContactCredit(employers[0], _employerMemberViewsQuery.GetProfessionalView(employers[0], members[0]));
            canContacts[0].Add(0);
            AssertViews(employers, members, canContacts);

            // employer1 => member1, organisation credits => organisation owns.

            _employerCreditsCommand.ExerciseContactCredit(employers[1], _employerMemberViewsQuery.GetProfessionalView(employers[1], members[1]));
            canContacts[0].Add(1);
            canContacts[1].Add(1);
            canContacts[2].Add(1);
            AssertViews(employers, members, canContacts);

            // employer1 => member0, organisation credits => organisation owns.

            _employerCreditsCommand.ExerciseContactCredit(employers[1], _employerMemberViewsQuery.GetProfessionalView(employers[1], members[0]));
            canContacts[1].Add(0);
            canContacts[2].Add(0);
            AssertViews(employers, members, canContacts);
        }

        [TestMethod]
        public void TestEmployerParentOrganisation()
        {
            //                                     Parent Organisation (credits)
            //                                   /               |              \
            //             Organisation (credits)       employer2 (credits)     employer3
            //            /                      \
            //      employer0 (credits)       employer1

            var credit = _creditsQuery.GetCredit<ContactCredit>();

            // Create employers and allocate credits.

            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, Guid.NewGuid());
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(2, parentOrganisation, Guid.NewGuid());

            var employer1 = _employersCommand.CreateTestEmployer(1, organisation);
            var employer2 = _employersCommand.CreateTestEmployer(2, organisation);
            var employer3 = _employersCommand.CreateTestEmployer(3, parentOrganisation);
            var employer4 = _employersCommand.CreateTestEmployer(4, parentOrganisation);
            var employers = new[] { employer1, employer2, employer3, employer4 };

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = parentOrganisation.Id, CreditId = credit.Id, InitialQuantity = 20 });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = organisation.Id, CreditId = credit.Id, InitialQuantity = 20 });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer1.Id, CreditId = credit.Id, InitialQuantity = 20 });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer3.Id, CreditId = credit.Id, InitialQuantity = 20 });

            // Create members to contact.

            var members = (from i in Enumerable.Range(0, 4) select CreateMember(i)).ToArray();
            var canContacts = (from i in Enumerable.Range(0, employers.Length) select new List<int>()).ToArray();

            // Check first.

            AssertViews(employers, members, canContacts);

            // employer0 => member0, personal credits => employer0 owns.

            _employerCreditsCommand.ExerciseContactCredit(employers[0], _employerMemberViewsQuery.GetProfessionalView(employers[0], members[0]));
            canContacts[0].Add(0);
            AssertViews(employers, members, canContacts);

            // employer1 => member1, organisation credits => organisation owns.

            _employerCreditsCommand.ExerciseContactCredit(employers[1], _employerMemberViewsQuery.GetProfessionalView(employers[1], members[1]));
            canContacts[0].Add(1);
            canContacts[1].Add(1);
            AssertViews(employers, members, canContacts);

            // employer2 => member2, personal credits => employer2 owns.

            _employerCreditsCommand.ExerciseContactCredit(employers[2], _employerMemberViewsQuery.GetProfessionalView(employers[2], members[2]));
            canContacts[2].Add(2);
            AssertViews(employers, members, canContacts);

            // employer3 => member3, parent organisation credits => parent organisation owns.

            _employerCreditsCommand.ExerciseContactCredit(employers[3], _employerMemberViewsQuery.GetProfessionalView(employers[3], members[3]));
            canContacts[0].Add(3);
            canContacts[1].Add(3);
            canContacts[2].Add(3);
            canContacts[3].Add(3);
            AssertViews(employers, members, canContacts);

            // employer3 => member2, parent organisation credits => parent organisation owns.

            _employerCreditsCommand.ExerciseContactCredit(employers[3], _employerMemberViewsQuery.GetProfessionalView(employers[3], members[2]));
            canContacts[0].Add(2);
            canContacts[1].Add(2);
            canContacts[3].Add(2);
            AssertViews(employers, members, canContacts);

            // employer3 => member1, parent organisation credits => parent organisation owns.

            _employerCreditsCommand.ExerciseContactCredit(employers[3], _employerMemberViewsQuery.GetProfessionalView(employers[3], members[1]));
            canContacts[2].Add(1);
            canContacts[3].Add(1);
            AssertViews(employers, members, canContacts);

            // employer3 => member0, parent organisation credits => parent organisation owns.

            _employerCreditsCommand.ExerciseContactCredit(employers[3], _employerMemberViewsQuery.GetProfessionalView(employers[3], members[0]));
            canContacts[1].Add(0);
            canContacts[2].Add(0);
            canContacts[3].Add(0);
            AssertViews(employers, members, canContacts);
        }

        private void AssertViews(Employer[] employers, Member[] members, List<int>[] canContacts)
        {
            for (var e = 0; e < employers.Length; ++e)
            {
                for (var m = 0; m < members.Length; ++m)
                {
                    if (canContacts[e].Contains(m))
                        AssertView(employers[e], members[m], CanContactStatus.YesWithoutCredit, true, ProfessionalContactDegree.Contacted);
                    else
                        AssertView(employers[e], members[m], CanContactStatus.YesWithCredit, true, ProfessionalContactDegree.NotContacted);
                }
            }
        }
    }
}