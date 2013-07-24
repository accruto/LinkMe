using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Web.Areas.Landing.Models.JobAds
{
    public class SearchModelConverter
        : Converter<SearchModel>
    {
        public override void Convert(SearchModel presentation, ISetValues values)
        {
            
        }

        public override SearchModel Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            return new SearchModel
            {
                Keywords = values.GetStringValue("ctl00$Body$ucJobSearch$txtKeywords") ?? values.GetStringValue("LinkMeKeywords"),
                Location = values.GetStringValue("ctl00$Body$ucJobSearch$txtLocation") ?? values.GetStringValue("LinkMeLocation"),
            };
        }
    }
}
