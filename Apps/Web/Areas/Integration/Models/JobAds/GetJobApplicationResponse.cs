using System;

namespace LinkMe.Web.Areas.Integration.Models.JobAds
{
    internal class GetJobApplicationResponse
        : XmlResponse
    {
        public GetJobApplicationResponse(string xml, string[] errors)
            : base(xml, errors)
        {
        }

        public GetJobApplicationResponse(Exception ex)
            : base(ex)
        {
        }

        protected override string RootName
        {
            get { return "GetJobApplicationResponse"; }
        }

        protected override bool WriteReturnCode
        {
            get { return true; }
        }
    }
}