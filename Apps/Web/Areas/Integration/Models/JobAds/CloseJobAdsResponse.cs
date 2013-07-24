using System;

namespace LinkMe.Web.Areas.Integration.Models.JobAds
{
    internal class CloseJobAdsResponse
        : XmlResponse
    {
        public CloseJobAdsResponse(string xml, string[] errors)
            : base(xml, errors)
        {
        }

        public CloseJobAdsResponse(Exception ex)
            : base(ex)
        {
        }

        protected override string RootName
        {
            get { return "CloseJobAdsResponse"; }
        }

        protected override bool WriteReturnCode
        {
            get { return true; }
        }
    }
}