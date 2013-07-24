using System.Globalization;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Partials
{
    [TestClass]
    public class EducationTests
        : PartialProfileTests
    {
        private ReadOnlyUrl _educationUrl;

        private HtmlDropDownListTester _highestEducationLevelDropDownList;
        private HtmlDropDownListTester _endDateMonthDropDownList;
        private HtmlDropDownListTester _endDateYearDropDownList;
        private HtmlTextBoxTester _degreeTextBox;
        private HtmlTextBoxTester _majorTextBox;
        private HtmlTextBoxTester _institutionTextBox;
        private HtmlTextBoxTester _cityTextBox;
        private HtmlTextAreaTester _descriptionTextBox;

        [TestInitialize]
        public void TestInitialize()
        {
            _educationUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/education");

            _highestEducationLevelDropDownList = new HtmlDropDownListTester(Browser, "HighestEducationLevel");
            _endDateMonthDropDownList = new HtmlDropDownListTester(Browser, "EndDateMonth");
            _endDateYearDropDownList = new HtmlDropDownListTester(Browser, "EndDateYear");
            _degreeTextBox = new HtmlTextBoxTester(Browser, "Degree");
            _majorTextBox = new HtmlTextBoxTester(Browser, "Major");
            _institutionTextBox = new HtmlTextBoxTester(Browser, "Institution");
            _cityTextBox = new HtmlTextBoxTester(Browser, "City");
            _descriptionTextBox = new HtmlTextAreaTester(Browser, "Description");
        }

        [TestMethod]
        public void TestNoSchoolDateMember()
        {
            Member member;
            Candidate candidate;
            Resume resume;
            CreateMember(TestResume.NoSchoolDate, out member, out candidate, out resume);

            LogIn(member);
            Get(_profileUrl);
            Get(GetPartialUrl());
            AssertMember(member, candidate, resume);
        }

        [TestMethod, Ignore]
        public void TestFutureSchoolDateMember()
        {
            Member member;
            Candidate candidate;
            Resume resume;
            CreateMember(TestResume.FutureSchoolDate, out member, out candidate, out resume);

            LogIn(member);
            Get(_profileUrl);
            Get(GetPartialUrl());
            AssertMember(member, candidate, resume);
        }

        protected override ReadOnlyUrl GetPartialUrl()
        {
            return _educationUrl;
        }

        protected override void AssertMember(IMember member, ICandidate candidate, IResume resume)
        {
            Assert.AreEqual(candidate.HighestEducationLevel == null ? "" : candidate.HighestEducationLevel.Value.ToString(), _highestEducationLevelDropDownList.SelectedItem.Value);

            // The first school is displayed.

            var school = resume == null || resume.Schools == null || resume.Schools.Count == 0
                ? null
                : resume.Schools[0];

            if (school == null)
            {
                Assert.AreEqual("", _endDateMonthDropDownList.SelectedItem.Value);
                Assert.AreEqual("", _endDateYearDropDownList.SelectedItem.Value);
                Assert.AreEqual("", _degreeTextBox.Text);
                Assert.AreEqual("", _majorTextBox.Text);
                Assert.AreEqual("", _institutionTextBox.Text);
                Assert.AreEqual("", _cityTextBox.Text);
                Assert.AreEqual("", _descriptionTextBox.Text.Trim());
            }
            else
            {
                Assert.AreEqual(school.CompletionDate == null || school.CompletionDate.End == null || school.CompletionDate.End.Value.Month == null ? "" : school.CompletionDate.End.Value.Month.Value.ToString(CultureInfo.InvariantCulture), _endDateMonthDropDownList.SelectedItem.Value);
                Assert.AreEqual(school.CompletionDate == null || school.CompletionDate.End == null ? "" : school.CompletionDate.End.Value.Year.ToString(CultureInfo.InvariantCulture), _endDateYearDropDownList.SelectedItem.Value);
                Assert.AreEqual(school.Degree, _degreeTextBox.Text);
                Assert.AreEqual(school.Major, _majorTextBox.Text);
                Assert.AreEqual(school.Institution, _institutionTextBox.Text);
                Assert.AreEqual(school.City, _cityTextBox.Text);
                Assert.AreEqual(school.Description, _descriptionTextBox.Text.Trim());
            }
        }
    }
}
