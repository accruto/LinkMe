using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Search.Criteria
{
    [TestClass]
    public class JobTypesTests
        : SearchTests
    {
        [TestMethod]
        public void TestJobTypes()
        {
            var fullTime = CreateMember(0, JobTypes.FullTime);
            var partTime = CreateMember(1, JobTypes.PartTime);
            var contract = CreateMember(2, JobTypes.Contract);
            var temp = CreateMember(3, JobTypes.Temp);
            var jobShare = CreateMember(4, JobTypes.JobShare);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            var model = Search(criteria);
            AssertMembers(model, fullTime, partTime, contract, temp, jobShare);

            criteria.JobTypes = JobTypes.FullTime;
            model = Search(criteria);
            AssertMembers(model, fullTime);

            criteria.JobTypes = JobTypes.PartTime;
            model = Search(criteria);
            AssertMembers(model, partTime);

            criteria.JobTypes = JobTypes.Contract;
            model = Search(criteria);
            AssertMembers(model, contract);

            criteria.JobTypes = JobTypes.Temp;
            model = Search(criteria);
            AssertMembers(model, temp);

            criteria.JobTypes = JobTypes.JobShare;
            model = Search(criteria);
            AssertMembers(model, jobShare);

            criteria.JobTypes = JobTypes.FullTime | JobTypes.Temp;
            model = Search(criteria);
            AssertMembers(model, fullTime, temp);

            criteria.JobTypes = JobTypes.All;
            model = Search(criteria);
            AssertMembers(model, fullTime, partTime, contract, temp, jobShare);
        }

        private Member CreateMember(int index, JobTypes jobTypes)
        {
            var member = CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredJobTypes = jobTypes;
            _candidatesCommand.UpdateCandidate(candidate);

            _memberSearchService.UpdateMember(member.Id);
            return member;
        }
    }
}
