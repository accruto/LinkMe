using System.Data;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Data;
using LinkMe.Domain.Requests;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Roles.Networking.Data
{
    public class NetworkingReportsRepository
        : ReportsRepository<NetworkingDataContext>, INetworkingReportsRepository
    {
        public NetworkingReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        int INetworkingReportsRepository.GetInvitationsSent(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from i in dc.NetworkInvitationEntities
                        join r in dc.UserToUserRequestEntities on i.id equals r.id
                        where r.firstSentTime >= timeRange.Start && r.firstSentTime < timeRange.End
                        select i).Count();
            }
        }

        int INetworkingReportsRepository.GetInvitationsAccepted(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from i in dc.NetworkInvitationEntities
                        join r in dc.UserToUserRequestEntities on i.id equals r.id
                        where r.actionedTime >= timeRange.Start && r.actionedTime < timeRange.End
                              && r.status == (byte)RequestStatus.Accepted
                        select i).Count();
            }
        }

        double INetworkingReportsRepository.GetInvitationAcceptancePercent(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                double acceptances = (from r in dc.UserToUserRequestEntities
                                      where (r.firstSentTime >= timeRange.Start && r.firstSentTime < timeRange.End)
                                            && (r.actionedTime >= timeRange.Start && r.actionedTime < timeRange.End)
                                            && r.status == 2
                                      select r).Count();

                double invitations = (from r in dc.UserToUserRequestEntities
                                      where (r.firstSentTime >= timeRange.Start && r.firstSentTime < timeRange.End)
                                      select r).Count();

                return acceptances / (invitations == 0 ? 1 : invitations);
            }
        }

        protected override NetworkingDataContext CreateDataContext(IDbConnection connection)
        {
            return new NetworkingDataContext(connection);
        }
    }
}
