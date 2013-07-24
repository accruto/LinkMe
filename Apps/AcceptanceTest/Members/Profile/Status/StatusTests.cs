using System;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Status
{
    [TestClass]
    public abstract class StatusTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        protected readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private ReadOnlyUrl _updateStatusUrl;

        protected HtmlRadioButtonTester _availableNowRadioButton;
        protected HtmlRadioButtonTester _activelyLookingRadioButton;
        protected HtmlRadioButtonTester _openToOffersRadioButton;
        protected HtmlRadioButtonTester _notLookingRadioButton;
        protected HtmlButtonTester _saveButton;

        [TestInitialize]
        public void StatusTestInitialize()
        {
            _updateStatusUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/status/update");

            _availableNowRadioButton = new HtmlRadioButtonTester(Browser, "AvailableNow");
            _activelyLookingRadioButton = new HtmlRadioButtonTester(Browser, "ActivelyLooking");
            _openToOffersRadioButton = new HtmlRadioButtonTester(Browser, "OpenToOffers");
            _notLookingRadioButton = new HtmlRadioButtonTester(Browser, "NotLooking");
            _saveButton = new HtmlButtonTester(Browser, "save");
        }

        protected Member CreateMember(int index, CandidateStatus status)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.Status = status;
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }

        protected void AssertStatus(Guid memberId, CandidateStatus status)
        {
            var candidate = _candidatesQuery.GetCandidate(memberId);
            Assert.AreEqual(status, candidate.Status);
        }

        protected ReadOnlyUrl GetUpdateStatusUrl()
        {
            return _updateStatusUrl;
        }

        protected ReadOnlyUrl GetUpdateStatusUrl(CandidateStatus status)
        {
            var url = _updateStatusUrl.AsNonReadOnly();
            url.QueryString["status"] = status.ToString();
            return url;
        }
    }
}
