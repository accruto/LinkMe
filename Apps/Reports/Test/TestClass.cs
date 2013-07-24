using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;
using LinkMe.Apps.Utility.Test;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Reports;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Reports.Test
{
    [TestClass]
    public abstract class TestClass
    {
        private const string ReportUrl = "http://localhost/reportserver/ReportExecution2005.asmx";
        private const string FailedFileName = "LinkMeLastFailedReport.html";

        protected static T Resolve<T>()
        {
            TestAssembly.InitialiseContainer();
            return Container.Current.Resolve<T>();
        }

        [TestInitialize]
        public void TestClassInitialize()
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
