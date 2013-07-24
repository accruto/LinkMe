using System;
using System.Collections.Generic;

namespace LinkMe.Query.Reports.Roles.JobAds
{
    public class JobAdTotalsReport
    {
        public int Applications { get; set; }
        public int Views { get; set; }
    }

    public class JobAdReport
    {
        public IList<Guid> OpenedJobAds { get; set; }
        public IList<Guid> ClosedJobAds { get; set; }
        public JobAdTotalsReport Totals { get; set; }
    }
}