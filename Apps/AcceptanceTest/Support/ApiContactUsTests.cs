using System.Collections.Specialized;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Support
{
    [TestClass]
    public class ApiContactUsTests
        : SupportTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        private const string EmailAddress = "from@test.linkme.net.au";
        private const string Message = "This is the message";
        private const string Name = "Paul Hodgman";
        private const string EnquiryType = "Report a site issue";
        private const string Phone = "0410635666";

        private ReadOnlyUrl _contactUsSendUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _emailServer.ClearEmails();
            _contactUsSendUrl = new ReadOnlyApplicationUrl("~/api/contactus/send");
        }
        
        [TestMethod]
        public void TestContactUsErrors()
        {
            AssertJsonError(ApiSend(string.Empty, EmailAddress, Phone, UserType.Member, EnquiryType, Message), "Name", "The name is required.");
            _emailServer.AssertNoEmailSent();

            AssertJsonError(ApiSend(Name, string.Empty, Phone, UserType.Member, EnquiryType, Message), "From", "The email address is required.");
            _emailServer.AssertNoEmailSent();
            
            AssertJsonError(ApiSend(Name, EmailAddress, Phone, UserType.Member, EnquiryType, string.Empty), "Message", "The message is required.");
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestLoggedInMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            
            AssertJsonSuccess(ApiSend(member.FullName, member.EmailAddresses[0].Address, Phone, UserType.Member, EnquiryType, Message));

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member, Return, MemberServicesInbox);
            email.AssertHtmlViewContains(member.FullName);
            email.AssertHtmlViewContains(member.EmailAddresses[0].Address);
            email.AssertHtmlViewDoesNotContain(Phone);
            email.AssertHtmlViewContains(EnquiryType);
            email.AssertHtmlViewContains(Message);
        }

        [TestMethod]
        public void TestAnonymousMember()
        {
            AssertJsonSuccess(ApiSend(Name, EmailAddress, Phone, UserType.Member, EnquiryType, Message));
            
            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(EmailAddress, Name), Return, MemberServicesInbox);
            email.AssertHtmlViewContains(Name);
            email.AssertHtmlViewContains(EmailAddress);
            email.AssertHtmlViewDoesNotContain(Phone);
            email.AssertHtmlViewContains(EnquiryType);
            email.AssertHtmlViewContains(Message);
        }

        [TestMethod]
        public void TestLoggedInEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            LogIn(employer);

            AssertJsonSuccess(ApiSend(employer.FullName, employer.EmailAddress.Address, Phone, UserType.Employer, EnquiryType, Message));

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(employer, Return, ClientServicesInbox);
            email.AssertHtmlViewContains(employer.FullName);
            email.AssertHtmlViewContains(employer.EmailAddress.Address);
            email.AssertHtmlViewContains(Phone);
            email.AssertHtmlViewContains(EnquiryType);
            email.AssertHtmlViewContains(Message);
        }

        [TestMethod]
        public void TestAnonymousEmployer()
        {
            AssertJsonSuccess(ApiSend(Name, EmailAddress, Phone, UserType.Employer, EnquiryType, Message));
            
            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(EmailAddress, Name), Return, ClientServicesInbox);
            email.AssertHtmlViewContains(Name);
            email.AssertHtmlViewContains(EmailAddress);
            email.AssertHtmlViewContains(Phone);
            email.AssertHtmlViewContains(EnquiryType);
            email.AssertHtmlViewContains(Message);
        }

        private JsonResponseModel ApiSend(string name, string from, string phoneNumber, UserType userType, string enquiryType, string message)
        {
            var parameters = new NameValueCollection
            {
                {"Name", name},
                {"From", from},
                {"UserType", userType.ToString()},
                {"EnquiryType", enquiryType},
                {"Message", message}
            };

            // Phone number is only shown for employers.

            if (userType == UserType.Employer)
                parameters.Add("PhoneNumber", phoneNumber);

            return Deserialize<JsonResponseModel>(Post(_contactUsSendUrl, parameters));
        }
    }
}
