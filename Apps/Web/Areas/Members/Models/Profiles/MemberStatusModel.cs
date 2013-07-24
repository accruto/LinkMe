using System;
using LinkMe.Domain.Users.Members.Status;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class MemberStatusModel
    {
        public int PercentComplete { get; set; }
        public bool PromptForResumeUpdate { get; set; }
        public int Age { get; set; }
        public MemberStatus MemberStatus { get; set; }
        public Guid MemberId { get; set; }
    }
}