using System.Collections.Generic;
using System.Data;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Data;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Roles.Registration.Data
{
    public class RegistrationReportsRepository
        : ReportsRepository<RegistrationDataContext>, IRegistrationReportsRepository
    {
        public RegistrationReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IDictionary<string, int> IRegistrationReportsRepository.GetJoinReferrals(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from r in dc.JoinReferralEntities
                        join u in dc.RegisteredUserEntities on r.userId equals u.id
                        where r.promotionCode != null
                        where u.createdTime >= timeRange.Start && u.createdTime < timeRange.End
                        group r by r.promotionCode into g
                        select new { PromotionCode = g.Key, Count = g.Count() }).ToDictionary(x => x.PromotionCode, x => x.Count);
            }
        }

        protected override RegistrationDataContext CreateDataContext(IDbConnection connection)
        {
            return new RegistrationDataContext(connection);
        }
    }
}
