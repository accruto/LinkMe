namespace LinkMe.Apps.Agents.Communications.Emails.InternalEmails
{
    public abstract class SalesEmail
        : InternalEmail
    {
        protected SalesEmail()
            : base(null)
        {
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}
