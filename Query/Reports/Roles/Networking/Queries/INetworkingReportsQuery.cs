using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Networking.Queries
{
    public interface INetworkingReportsQuery
    {
        int GetInvitationsSent(DateTimeRange timeRange);
        int GetInvitationsAccepted(DateTimeRange timeRange);

        double GetMonthlyInvitationAcceptancePercent();
        double Get48HourInvitationAcceptancePercent();
    }
}