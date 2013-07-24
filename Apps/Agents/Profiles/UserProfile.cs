using System;

namespace LinkMe.Apps.Agents.Profiles
{
    public class Reminder
    {
        public DateTime? FirstShownTime { get; set; }
        public bool Hide { get; set; }
    }

    public abstract class UserProfile
    {
        private readonly Reminder _updatedTermsReminder = new Reminder();

        public Reminder UpdatedTermsReminder
        {
            get { return _updatedTermsReminder; }
        }
    }
}