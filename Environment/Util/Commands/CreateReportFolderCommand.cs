namespace LinkMe.Environment.Util.Commands
{
    public class CreateReportFolderCommand
        : ReportsCommand
    {
        public override void Execute()
        {
            var url = Options["ru"].Values[0];
            var folder = GetPath(Options["rf"].Values[0]);
            ReportServiceUtil.CreateFolder(url, folder);
        }
    }
}
