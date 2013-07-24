using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Resumes
{
    [TestClass]
    public class DownloadTests
        : ResumesTests
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        private const string HomePhoneNumber = "99999999";
        private const string MobilePhoneNumber = "88888888";
        private const string WorkPhoneNumber = "77777777";
        private ReadOnlyUrl _downloadUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _downloadUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/download");
        }

        [TestMethod]
        public void TestAllPhoneNumbers()
        {
            TestPhoneNumbers(SetAllPhoneNumbers, true, true, true);
        }

        [TestMethod]
        public void TestHomePhoneNumber()
        {
            TestPhoneNumbers(m => m.PhoneNumbers = new List<PhoneNumber> {new PhoneNumber {Number = HomePhoneNumber, Type = PhoneNumberType.Home}}, true, false, false);
        }

        [TestMethod]
        public void TestMobilePhoneNumber()
        {
            TestPhoneNumbers(m => m.PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = MobilePhoneNumber, Type = PhoneNumberType.Mobile } }, false, true, false);
        }

        [TestMethod]
        public void TestWorkPhoneNumber()
        {
            TestPhoneNumbers(m => m.PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = WorkPhoneNumber, Type = PhoneNumberType.Work } }, false, false, true);
        }

        private static void SetAllPhoneNumbers(Member member)
        {
            member.PhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber {Number = HomePhoneNumber, Type = PhoneNumberType.Home},
                new PhoneNumber {Number = MobilePhoneNumber, Type = PhoneNumberType.Mobile},
                new PhoneNumber {Number = WorkPhoneNumber, Type = PhoneNumberType.Work},
            };
        }

        private void TestPhoneNumbers(Action<Member> setPhoneNumbers, bool homeIsExpected, bool mobileIsExpected, bool workIsExpected)
        {
            var member = CreateMember(0);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Resume);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.PhoneNumbers);

            var employer = CreateEmployer();
            LogIn(employer);

            // No phone numbers.

            member.PhoneNumbers = null;
            _memberAccountsCommand.UpdateMember(member);
            AssertPhoneNumbers(DownloadResume(member.Id), false, false, false);

            // Set phone numbers.

            setPhoneNumbers(member);
            _memberAccountsCommand.UpdateMember(member);
            AssertPhoneNumbers(DownloadResume(member.Id), homeIsExpected, mobileIsExpected, workIsExpected);

            // Hide all phone numbers.

            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);
            _memberAccountsCommand.UpdateMember(member);
            AssertPhoneNumbers(DownloadResume(member.Id), false, false, false);

            // Show everything.

            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.PhoneNumbers);
            _memberAccountsCommand.UpdateMember(member);
            AssertPhoneNumbers(DownloadResume(member.Id), homeIsExpected, mobileIsExpected, workIsExpected);
        }

        private static void AssertPhoneNumbers(string contents, bool homeIsExpected, bool mobileIsExpected, bool workIsExpected)
        {
            if (homeIsExpected)
                Assert.IsTrue(contents.Contains("h: " + HomePhoneNumber));
            else
                Assert.IsFalse(contents.Contains("h:"));

            if (mobileIsExpected)
                Assert.IsTrue(contents.Contains("m: " + MobilePhoneNumber));
            else
                Assert.IsFalse(contents.Contains("m:"));

            if (workIsExpected)
                Assert.IsTrue(contents.Contains("w: " + WorkPhoneNumber));
            else
                Assert.IsFalse(contents.Contains("w:"));
        }

        private string DownloadResume(Guid memberId)
        {
            using (var response = DownloadResume(GetDownloadUrl(memberId)))
            {
                using (var reader = new StreamReader(response))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private ReadOnlyUrl GetDownloadUrl(Guid memberId)
        {
            var downloadUrl = _downloadUrl.AsNonReadOnly();
            downloadUrl.QueryString.Add("candidateId", memberId.ToString());
            return downloadUrl;
        }

        private Stream DownloadResume(ReadOnlyUrl url)
        {
            // Create the request, copying appropriate cookies.

            var webRequest = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            webRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            webRequest.CookieContainer = new CookieContainer();
            webRequest.CookieContainer.Add(Browser.Cookies.GetCookies(new Uri(url.AbsoluteUri)));

            // Check the response.

            using (var webResponse = webRequest.GetResponse())
            {
                var responseStream = webResponse.GetResponseStream();
                var stream = new MemoryStream();
                var buffer = new byte[65536];
                int read;
                do
                {
                    read = responseStream.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, read);
                } while (read != 0);
                stream.Seek(0, SeekOrigin.Begin);

                return stream;
            }
        }

        protected override Employer CreateEmployer()
        {
            var employer = base.CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation {CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id});
            return employer;
        }
    }
}
