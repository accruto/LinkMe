using System;
using System.Collections.Generic;
using System.Net;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.External
{
    public abstract class ExternalCredentialsTests
        : CommunityTests
    {
        private const string ExternalCookieName = "LinkMeAuthExt";

        protected readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        protected readonly IExternalCredentialsQuery _externalCredentialsQuery = Resolve<IExternalCredentialsQuery>();

        protected Vertical CreateVertical()
        {
            var community = TestCommunity.LiveInAustralia.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            return _verticalsCommand.GetVertical(community);
        }

        protected Member CreateMember(string emailAddress, string firstName, string lastName, Guid verticalId, bool isAffiliated, string externalId)
        {
            var member = new Member
            {
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress } },
                FirstName = firstName,
                LastName = lastName,
                Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Sydney 2000 NSW") },
                IsActivated = true,
            };

            var externalCredentals = new ExternalCredentials { ProviderId = verticalId, ExternalId = externalId };
            _memberAccountsCommand.CreateMember(member, externalCredentals, isAffiliated ? verticalId : (Guid?) null);

            return member;
        }

        protected void CreateExternalCookies(ReadOnlyUrl url, string externalId, string emailAddress, string name, string cookieDomain)
        {
            var cookieValue = new ExternalCookieData(externalId, emailAddress, name).CookieValue;
            var cookie = new Cookie(ExternalCookieName, cookieValue) { Domain = cookieDomain };

            // Cookie Uri should be with a path "/".

            var cookieUrl = url.AsNonReadOnly();
            cookieUrl.Path = "/";
            var uri = new Uri(cookieUrl.AbsoluteUri);

            Browser.Cookies.Add(uri, cookie);
        }

        protected static void AssertMember(string emailAddress, string firstName, string lastName, Guid? affiliateId, IMember member)
        {
            Assert.IsNotNull(member);
            Assert.AreEqual(emailAddress, member.EmailAddresses[0].Address);
            Assert.AreEqual(firstName, member.FirstName);
            Assert.AreEqual(lastName, member.LastName);
            Assert.AreEqual(true, member.IsEnabled);
            Assert.AreEqual(true, member.IsActivated);
            Assert.AreEqual(affiliateId, member.AffiliateId);
        }

        protected void AssertCredentials(Guid providerId, string externalId, ExternalCredentials credentials)
        {
            Assert.AreEqual(providerId, credentials.ProviderId);
            Assert.AreEqual(externalId, credentials.ExternalId);
        }
    }
}
