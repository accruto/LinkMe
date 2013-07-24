namespace LinkMe.Apps.Agents.Communications.Emails.InternalEmails
{
    public abstract class EmployerServicesEmail
        : InternalEmail
    {
        protected EmployerServicesEmail()
            : base(null)
        {
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}
