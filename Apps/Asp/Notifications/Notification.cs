using System;

namespace LinkMe.Apps.Asp.Notifications
{
    public enum NotificationType
    {
        Error,
        Information,
        Confirmation
    }

    public class Notification
    {
        private readonly Guid _id;
        private readonly NotificationType _type;
        private readonly string _message;

        public Notification(NotificationType type, string message)
        {
            _id = Guid.NewGuid();
            _type = type;
            _message = message;
        }

        public Guid Id
        {
            get { return _id; }
        }

        public NotificationType Type
        {
            get { return _type; }
        }

        public string Message
        {
            get { return _message; }
        }
    }
}
