using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Users.Employers.Views
{
    public class MemberViewing
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime Time { get; set; }

        public Guid? EmployerId { get; set; }
        public Guid MemberId { get; set; }
        public Guid? JobAdId { get; set; }

        public Guid ChannelId { get; set; }
        public Guid AppId { get; set; }
    }
}