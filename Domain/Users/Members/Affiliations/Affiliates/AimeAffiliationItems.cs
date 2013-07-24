namespace LinkMe.Domain.Users.Members.Affiliations.Affiliates
{
    public enum AimeMemberStatus
    {
        BecomeMentor,
        BecomeEmployee,
        CurrentMentor,
        CurrentMentoree,
    }

    public class AimeAffiliationItems
        : AffiliationItems
    {
        public AimeMemberStatus? Status { get; set; }
    }
}
