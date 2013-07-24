namespace LinkMe.Web.Areas.Api.Models.Search
{
    public class SplitKeywordsModel
    {
        public string Keywords { get; set; }
        public string AllKeywords { get; set; }
        public string ExactPhrase { get; set; }
        public string AnyKeywords { get; set; }
        public string WithoutKeywords { get; set; }
    }
}