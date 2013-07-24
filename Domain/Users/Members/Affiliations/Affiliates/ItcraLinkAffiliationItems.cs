namespace LinkMe.Domain.Users.Members.Affiliations.Affiliates
{
    public enum ItcraLinkMemberStatus
    {
        Certified,
        ProfessionalMember,
    }

    public class ItcraLinkAffiliationItems
        : AffiliationItems
    {
        public ItcraLinkMemberStatus? Status { get; set; }
        public string MemberId { get; set; }
    }
}
