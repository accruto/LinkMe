using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Users.Employers.Views
{
    public enum MemberUnlockingReason
    {
        EmailSent = 0,
        PhoneNumberViewed = 1,
        ResumeDownloaded = 2,
        ResumeSent = 3,
        Unlock,
    }

    public class MemberUnlocking
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime Time { get; set; }
        public MemberUnlockingReason Reason { get; set; }
        public Guid EmployerId { get; set; }
        public Guid MemberId { get; set; }
        public Guid? ExercisedCreditId { get; set; }
    }
}