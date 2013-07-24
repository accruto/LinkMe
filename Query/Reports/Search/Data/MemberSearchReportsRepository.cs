using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Search.Data
{
    public class MemberSearchReportsRepository
        : ReportsRepository<SearchDataContext>, IMemberSearchReportsRepository
    {
        [Flags]
        private enum UserFlags
        {
            Disabled = 0x04
        }

        private static readonly Func<SearchDataContext, DateTimeRange, int> GetAllMemberSearches
            = CompiledQuery.Compile((SearchDataContext dc, DateTimeRange timeRange)
                                    => (from s in dc.ResumeSearchEntities
                                        where s.startTime >= timeRange.Start && s.startTime < timeRange.End
                                        select s).Count());

        private static readonly Func<SearchDataContext, Guid, DateTimeRange, int> GetMemberSearches
            = CompiledQuery.Compile((SearchDataContext dc, Guid channelId, DateTimeRange timeRange)
                                    => (from s in dc.ResumeSearchEntities
                                        where s.startTime >= timeRange.Start && s.startTime < timeRange.End
                                              && !SqlMethods.Like(s.context, "%suggested%")
                                              &&
                                              (
                                                  Equals(s.searcherId, null)
                                                  ||
                                                  (
                                                      from e in dc.EmployerEntities
                                                      join u in dc.RegisteredUserEntities on e.id equals u.id
                                                      where e.id == s.searcherId
                                                            && !SqlMethods.Like(u.emailAddress, "%linkme.com.au")
                                                      select e
                                                  ).Any()
                                              )
                                              && s.channelId == channelId
                                        select s).Count());

        private static readonly Func<SearchDataContext, string, Guid, DateTimeRange, int> GetContextMemberSearches
            = CompiledQuery.Compile((SearchDataContext dc, string context, Guid channelId, DateTimeRange timeRange)
                                    => (from s in dc.ResumeSearchEntities
                                        where s.startTime >= timeRange.Start && s.startTime < timeRange.End
                                              && s.context == context
                                              && !SqlMethods.Like(s.context, "%suggested%")
                                              &&
                                              (
                                                  Equals(s.searcherId, null)
                                                  ||
                                                  (
                                                      from e in dc.EmployerEntities
                                                      join u in dc.RegisteredUserEntities on e.id equals u.id
                                                      where e.id == s.searcherId
                                                            && !SqlMethods.Like(u.emailAddress, "%linkme.com.au")
                                                      select e
                                                  ).Any()
                                              )
                                              && s.channelId == channelId
                                        select s).Count());

        private static readonly Func<SearchDataContext, Guid, DateTimeRange, int> GetAnonymousMemberSearches
            = CompiledQuery.Compile((SearchDataContext dc, Guid channelId, DateTimeRange timeRange)
                                    => (from s in dc.ResumeSearchEntities
                                        where s.startTime >= timeRange.Start && s.startTime < timeRange.End
                                              && !SqlMethods.Like(s.context, "%suggested%")
                                              && Equals(s.searcherId, null)
                                              && s.channelId == channelId
                                        select s).Count());

        private static readonly Func<SearchDataContext, Guid, DateTimeRange, int> GetMemberSearchesBySearcherId
            = CompiledQuery.Compile((SearchDataContext dc, Guid searcherId, DateTimeRange timeRange)
                                    => (from s in dc.ResumeSearchEntities
                                        where s.searcherId == searcherId
                                              && s.startTime >= timeRange.Start && s.startTime < timeRange.End
                                              && (SqlMethods.Like(s.context, "SimpleSearch%") || SqlMethods.Like(s.context, "AdvancedSearch%") || SqlMethods.Like(s.context, "LikeResumeSearch%"))
                                        select s).Count());

        private static readonly Func<SearchDataContext, string, DateTimeRange, IQueryable<Tuple<Guid, int>>> GetAllFilteredMemberSearches
            = CompiledQuery.Compile((SearchDataContext dc, string searcherIds, DateTimeRange timeRange)
                                    => (from s in dc.ResumeSearchEntities
                                        join i in dc.SplitGuids(SplitList<Guid>.Delimiter, searcherIds) on s.searcherId equals i.value
                                        where s.startTime >= timeRange.Start && s.startTime < timeRange.End
                                              && (SqlMethods.Like(s.context, "SimpleSearch%") || SqlMethods.Like(s.context, "AdvancedSearch%") || SqlMethods.Like(s.context, "LikeResumeSearch%"))
                                        group s by s.searcherId into searches
                                                                    select new Tuple<Guid, int>(searches.Key.Value, searches.Count())));

        private static readonly Func<SearchDataContext, Guid, int> GetMemberSearchesAlerts
            = CompiledQuery.Compile((SearchDataContext dc, Guid searcherId)
                                    => (from a in dc.SavedResumeSearchAlertEntities
                                        join s in dc.SavedResumeSearchEntities on a.savedResumeSearchId equals s.id
                                        where s.ownerId == searcherId
                                        select a).Count());

        private static readonly Func<SearchDataContext, string, IQueryable<Tuple<Guid, int>>> GetAllMemberSearchesAlerts
            = CompiledQuery.Compile((SearchDataContext dc, string searcherIds)
                                    => (from s in dc.SavedResumeSearchEntities
                                        join i in dc.SplitGuids(SplitList<Guid>.Delimiter, searcherIds) on s.ownerId equals i.value
                                        join a in dc.SavedResumeSearchAlertEntities on s.id equals a.savedResumeSearchId
                                        group s by s.ownerId into alerts
                                                                 select new Tuple<Guid, int>(alerts.Key, alerts.Count())));

        public MemberSearchReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        int IMemberSearchReportsRepository.GetAllMemberSearches(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetAllMemberSearches(dc, timeRange);
            }
        }

        int IMemberSearchReportsRepository.GetMemberSearches(Channel channel, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetMemberSearches(dc, channel.Id, timeRange);
            }
        }

        int IMemberSearchReportsRepository.GetFilterMemberSearches(Channel channel, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetContextMemberSearches(dc, "Filter", channel.Id, timeRange);
            }
        }

        int IMemberSearchReportsRepository.GetSavedMemberSearches(Channel channel, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetContextMemberSearches(dc, "Saved", channel.Id, timeRange);
            }
        }

        int IMemberSearchReportsRepository.GetAnonymousMemberSearches(Channel channel, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetAnonymousMemberSearches(dc, channel.Id, timeRange);
            }
        }

        int IMemberSearchReportsRepository.GetMemberSearches(Guid searcherId, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetMemberSearchesBySearcherId(dc, searcherId, timeRange);
            }
        }

        IDictionary<Guid, int> IMemberSearchReportsRepository.GetMemberSearches(IEnumerable<Guid> searcherIds, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                var searches = GetAllFilteredMemberSearches(dc, new SplitList<Guid>(searcherIds).ToString(), timeRange).ToDictionary(s => s.Item1, s => s.Item2);
                return (from s in searcherIds select new { Id = s, Count = searches.ContainsKey(s) ? searches[s] : 0 }).ToDictionary(s => s.Id, s => s.Count);
            }
        }

        int IMemberSearchReportsRepository.GetMemberSearchAlerts()
        {
            using (var dc = CreateDataContext(true))
            {
                return (from a in dc.SavedResumeSearchAlertEntities
                        join s in dc.SavedResumeSearchEntities on a.savedResumeSearchId equals s.id
                        join u in GetEnabledUsers(dc) on s.ownerId equals u.id
                        select a).Count();
            }
        }

        int IMemberSearchReportsRepository.GetMemberSearchAlerts(Guid searcherId)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetMemberSearchesAlerts(dc, searcherId);
            }
        }

        IDictionary<Guid, int> IMemberSearchReportsRepository.GetMemberSearchAlerts(IEnumerable<Guid> searcherIds)
        {
            using (var dc = CreateDataContext(true))
            {
                var alerts = GetAllMemberSearchesAlerts(dc, new SplitList<Guid>(searcherIds).ToString()).ToDictionary(a => a.Item1, a => a.Item2);
                return (from s in searcherIds select new { SearcherId = s, Count = alerts.ContainsKey(s) ? alerts[s] : 0 }).ToDictionary(s => s.SearcherId, s => s.Count);
            }
        }

        private static IEnumerable<RegisteredUserEntity> GetEnabledUsers(SearchDataContext dc)
        {
            return from u in dc.RegisteredUserEntities
                   where (u.flags & (int)UserFlags.Disabled) == 0
                   select u;
        }

        protected override SearchDataContext CreateDataContext(IDbConnection connection)
        {
            return new SearchDataContext(connection);
        }
    }
}