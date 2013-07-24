using LinkMe.Domain.Resources;
using System.Collections.Generic;

namespace LinkMe.Web.Areas.Members.Models.Resources
{
    public class FeaturedArticleModel
    {
        public FeaturedResource FeaturedResource { get; set; }
        public Article Article { get; set; }
    }

    public class ResourcesModel
    {
        public IList<Category> Categories { get; set; }
        public IList<FeaturedArticleModel> FeaturedArticles { get; set; }
        public Video FeaturedVideo { get; set; }
        public QnA FeaturedQnA { get; set; }
        public int FeaturedQnAViews { get; set; }
        public int FeaturedQnAComments { get; set; }
        public PollModel ActivePoll { get; set; }
    }
}