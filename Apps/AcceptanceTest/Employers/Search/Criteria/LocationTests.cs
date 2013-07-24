using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Verticals;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Verticals;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Framework.Content;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMembersRepository=LinkMe.Domain.Users.Members.IMembersRepository;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class LocationTests
        : CriteriaTests
    {
        private readonly IMembersRepository _membersRepository = Resolve<IMembersRepository>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

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
            _newZealand = _locationQuery.GetCountry("New Zealand");
        }

        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.Location = null;
            TestDisplay(criteria);

            criteria.Location = _locationQuery.ResolveLocation(Australia, MelbourneVic3000);
            TestDisplay(criteria);

            criteria.Distance = 100;
            TestDisplay(criteria);

            criteria.Location = _locationQuery.ResolveLocation(_newZealand, null);
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestPostalSuburb()
        {
            var members = CreateMembers();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // No location.

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members.Values.ToArray());

            // Search Melbourne VIC 3000.

            criteria.Location = _locationQuery.ResolveLocation(Australia, MelbourneVic3000);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[MelbourneVic3000], members[ArmadaleVic3143], members[Melbourne]);

            // Search Norlane VIC 3124.

            criteria.Location = _locationQuery.ResolveLocation(Australia, NorlaneVic3214);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[NorlaneVic3214]);

            // Search Sydney NSW 2000.

            criteria.Location = _locationQuery.ResolveLocation(Australia, SydneyNsw2000);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[SydneyNsw2000], members[Sydney]);

            // Search Perth WA 6000.

            criteria.Location = _locationQuery.ResolveLocation(Australia, PerthWa6000);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();
        }

        [TestMethod]
        public void TestPostalSuburbDistance()
        {
            var members = CreateMembers();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Search in Melbourne VIC 3000 (default distance = 50).

            criteria.Location = _locationQuery.ResolveLocation(Australia, MelbourneVic3000);
            criteria.Distance = null;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[MelbourneVic3000], members[ArmadaleVic3143], members[Melbourne]);

            // Search within 5 km: no Armadale jobs.

            criteria.Location = _locationQuery.ResolveLocation(Australia, MelbourneVic3000);
            criteria.Distance = 5;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[MelbourneVic3000]);

            // Search within 10 km: Armadale back in.

            criteria.Location = _locationQuery.ResolveLocation(Australia, MelbourneVic3000);
            criteria.Distance = 10;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[MelbourneVic3000], members[ArmadaleVic3143]);

            // Search within 100 km: Norlane included.

            criteria.Location = _locationQuery.ResolveLocation(Australia, MelbourneVic3000);
            criteria.Distance = 100;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[MelbourneVic3000], members[ArmadaleVic3143], members[NorlaneVic3214], members[Melbourne]);

            // Search in Norlane VIC 3124 (default distance = 50).

            criteria.Location = _locationQuery.ResolveLocation(Australia, NorlaneVic3214);
            criteria.Distance = null;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[NorlaneVic3214]);

            // Search within 5 km.

            criteria.Location = _locationQuery.ResolveLocation(Australia, NorlaneVic3214);
            criteria.Distance = 5;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[NorlaneVic3214]);

            // Search within 10 km.

            criteria.Location = _locationQuery.ResolveLocation(Australia, NorlaneVic3214);
            criteria.Distance = 10;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[NorlaneVic3214]);

            // Search within 100 km.

            criteria.Location = _locationQuery.ResolveLocation(Australia, NorlaneVic3214);
            criteria.Distance = 100;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[MelbourneVic3000], members[ArmadaleVic3143], members[NorlaneVic3214]);
        }

        [TestMethod]
        public void TestRegion()
        {
            var members = CreateMembers();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Search within Melbourne.

            criteria.Location = _locationQuery.ResolveLocation(Australia, Melbourne);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[MelbourneVic3000], members[ArmadaleVic3143], members[Melbourne]);

            // Search within Sydney.

            criteria.Location = _locationQuery.ResolveLocation(Australia, Sydney);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[SydneyNsw2000], members[Sydney]);

            // Search within Perth.

            criteria.Location = _locationQuery.ResolveLocation(Australia, Perth);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();
        }

        [TestMethod]
        public void TestCountrySubdivision()
        {
            var members = CreateMembers();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Search within VIC.

            criteria.Location = _locationQuery.ResolveLocation(Australia, Vic);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[MelbourneVic3000], members[ArmadaleVic3143], members[NorlaneVic3214], members[Melbourne], members[Vic]);

            // Search within NSW.

            criteria.Location = _locationQuery.ResolveLocation(Australia, Nsw);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[SydneyNsw2000], members[Sydney], members[Nsw]);

            // Search within WA.

            criteria.Location = _locationQuery.ResolveLocation(Australia, Wa);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();
        }

        [TestMethod]
        public void TestCountry()
        {
            var members = CreateMembers();

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Search within Australia.

            criteria.Location = _locationQuery.ResolveLocation(Australia, "");
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[MelbourneVic3000], members[ArmadaleVic3143], members[NorlaneVic3214], members[Melbourne], members[Vic], members[SydneyNsw2000], members[Sydney], members[Nsw], members[Australia.Name]);

            // Search within New Zealand.

            criteria.Location = _locationQuery.ResolveLocation(_newZealand, "");
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[Auckland], members[NorthIsland], members[_newZealand.Name]);

            // No matter what you put in for New Zealand the same jobs will come up.

            criteria.Location = _locationQuery.ResolveLocation(_newZealand, Auckland);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[Auckland], members[NorthIsland], members[_newZealand.Name]);

            criteria.Location = _locationQuery.ResolveLocation(_newZealand, NorthIsland);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[Auckland], members[NorthIsland], members[_newZealand.Name]);

            criteria.Location = _locationQuery.ResolveLocation(_newZealand, "xyz");
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(members[Auckland], members[NorthIsland], members[_newZealand.Name]);
        }

        [TestMethod]
        public void TestAustraliaVertical()
        {
            Get(HomeUrl);
            Get(GetSearchUrl());
            TestCountryVertical(Australia);
        }

        [TestMethod]
        public void TestNewZealandVertical()
        {
            var vertical = TestVertical.NewZealand.CreateTestVertical(_verticalsCommand, _contentEngine);
            var verticalUrl = vertical.GetVerticalUrl("");
            Get(verticalUrl);

            var searchUrl = GetSearchUrl().AsNonReadOnly();
            searchUrl.Host = verticalUrl.Host;
            Get(searchUrl);

            TestCountryVertical(_newZealand);
        }

        private void TestCountryVertical(Country country)
        {
            // Do a search.

            _keywordsTextBox.Text = BusinessAnalyst;
            _searchButton.Click();

            // Check the default country.

            Assert.AreEqual(country.Id.ToString(), _countryIdDropDownList.SelectedItem.Value);
        }

        private IDictionary<string, Member> CreateMembers()
        {
            var members = new Dictionary<string, Member>();

            var index = 0;

            // Create members in the localities.

            members[MelbourneVic3000] = CreateMember(ref index, Australia, MelbourneVic3000);
            members[ArmadaleVic3143] = CreateMember(ref index, Australia, ArmadaleVic3143);
            members[NorlaneVic3214] = CreateMember(ref index, Australia, NorlaneVic3214);
            members[SydneyNsw2000] = CreateMember(ref index, Australia, SydneyNsw2000);

            // Create members in the regions.

            members[Melbourne] = CreateMember(ref index, Australia, Melbourne);
            members[Sydney] = CreateMember(ref index, Australia, Sydney);

            // Create members in the subdivisions.

            members[Vic] = CreateMember(ref index, Australia, Vic);
            members[Nsw] = CreateMember(ref index, Australia, Nsw);

            // Create members in the countries.

            members[Australia.Name] = CreateMember(ref index, Australia, "");
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