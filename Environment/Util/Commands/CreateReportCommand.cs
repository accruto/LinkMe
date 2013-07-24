using System.IO;

namespace LinkMe.Environment.Util.Commands
{
    public class CreateReportCommand
        : ReportsCommand
    {
        public override void Execute()
        {
            var url = Options["ru"].Values[0];
            var folder = GetPath(Options["rf"].Values[0]);
            var definitionFile = Options["rd"].Values[0];
            var name = Path.GetFileNameWithoutExtension(definitionFile);
            var dataSources = Options["rds"] != null ? GetDataSources(Options["rds"].Values.ToArray()) : null;
            ReportServiceUtil.CreateReport(url, name, folder, dataSources, definitionFile);
        }
    }
}
