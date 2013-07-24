using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Members.Models.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class EducationTests
        : ApiTests
    {
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private ReadOnlyUrl _educationUrl;

        private const string CityFormat = "City {0}";
        private const string DegreeFormat = "Degree {0}";
        private const string InstitutionFormat = "Degree {0}";
        private const string MajorFormat = "Major {0}";
        private const string DescriptionFormat = "Description {0}";

        private const string NewDegree = "My new degree";
        private const string NewDescription = "My new description";

        [TestInitialize]
        public void TestInitialize()
        {
            _educationUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/education");
        }

        [TestMethod]
        public void TestHighestEducationLevel()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            candidate.HighestEducationLevel = EducationLevel.Masters;
            var parameters = GetParameters(candidate, null);
            AssertJsonSuccess(Education(parameters));
            AssertMember(member, candidate, null, true);

            // Change it.

            candidate.HighestEducationLevel = EducationLevel.Postgraduate;
            parameters = GetParameters(candidate, null);
            AssertJsonSuccess(Education(parameters));
            AssertMember(member, candidate, null, true);

            // Reset it.

            candidate.HighestEducationLevel = null;
            parameters = GetParameters(candidate, null);
            AssertJsonSuccess(Education(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestAddSchool()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Add school with date.

            var endDate = DateTime.Now.AddMonths(-3).Date;

            var school1 = CreateSchool(1, new PartialCompletionDate(new PartialDate(endDate.Year, endDate.Month)));
            var resume = new Resume { Schools = new List<School> { school1 } };
            var parameters = GetParameters(candidate, school1);
            AssertModel(true, Education(parameters));
            AssertMember(member, candidate, resume, true);

            // Add school with no date.

            var school4 = CreateSchool(4, new PartialCompletionDate());
            resume.Schools.Add(school4);
            parameters = GetParameters(candidate, school4);
            AssertModel(true, Education(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestAddSchoolErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Add school with no degree.

            var endDate = DateTime.Now.AddMonths(-3).Date;

            var school = CreateSchool(1, new PartialCompletionDate(new PartialDate(endDate.Year, endDate.Month)));
            school.Degree = null;
            var parameters = GetParameters(candidate, school);
            AssertJsonError(Education(parameters), "Degree", "The degree is required.");

            // Add school with no institution.

            school = CreateSchool(2, new PartialCompletionDate(new PartialDate(endDate.Year, endDate.Month)));
            school.Institution = null;
            parameters = GetParameters(candidate, school);
            AssertJsonError(Education(parameters), "Institution", "The institution is required.");

            // Add school with no date.

            school = CreateSchool(2, null);
            parameters = GetParameters(candidate, school);
            AssertJsonError(Education(parameters), "CompletionDate", "The completion date is required.");
        }

        [TestMethod]
        public void TestUpdateSchool()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            // Create some schools.

            var endDate = DateTime.Now.AddMonths(-3).Date;

            var school1 = CreateSchool(1, new PartialCompletionDate(new PartialDate(endDate.Year, endDate.Month)));
            var school2 = CreateSchool(2, new PartialCompletionDate(new PartialDate(endDate.Year, endDate.Month)));
            var resume = new Resume { Schools = new List<School> { school1, school2 } };
            _candidateResumesCommand.CreateResume(candidate, resume);
            LogIn(member);

            // Update one.

            school1.Degree = NewDegree;
            var parameters = GetParameters(candidate, school1);
            AssertModel(true, Education(parameters));
            AssertMember(member, candidate, resume, true);

            // Update another.

            school2.Description = NewDescription;
            parameters = GetParameters(candidate, school2);
            AssertModel(true, Education(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestUpdateSchoolErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            // Create some schools.

            var endDate = DateTime.Now.AddMonths(-3).Date;

            var school = CreateSchool(1, new PartialCompletionDate(new PartialDate(endDate.Year, endDate.Month)));
            var resume = new Resume { Schools = new List<School> { school } };
            _candidateResumesCommand.CreateResume(candidate, resume);
            LogIn(member);

            // Add school with no degree.

            var originalDegree = school.Degree;
            school.Degree = null;
            var parameters = GetParameters(candidate, school);
            AssertJsonError(Education(parameters), "Degree", "The degree is required.");
            school.Degree = originalDegree;

            // Add school with no institution.

            var originalInstitution = school.Institution;
            school.Institution = null;
            parameters = GetParameters(candidate, school);
            AssertJsonError(Education(parameters), "Institution", "The institution is required.");
            school.Institution = originalInstitution;

            // Add school with no date.

            school.CompletionDate = null;
            parameters = GetParameters(candidate, school);
            AssertJsonError(Education(parameters), "CompletionDate", "The completion date is required.");
        }

        private static NameValueCollection GetParameters(ICandidate candidate, ISchool school)
        {
            var parameters = new NameValueCollection
            {
                {"HighestEducationLevel", candidate.HighestEducationLevel == null ? null : candidate.HighestEducationLevel.Value.ToString()},
            };

            if (school != null)
            {
                if (school.Id != Guid.Empty)
                    parameters.Add("Id", school.Id.ToString());
                parameters.Add("City", school.City);
                parameters.Add("Degree", school.Degree);
                parameters.Add("Description", school.Description);
                parameters.Add("Institution", school.Institution);
                parameters.Add("Major", school.Major);
                parameters.Add("EndDateMonth", school.CompletionDate == null || school.CompletionDate.End == null || school.CompletionDate.End.Value.Month == null ? null : school.CompletionDate.End.Value.Month.Value.ToString());
                parameters.Add("EndDateYear", school.CompletionDate == null || school.CompletionDate.End == null ? null : school.CompletionDate.End.Value.Year.ToString());
                parameters.Add("IsCurrent", (school.CompletionDate != null && school.CompletionDate.End == null).ToString());
            }

            return parameters;
        }

        private JsonProfileSchoolModel Education(NameValueCollection parameters)
        {
            var response = Post(_educationUrl, parameters);
            return new JavaScriptSerializer().Deserialize<JsonProfileSchoolModel>(response);
        }

        private static void AssertModel(bool expectSchoolId, JsonProfileSchoolModel model)
        {
            AssertJsonSuccess(model);
            if (expectSchoolId)
            {
                Assert.IsNotNull(model.SchoolId);
                Assert.AreNotEqual(Guid.Empty, model.SchoolId.Value);
            }
            else
            {
                Assert.IsNull(model.SchoolId);
            }
        }

        private static School CreateSchool(int index, PartialCompletionDate completionDate)
        {
            return new School
            {
                Id = Guid.NewGuid(),
                City = string.Format(CityFormat, index),
                Degree = string.Format(DegreeFormat, index),
                Description = string.Format(DescriptionFormat, index),
                Institution = string.Format(InstitutionFormat, index),
                Major = string.Format(MajorFormat, index),
                CompletionDate = completionDate,
            };
        }

        protected override JsonResponseModel Call()
        {
            return Education(new NameValueCollection());
        }
    }
}
