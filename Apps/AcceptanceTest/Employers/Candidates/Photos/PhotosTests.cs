using System;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Photos
{
    [TestClass]
    public class PhotosTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private ReadOnlyUrl _photoUrl;
        private ReadOnlyUrl _loginUrl;

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _photoUrl = new ReadOnlyApplicationUrl("~/employers/candidates/photo");
            _loginUrl = new ReadOnlyApplicationUrl(true, "~/employers/login");
        }

        [TestMethod]
        public void TestLimitedContactedPhoto()
        {
            var member = CreateMember();
            var employer = CreateEmployer(100);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);

            // Get the photo.

            LogIn(employer);
            Get(GetPhotoUrl(member.Id));

            // Can see the photo because the member has been unlocked.

            Assert.AreEqual(HttpStatusCode.OK, Browser.CurrentStatusCode);
            Assert.AreEqual("image/jpeg", Browser.ResponseHeaders["Content-Type"]);
        }

        [TestMethod]
        public void TestUnlimitedContactedPhoto()
        {
            var member = CreateMember();
            var employer = CreateEmployer(null);

            // Get the photo.

            LogIn(employer);
            Get(GetPhotoUrl(member.Id));

            // Can see the photo because the employer has unlimited credits, effectively unlocking everyone.

            Assert.AreEqual(HttpStatusCode.OK, Browser.CurrentStatusCode);
            Assert.AreEqual("image/jpeg", Browser.ResponseHeaders["Content-Type"]);
        }

        [TestMethod]
        public void TestNotLoggedIn()
        {
            var member = CreateMember();

            // Get the photo.

            Get(GetPhotoUrl(member.Id));
            AssertPath(_loginUrl);
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestNoPhoto()
        {
            var member = CreateMember();
            var employer = CreateEmployer(null);

            member.PhotoId = null;
            _memberAccountsCommand.UpdateMember(member);

            // Get the photo.

            LogIn(employer);
            Get(GetPhotoUrl(member.Id));
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestResumeNotVisible()
        {
            var member = CreateMember();
            var employer = CreateEmployer(null);

            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Resume);
            _memberAccountsCommand.UpdateMember(member);

            // Get the photo.

            LogIn(employer);
            Get(GetPhotoUrl(member.Id));
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestPhotoNotVisible()
        {
            var member = CreateMember();
            var employer = CreateEmployer(null);

            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.ProfilePhoto);
            _memberAccountsCommand.UpdateMember(member);

            // Get the photo.

            LogIn(employer);
            Get(GetPhotoUrl(member.Id));
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestNotUnlocked()
        {
            var member = CreateMember();
            var employer = CreateEmployer(100);

            // Get the photo.

            LogIn(employer);
            Get(GetPhotoUrl(member.Id));
        }

        private ReadOnlyUrl GetPhotoUrl(Guid memberId)
        {
            var url = _photoUrl.AsNonReadOnly();
            url.QueryString.Add("candidateId", memberId.ToString());
            return url;
        }

        private Employer CreateEmployer(int? credits)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation {CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id, InitialQuantity = credits});
            return employer;
        }

        private Member CreateMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            _memberAccountsCommand.AddTestProfilePhoto(member);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.ProfilePhoto);
            _memberAccountsCommand.UpdateMember(member);
            return member;
        }
    }
}
