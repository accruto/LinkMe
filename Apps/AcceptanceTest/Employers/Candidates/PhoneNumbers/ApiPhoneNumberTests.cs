using System.Collections.Generic;
using System.Collections.Specialized;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.PhoneNumbers
{
    [TestClass]
    public class ApiPhoneNumberTests
        : WebTestClass
    {
        private const string MobilePhoneNumberFormat = "1999999{0}";
        private const string HomePhoneNumberFormat = "2999999{0}";
        private const string WorkPhoneNumberFormat = "3999999{0}";

        private ReadOnlyUrl _phoneNumbersUrl;

        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            _phoneNumbersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/api/phonenumbers");
        }

        [TestMethod]
        public void TestVisible()
        {
            var member = CreateMember(0, true, true, true);
            var employer = CreateEmployer(0);

            LogIn(employer);
            AssertJsonSuccess(PhoneNumbers(member));
        }

        [TestMethod]
        public void TestNoPhoneNumber()
        {
            var member = CreateMember(0, true, true, false);
            var employer = CreateEmployer(0);

            LogIn(employer);
            AssertJsonError(PhoneNumbers(member), null, "The candidate details have been hidden by the candidate.");
        }

        [TestMethod]
        public void TestResumeNotVisible()
        {
            var member = CreateMember(0, false, true, true);
            var employer = CreateEmployer(0);

            LogIn(employer);
            AssertJsonError(PhoneNumbers(member), null, "The candidate details have been hidden by the candidate.");
        }

        [TestMethod]
        public void TestPhoneNumberNotVisible()
        {
            var member = CreateMember(0, true, false, true);
            var employer = CreateEmployer(0);

            LogIn(employer);
            AssertJsonError(PhoneNumbers(member), null, "The candidate details have been hidden by the candidate.");
        }

        [TestMethod]
        public void TestResumeNotVisibleNoPhoneNumber()
        {
            var member = CreateMember(0, false, true, false);
            var employer = CreateEmployer(0);

            LogIn(employer);
            AssertJsonError(PhoneNumbers(member), null, "The candidate details have been hidden by the candidate.");
        }

        [TestMethod]
        public void TestPhoneNumberNotVisibleNoPhoneNumber()
        {
            var member = CreateMember(0, true, false, false);
            var employer = CreateEmployer(0);

            LogIn(employer);
            AssertJsonError(PhoneNumbers(member), null, "The candidate details have been hidden by the candidate.");
        }

        private Employer CreateEmployer(int index)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });
            return employer;
        }

        private Member CreateMember(int index, bool isResumeVisible, bool isPhoneNumberVisible, bool hasPhoneNumbers)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);

            if (hasPhoneNumbers)
            {
                member.PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber {Number = string.Format(MobilePhoneNumberFormat, index), Type = PhoneNumberType.Mobile},
                    new PhoneNumber {Number = string.Format(HomePhoneNumberFormat, index), Type = PhoneNumberType.Home},
                    new PhoneNumber {Number = string.Format(WorkPhoneNumberFormat, index), Type = PhoneNumberType.Work},
                };
            }
            else
            {
                member.PhoneNumbers = null;
            }

            member.VisibilitySettings.Professional.EmploymentVisibility = isResumeVisible
                ? member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Resume)
                : member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Resume);

            member.VisibilitySettings.Professional.EmploymentVisibility = isPhoneNumberVisible
                ? member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.PhoneNumbers)
                : member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);

            _memberAccountsCommand.UpdateMember(member);
            return member;
        }

        private JsonResponseModel PhoneNumbers(params Member[] members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            return Deserialize<JsonResponseModel>(Post(_phoneNumbersUrl, parameters));
        }
    }
}
