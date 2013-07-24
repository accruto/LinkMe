using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMembersRepository=LinkMe.Domain.Users.Members.IMembersRepository;

namespace LinkMe.Apps.Api.Test.Employers.Search.Criteria
{
    [TestClass]
    public class DistanceTests
        : SearchTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IMembersRepository _membersRepository = Resolve<IMembersRepository>();

        private Country _australia;
        private const string MelbourneVic3000 = "Melbourne VIC 3000";
        private const string ArmadaleVic3143 = "Armadale VIC 3143";
        private const string NorlaneVic3214 = "Norlane VIC 3214";
        private const string SydneyNsw2000 = "Sydney NSW 2000";

        private const string Melbourne = "Melbourne";
        private const string Sydney = "Sydney";

        private const string Vic = "VIC";
        private const string Nsw = "NSW";

        [TestInitialize]
        public void TestInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");
        }

        [TestMethod]
        public void TestDistance()
        {
            var members = CreateMembers();

            // Simple search in Melbourne VIC 3000 (default distance = 50).

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            criteria.Location = _locationQuery.ResolveLocation(_australia, MelbourneVic3000);
            var model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143], members[Melbourne]);

            // Search within 5 km: no Armadale jobs.

            criteria.Distance = 5;
            model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000]);

            // Search within 10 km: Armadale back in.

            criteria.Distance = 10;
            model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143]);

            // Search within 100 km: Norlane included.

            criteria.Distance = 100;
            model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143], members[NorlaneVic3214], members[Melbourne]);

            // Simple search in Norlane VIC 3124 (default distance = 50).

            criteria.Location = _locationQuery.ResolveLocation(_australia, NorlaneVic3214);
            criteria.Distance = null;
            model = Search(criteria);
            AssertMembers(model, members[NorlaneVic3214]);

            // Search within 5 km.

            criteria.Distance = 5;
            model = Search(criteria);
            AssertMembers(model, members[NorlaneVic3214]);

            // Search within 10 km.

            criteria.Distance = 10;
            model = Search(criteria);
            AssertMembers(model, members[NorlaneVic3214]);

            // Search within 100 km.

            criteria.Distance = 100;
            model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143], members[NorlaneVic3214]);
        }

        private IDictionary<string, Member> CreateMembers()
        {
            var members = new Dictionary<string, Member>();

            var index = 0;

            // Create members in the localities.

            members[MelbourneVic3000] = CreateMember(ref index, _australia, MelbourneVic3000);
            members[ArmadaleVic3143] = CreateMember(ref index, _australia, ArmadaleVic3143);
            members[NorlaneVic3214] = CreateMember(ref index, _australia, NorlaneVic3214);
            members[SydneyNsw2000] = CreateMember(ref index, _australia, SydneyNsw2000);

            // Create members in the regions.

            members[Melbourne] = CreateMember(ref index, _australia, Melbourne);
            members[Sydney] = CreateMember(ref index, _australia, Sydney);

            // Create members in the subdivisions.

            members[Vic] = CreateMember(ref index, _australia, Vic);
            members[Nsw] = CreateMember(ref index, _australia, Nsw);

            // Create members in the countries.

            members[_australia.Name] = CreateMember(ref index, _australia, "");

            return members;
        }

        private Member CreateMember(ref int index, Country country, string location)
        {
            var member = _memberAccountsCommand.CreateTestMember(++index);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Update the location.

            member.Address.Location = _locationQuery.ResolveLocation(country, location);
            _membersRepository.UpdateMember(member);
            _memberSearchService.UpdateMember(member.Id);

            return member;
        }
    }
}
