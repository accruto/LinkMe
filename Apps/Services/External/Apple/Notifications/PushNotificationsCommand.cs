using System;
using System.Collections.Generic;
using System.Linq;
using JdSoft.Apple.Apns.Notifications;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Devices.Apple.Queries;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Members;

namespace LinkMe.Apps.Services.External.Apple.Notifications
{
    public class PushNotificationsCommand
        : IPushNotificationsCommand, IDisposable
    {
        private static readonly EventSource EventSource = new EventSource(typeof(PushNotificationsCommand));

        private NotificationService _service;
        private readonly string _p12FileName;
        private readonly string _p12Password;
        private readonly bool _sandbox;
        private bool _disposed;

        private readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery;
        private readonly IAppleDevicesQuery _appleDevicesQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResumesQuery _resumesQuery;

        private readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand;

        public PushNotificationsCommand(string p12FileName, string p12Password, bool sandbox, IAppleDevicesQuery appleDevicesQuery, IMemberSearchAlertsQuery memberSearchAlertsQuery, IMemberSearchAlertsCommand memberSearchAlertsCommand, IMembersQuery membersQuery, ICandidatesQuery candidatesQuery, IResumesQuery resumesQuery)
        {
            _p12FileName = string.Format(".\\{0}", p12FileName);
            _p12Password = p12Password;
            _sandbox = sandbox;

            _memberSearchAlertsQuery = memberSearchAlertsQuery;
            _appleDevicesQuery = appleDevicesQuery;
            _membersQuery = membersQuery;
            _candidatesQuery = candidatesQuery;
            _resumesQuery = resumesQuery;

            _memberSearchAlertsCommand = memberSearchAlertsCommand;

            CreateService();
        }

        public void SendNotification(Employer employer, MemberSearchResults results, Guid savedSearchId, Guid savedSearchAlertId)
        {
            const string method = "SendNotification";

            var devices = _appleDevicesQuery.GetDevices(employer.Id);
            var savedSearch = _memberSearchAlertsQuery.GetMemberSearch(savedSearchId);

            if (devices == null || devices.Count == 0)
            {
                EventSource.Raise(Event.Error, method, "No devices registered for user with notifications turned on", Event.Arg("employer", employer.Id));
                return;
            }

            foreach (var device in devices)
            {
                if (!device.Active)
                    continue;

                if (results.MemberIds.Count == 0)
                    continue;

                //Exclude any ids that haven't already been viewed
                var unviewed = _memberSearchAlertsQuery.GetUnviewedCandidates(employer.Id, savedSearchAlertId);
                var potentationAlertableMemberIds = unviewed == null || unviewed.Count() == 0 ? results.MemberIds : results.MemberIds.Except(unviewed.Select(r => r.CandidateId));
                var alertableMemberIds = new List<Guid>();

                //Exclude any ids that haven't been updated since last alert (possible where the alert was sent earlier today)
                foreach (var memberId in potentationAlertableMemberIds)
                {
                    var lastAlert = _memberSearchAlertsQuery.LastAlert(memberId, savedSearchAlertId);
                    if (lastAlert == null)
                    {
                        alertableMemberIds.Add(memberId);
                    }
                    else
                    {
                        //Need to determine if this is genuinely a new alert or just an update from earlier today
                        var member = _membersQuery.GetMember(memberId);
                        var candidate = _candidatesQuery.GetCandidate(memberId);
                        var resume = candidate == null ? null : candidate.ResumeId.HasValue ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);

                        var lastUpdatedTime = new[]
                              {
                                  member.LastUpdatedTime,
                                  candidate == null ? DateTime.MinValue : candidate.LastUpdatedTime,
                                  resume == null ? DateTime.MinValue : resume.LastUpdatedTime,
                              }.Max();

                        //if the member has updated since the last alert was sent then send a new one
                        if (lastUpdatedTime > lastAlert.CreatedTime)
                        {
                            alertableMemberIds.Add(memberId);
                        }
                    }
                }

                if (alertableMemberIds.Count() == 0)
                    continue;

                _memberSearchAlertsCommand.AddResults(employer.Id,
                    alertableMemberIds.Select(
                        r =>
                            new SavedResumeSearchAlertResult
                                {
                                    CandidateId = r,
                                    SavedResumeSearchAlertId = savedSearchAlertId,
                                    Viewed = false
                                }).ToList());

                var alertNotification = new Notification(device.DeviceToken);
                alertNotification.Payload.Alert.ActionLocalizedKey = "View";
                alertNotification.Payload.Sound = "default";

                var badgeCount = _memberSearchAlertsQuery.GetBadgeCount(employer.Id);
                alertNotification.Payload.Badge = badgeCount;

                //Max payload size is 256 bytes; can't add memberIds
                //alertNotification.Payload.AddCustom("SavedSearchId", new[] {savedSearchId.ToString()});
                //alertNotification.Payload.AddCustom("MemberIds", alertableMemberIds.Select(a => a.ToString()).ToList());

                alertNotification.Payload.Alert.Body = alertableMemberIds.Count() == 1
                    ? string.Format("A new candidate has matched your \"{0}\" saved search", savedSearch.Name)
                    : string.Format("Your \"{0}\" saved search has new candidates", savedSearch.Name);

                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "Sending push notification",
                        Event.Arg("employerId", employer.Id),
                        Event.Arg("result count", alertableMemberIds.Count()),
                        Event.Arg("saved search", savedSearchId),
                        Event.Arg("saved searchname ", savedSearch.Name),
                        Event.Arg("badge count", badgeCount),
                        Event.Arg("deviceToken", device.DeviceToken),
                        Event.Arg("alert payload", alertNotification.Payload.ToJson()));

                //Queue the notification to be sent
                if (!_service.QueueNotification(alertNotification))
                {
                    EventSource.Raise(Event.Error, method, "Unable to queue notification", Event.Arg("deviceToken", device.DeviceToken));
                }
            }
        }

        private void CreateService()
        {
            _service = new NotificationService(_sandbox, _p12FileName, _p12Password, 1)
                              {
                                  SendRetries = 5,
                                  ReconnectDelay = 5000
                              };

            _service.Error += ServiceError;
            _service.NotificationTooLong += ServiceNotificationTooLong;

            _service.BadDeviceToken += ServiceBadDeviceToken;
            _service.NotificationFailed += ServiceNotificationFailed;
            _service.NotificationSuccess += ServiceNotificationSuccess;
            _service.Connecting += ServiceConnecting;
            _service.Connected += ServiceConnected;
            _service.Disconnected += ServiceDisconnected;

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            const string method = "Service Disposal";

            if (!_disposed)
            {
                if (disposing)
                {
                    if (EventSource.IsEnabled(Event.Trace))
                        EventSource.Raise(Event.Trace, method, "Disposing push notify service");

                    _service.Close();
                    _service.Dispose();
                }
            }
            _disposed = true;
        }

        #region Service Events
        static void ServiceBadDeviceToken(object sender, BadDeviceTokenException ex)
        {
            EventSource.Raise(Event.Error, "ServiceBadDeviceToken", string.Format("Bad Device Token: {0}", ex.Message));
        }

        static void ServiceDisconnected(object sender)
        {
            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, "ServiceDisconnected", "Disconnected...");
        }

        static void ServiceConnected(object sender)
        {
            if (EventSource.IsEnabled(Event.Trace))
            {
                var connection = sender as NotificationConnection;

                if (connection == null)
                    EventSource.Raise(Event.Trace, "ServiceConnected", "Connected...");
                else
                    EventSource.Raise(Event.Trace, "ServiceConnected", "Connected...",
                        Event.Arg("host", connection.Host),
                        Event.Arg("port", connection.Port),
                        Event.Arg("notifications to send", connection.QueuedNotificationsCount));
            }
        }

        static void ServiceConnecting(object sender)
        {
            if (EventSource.IsEnabled(Event.Trace))
            {
                var connection = sender as NotificationConnection;

                if (connection == null)
                    EventSource.Raise(Event.Trace, "ServiceConnecting", "Connecting...");
                else
                    EventSource.Raise(Event.Trace, "ServiceConnecting", "Connecting...",
                        Event.Arg("host", connection.Host),
                        Event.Arg("port", connection.Port),
                        Event.Arg("notifications to send", connection.QueuedNotificationsCount));
            }
        }

        static void ServiceNotificationTooLong(object sender, NotificationLengthException ex)
        {
            EventSource.Raise(Event.Error, "ServiceNotificationTooLong", string.Format("Notification Too Long: {0}", ex.Notification));
        }

        static void ServiceNotificationSuccess(object sender, Notification notification)
        {
            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, "ServiceNotificationSuccess",
                    string.Format("Notification Success: {0}", notification),
                    Event.Arg("device token", notification.DeviceToken));
        }

        static void ServiceNotificationFailed(object sender, Notification notification)
        {
            EventSource.Raise(Event.Error, "ServiceNotificationFailed", string.Format("Notification Failed: {0}", notification));
        }

        static void ServiceError(object sender, Exception ex)
        {
            EventSource.Raise(Event.Error, "ServiceError", string.Format("Error: {0}", ex.Message));
        }

        #endregion
    }
}
