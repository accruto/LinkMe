using System;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Services.External.Apple.Notifications;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public class PushNotifyResumeSearchAlertsTask
        : ResumeSearchAlertsTask
    {
        private readonly IPushNotificationsCommand _pushNotificationsCommand;
        private readonly IPushDevicesFeedbackCommand _pushDevicesFeedbackCommand;

        public PushNotifyResumeSearchAlertsTask(IExecuteMemberSearchCommand executeMemberSearchCommand, IMemberSearchesQuery memberSearchesQuery, IMemberSearchAlertsCommand memberSearchAlertsCommand, IMemberSearchAlertsQuery memberSearchAlertsQuery, IEmployersQuery employersQuery, IPushNotificationsCommand pushNotificationsCommand, IPushDevicesFeedbackCommand pushDevicesFeedbackCommand)
            : base(executeMemberSearchCommand, memberSearchesQuery, memberSearchAlertsCommand, memberSearchAlertsQuery, employersQuery, AlertType.AppleDevice)
        {
            _pushNotificationsCommand = pushNotificationsCommand;
            _pushDevicesFeedbackCommand = pushDevicesFeedbackCommand;
        }

        public override void ExecuteTask(string[] args)
        {
            DisableDevices();
            base.ExecuteTask(args);

            //Ensure Service is closed
            ((IDisposable)_pushNotificationsCommand).Dispose();
        }

        protected override void Alert(Employer employer, MemberSearch search, MemberSearchAlert alert, MemberSearchResults results)
        {
            _pushNotificationsCommand.SendNotification(employer, results, alert.MemberSearchId, alert.Id);
        }

        private void DisableDevices()
        {
            _pushDevicesFeedbackCommand.DisableDevices();
        }
    }
}