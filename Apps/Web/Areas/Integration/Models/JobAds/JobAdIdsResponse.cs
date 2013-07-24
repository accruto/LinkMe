using System;

namespace LinkMe.Web.Areas.Integration.Models.JobAds
{
    public class JobAdIdsResponse
        : XmlResponse
    {
        public JobAdIdsResponse(string xml)
            : base(xml)
        {
        }

        public JobAdIdsResponse(Exception ex)
            : base(ex)
        {
        }

        protected override string RootName
        {
            get { return "GetJobAdIdsResponse"; }
        }

        protected override bool WriteReturnCode
        {
            get { return false; }
        }
    }
}
