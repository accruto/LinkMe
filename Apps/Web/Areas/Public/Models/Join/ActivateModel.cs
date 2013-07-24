using System;
using LinkMe.Domain.Users.Members.Status;

namespace LinkMe.Web.Areas.Public.Models.Join
{
    public class ActivateModel
        : JoinModel
    {
        public Guid MemberId { get; set; }
        public string EmailAddress { get; set; }
        public MemberStatus MemberStatus { get; set; }
    }
}
