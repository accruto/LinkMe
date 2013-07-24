using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Apps.Services.External.Apple.Notifications;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Devices.Apple;
using LinkMe.Domain.Devices.Apple.Commands;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications
{
    [TestClass, Ignore]
	public class PushNotificationResumeSearchAlertsTaskTests
        : TaskTests
	{
        private readonly IDbConnectionFactory _connectionFactory = Resolve<IDbConnectionFactory>();

        private readonly IAppleDevicesCommand _appleDevicesCommand = Resolve<IAppleDevicesCommand>();
        private readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand = Resolve<IMemberSearchAlertsCommand>();
        private readonly IPushNotificationsCommand _pushNotificationsCommand = Resolve<IPushNotificationsCommand>();
        private readonly IPushDevicesFeedbackCommand _pushDevicesFeedbackCommand = Resolve<IPushDevicesFeedbackCommand>();
        private readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery = Resolve<IMemberSearchAlertsQuery>();
        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand = Resolve<IExecuteMemberSearchCommand>();
        private readonly IMemberSearchesQuery _memberSearchesQuery = Resolve<IMemberSearchesQuery>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();

		private const string EmployerLoginId = "employer";
	    private const string MemberEmailAddress = "member@test.linkme.net.au";
	    private const string SearchAlertNameFormat = "My search alert {0}";

        private Member _member;
        private Candidate _candidate;
	    private Resume _resume;
        private Employer _employer;
	    private MemberSearch _search0;
	    private MemberSearch _search1;

        [TestInitialize]
        public void PushNotificationResumeSearchAlertsTaskTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            MemberSearchHost.ClearIndex();

            _member = _memberAccountsCommand.CreateTestMember(MemberEmailAddress);
            _candidate = _candidatesCommand.GetCandidate(_member.Id);
            _resume = _candidateResumesCommand.AddTestResume(_candidate);
            _candidate.DesiredJobTitle = "mushroom picker";
            _candidatesCommand.UpdateCandidate(_candidate);

            _employer = _employerAccountsCommand.CreateTestEmployer(EmployerLoginId, _organisationsCommand.CreateTestOrganisation(0));

            _appleDevicesCommand.CreateDevice(new AppleDevice
            {
                OwnerId = _employer.Id,
                Active = true,
                DeviceToken = "b6080e7144f8ce3d53c67ac08416b51114cc1008fac1912b3879dd1d485e236a",
            });

            var jobTitle = _resume.Jobs[0].Title;
            _search0 = CreateSearchAlert(_employer, 0,
                new MemberSearchCriteria
                {
                    JobTitle = jobTitle,
                    SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated }
                },
                AlertType.AppleDevice);
            _search1 = CreateSearchAlert(_employer, 1,
                new MemberSearchCriteria
                {
                    JobTitle = jobTitle,
                    SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated }
                },
                AlertType.Email);
        }

        [TestMethod]
        public void BasicSend()
        {
            // Run the task.

            ExecuteTask();
            AssertNotifications(_search0, AlertType.AppleDevice, 1);
            AssertNotifications(_search1, AlertType.Email, 0);
        }

        [TestMethod]
        public void TwoSends()
        {
            // Run the task twice. Should only send one alert (as the previous alert hasn't been viewed)

            ExecuteTask();

            AssertNotifications(_search0, AlertType.AppleDevice, 1);
            AssertNotifications(_search1, AlertType.Email, 0);

            ExecuteTask();

            AssertNotifications(_search0, AlertType.AppleDevice, 1);
            AssertNotifications(_search1, AlertType.Email, 0);
        }

        [TestMethod]
        public void TwoSendsWithViewing()
        {
            // Run the task twice. Should only send one alert (as the previous alert is for today)

            ExecuteTask();

            AssertNotifications(_search0, AlertType.AppleDevice, 1);
            AssertNotifications(_search1, AlertType.Email, 0);

            ViewAlertResults();

            ExecuteTask();

            AssertNotifications(_search0, AlertType.AppleDevice, 1);
            AssertNotifications(_search1, AlertType.Email, 0);
        }

        [TestMethod]
        public void TwoSendsWithOldResult()
        {
            // Run the task twice. Should only send one alert (as the previous alert is not viewed)

            ExecuteTask();

            AssertNotifications(_search0, AlertType.AppleDevice, 1);
            AssertNotifications(_search1, AlertType.Email, 0);

            MakeOldAlertResult(_search0, AlertType.AppleDevice, false);

            ExecuteTask();

            AssertNotifications(_search0, AlertType.AppleDevice, 1);
            AssertNotifications(_search1, AlertType.Email, 0);
        }

        [TestMethod]
        public void TwoSendsWithOldViewing()
        {
            // Run the task twice. Should send two alerts (as the previous alert is viewed)

            ExecuteTask();

            AssertNotifications(_search0, AlertType.AppleDevice, 1);
            AssertNotifications(_search1, AlertType.Email, 0);

            DeleteAlertResults();
            MakeOldAlertResult(_search0, AlertType.AppleDevice, true);

            ExecuteTask();

            AssertNotifications(_search0, AlertType.AppleDevice, 2);
            AssertNotifications(_search1, AlertType.Email, 0);
        }

        private void ExecuteTask()
        {
            new PushNotifyResumeSearchAlertsTask(_executeMemberSearchCommand, _memberSearchesQuery, _memberSearchAlertsCommand, _memberSearchAlertsQuery, _employersQuery, _pushNotificationsCommand, _pushDevicesFeedbackCommand).ExecuteTask();
        }

        private MemberSearch CreateSearchAlert(IUser employer, int index, MemberSearchCriteria criteria, AlertType alertType)
        {
            var search = new MemberSearch { Criteria = criteria, Name = string.Format(SearchAlertNameFormat, index) };
            _memberSearchAlertsCommand.CreateMemberSearchAlert(employer, search, alertType);
            _memberSearchAlertsCommand.UpdateLastRunTime(search.Id, DateTime.Now.AddDays(-3), alertType);
            return search;
        }

        private void ViewAlertResults()
        {
            _memberSearchAlertsCommand.MarkAsViewed(_employer.Id, _member.Id);
        }

        private void MakeOldAlertResult(MemberSearch search, AlertType alertType, bool viewed)
        {
            var searchAlert = _memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, alertType);

            _memberSearchAlertsCommand.AddResults(_employer.Id, new List<SavedResumeSearchAlertResult>
                                                                    {
                                                                        new SavedResumeSearchAlertResult
                                                                            {
                                                                                CandidateId = _member.Id,
                                                                                CreatedTime = DateTime.Now.AddDays(-5),
                                                                                SavedResumeSearchAlertId = searchAlert.Id,
                                                                                Viewed = viewed,
                                                                            }
                                                                    });
        }

        private void DeleteAlertResults()
        {
            // Update directly to avoid checks etc in code.

            DatabaseHelper.ExecuteNonQuery(
                _connectionFactory,
                "DELETE FROM savedSearchAlertResult");
        }

        private void AssertNotifications(MemberSearch search, AlertType alertType, int expectedCount)
        {
            var searchAlert = _memberSearchAlertsQuery.GetMemberSearchAlert(search.Id, alertType);
            var results = _memberSearchAlertsQuery.GetAlertResults(searchAlert.Id);

            Assert.AreEqual(expectedCount, results.Count);
        }
    }
}
