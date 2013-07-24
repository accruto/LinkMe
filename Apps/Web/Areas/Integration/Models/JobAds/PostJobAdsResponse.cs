using System;

namespace LinkMe.Web.Areas.Integration.Models.JobAds
{
    internal class PostJobAdsResponse
        : XmlResponse
    {
        public PostJobAdsResponse(string xml, string[] errors)
            : base(xml, errors)
        {
        }

        public PostJobAdsResponse(Exception ex)
            : base(ex)
        {
        }

        protected override string RootName
        {
            get { return "PostJobAdsResponse"; }
        }

        protected override bool WriteReturnCode
        {
            get { return true; }
        }
    }
}