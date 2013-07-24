using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Integration.LinkedIn;
using LinkMe.Domain.Roles.Integration.LinkedIn.Commands;
using LinkMe.Domain.Roles.Integration.LinkedIn.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Integration.LinkedIn
{
    [TestClass]
    public class LinkedInTests
        : TestClass
    {
        private readonly ILinkedInCommand _linkedInCommand = Resolve<ILinkedInCommand>();
        private readonly ILinkedInQuery _linkedInQuery = Resolve<ILinkedInQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        private const string LinkedInId = "abc";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string OrganisationName = "Acme";
        private const string Industry = "Accounting";
        private const string Country = "Australia";
        private const string Location = "Norlane VIC 3214";

        private const string OtherFirstName = "Barney";
        private const string OtherLastName = "Gumble";
        private const string OtherOrganisationName = "Ajax";
        private const string OtherIndustry = "Administration";
        private const string OtherLocation = "Sydney NSW 2000";

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNoProfile()
        {
            Assert.IsNull(_linkedInQuery.GetProfile(LinkedInId));
            Assert.IsNull(_linkedInQuery.GetProfile(Guid.NewGuid()));
        }

        [TestMethod]
        public void TestCreateProfile()
        {
            var profile = new LinkedInProfile
            {
                Id = LinkedInId,
                UserId = Guid.NewGuid(),
            };

            _linkedInCommand.UpdateProfile(profile);
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            AssertProfile(profile, _linkedInQuery.GetProfile(profile.UserId));
        }

        [TestMethod]
        public void TestCreateFullProfile()
        {
            var userId = Guid.NewGuid();

            var profile = new LinkedInProfile
            {
                Id = LinkedInId,
                UserId = userId,
                FirstName = FirstName,
                LastName = LastName,
                OrganisationName = OrganisationName,
                Industries = new[] { _industriesQuery.GetIndustry(Industry) },
                Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location),
            };

            _linkedInCommand.UpdateProfile(profile);
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            AssertProfile(profile, _linkedInQuery.GetProfile(profile.UserId));
        }

        [TestMethod]
        public void TestUpdateProfile()
        {
            var profile = new LinkedInProfile
            {
                Id = LinkedInId,
                UserId = Guid.NewGuid(),
            };

            _linkedInCommand.UpdateProfile(profile);
            _linkedInCommand.UpdateProfile(profile);

            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            AssertProfile(profile, _linkedInQuery.GetProfile(profile.UserId));
        }

        [TestMethod]
        public void TestUpdateFullProfile()
        {
            var profile = new LinkedInProfile
            {
                Id = LinkedInId,
                UserId = Guid.NewGuid(),
                FirstName = FirstName,
                LastName = LastName,
                OrganisationName = OrganisationName,
                Industries = new[] { _industriesQuery.GetIndustry(Industry) },
                Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location),
            };

            _linkedInCommand.UpdateProfile(profile);
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            AssertProfile(profile, _linkedInQuery.GetProfile(profile.UserId));

            profile.UserId = Guid.NewGuid();
            profile.FirstName = OtherFirstName;
            profile.LastName = OtherLastName;
            profile.OrganisationName = OtherOrganisationName;
            profile.Industries = new[] { _industriesQuery.GetIndustry(Industry), _industriesQuery.GetIndustry(OtherIndustry) };
            profile.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), OtherLocation);

            _linkedInCommand.UpdateProfile(profile);
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            AssertProfile(profile, _linkedInQuery.GetProfile(profile.UserId));

            profile.FirstName = null;
            profile.LastName = null;
            profile.OrganisationName = null;
            profile.Industries = null;
            profile.Location = null;

            _linkedInCommand.UpdateProfile(profile);
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            AssertProfile(profile, _linkedInQuery.GetProfile(profile.UserId));
        }

        [TestMethod]
        public void TestIndustries()
        {
            AssertIndustries(_linkedInQuery.GetIndustries("abc"));
            AssertIndustries(_linkedInQuery.GetIndustries("Dairy"), "Primary Industry & Agriculture");
            AssertIndustries(_linkedInQuery.GetIndustries("Alternative Dispute Resolution"), "Legal", "Consulting & Corporate Strategy");
            AssertIndustries(_linkedInQuery.GetIndustries("Program Development"), "Consulting & Corporate Strategy");
            AssertIndustries(_linkedInQuery.GetIndustries("Security and Investigations"), "Trades & Services", "Consulting & Corporate Strategy");
        }

        [TestMethod]
        public void TestLocations()
        {
            Assert.IsNull(_linkedInQuery.GetLocation("ab", "abc"));
            Assert.AreEqual(_locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Norlane VIC 3214"), _linkedInQuery.GetLocation("au", "Norlane"));
        }

        private static void AssertIndustries(IEnumerable<Industry> industries, params string[] expectedIndustries)
        {
            Assert.IsTrue((from i in industries select i.Name).CollectionEqual(expectedIndustries));
        }

        private static void AssertProfile(LinkedInProfile expectedProfile, LinkedInProfile profile)
        {
            Assert.AreEqual(expectedProfile.Id, profile.Id);
            Assert.AreEqual(expectedProfile.UserId, profile.UserId);
            Assert.AreEqual(expectedProfile.FirstName, profile.FirstName);
            Assert.AreEqual(expectedProfile.LastName, profile.LastName);
            Assert.AreEqual(expectedProfile.OrganisationName, profile.OrganisationName);
            Assert.IsTrue((expectedProfile.Industries ?? new Industry[0]).Select(i => i.Id).CollectionEqual((profile.Industries ?? new Industry[0]).Select(i => i.Id)));
            Assert.AreEqual(expectedProfile.Location, profile.Location);
        }
    }
}
