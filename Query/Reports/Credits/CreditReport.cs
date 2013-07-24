namespace LinkMe.Query.Reports.Credits
{
    public class CreditReport
    {
        public int? OpeningCredits { get; set; }
        public int? CreditsUsed { get; set; }
        public int? CreditsAdded { get; set; }
        public int? CreditsExpired { get; set; }
        public int? CreditsDeallocated { get; set; }
        public int? ClosingCredits { get; set; }
    }
}