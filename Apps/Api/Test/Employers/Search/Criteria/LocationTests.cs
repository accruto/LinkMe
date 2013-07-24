using System.Collections.Generic;
using System.Linq;
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
    public class LocationTests
        : SearchTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IMembersRepository _membersRepository = Resolve<IMembersRepository>();

        private Country _australia;
        private Country _newZealand;

        private const string MelbourneVic3000 = "Melbourne VIC 3000";
        private const string ArmadaleVic3143 = "Armadale VIC 3143";
        private const string NorlaneVic3214 = "Norlane VIC 3214";
        private const string SydneyNsw2000 = "Sydney NSW 2000";
        private const string PerthWa6000 = "Perth WA 6000";

        private const string Melbourne = "Melbourne";
        private const string Sydney = "Sydney";
        private const string Perth = "Perth";

        private const string Vic = "VIC";
        private const string Nsw = "NSW";
        private const string Wa = "WA";

        private const string Auckland = "Auckland";
        private const string NorthIsland = "North Island";

        [TestInitialize]
        public void TestInitialize()
        {
            _australia = _locationQuery.GetCountry("Australia");
            _newZealand = _locationQuery.GetCountry("New Zealand");
        }

        [TestMethod]
        public void TestPostalSuburb()
        {
            var members = CreateMembers();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // No location.

            var model = Search(criteria);
            AssertMembers(model, members.Values.ToArray());

            // Search Melbourne VIC 3000.

            criteria.Location = _locationQuery.ResolveLocation(_australia, MelbourneVic3000);
            model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143], members[Melbourne]);

            // Search Norlane VIC 3124.

            criteria.Location = _locationQuery.ResolveLocation(_australia, NorlaneVic3214);
            model = Search(criteria);
            AssertMembers(model, members[NorlaneVic3214]);

            // Search Sydney NSW 2000.

            criteria.Location = _locationQuery.ResolveLocation(_australia, SydneyNsw2000);
            model = Search(criteria);
            AssertMembers(model, members[SydneyNsw2000], members[Sydney]);

            // Search Perth WA 6000.

            criteria.Location = _locationQuery.ResolveLocation(_australia, PerthWa6000);
            model = Search(criteria);
            AssertMembers(model);
        }

        [TestMethod]
        public void TestPostalSuburbDistance()
        {
            var members = CreateMembers();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Search in Melbourne VIC 3000 (default distance = 50).

            criteria.Location = _locationQuery.ResolveLocation(_australia, MelbourneVic3000);
            criteria.Distance = null;
            var model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143], members[Melbourne]);

            // Search within 5 km: no Armadale jobs.

            criteria.Location = _locationQuery.ResolveLocation(_australia, MelbourneVic3000);
            criteria.Distance = 5;
            model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000]);

            // Search within 10 km: Armadale back in.

            criteria.Location = _locationQuery.ResolveLocation(_australia, MelbourneVic3000);
            criteria.Distance = 10;
            model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143]);

            // Search within 100 km: Norlane included.

            criteria.Location = _locationQuery.ResolveLocation(_australia, MelbourneVic3000);
            criteria.Distance = 100;
            model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143], members[NorlaneVic3214], members[Melbourne]);

            // Search in Norlane VIC 3124 (default distance = 50).

            criteria.Location = _locationQuery.ResolveLocation(_australia, NorlaneVic3214);
            criteria.Distance = null;
            model = Search(criteria);
            AssertMembers(model, members[NorlaneVic3214]);

            // Search within 5 km.

            criteria.Location = _locationQuery.ResolveLocation(_australia, NorlaneVic3214);
            criteria.Distance = 5;
            model = Search(criteria);
            AssertMembers(model, members[NorlaneVic3214]);

            // Search within 10 km.

            criteria.Location = _locationQuery.ResolveLocation(_australia, NorlaneVic3214);
            criteria.Distance = 10;
            model = Search(criteria);
            AssertMembers(model, members[NorlaneVic3214]);

            // Search within 100 km.

            criteria.Location = _locationQuery.ResolveLocation(_australia, NorlaneVic3214);
            criteria.Distance = 100;
            model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143], members[NorlaneVic3214]);
        }

        [TestMethod]
        public void TestRegion()
        {
            var members = CreateMembers();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Search within Melbourne.

            criteria.Location = _locationQuery.ResolveLocation(_australia, Melbourne);
            var model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143], members[Melbourne]);

            // Search within Sydney.

            criteria.Location = _locationQuery.ResolveLocation(_australia, Sydney);
            model = Search(criteria);
            AssertMembers(model, members[SydneyNsw2000], members[Sydney]);

            // Search within Perth.

            criteria.Location = _locationQuery.ResolveLocation(_australia, Perth);
            model = Search(criteria);
            AssertMembers(model);
        }

        [TestMethod]
        public void TestCountrySubdivision()
        {
            var members = CreateMembers();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Search within VIC.

            criteria.Location = _locationQuery.ResolveLocation(_australia, Vic);
            var model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143], members[NorlaneVic3214], members[Melbourne], members[Vic]);

            // Search within NSW.

            criteria.Location = _locationQuery.ResolveLocation(_australia, Nsw);
            model = Search(criteria);
            AssertMembers(model, members[SydneyNsw2000], members[Sydney], members[Nsw]);

            // Search within WA.

            criteria.Location = _locationQuery.ResolveLocation(_australia, Wa);
            model = Search(criteria);
            AssertMembers(model);
        }

        [TestMethod]
        public void TestCountry()
        {
            var members = CreateMembers();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Search within Australia.

            criteria.Location = _locationQuery.ResolveLocation(_australia, "");
            var model = Search(criteria);
            AssertMembers(model, members[MelbourneVic3000], members[ArmadaleVic3143], members[NorlaneVic3214], members[Melbourne], members[Vic], members[SydneyNsw2000], members[Sydney], members[Nsw], members[_australia.Name]);

            // Search within New Zealand.

            criteria.Location = _locationQuery.ResolveLocation(_newZealand, "");
            model = Search(criteria);
            AssertMembers(model, members[Auckland], members[NorthIsland], members[_newZealand.Name]);

            // No matter what you put in for New Zealand the same jobs will come up.

            criteria.Location = _locationQuery.ResolveLocation(_newZealand, Auckland);
            model = Search(criteria);
            AssertMembers(model, members[Auckland], members[NorthIsland], members[_newZealand.Name]);

            criteria.Location = _locationQuery.ResolveLocation(_newZealand, NorthIsland);
            model = Search(criteria);
            AssertMembers(model, members[Auckland], members[NorthIsland], members[_newZealand.Name]);

            criteria.Location = _locationQuery.ResolveLocation(_newZealand, "xyz");
            model = Search(criteria);
            AssertMembers(model, members[Auckland], members[NorthIsland], members[_newZealand.Name]);
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
            members[_newZealand.Name] = CreateMember(ref index, _newZealand, "");

            // Create some New Zealand members.

            members[Auckland] = CreateMember(ref index, _newZealand, Auckland);
            members[NorthIsland] = CreateMember(ref index, _newZealand, NorthIsland);

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