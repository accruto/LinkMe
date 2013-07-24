using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;
using LinkMe.Analyse.Engine.Unity;
using LinkMe.Analyse.Unity;
using LinkMe.Apps.Agents.Unity;
using LinkMe.Apps.Asp.Unity;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Apps.Presentation.Unity;
using LinkMe.Apps.Services.Unity;
using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Unity;
using LinkMe.Framework.Reports;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Reports.Unity;
using LinkMe.Query.Search.Engine.Unity;
using LinkMe.Query.Search.Unity;
using LinkMe.Query.Unity;
using LinkMe.Utility.Test;
using NUnit.Framework;

namespace LinkMe.Apps.Reports.Test
{
    public abstract class TestFixture
    {
        private const string ReportUrl = "http://localhost/reportserver/ReportExecution2005.asmx";
        private const string FailedFileName = "LinkMeLastFailedReport.html";
        private static bool _initialised;

        protected static T Resolve<T>()
        {
            if (!_initialised)
            {
                InitialiseContainer();
                _initialised = true;
            }
            return Container.Current.Resolve<T>();
        }

        private static void InitialiseContainer()
        {
            new ContainerConfigurer()
                .Add(new DomainConfigurator())
                .Add(new RolesConfigurator())
                .Add(new UsersConfigurator())
                .Add(new QueryConfigurator())
                .Add(new SearchConfigurator())
                .Add(new QueryEngineConfigurator())
                .Add(new AnalyseConfigurator())
                .Add(new AnalysisEngineConfigurator())
                .Add(new ReportsConfigurator())
                .Add(new PresentationConfigurator())
                .Add(new AgentsConfigurator())
                .Add(new ServicesConfigurator())
                .Add(new AspConfigurator())
                .Add("linkme.resources.container")
                .Configure(Container.Current);
        }

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            MemberSearchHost.Start();
        }

        [SetUp]
        public virtual void SetUp()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        /// <summary>
        /// A number of the assert methods are simple attempts to reverse engineer the HTML
        /// coming from the report engine and are best guesses.  It is expected that they will
        /// need further refinement.
        /// </summary>
        private string _currentReportText;
        private HtmlDocument _currentHtmlDocument;

        protected abstract string ItemPath { get; }

        private readonly IReportsEngine _reportsEngine = new ReportsEngine(ReportUrl);

        protected void GetReport()
        {
            GetReport(null);
        }

        protected void GetReport(IDictionary<string, object> parameters)
        {
            _currentReportText = _reportsEngine.RunHtmlReport(ItemPath, parameters);
            _currentHtmlDocument = new HtmlDocument();
            _currentHtmlDocument.LoadHtml(_currentReportText);
        }

        protected void AssertColumn(string name)
        {
            var node = _currentHtmlDocument.DocumentNode.SelectSingleNode("//div[text()='" + name + "']");
            Assert.IsNotNull(node, "Cannot find column with name '" + name + "'.");
        }

        protected void AssertReportContains(string text)
        {
            AssertReportContainment(text, true);
        }

        protected void AssertReportContains(string text, bool ignoreCase)
        {
            AssertReportContainment(text, true, ignoreCase);
        }

        protected void AssertReportDoesNotContain(string text)
        {
            AssertReportContainment(text, false);
        }

        protected void AssertReportContainment(string text, bool expected)
        {
            AssertReportContainment(text, expected, false);
        }

        private void AssertReportContainment(string text, bool expected, bool ignoreCase)
        {
            var reportText = _currentReportText;
            var contained = reportText.IndexOf(text, ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture) > -1;
            if (contained != expected)
                Assert.Fail(string.Format("'{0}' was {1} in the page. {2}", text, expected ? "expected" : "not expected", SaveReportToFile(reportText)));
        }

        private static string SaveReportToFile(string text)
        {
            var file = Path.GetTempFileName() + FailedFileName;
            TestUtils.SaveToFile(file, text);
            return "The report has been saved to a file: '" + file + "'.";
        }
   }
}
