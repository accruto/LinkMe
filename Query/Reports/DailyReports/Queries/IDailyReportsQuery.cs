using LinkMe.Domain;

namespace LinkMe.Query.Reports.DailyReports.Queries
{
    public interface IDailyReportsQuery
    {
        DailyReport GetDailyReport(DayRange day);
    }
}