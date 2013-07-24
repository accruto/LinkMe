using System;
using System.Linq;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Partials
{
    [TestClass]
    public class EmploymentHistoryTests
        : PartialProfileTests
    {
        private ReadOnlyUrl _employmentHistoryUrl;

        private HtmlDropDownListTester _recentProfessionDropDownList;
        private HtmlDropDownListTester _recentSeniorityDropDownList;
        private HtmlListBoxTester _industryIdsListBox;
        private HtmlDropDownListTester _startDateMonthDropDownList;
        private HtmlDropDownListTester _startDateYearDropDownList;
        private HtmlDropDownListTester _endDateMonthDropDownList;
        private HtmlDropDownListTester _endDateYearDropDownList;
        private HtmlTextBoxTester _titleTextBox;
        private HtmlTextBoxTester _companyTextBox;
        private HtmlTextAreaTester _descriptionTextBox;

        [TestInitialize]
        public void TestInitialize()
        {
            _employmentHistoryUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/employmenthistory");

            _recentProfessionDropDownList = new HtmlDropDownListTester(Browser, "RecentProfession");
            _recentSeniorityDropDownList = new HtmlDropDownListTester(Browser, "RecentSeniority");
            _industryIdsListBox = new HtmlListBoxTester(Browser, "IndustryIds");
            _startDateMonthDropDownList = new HtmlDropDownListTester(Browser, "StartDateMonth");
            _startDateYearDropDownList = new HtmlDropDownListTester(Browser, "StartDateYear");
            _endDateMonthDropDownList = new HtmlDropDownListTester(Browser, "EndDateMonth");
            _endDateYearDropDownList = new HtmlDropDownListTester(Browser, "EndDateYear");
            _titleTextBox = new HtmlTextBoxTester(Browser, "Title");
            _companyTextBox = new HtmlTextBoxTester(Browser, "Company");
            _descriptionTextBox = new HtmlTextAreaTester(Browser, "Description");
        }

        protected override ReadOnlyUrl GetPartialUrl()
        {
            return _employmentHistoryUrl;
        }

        protected override void AssertMember(IMember member, ICandidate candidate, IResume resume)
        {
            Assert.AreEqual(candidate.RecentProfession == null ? "" : candidate.RecentProfession.Value.ToString(), _recentProfessionDropDownList.SelectedItem.Value);
            Assert.AreEqual(candidate.RecentSeniority == null ? "" : candidate.RecentSeniority.Value.ToString(), _recentSeniorityDropDownList.SelectedItem.Value);

            var industryIds = candidate.Industries == null
                ? null
                : from i in candidate.Industries select i.Id;
            Assert.IsTrue(industryIds.NullableCollectionEqual(from i in _industryIdsListBox.SelectedItems select new Guid(i.Value)));

            // The first job is displayed.

            var job = resume == null || resume.Jobs == null || resume.Jobs.Count == 0
                ? null
                : resume.Jobs[0];

            if (job == null)
            {
                Assert.AreEqual("", _startDateMonthDropDownList.SelectedItem.Value);
                Assert.AreEqual("", _startDateYearDropDownList.SelectedItem.Value);
                Assert.AreEqual("", _endDateMonthDropDownList.SelectedItem.Value);
                Assert.AreEqual("", _endDateYearDropDownList.SelectedItem.Value);
                Assert.AreEqual("", _titleTextBox.Text);
                Assert.AreEqual("", _companyTextBox.Text);
                Assert.AreEqual("", _descriptionTextBox.Text.Trim());
            }
            else
            {
                Assert.AreEqual(job.Dates == null || job.Dates.Start == null || job.Dates.Start.Value.Month == null ? "" : job.Dates.Start.Value.Month.Value.ToString(), _startDateMonthDropDownList.SelectedItem.Value);
                Assert.AreEqual(job.Dates == null || job.Dates.Start == null ? "" : job.Dates.Start.Value.Year.ToString(), _startDateYearDropDownList.SelectedItem.Value);
                Assert.AreEqual(job.Dates == null || job.Dates.End == null || job.Dates.End.Value.Month == null ? "" : job.Dates.End.Value.Month.Value.ToString(), _endDateMonthDropDownList.SelectedItem.Value);
                Assert.AreEqual(job.Dates == null || job.Dates.End == null ? "" : job.Dates.End.Value.Year.ToString(), _endDateYearDropDownList.SelectedItem.Value);
                Assert.AreEqual(job.Title, _titleTextBox.Text);
                Assert.AreEqual(job.Company, _companyTextBox.Text);
                Assert.AreEqual(job.Description, _descriptionTextBox.Text.Trim());
            }
        }
    }
}
