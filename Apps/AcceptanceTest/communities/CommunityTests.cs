using System.Linq;
using System.Web;
using LinkMe.AcceptanceTest.ui;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Affiliations.Commands;
using LinkMe.Domain.Users.Members.Affiliations.Queries;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities
{
    [TestClass]
    public abstract class CommunityTests
        : WebFormDataTestCase
    {
        protected readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        protected readonly IMemberAffiliationsCommand _memberAffiliationsCommand = Resolve<IMemberAffiliationsCommand>();
        protected readonly IMemberAffiliationsQuery _memberAffiliationsQuery = Resolve<IMemberAffiliationsQuery>();
        protected readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();
        protected readonly IVerticalsQuery _verticalsQuery = Resolve<IVerticalsQuery>();
        protected readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        protected readonly IContentEngine _contentEngine = Resolve<IContentEngine>();
        
        [TestInitialize]
        public void CommunityTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected ReadOnlyUrl GetCommunityUrl(Community community, string path)
        {
            return _verticalsCommand.GetCommunityUrl(community, path);
        }

        protected ReadOnlyUrl GetCommunityUrl(Community community, bool secure, string path)
        {
            return _verticalsCommand.GetCommunityUrl(community, secure, path);
        }

        protected ReadOnlyUrl GetCommunityHostUrl(Community community, bool secure, string path)
        {
            return _verticalsCommand.GetCommunityHostUrl(community, secure, path);
        }

        protected ReadOnlyUrl GetCommunityHostUrl(Community community, string path)
        {
            return _verticalsCommand.GetCommunityHostUrl(community, path);
        }

        protected ReadOnlyUrl GetCommunitySecondaryHostUrl(Community community, string path)
        {
            return _verticalsCommand.GetCommunitySecondaryHostUrl(community, path);
        }

        protected ReadOnlyUrl GetCommunityTertiaryHostUrl(Community community, string path)
        {
            return _verticalsCommand.GetCommunityTertiaryHostUrl(community, path);
        }

        protected ReadOnlyUrl GetCommunityPathUrl(Community community, bool secure, string path)
        {
            return _verticalsCommand.GetCommunityPathUrl(community, secure, path);
        }

        protected ReadOnlyUrl GetCommunityPathUrl(Community community, string path)
        {
            return _verticalsCommand.GetCommunityPathUrl(community, path);
        }

        protected void AssertHeader(string headerSnippet)
        {
            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='community-header']");

            if ((xmlNode != null) != (headerSnippet != null))
            {
                if (headerSnippet != null)
                {
                    Assert.Fail("Header not found when one was expected." + SaveCurrentPageToFile());
                }
                else
                {
                    // Should headerSnippet == null here? Does that indicate something wrong higher up?
                    Assert.Fail("Header found when one was not expected in AssertHeader()" + SaveCurrentPageToFile());
                }
            }

            if (headerSnippet != null)
                AssertPageContains(headerSnippet);
        }

        protected void AssertNoHeader(string headerSnippet)
        {
            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='community-header']");
            Assert.IsTrue(xmlNode == null, "Header found when none was expected.");
            if (headerSnippet != null)
                AssertPageDoesNotContain(headerSnippet);
        }

        protected void AssertFavicon(string rootFolder, string favicon)
        {
            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode("/html/head/link[@type='image/x-icon']");
            if (rootFolder == null || favicon == null)
            {
                Assert.IsNull(xmlNode);
            }
            else
            {
                Assert.IsNotNull(xmlNode);
                var url = new ReadOnlyApplicationUrl(new ReadOnlyApplicationUrl(rootFolder), favicon);
                Assert.AreEqual(url.PathAndQuery, xmlNode.Attributes["href"].Value);
                Assert.AreEqual("shortcut icon", xmlNode.Attributes["rel"].Value);
            }
        }

        protected void AssertHomePageTitle(string homePageTitle)
        {
            var pageTitle = HttpUtility.HtmlDecode(Browser.CurrentHtml.DocumentNode.SelectSingleNode("/html/head/title").InnerText.Trim());
            Assert.AreEqual(homePageTitle ?? "Jobs - Online Job Search for Jobs, Employment & Careers in Australia", pageTitle);
        }

        protected void AssertMember(string emailAddress, Community community, bool isDeleted)
        {
            var member = _membersQuery.GetMember(emailAddress);
            Assert.IsNotNull(member);

            var primaryCommunityId = member.AffiliateId;
            if (community.HasMembers && !isDeleted)
            {
                Assert.IsNotNull(primaryCommunityId);
                Assert.AreEqual(community.Id, primaryCommunityId);
            }
            else
            {
                Assert.IsNull(primaryCommunityId);
            }
        }

        protected Employer AssertEmployer(string loginId, Community community)
        {
            var employer = _employerAccountsQuery.GetEmployer(loginId);

            var orgUnit = employer.Organisation;

            if (community == null)
            {
                if (orgUnit != null)
                    Assert.IsNull(orgUnit.AffiliateId);
            }
            else
            {
                Assert.IsNotNull(orgUnit);
                Assert.AreEqual(community.Id, orgUnit.AffiliateId);
            }

            return employer;
        }

        protected static void SetCommunity(HtmlDropDownListTester ddlCommunity, Community community)
        {
            Assert.IsTrue(ddlCommunity.IsVisible);
            Assert.AreEqual(true, ddlCommunity.IsEnabled);
            if (community != null)
            {
                // It should only be selected if it is there.

                var item = ddlCommunity.Items.SingleOrDefault(i => i.Value == community.Id.ToString());
                ddlCommunity.SelectedValue = item != null ? item.Value : string.Empty;
            }
            else
            {
                ddlCommunity.SelectedValue = string.Empty;
            }
        }
    }
}
