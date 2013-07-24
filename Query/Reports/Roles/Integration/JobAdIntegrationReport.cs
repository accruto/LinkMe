namespace LinkMe.Query.Reports.Roles.Integration
{
    public abstract class JobAdReport
    {
        public int Events { get; set; }
        public int Successes { get; set; }
        public int JobAds { get; set; }
    }

    public class JobAdExportFeedReport
        : JobAdReport
    {
    }

    public class JobAdExportFeedIdReport
        : JobAdReport
    {
    }

    public class JobAdImportPostReport
        : JobAdReport
    {
        public int Failed { get; set; }
        public int Posted { get; set; }
        public int Closed { get; set; }
        public int Updated { get; set; }
        public int Duplicates { get; set; }
        public int Ignored { get; set; }
    }

    public class JobAdImportCloseReport
        : JobAdReport
    {
        public int Failed { get; set; }
        public int Closed { get; set; }
        public int NotFound { get; set; }
    }

    public class JobAdExportPostReport
        : JobAdReport
    {
        public int Failed { get; set; }
        public int Posted { get; set; }
        public int Updated { get; set; }
    }

    public class JobAdExportCloseReport
        : JobAdReport
    {
        public int Failed { get; set; }
        public int Closed { get; set; }
        public int NotFound { get; set; }
    }

    public class JobAdIntegrationReport
    {
        public JobAdExportFeedReport ExportFeedReport { get; set; }
        public JobAdExportFeedIdReport ExportFeedIdReport { get; set; }
        public JobAdImportPostReport ImportPostReport { get; set; }
        public JobAdImportCloseReport ImportCloseReport { get; set; }
        public JobAdExportPostReport ExportPostReport { get; set; }
        public JobAdExportCloseReport ExportCloseReport { get; set; }
    }
}
