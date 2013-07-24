using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMembersRepository = LinkMe.Domain.Users.Members.IMembersRepository;

namespace LinkMe.Query.Search.Test.Members.Search
{
    [TestClass]
    public class RangeTests
        : ExecuteMemberSearchTests
    {
        private readonly IMembersRepository _membersRepository = Resolve<IMembersRepository>();
        private readonly ICandidatesRepository _candidatesRepository = Resolve<ICandidatesRepository>();
        private readonly IResumesRepository _resumesRepository = Resolve<IResumesRepository>();

        [TestMethod]
        public void TestNoRange()
        {
            var members = CreateMembers(127);

            var criteria = new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated } };
            var execution = _executeMemberSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(members.Count, execution.Results.TotalMatches);
            Assert.IsTrue(members.SequenceEqual(execution.Results.MemberIds));

            criteria = new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated, ReverseSortOrder = true } };
            execution = _executeMemberSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(members.Count, execution.Results.TotalMatches);
            Assert.IsTrue(members.Reverse().SequenceEqual(execution.Results.MemberIds));
        }

        [TestMethod]
        public void TestOpenRange()
        {
            var members = CreateMembers(127);

            var criteria = new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated } };
            var execution = _executeMemberSearchCommand.Search(null, criteria, new Range());

            Assert.AreEqual(members.Count, execution.Results.TotalMatches);
            Assert.IsTrue(members.SequenceEqual(execution.Results.MemberIds));

            criteria = new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated, ReverseSortOrder = true } };
            execution = _executeMemberSearchCommand.Search(null, criteria, new Range());

            Assert.AreEqual(members.Count, execution.Results.TotalMatches);
            Assert.IsTrue(members.Reverse().SequenceEqual(execution.Results.MemberIds));
        }

        [TestMethod]
        public void TestMaximum()
        {
            var members = CreateMembers(127);

            for (var max = 0; max < 200; max += 10)
            {
                var criteria = new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated } };
                var execution = _executeMemberSearchCommand.Search(null, criteria, new Range(0, max));

                Assert.AreEqual(members.Count, execution.Results.TotalMatches);
                Assert.IsTrue(members.Take(max).SequenceEqual(execution.Results.MemberIds));

                criteria = new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated, ReverseSortOrder = true } };
                execution = _executeMemberSearchCommand.Search(null, criteria, new Range(0, max));

                Assert.AreEqual(members.Count, execution.Results.TotalMatches);
                Assert.IsTrue(members.Reverse().Take(max).SequenceEqual(execution.Results.MemberIds));
            }
        }

        [TestMethod]
        public void TestPaging()
        {
            var members = CreateMembers(127);

            const int page = 25;
            for (var index = 0; index * page < 200; ++index)
            {
                var criteria = new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated } };
                var execution = _executeMemberSearchCommand.Search(null, criteria, new Range(index * page, page));

                Assert.AreEqual(members.Count, execution.Results.TotalMatches);
                Assert.IsTrue(members.Skip(index * page).Take(page).SequenceEqual(execution.Results.MemberIds));

                criteria = new MemberSearchCriteria { SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated, ReverseSortOrder = true } };
                execution = _executeMemberSearchCommand.Search(null, criteria, new Range(index * page, page));

                Assert.AreEqual(members.Count, execution.Results.TotalMatches);
                Assert.IsTrue(members.Reverse().Skip(index * page).Take(page).SequenceEqual(execution.Results.MemberIds));
            }
        }

        private IList<Guid> CreateMembers(int count)
        {
            return (from i in Enumerable.Range(0, count)
                    select CreateMember(i).Id).ToList();
        }

        private Member CreateMember(int index)
        {
            var member = CreateMember(index, null);

            // Update all the times.

            var date = DateTime.Now.AddDays(-1*index);
            member.CreatedTime = date;
            member.LastUpdatedTime = date;
            _membersRepository.UpdateMember(member);

            var candidate = _candidatesRepository.GetCandidate(member.Id);
            candidate.LastUpdatedTime = date;
            _candidatesRepository.UpdateCandidate(candidate);

            var resume = _resumesRepository.GetResume(candidate.ResumeId.Value);
            resume.CreatedTime = date;
            resume.LastUpdatedTime = date;
            _resumesRepository.UpdateResume(resume);

            _memberSearchService.UpdateMember(member.Id);
            return member;
        }
    }
}