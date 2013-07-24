using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Management.Areas.Communications.Models.Employers
{
    public class NewsletterSearchModel
    {
        public string Criteria { get; set; }
        public int Results { get; set; }
        public int NewLastMonth { get; set; }
    }

    public class NewsletterRankModel
    {
        public string Description { get; set; }
        public IList<int> PreviousMonths { get; set; }
        public int TopInOrganisation { get; set; }
        public int Rank { get; set; }
    }

    public class NewsletterModel
        : CommunicationsModel
    {
        public string LoginId { get; set; }
        public IEmployer Employer { get; set; }
        public IList<NewsletterSearchModel> SampleSearches { get; set; }
        public IList<NewsletterSearchModel> PreviousSearches { get; set; }
        public IList<NewsletterRankModel> Ranks { get; set; }
    }
}
