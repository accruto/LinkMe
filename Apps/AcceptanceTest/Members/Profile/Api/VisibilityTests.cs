using System.Collections.Specialized;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class VisibilityTests
        : ApiTests
    {
        private ReadOnlyUrl _visibilityUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _visibilityUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/visibility");
        }

        [TestMethod]
        public void TestVisibility()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            var visibility = member.VisibilitySettings.Professional.EmploymentVisibility
                .ResetFlag(ProfessionalVisibility.Resume)
                .ResetFlag(ProfessionalVisibility.Name)
                .ResetFlag(ProfessionalVisibility.PhoneNumbers)
                .ResetFlag(ProfessionalVisibility.ProfilePhoto)
                .ResetFlag(ProfessionalVisibility.RecentEmployers);

            member.VisibilitySettings.Professional.EmploymentVisibility = ProfessionalVisibility.Resume | ProfessionalVisibility.RecentEmployers;
            var parameters = GetParameters(member);
            AssertJsonSuccess(Visibility(parameters));

            member.VisibilitySettings.Professional.EmploymentVisibility = visibility.SetFlag(ProfessionalVisibility.Resume).SetFlag(ProfessionalVisibility.RecentEmployers);
            AssertMember(member, candidate, null, true);

            // Turn the resume off, which should turn everything off.

            member.VisibilitySettings.Professional.EmploymentVisibility = ProfessionalVisibility.RecentEmployers;
            parameters = GetParameters(member);
            AssertJsonSuccess(Visibility(parameters));

            member.VisibilitySettings.Professional.EmploymentVisibility = visibility;
            AssertMember(member, candidate, null, true);

            // Turn it on.

            member.VisibilitySettings.Professional.EmploymentVisibility = ProfessionalVisibility.Resume | ProfessionalVisibility.RecentEmployers | ProfessionalVisibility.ProfilePhoto;
            parameters = GetParameters(member);
            AssertJsonSuccess(Visibility(parameters));

            member.VisibilitySettings.Professional.EmploymentVisibility = visibility.SetFlag(ProfessionalVisibility.Resume).SetFlag(ProfessionalVisibility.RecentEmployers).SetFlag(ProfessionalVisibility.ProfilePhoto);
            AssertMember(member, candidate, null, true);
        }

        private static NameValueCollection GetParameters(IMember member)
        {
            return new NameValueCollection
            {
                {"ShowResume", member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume) ? "true" : "false"},
                {"ShowName", member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Name) ? "true" : "false"},
                {"ShowPhoneNumbers", member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers) ? "true" : "false"},
                {"ShowProfilePhoto", member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.ProfilePhoto) ? "true" : "false"},
                {"ShowRecentEmployers", member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.RecentEmployers) ? "true" : "false"},
            };
        }

        private JsonResponseModel Visibility(NameValueCollection parameters)
        {
            return Deserialize<JsonResponseModel>(Post(_visibilityUrl, parameters));
        }

        protected override JsonResponseModel Call()
        {
            return Visibility(new NameValueCollection());
        }
    }
}
