namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class CareerObjectivesMemberModel
    {
        public string Objective { get; set; }
        public string Summary { get; set; }
        public string Skills { get; set; }
    }

    public class CareerObjectivesModel
    {
        public CareerObjectivesMemberModel Member { get; set; }
    }
}