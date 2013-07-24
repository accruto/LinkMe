namespace LinkMe.Apps.Agents.Communications.Emails.InternalEmails
{
    public abstract class AllStaffEmail
        : InternalEmail
    {
        protected AllStaffEmail()
            : base(null)
        {
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}
