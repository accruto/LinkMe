using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Notifications
{
    public static class NotificationsExtensions
    {
        public const string NotificationIdParameter = "msgId";

        public static Guid SetNotification(this HttpSessionStateBase session, NotificationType notificationType, string message)
        {
            return SetNotification(GetNotifications(session), notificationType, message);
        }

        public static ReadOnlyUrl SetNotification(this HttpSessionState session, ReadOnlyUrl url, NotificationType notificationType, string message)
        {
            return SetNotification(GetNotifications(session), url, notificationType, message);
        }

        public static ReadOnlyUrl SetNotification(this HttpSessionStateBase session, ReadOnlyUrl url, NotificationType notificationType, string message)
        {
            return SetNotification(GetNotifications(session), url, notificationType, message);
        }

        public static Notification GetNotification(this HttpSessionState session, Guid id)
        {
            return GetNotification(GetNotifications, session, id);
        }

        public static Notification GetNotification(this HttpSessionStateBase session, Guid id)
        {
            return GetNotification(GetNotifications, session, id);
        }

        public static Notification GetNotification(this HttpSessionState session, ReadOnlyUrl url)
        {
            return GetNotification(GetNotifications, session, url);
        }

        public static Notification GetNotification(this HttpSessionStateBase session, ReadOnlyUrl url)
        {
            return GetNotification(GetNotifications, session, url);
        }

        public static void RemoveNotification(this HttpSessionState session, Guid id)
        {
            RemoveNotification(GetNotifications(session), id);
        }

        public static void RemoveNotification(this HttpSessionStateBase session, Guid id)
        {
            RemoveNotification(GetNotifications(session), id);
        }

        private static Guid SetNotification(IDictionary<Guid, Notification> notifications, NotificationType notificationType, string message)
        {
            var notification = new Notification(notificationType, message);
            notifications.Add(notification.Id, notification);
            return notification.Id;
        }

        private static ReadOnlyUrl SetNotification(IDictionary<Guid, Notification> notifications, ReadOnlyUrl url, NotificationType notificationType, string message)
        {
            var notification = new Notification(notificationType, message);
            notifications.Add(notification.Id, notification);

            // The URL may already contain a message which needs to be replaced. Don't just add a new one.

            var newUrl = url.AsNonReadOnly();
            newUrl.QueryString[NotificationIdParameter] = notification.Id.ToString("n");
            return newUrl;
        }

        private static Notification GetNotification<TSession>(Func<TSession, IDictionary<Guid, Notification>> getNotifications, TSession session, Guid id)
        {
            Notification notification;
            getNotifications(session).TryGetValue(id, out notification);
            return notification;
        }

        private static Notification GetNotification<TSession>(Func<TSession, IDictionary<Guid, Notification>> getNotifications, TSession session, ReadOnlyUrl url)
        {
            var id = ParseUtil.ParseUserInputGuidOptional(url.QueryString[NotificationIdParameter], "message ID");
            return id == null ? null : GetNotification(getNotifications, session, id.Value);
        }

        private static void RemoveNotification(IDictionary<Guid, Notification> notifications, Guid id)
        {
            notifications.Remove(id);
        }

        private static IDictionary<Guid, Notification> GetNotifications(HttpSessionState session)
        {
            var notifications = session["Notifications"] as IDictionary<Guid, Notification>;
            if (notifications == null)
            {
                notifications = new Dictionary<Guid, Notification>();
                session["Notifications"] = notifications;
            }

            return notifications;
        }

        private static IDictionary<Guid, Notification> GetNotifications(HttpSessionStateBase session)
        {
            var notifications = session["Notifications"] as IDictionary<Guid, Notification>;
            if (notifications == null)
            {
                notifications = new Dictionary<Guid, Notification>();
                session["Notifications"] = notifications;
            }

            return notifications;
        }
    }
}