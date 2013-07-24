using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.Members.Filters
{
    [TestClass]
    public class DesiredSalaryTests
        : FilterTests
    {
        [TestMethod]
        public void TestSalary()
        {
            // Create content.

            var includedRange = new Guid("{83B1EA37-A38D-463f-BD8F-06BA1D7D9F87}");
            IndexContent(new MemberContent { Member = new Member { Id = includedRange }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 85000, UpperBound = 87000, Rate = SalaryRate.Year } } });

            var equalRange = new Guid("{7A800650-1D51-425e-9903-5747A86D4C3F}");
            IndexContent(new MemberContent { Member = new Member { Id = equalRange }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 80000, UpperBound = 80000, Rate = SalaryRate.Year } } });

            var inclusiveRange = new Guid("{8D4CE5DF-D7AC-4409-953E-97909B8D230E}");
            IndexContent(new MemberContent { Member = new Member { Id = inclusiveRange }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 70000, UpperBound = 100000, Rate = SalaryRate.Year } } });

            var overlappingRange1 = new Guid("{ED3A097F-53A0-4b27-88AD-085EB1E2C143}");
            IndexContent(new MemberContent { Member = new Member { Id = overlappingRange1 }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 85000, UpperBound = 100000, Rate = SalaryRate.Year } } });

            var overlappingRange2 = new Guid("{957E6602-90CA-4bdb-840C-8FE3FFAF960C}");
            IndexContent(new MemberContent { Member = new Member { Id = overlappingRange2 }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 70000, UpperBound = 85000, Rate = SalaryRate.Year } } });

            var overlappingBoundaryRange = new Guid("{6A5D57C0-38EF-4ee6-9F1D-7F13D5B3730A}");
            IndexContent(new MemberContent { Member = new Member { Id = overlappingBoundaryRange }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 70000, UpperBound = 80000, Rate = SalaryRate.Year } } });

            var rangeBelow = new Guid("{89E23445-842C-4bf6-B13C-32B1CCB318DB}");
            IndexContent(new MemberContent { Member = new Member { Id = rangeBelow }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 60000, UpperBound = 70000, Rate = SalaryRate.Year } } });

            var rangeAbove = new Guid("{0B50E3B5-323A-41c6-82AB-6345BA71504C}");
            IndexContent(new MemberContent { Member = new Member { Id = rangeAbove }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 95000, UpperBound = 100000, Rate = SalaryRate.Year } } });

            var lowerWithinRange = new Guid("{7C0E3455-9F7D-4232-B3D3-DD2B5C6F74AA}");
            IndexContent(new MemberContent { Member = new Member { Id = lowerWithinRange }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 85000, UpperBound = null, Rate = SalaryRate.Year } } });

            var upperWithinRange = new Guid("{794D1116-649C-4d70-B8E2-8FC4C790649D}");
            IndexContent(new MemberContent { Member = new Member { Id = upperWithinRange }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = null, UpperBound = 85000, Rate = SalaryRate.Year } } });

            var lowerBelow = new Guid("{A54E67E7-30D3-4652-BDE3-9806D5E236F7}");
            IndexContent(new MemberContent { Member = new Member { Id = lowerBelow }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 60000, UpperBound = null, Rate = SalaryRate.Year } } });

            var lowerEqual = new Guid("{FC9A0731-5AD5-4e32-B9E4-BA6022F15A8A}");
            IndexContent(new MemberContent { Member = new Member { Id = lowerEqual }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 80000, UpperBound = null, Rate = SalaryRate.Year } } });

            var lowerAbove = new Guid("{CB18AB2C-3156-4f22-93F8-45218214098E}");
            IndexContent(new MemberContent { Member = new Member { Id = lowerAbove }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 95000, UpperBound = null, Rate = SalaryRate.Year } } });

            var upperBelow = new Guid("{E2A3D525-91FE-4034-BFAC-BFC7D9A792DB}");
            IndexContent(new MemberContent { Member = new Member { Id = upperBelow }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = null, UpperBound = 70000, Rate = SalaryRate.Year } } });

            var upperEqual = new Guid("{EAFEE8DF-D57F-4b65-A0EB-B9DC12791144}");
            IndexContent(new MemberContent { Member = new Member { Id = upperEqual }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = null, UpperBound = 80000, Rate = SalaryRate.Year } } });

            var upperAbove = new Guid("{203E37E4-053F-4e56-882A-3C5CEC57BD2D}");
            IndexContent(new MemberContent { Member = new Member { Id = upperAbove }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = null, UpperBound = 100000, Rate = SalaryRate.Year } } });

            var noInfo = new Guid("{85DDA8BE-53C8-4798-BC8D-C8E24FCAF957}");
            IndexContent(new MemberContent { Member = new Member { Id = noInfo }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = null, UpperBound = null, Rate = SalaryRate.Year } } });

            // Search without filter.

            var memberQuery = new MemberSearchQuery();
            var results = Search(memberQuery, 0, 20);
            Assert.AreEqual(17, results.MemberIds.Count);

            // Range search.

            var salary = new Salary {LowerBound = 80000, UpperBound = 90000};
            memberQuery = new MemberSearchQuery { Salary = salary, ExcludeNoSalary = false };
            results = Search(memberQuery, 0, 20);
            var expected = new[] { includedRange, equalRange, inclusiveRange, overlappingRange1,
                                   overlappingRange2, overlappingBoundaryRange, lowerWithinRange, upperWithinRange,
                                   lowerEqual, upperEqual, upperAbove, noInfo };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));

            memberQuery = new MemberSearchQuery { Salary = salary, ExcludeNoSalary = true };
            results = Search(memberQuery, 0, 20);
            expected = new[] { includedRange, equalRange, inclusiveRange, overlappingRange1,
                               overlappingRange2, overlappingBoundaryRange, lowerWithinRange, upperWithinRange,
                               lowerEqual, upperEqual, upperAbove };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));

            // Search with lower salary only.

            salary = new Salary { LowerBound = 80000, UpperBound = null };
            memberQuery = new MemberSearchQuery { Salary = salary, ExcludeNoSalary = false };
            results = Search(memberQuery, 0, 20);
            expected = new[] { includedRange, equalRange, inclusiveRange, overlappingRange1,
                               overlappingRange2, overlappingBoundaryRange, rangeAbove, lowerWithinRange, upperWithinRange,
                               lowerEqual, lowerAbove, upperEqual, upperAbove, noInfo };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));

            memberQuery = new MemberSearchQuery { Salary = salary, ExcludeNoSalary = true };
            results = Search(memberQuery, 0, 20);
            expected = new[] { includedRange, equalRange, inclusiveRange, overlappingRange1,
                               overlappingRange2, overlappingBoundaryRange, rangeAbove, lowerWithinRange, upperWithinRange,
                               lowerEqual, lowerAbove, upperEqual, upperAbove };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));

            // Search with upper salary only.

            salary = new Salary { LowerBound = null, UpperBound = 80000 };
            memberQuery = new MemberSearchQuery { Salary = salary, ExcludeNoSalary = false };
            results = Search(memberQuery, 0, 20);
            expected = new[] { equalRange, inclusiveRange, overlappingRange2,
                               overlappingBoundaryRange, rangeBelow, upperWithinRange, lowerBelow, lowerEqual, upperBelow,
                               upperEqual, upperAbove, noInfo };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));

            memberQuery = new MemberSearchQuery { Salary = salary, ExcludeNoSalary = true };
            results = Search(memberQuery, 0, 20);
            expected = new[] { equalRange, inclusiveRange, overlappingRange2,
                               overlappingBoundaryRange, rangeBelow, upperWithinRange, lowerBelow, lowerEqual, upperBelow,
                               upperEqual, upperAbove };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));

            // No salary

            memberQuery = new MemberSearchQuery { Salary = null, ExcludeNoSalary = false };
            results = Search(memberQuery, 0, 20);
            expected = new[] { includedRange, equalRange, inclusiveRange,
                               overlappingRange1, overlappingRange2, overlappingBoundaryRange,
                               rangeBelow, rangeAbove, lowerWithinRange, upperWithinRange,
                               lowerBelow, lowerEqual, lowerAbove, upperBelow, upperEqual, upperAbove,
                               noInfo };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));

            memberQuery = new MemberSearchQuery { Salary = null, ExcludeNoSalary = true };
            results = Search(memberQuery, 0, 20);
            expected = new[] { includedRange, equalRange, inclusiveRange,
                               overlappingRange1, overlappingRange2, overlappingBoundaryRange,
                               rangeBelow, rangeAbove, lowerWithinRange, upperWithinRange,
                               lowerBelow, lowerEqual, lowerAbove, upperBelow, upperEqual, upperAbove };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));
        }

        [TestMethod]
        public void TestHourlySalary()
        {
            // Create content.

            var yearly = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = yearly }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 80000, UpperBound = 100000, Rate = SalaryRate.Year } } });

            var hourly = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = hourly }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 40, UpperBound = 50, Rate = SalaryRate.Hour } } });

            var yearlyBelow = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = yearlyBelow }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 60000, UpperBound = 70000, Rate = SalaryRate.Year } } });

            var hourlyBelow = Guid.NewGuid();
            IndexContent(new MemberContent { Member = new Member { Id = hourlyBelow }, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 30, UpperBound = 35, Rate = SalaryRate.Hour } } });

            // Search without filter.

            var memberQuery = new MemberSearchQuery();
            var results = Search(memberQuery, 0, 20);
            var expected = new[] { yearly, hourly, yearlyBelow, hourlyBelow };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));

            // Search above.

            memberQuery = new MemberSearchQuery { Salary = new Salary { LowerBound = 80000, UpperBound = 120000, Rate = SalaryRate.Year }, ExcludeNoSalary = true };
            results = Search(memberQuery, 0, 20);
            expected = new[] { yearly, hourly };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));

            // Search below.

            memberQuery = new MemberSearchQuery { Salary = new Salary { LowerBound = 50000, UpperBound = 70000, Rate = SalaryRate.Year }, ExcludeNoSalary = true };
            results = Search(memberQuery, 0, 20);
            expected = new[] { yearlyBelow, hourlyBelow };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));
        }

        [TestMethod]
        public void TestHiddenSalary()
        {
            // Create content.

            var notHidden = Guid.NewGuid();
            var member = new Member {Id = notHidden};
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Salary);
            IndexContent(new MemberContent { Member = member, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 80000, UpperBound = 100000, Rate = SalaryRate.Year } } });

            var hidden = Guid.NewGuid();
            member = new Member { Id = hidden };
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Salary);
            IndexContent(new MemberContent { Member = member, Candidate = new Candidate { DesiredSalary = new Salary { LowerBound = 80000, UpperBound = 100000, Rate = SalaryRate.Year } } });

            // Search without filter.

            var memberQuery = new MemberSearchQuery();
            var results = Search(memberQuery, 0, 20);
            var expected = new[] { hidden, notHidden };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));

            // Search, excluding no salary.

            memberQuery = new MemberSearchQuery { Salary = new Salary { LowerBound = 80000, UpperBound = 120000, Rate = SalaryRate.Year }, ExcludeNoSalary = true };
            results = Search(memberQuery, 0, 20);
            expected = new[] { notHidden };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));

            // Search, not excluding no salary.

            memberQuery = new MemberSearchQuery { Salary = new Salary { LowerBound = 80000, UpperBound = 120000, Rate = SalaryRate.Year }, ExcludeNoSalary = false };
            results = Search(memberQuery, 0, 20);
            expected = new[] { notHidden, hidden };
            Assert.IsTrue(expected.CollectionEqual(results.MemberIds));
        }
    }
}