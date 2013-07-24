using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Registration.Queries
{
    public class RegistrationReportsQuery
        : IRegistrationReportsQuery
    {
        private readonly IRegistrationReportsRepository _repository;

        public RegistrationReportsQuery(IRegistrationReportsRepository repository)
        {
            _repository = repository;
        }

        IDictionary<string, PromotionCodeReport> IRegistrationReportsQuery.GetPromotionCodeReports(DayRange day)
        {
            var promotionCodeReports = new Dictionary<string, PromotionCodeReport>();
            GetNewMembers(promotionCodeReports, day);
            return promotionCodeReports.ToDictionary(x => x.Key, x => x.Value);
        }

        private void GetNewMembers(IDictionary<string, PromotionCodeReport> promotionCodeReports, DateTimeRange day)
        {
            GetNewMembers(promotionCodeReports, _repository.GetJoinReferrals(day));
        }

        private static void GetNewMembers(IDictionary<string, PromotionCodeReport> promotionCodeReports, IEnumerable<KeyValuePair<string, int>> referrals)
        {
            foreach (var referral in referrals)
            {
                PromotionCodeReport report;
                if (promotionCodeReports.TryGetValue(referral.Key, out report))
                    report.NewMembers = referral.Value;
                else
                    promotionCodeReports[referral.Key] = new PromotionCodeReport { NewMembers = referral.Value };
            }
        }
    }
}