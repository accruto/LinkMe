using System;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Members;

namespace LinkMe.Apps.Services.External.Apple.Notifications
{
    public interface IPushNotificationsCommand
    {
        void SendNotification(Employer employer, MemberSearchResults results, Guid savedSearchId, Guid savedSearchAlertId);
    }
}
