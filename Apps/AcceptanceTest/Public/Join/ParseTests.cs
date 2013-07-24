using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Web.Areas.Api.Models.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join
{
    [TestClass]
    public class ParseTests
        : JoinTests
    {
        [TestMethod]
        public void TestCompleteResume()
        {
            Test(TestResume.Complete, "Resume.doc");
        }

        [TestMethod]
        public void TestCompleteResumeDocx()
        {
            Test(TestResume.Complete, "Resume.docx");
        }

        [TestMethod]
        public void TestNoPhoneNumberResume()
        {
            Test(TestResume.NoPhoneNumber, "Resume.doc");
        }

        [TestMethod]
        public void TestNoNameResume()
        {
            Test(TestResume.NoName, "Resume.doc");
        }

        [TestMethod]
        public void TestNoEmailAddressResume()
        {
            Test(TestResume.NoEmailAddress, "Resume.doc");
        }

        [TestMethod]
        public void TestNoLocationResume()
        {
            Test(TestResume.NoLocation, "Resume.doc");
        }

        [TestMethod]
        public void TestInvalidLocationResume()
        {
            Test(TestResume.InvalidLocation, "Resume.doc");
        }

        [TestMethod]
        public void TestFutureSchoolDateResume()
        {
            Test(TestResume.FutureSchoolDate, "Resume.doc");
        }

        [TestMethod]
        public void TestFutureJobDateResume()
        {
            Test(TestResume.FutureJobDate, "Resume.doc");
        }

        private void Test(TestResume testResume, string fileName)
        {
            // Upload the file.

            var fileReferenceId = Upload(testResume, fileName);

            // Parse the resume.

            var parsedResumeId = Parse(fileReferenceId);

            // Join.

            Get(GetJoinUrl());
            SubmitJoin(fileReferenceId, parsedResumeId);
            var instanceId = GetInstanceId();

            var parsedResume = testResume.GetParsedResume();
            var member = GetMember(parsedResume);
            var candidate = GetCandidate();
            AssertPersonalDetails(instanceId, member, candidate, string.Empty, string.Empty, false);
        }

        private IMember GetMember(ParsedResume parsedResume)
        {
            var country = _locationQuery.GetCountry(Country);
            return new Member
            {
                Address = new Address { Location = _locationQuery.ResolveLocation(country, parsedResume.Address.Location) },
                AffiliateId = null,
                DateOfBirth = parsedResume.DateOfBirth,
                EthnicStatus = Defaults.EthnicStatus,
                FirstName = parsedResume.FirstName,
                Gender = Defaults.Gender,
                LastName = parsedResume.LastName,
                EmailAddresses = parsedResume.EmailAddresses == null || parsedResume.EmailAddresses.Count == 0
                    ? null
                    : (from e in parsedResume.EmailAddresses select new EmailAddress { Address = e.Address, IsVerified = e.IsVerified }).ToList(),
                PhoneNumbers = parsedResume.PhoneNumbers == null || parsedResume.PhoneNumbers.Count == 0
                    ? null
                    : (from p in parsedResume.PhoneNumbers select new PhoneNumber { Number = p.Number, Type = p.Type }).ToList(),
                PhotoId = null,
                VisibilitySettings = new VisibilitySettings(),
            };
        }

        private static ICandidate GetCandidate()
        {
            return new Candidate
            {
                DesiredJobTitle = null,
                DesiredJobTypes = Defaults.DesiredJobTypes,
                DesiredSalary = null,
                HighestEducationLevel = null,
                Industries = null,
                RecentProfession = null,
                RecentSeniority = null,
                RelocationLocations = null,
                RelocationPreference = Defaults.RelocationPreference,
                Status = CandidateStatus.Unspecified,
                VisaStatus = null,
            };
        }

        private Guid Upload(TestResume resume, string fileName)
        {
            using (var files = _filesCommand.SaveTempFile(resume.GetData(), fileName))
            {
                var path = files.FilePaths[0];
                return Upload(path);
            }
        }

        private Guid Upload(string file)
        {
            var files = new NameValueCollection { { "file", file } };
            var response = Post(_uploadUrl, null, files);
            var model = new JavaScriptSerializer().Deserialize<JsonResumeModel>(response);
            AssertJsonSuccess(model);
            return model.Id;
        }

        private Guid? Parse(Guid? fileReferenceId)
        {
            var response = Post(_parseUrl, new NameValueCollection { { "fileReferenceId", fileReferenceId.ToString() } });
            var model = new JavaScriptSerializer().Deserialize<JsonParsedResumeModel>(response);
            AssertJsonSuccess(model);
            return model.Id;
        }
    }
}
