using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Networking
{
    public interface INetworkingReportsRepository
    {
        int GetInvitationsSent(DateTimeRange timeRange);
        int GetInvitationsAccepted(DateTimeRange timeRange);
        double GetInvitationAcceptancePercent(DateTimeRange timeRange);
    }
}
