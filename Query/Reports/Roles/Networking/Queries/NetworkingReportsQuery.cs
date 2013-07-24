using System;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Networking.Queries
{
    public class NetworkingReportsQuery
        : INetworkingReportsQuery
    {
        private readonly INetworkingReportsRepository _repository;

        public NetworkingReportsQuery(INetworkingReportsRepository repository)
        {
            _repository = repository;
        }

        int INetworkingReportsQuery.GetInvitationsSent(DateTimeRange dateRange)
        {
            return _repository.GetInvitationsSent(dateRange);
        }

        int INetworkingReportsQuery.GetInvitationsAccepted(DateTimeRange dateRange)
        {
            return _repository.GetInvitationsAccepted(dateRange);
        }

        double INetworkingReportsQuery.GetMonthlyInvitationAcceptancePercent()
        {
            var dateRange = new DateTimeRange(DateTime.Today.AddMonths(-1).AddDays(-DateTime.Today.AddMonths(-1).Day + 1), DateTime.Today.AddDays(-DateTime.Today.AddMonths(-1).Day));
            return _repository.GetInvitationAcceptancePercent(dateRange);
        }

        double INetworkingReportsQuery.Get48HourInvitationAcceptancePercent()
        {
            var dateRange = new DateTimeRange(DateTime.Today.AddDays(-2), DateTime.Today);
            return _repository.GetInvitationAcceptancePercent(dateRange);
        }
    }
}