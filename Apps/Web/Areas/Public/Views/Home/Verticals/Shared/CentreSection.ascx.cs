using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared
{
    public class CentreSection
        : ViewUserControl
    {
        protected class Featured
        {
            public ReadOnlyUrl SearchUrl { get; set; }
            public ReadOnlyUrl ImageUrl { get; set; }
            public string Margin { get; set; }
            public string Alt { get; set; }
        }

        protected static readonly IList<Featured> TopFeaturedEmployers = new List<Featured>
        {
            new Featured
            {
                SearchUrl = new ReadOnlyApplicationUrl("~/search/jobs/advanced?Advertiser=Telstra"),
                ImageUrl = new ReadOnlyApplicationUrl("~/themes/communities/linkme/img/employers/2010-06-10/telstra.png"),
                Margin = "0pt 20px 10px 0pt",
                Alt = "Job Search - Telstra",
            },
            new Featured
            {
                SearchUrl = new ReadOnlyApplicationUrl("~/search/jobs/advanced?Advertiser=Michael+Page"),
                ImageUrl = new ReadOnlyApplicationUrl("~/themes/communities/linkme/img/employers/2010-06-10/michael-page-international.png"),
                Margin = "0pt 20px 0pt 0pt",
                Alt = "Job Search - Michael Page International Jobs",
            }
        };

        protected static readonly IList<Featured> BottomFeaturedEmployers = new List<Featured>
        {
            new Featured
            {
                SearchUrl = new ReadOnlyApplicationUrl("~/search/jobs/advanced?Advertiser=Citak"),
                ImageUrl = new ReadOnlyApplicationUrl("~/themes/communities/linkme/img/employers/2010-06-10/citak.png"),
                Margin = "0pt 0pt 15px 0pt",
                Alt = "Job Search - Citak Jobs",
            },
            new Featured
            {
                SearchUrl = new ReadOnlyApplicationUrl("~/search/jobs/advanced?Advertiser=MYOB"),
                ImageUrl = new ReadOnlyApplicationUrl("~/themes/communities/linkme/img/employers/2010-06-10/myob.png"),
                Margin = "0pt 0pt 15px 15px",
                Alt = "Job Search - MYOB Jobs",
            }
        };

        protected static readonly IList<Featured> TopFeaturedPartners = new List<Featured>
        {
            new Featured
            {
                SearchUrl = new ReadOnlyApplicationUrl("http://www.ahri.com.au/"),
                ImageUrl = new ReadOnlyApplicationUrl("~/ui/images/tiles/ahri_78x36.png"),
                Margin = "0pt 20px 10px 0pt",
                Alt = "Australian Human Resource Institute",
            },
        };

        protected static readonly IList<Featured> BottomFeaturedPartners = new List<Featured>
        {
            new Featured
            {
                SearchUrl = new ReadOnlyApplicationUrl("http://ahricareers.linkme.com.au/"),
                ImageUrl = new ReadOnlyApplicationUrl("~/ui/images/tiles/hrcareers_78x36.png"),
                Margin = "0pt 20px 10px 0pt",
                Alt = "HRcareers",
            },
            new Featured
            {
                SearchUrl = new ReadOnlyApplicationUrl("http://www.rcsa.com.au/"),
                ImageUrl = new ReadOnlyApplicationUrl("~/ui/images/tiles/rcsa-affiliate_78x36.png"),
                Margin = "0pt 20px 10px 0pt",
                Alt = "RCSA",
            },
        };
    }
}
