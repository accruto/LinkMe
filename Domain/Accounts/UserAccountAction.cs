using System;

namespace LinkMe.Domain.Accounts
{
    public class UserAccountAction
    {
        public Guid UserId { get; set; }
        public Guid ActionedById { get; set; }
        public DateTime Time { get; set; }
    }
}
