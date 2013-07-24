using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Shared.Models
{
    public class FeaturedEmployerModel
    {
        public string Name { get; set; }
        public ReadOnlyUrl SearchUrl { get; set; }
        public ReadOnlyUrl LogoUrl { get; set; }
    }
}