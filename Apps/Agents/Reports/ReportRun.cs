using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Reports
{
    public enum ReportRunOutcome
    {
        /// <summary>
        ///  The report was not run, because the parameters are invalid.
        ///  </summary>
        InvalidParameters,
        /// <summary>
        ///  The report was run and returned no results.
        ///  </summary>
        NoResults,
        /// <summary>
        /// The report returned only a text result, no file. The output "result" parameter is set.
        /// </summary>
        TextResultOnly,
        /// <summary>
        /// The report results were written to the supplied streams.
        /// </summary>
        FileResult
    }

    public class ReportRun
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [IsSet]
        public Guid ReportId { get; set; }
        [IsSet]
        public DateTime SentTime { get; set; }
        [IsSet]
        public DateTime PeriodStart { get; set; }
        [IsSet]
        public DateTime PeriodEnd { get; set; }
        public Guid? SentToAccountManagerId { get; set; }
        public string SentToClientEmail { get; set; }
    }
}
