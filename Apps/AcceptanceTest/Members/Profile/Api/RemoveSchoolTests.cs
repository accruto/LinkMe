using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class RemoveSchoolTests
        : ApiTests
    {
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();

        private ReadOnlyUrl _removeSchoolUrl;

        private const string CityFormat = "City {0}";
        private const string DegreeFormat = "Degree {0}";
        private const string InstitutionFormat = "Degree {0}";
        private const string MajorFormat = "Major {0}";
        private const string DescriptionFormat = "Description {0}";

        [TestInitialize]
        public void TestInitialize()
        {
            _removeSchoolUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/education/removeschool");
        }

        [TestMethod]
        public void TestRemoveSchool()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var school1 = CreateSchool(1);
            var school2 = CreateSchool(2);
            var resume = new Resume { Schools = new List<School> { school1, school2 } };
            _candidateResumesCommand.CreateResume(candidate, resume);
            LogIn(member);

            // Remove one.

            resume.Schools = new List<School> { school2 };
            var parameters = GetParameters(school1);
            AssertJsonSuccess(RemoveSchool(parameters));
            AssertMember(member, candidate, resume, true);

            // Remove another.

            resume.Schools = null;
            parameters = GetParameters(school2);
            AssertJsonSuccess(RemoveSchool(parameters));
            AssertMember(member, candidate, resume, true);
        }

        private static School CreateSchool(int index)
        {
            var now = DateTime.Now;
            return new School
            {
                City = string.Format(CityFormat, index),
                Degree = string.Format(DegreeFormat, index),
                Description = string.Format(DescriptionFormat, index),
                Institution = string.Format(InstitutionFormat, index),
                Major = string.Format(MajorFormat, index),
                CompletionDate = new PartialCompletionDate(new PartialDate(now.AddYears(-1 * (index + 1)).Year)),
            };
        }

        private static NameValueCollection GetParameters(ISchool school)
        {
            return new NameValueCollection
            {
                {"Id", school.Id.ToString()},
            };
        }

        private JsonResponseModel RemoveSchool(NameValueCollection parameters)
        {
            return Deserialize<JsonResponseModel>(Post(_removeSchoolUrl, parameters));
        }

        protected override JsonResponseModel Call()
        {
            return RemoveSchool(new NameValueCollection());
        }
    }
}
