using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Users.Employers.Views
{
    public enum MemberAccessReason
    {
        MessageSent = 0,
        PhoneNumberViewed = 1,
        ResumeDownloaded = 2,
        ResumeSent = 3,
        Unlock,
    }

    public class MemberAccess
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime Time { get; set; }
        public MemberAccessReason Reason { get; set; }
        public Guid EmployerId { get; set; }
        public Guid MemberId { get; set; }
        public Guid? ExercisedCreditId { get; set; }

        public Guid ChannelId { get; set; }
        public Guid AppId { get; set; }
    }
}