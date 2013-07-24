using System;

namespace LinkMe.Web.Areas.Integration.Models.JobAds
{
    public class JobAdsResponse
        : XmlResponse
    {
        public JobAdsResponse(string xml)
            : base(xml)
        {
        }

        public JobAdsResponse(Exception ex)
            : base(ex)
        {
        }

        protected override string RootName
        {
            get { return "GetJobAdsResponse"; }
        }

        protected override bool WriteReturnCode
        {
            get { return false; }
        }
    }
}
