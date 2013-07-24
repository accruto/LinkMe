using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Status
{
    [TestClass]
    public abstract class StatusEmailTests
        : StatusTests
    {
        protected readonly IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();

        [TestInitialize]
        public void StatusEmailTestInitialize()
        {
            _emailServer.ClearEmails();
        }

        protected ReadOnlyUrl GetLinkUrl(int index, int total)
        {
            var email = _emailServer.AssertEmailSent();
            var links = GetLinks(email.GetHtmlView().Body);
            Assert.AreEqual(total, links.Count);
            return new ReadOnlyUrl(links[index]);
        }

        protected void AssertLink(ReadOnlyUrl linkUrl, ReadOnlyUrl expectedUrl)
        {
            Get(linkUrl);
            AssertUrl(expectedUrl);
        }

        private static IList<string> GetLinks(string body)
        {
            return XElement.Load(new StringReader(body))
                .Descendants("a").Attributes("href")
                .Select(a => a.Value)
                .Where(a => !a.StartsWith("mailto:")).ToList();
        }

        protected void ConfirmStatus(Member member, CandidateStatus status)
        {
            AssertPageContains("Your work status has been confirmed as");

            // Should not be shown options.

            Assert.IsFalse(_availableNowRadioButton.IsVisible);
            Assert.IsFalse(_activelyLookingRadioButton.IsVisible);
            Assert.IsFalse(_openToOffersRadioButton.IsVisible);
            Assert.IsFalse(_notLookingRadioButton.IsVisible);
            Assert.IsFalse(_saveButton.IsVisible);

            Assert.AreEqual(status, _candidatesCommand.GetCandidate(member.Id).Status);
        }

        protected void ChangeStatus(Member member, CandidateStatus previousStatus, CandidateStatus newStatus)
        {
            // Should be shown options.

            Assert.IsTrue(_availableNowRadioButton.IsVisible);
            Assert.IsTrue(_activelyLookingRadioButton.IsVisible);
            Assert.IsTrue(_openToOffersRadioButton.IsVisible);
            Assert.IsTrue(_notLookingRadioButton.IsVisible);
            Assert.IsTrue(_saveButton.IsVisible);

            Assert.AreEqual(previousStatus == CandidateStatus.AvailableNow, _availableNowRadioButton.IsChecked);
            Assert.AreEqual(previousStatus == CandidateStatus.ActivelyLooking, _activelyLookingRadioButton.IsChecked);
            Assert.AreEqual(previousStatus == CandidateStatus.OpenToOffers, _openToOffersRadioButton.IsChecked);
            Assert.AreEqual(previousStatus == CandidateStatus.NotLooking, _notLookingRadioButton.IsChecked);

            // Change status.

            switch (newStatus)
            {
                case CandidateStatus.ActivelyLooking:
                    _activelyLookingRadioButton.IsChecked = true;
                    break;
                    
                case CandidateStatus.AvailableNow:
                    _availableNowRadioButton.IsChecked = true;
                    break;
                    
                case CandidateStatus.NotLooking:
                    _notLookingRadioButton.IsChecked = true;
                    break;
                    
                case CandidateStatus.OpenToOffers:
                    _openToOffersRadioButton.IsChecked = true;
                    break;
            }

            _saveButton.Click();

            var url = GetUpdateStatusUrl(newStatus);
            AssertUrl(url);

            AssertPageContains("Your work status has been updated to");
            Assert.IsFalse(_availableNowRadioButton.IsVisible);
            Assert.IsFalse(_activelyLookingRadioButton.IsVisible);
            Assert.IsFalse(_openToOffersRadioButton.IsVisible);
            Assert.IsFalse(_notLookingRadioButton.IsVisible);
            Assert.IsFalse(_saveButton.IsVisible);

            AssertStatus(member.Id, newStatus);
        }
    }
}
