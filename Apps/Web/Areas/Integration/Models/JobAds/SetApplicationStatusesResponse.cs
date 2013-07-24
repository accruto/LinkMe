using System;

namespace LinkMe.Web.Areas.Integration.Models.JobAds
{
    internal class SetApplicationStatusesResponse
        : XmlResponse
    {
        public SetApplicationStatusesResponse(string xml, string[] errors)
            : base(xml, errors)
        {
        }

        public SetApplicationStatusesResponse(Exception ex)
            : base(ex)
        {
        }

        protected override string RootName
        {
            get { return "SetJobApplicationStatusResponse"; }
        }

        protected override bool WriteReturnCode
        {
            get { return true; }
        }
    }
}