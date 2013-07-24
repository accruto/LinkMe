namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class OtherMemberModel
    {
        public string Courses { get; set; }
        public string Awards { get; set; }
        public string Professional { get; set; }
        public string Interests { get; set; }
        public string Affiliations { get; set; }
        public string Other { get; set; }
        public string Referees { get; set; }
    }

    public class OtherModel
    {
        public OtherMemberModel Member { get; set; }
    }
}