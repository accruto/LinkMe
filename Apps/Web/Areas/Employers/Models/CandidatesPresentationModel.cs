using LinkMe.Web.Models;

namespace LinkMe.Web.Areas.Employers.Models
{
    public enum DetailLevel { Expanded, Compact }

    public class CandidatesPresentationModel
        : PresentationModel
    {
        public DetailLevel DetailLevel { get; set; }
    }
}