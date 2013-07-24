using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Anonymous;
using LinkMe.Domain.Users.Anonymous.Commands;
using LinkMe.Domain.Users.Anonymous.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.EmailJobSearchAlerts
{
    [TestClass]
    public abstract class EmailJobSearchAlertsTaskTests
        : TaskTests
    {
        protected readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        private readonly IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();
        protected readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        protected readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand = Resolve<IJobAdSearchAlertsCommand>();
        private readonly IAnonymousUsersCommand _anonymousUsersCommand = Resolve<IAnonymousUsersCommand>();
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand = Resolve<IExecuteJobAdSearchCommand>();
        private readonly IJobAdSearchAlertsQuery _jobAdSearchAlertsQuery = Resolve<IJobAdSearchAlertsQuery>();
        private readonly IJobAdSearchesQuery _jobAdSearchesQuery = Resolve<IJobAdSearchesQuery>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IAnonymousUsersQuery _anonymousUsersQuery = Resolve<IAnonymousUsersQuery>();

        protected const string BusinessAnalyst = "business analyst";
        private const string EmailAddressFormat = "homer{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Homer{0}";
        private const string LastNameFormat = "Simpson{0}";

        [TestInitialize]
        public void EmailJobSearchAlertsTaskTestsInitialize()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();
            JobAdSearchHost.ClearIndex();
            JobAdSortHost.ClearIndex();
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        protected AnonymousContact CreateAnonymousContact(int index)
        {
            var contactDetails = new ContactDetails
            {
                EmailAddress = string.Format(EmailAddressFormat, index),
                FirstName = string.Format(FirstNameFormat, index),
                LastName = string.Format(LastNameFormat, index),
            };
            return _anonymousUsersCommand.CreateContact(new AnonymousUser(), contactDetails);
        }

        protected MockEmail ExecuteTask()
        {
            _emailServer.ClearEmails();
            new EmailJobSearchAlertsTask(_emailsCommand, _jobAdsQuery, _executeJobAdSearchCommand, _jobAdSearchAlertsCommand, _jobAdSearchAlertsQuery, _jobAdSearchesQuery, _membersQuery, _anonymousUsersQuery).ExecuteTask();
            var emails = _emailServer.GetEmails().ToList();
            _emailServer.ClearEmails();
            if (emails.Count == 0)
                return null;
            Assert.AreEqual(1, emails.Count);
            return emails[0];
        }

        protected void AssertJobAds(MockEmail email, params JobAdEntry[] jobAds)
        {
            var urls = GetJobAdUrls(email.GetHtmlView().Body);
            Assert.AreEqual(urls.Count, jobAds.Length);

            var browser = new HttpClient();

            foreach (var jobAd in jobAds)
            {
                var found = false;
                foreach (var url in urls)
                {
                    browser.Get(HttpStatusCode.OK, url.ToString());
                    if (browser.CurrentPageText.IndexOf(jobAd.Id.ToString().ToLower(), StringComparison.Ordinal) > -1)
                    {
                        found = true;
                        break;
                    }
                }

                Assert.IsTrue(found);
            }
        }

        private static IList<ReadOnlyUrl> GetJobAdUrls(string body)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(body);

            // Job ads are 3rd links in, but not the last 3.

            var nodes = xmlDoc.SelectNodes("//a/@href");
            Assert.IsNotNull(nodes);
            var urls = new List<ReadOnlyUrl>();
            for (var index = 2; index < nodes.Count - 3; ++index)
                urls.Add(new ReadOnlyUrl(nodes[index].InnerText));
            return urls;
        }
    }
}