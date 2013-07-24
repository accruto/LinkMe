using System;

namespace LinkMe.Domain.Credits
{
    public abstract class Credit
    {
        internal Credit()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    }

    public sealed class ContactCredit
        : Credit
    {
        internal ContactCredit()
        {
        }
    }

    public sealed class JobAdCredit
        : Credit
    {
        internal JobAdCredit()
        {
        }
    }

    public sealed class ApplicantCredit
        : Credit
    {
        internal ApplicantCredit()
        {
        }
    }
}
