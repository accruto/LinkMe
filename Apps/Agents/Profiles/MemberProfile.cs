namespace LinkMe.Apps.Agents.Profiles
{
    public class MemberProfile
        : UserProfile
    {
        private readonly Reminder _updateStatusReminder = new Reminder();

        public Reminder UpdateStatusReminder
        {
            get { return _updateStatusReminder; }
        }
    }
}