using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Data;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Users.Employers.Data
{
    public class EmployerMemberAccessReportsRepository
        : ReportsRepository<EmployersDataContext>, IEmployerMemberAccessReportsRepository
    {
        private static readonly Func<EmployersDataContext, Guid, DateTimeRange, int> GetMemberViewings
            = CompiledQuery.Compile((EmployersDataContext dc, Guid channelId, DateTimeRange timeRange)
                => (from v in dc.MemberViewingEntities
                    where v.time >= timeRange.Start && v.time < timeRange.End
                    &&
                    (
                        Equals(v.employerId, null)
                        ||
                        (from e in dc.EmployerEntities
                         join u in dc.RegisteredUserEntities on e.id equals u.id
                         where e.id == v.employerId
                         && !SqlMethods.Like(u.emailAddress, "%linkme.com.au")
                         select e).Any()
                    )
                    && v.channelId == channelId
                    select v).Count());

        private static readonly Func<EmployersDataContext, Guid, DateTimeRange, int> GetDistinctMemberViewings
            = CompiledQuery.Compile((EmployersDataContext dc, Guid channelId, DateTimeRange timeRange)
                                    => (from v in dc.MemberViewingEntities
                                        join e in dc.EmployerEntities on v.employerId equals e.id
                                        join u in dc.RegisteredUserEntities on e.id equals u.id
                                        where v.time >= timeRange.Start && v.time < timeRange.End
                                              && !Equals(v.employerId, null)
                                              && !SqlMethods.Like(u.emailAddress, "%linkme.com.au")
                                              && v.channelId == channelId
                                        select new {v.employerId, v.memberId}).Distinct().Count());

        private static readonly Func<EmployersDataContext, Guid, DateTimeRange, int> GetAnonymousMemberViewings
            = CompiledQuery.Compile((EmployersDataContext dc, Guid channelId, DateTimeRange timeRange)
                                    => (from v in dc.MemberViewingEntities
                                        where v.time >= timeRange.Start && v.time < timeRange.End
                                              && Equals(v.employerId, null)
                                              && v.channelId == channelId
                                        select v).Count());

        private static readonly Func<EmployersDataContext, Guid, DateTimeRange, int> GetMemberViewsByEmployer
            = CompiledQuery.Compile((EmployersDataContext dc, Guid employerId, DateTimeRange timeRange)
                                    => (from v in dc.MemberViewingEntities
                                        where v.employerId == employerId
                                              && v.time >= timeRange.Start && v.time < timeRange.End
                                        select v).Count());

        private static readonly Func<EmployersDataContext, string, DateTimeRange, IQueryable<Tuple<Guid, int>>> GetMemberViewsByEmployers
            = CompiledQuery.Compile((EmployersDataContext dc, string employerIds, DateTimeRange timeRange)
                                    => (from v in dc.MemberViewingEntities
                                        join i in dc.SplitGuids(SplitList<Guid>.Delimiter, employerIds) on v.employerId equals i.value
                                        where v.time >= timeRange.Start && v.time < timeRange.End
                                        group v by v.employerId into views
                                                                    select new Tuple<Guid, int>(views.Key.Value, views.Count())));

        private static readonly Func<EmployersDataContext, DateTimeRange, int> GetMemberAccesses
            = CompiledQuery.Compile((EmployersDataContext dc, DateTimeRange timeRange)
                                    => (from c in dc.MemberContactEntities
                                        where c.time >= timeRange.Start && c.time < timeRange.End
                                              &&
                                              (
                                                  Equals(c.employerId, null)
                                                  ||
                                                  (
                                                      from e in dc.EmployerEntities
                                                      join u in dc.RegisteredUserEntities on e.id equals u.id
                                                      where e.id == c.employerId
                                                            && !SqlMethods.Like(u.emailAddress, "%linkme.com.au")
                                                      select e
                                                  ).Any()
                                              )
                                        select c).Count());

        private static readonly Func<EmployersDataContext, Guid, DateTimeRange, int> GetChannelMemberAccesses
            = CompiledQuery.Compile((EmployersDataContext dc, Guid channelId, DateTimeRange timeRange)
                                    => (from c in dc.MemberContactEntities
                                        where c.time >= timeRange.Start && c.time < timeRange.End
                                              &&
                                              (
                                                  Equals(c.employerId, null)
                                                  ||
                                                  (
                                                      from e in dc.EmployerEntities
                                                      join u in dc.RegisteredUserEntities on e.id equals u.id
                                                      where e.id == c.employerId
                                                            && !SqlMethods.Like(u.emailAddress, "%linkme.com.au")
                                                      select e
                                                  ).Any()
                                              )
                                              && c.channelId == channelId
                                        select c).Count());

        private static readonly Func<EmployersDataContext, Guid, DateTimeRange, int> GetDistinctMemberAccesses
            = CompiledQuery.Compile((EmployersDataContext dc, Guid channelId, DateTimeRange timeRange)
                                    => (from c in dc.MemberContactEntities
                                        join e in dc.EmployerEntities on c.employerId equals e.id
                                        join u in dc.RegisteredUserEntities on e.id equals u.id
                                        where c.time >= timeRange.Start && c.time < timeRange.End
                                              && !Equals(c.employerId, null)
                                              && !SqlMethods.Like(u.emailAddress, "%linkme.com.au")
                                              && c.channelId == channelId
                                        select new {c.employerId, c.memberId}).Distinct().Count());

        private static readonly Func<EmployersDataContext, MemberAccessReason, Guid, DateTimeRange, int> GetMemberAccessesByReason
            = CompiledQuery.Compile((EmployersDataContext dc, MemberAccessReason reason, Guid channelId, DateTimeRange timeRange)
                                    => (from c in dc.MemberContactEntities
                                        where c.reason == (int)reason
                                              && c.time >= timeRange.Start && c.time < timeRange.End
                                              &&
                                              (
                                                  Equals(c.employerId, null)
                                                  ||
                                                  (
                                                      from e in dc.EmployerEntities
                                                      join u in dc.RegisteredUserEntities on e.id equals u.id
                                                      where e.id == c.employerId
                                                            && !SqlMethods.Like(u.emailAddress, "%linkme.com.au")
                                                      select e
                                                  ).Any()
                                              )
                                              && c.channelId == channelId
                                        select c).Count());

        private static readonly Func<EmployersDataContext, Guid, MemberAccessReason, DateTimeRange, int> GetMemberAccessesByEmployer
            = CompiledQuery.Compile((EmployersDataContext dc, Guid employerId, MemberAccessReason reason, DateTimeRange timeRange)
                                    => (from c in dc.MemberContactEntities
                                        where c.employerId == employerId
                                              && c.reason == (int)reason
                                              && c.time >= timeRange.Start && c.time < timeRange.End
                                        select c).Count());

        private static readonly Func<EmployersDataContext, string, MemberAccessReason, DateTimeRange, IQueryable<Tuple<Guid, int>>> GetAllMemberUnlockingsByEmployer
            = CompiledQuery.Compile((EmployersDataContext dc, string employerIds, MemberAccessReason reason, DateTimeRange timeRange)
                                    => (from c in dc.MemberContactEntities
                                        join i in dc.SplitGuids(SplitList<Guid>.Delimiter, employerIds) on c.employerId equals i.value
                                        where c.reason == (int)reason
                                              && c.time >= timeRange.Start && c.time < timeRange.End
                                        group c by c.employerId into contacts
                                                                    select new Tuple<Guid, int>(contacts.Key, contacts.Count())));

        private static readonly Func<EmployersDataContext, Guid, long?> GetMemberViewsByMember
            = CompiledQuery.Compile((EmployersDataContext dc, Guid memberId)
                                    => (from v in dc.MemberViewingsByMemberEntities
                                        where v.memberId == memberId
                                        select v.views).SingleOrDefault());

        private static readonly Func<EmployersDataContext, Guid, DateTimeRange, int> GetMemberViewsByMemberAndDateRange
            = CompiledQuery.Compile((EmployersDataContext dc, Guid memberId, DateTimeRange timeRange)
                                    => (from v in dc.MemberViewingEntities
                                        where v.memberId == memberId
                                              && v.time >= timeRange.Start && v.time < timeRange.End
                                        select v).Count());

        private static readonly Func<EmployersDataContext, Guid, long?> GetMemberAccessesByMember
            = CompiledQuery.Compile((EmployersDataContext dc, Guid memberId)
                                    => (from c in dc.MemberContactsByMemberEntities
                                        where c.memberId == memberId
                                        select c.contacts).SingleOrDefault());

        private static readonly Func<EmployersDataContext, Guid, MemberAccessReason, long?> GetMemberAccessesByMemberAndReason
            = CompiledQuery.Compile((EmployersDataContext dc, Guid memberId, MemberAccessReason reason)
                                    => (from c in dc.MemberContactsByMemberEntities
                                        where c.memberId == memberId
                                              && c.reason == (int)reason
                                        select c.contacts).SingleOrDefault());

        private static readonly Func<EmployersDataContext, Guid, int, int> GetMemberSearchResults
            = CompiledQuery.Compile((EmployersDataContext dc, Guid memberId, int rank)
                                    => (from r in dc.ResumeSearchResultEntities
                                        where r.rank <= rank
                                              && r.resumeId == memberId
                                        select r).Count());

        public EmployerMemberAccessReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        int IEmployerMemberAccessReportsRepository.GetMemberViewings(Channel channel, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetMemberViewings(dc, channel.Id, timeRange);
            }
        }

        int IEmployerMemberAccessReportsRepository.GetDistinctMemberViewings(Channel channel, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetDistinctMemberViewings(dc, channel.Id, timeRange);
            }
        }

        int IEmployerMemberAccessReportsRepository.GetAnonymousMemberViewings(Channel channel, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetAnonymousMemberViewings(dc, channel.Id, timeRange);
            }
        }

        int IEmployerMemberAccessReportsRepository.GetEmployerViewings(Guid employerId, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetMemberViewsByEmployer(dc, employerId, timeRange);
            }
        }

        IDictionary<Guid, int> IEmployerMemberAccessReportsRepository.GetEmployerViewings(IEnumerable<Guid> employerIds, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                var views = GetMemberViewsByEmployers(dc, new SplitList<Guid>(employerIds).ToString(), timeRange).ToDictionary(v => v.Item1, v => v.Item2);
                return (from e in employerIds select new { EmployerId = e, Count = views.ContainsKey(e) ? views[e] : 0 }).ToDictionary(e => e.EmployerId, e => e.Count);
            }
        }

        int IEmployerMemberAccessReportsRepository.GetMemberAccesses(DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetMemberAccesses(dc, timeRange);
            }
        }

        int IEmployerMemberAccessReportsRepository.GetMemberAccesses(Channel channel, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetChannelMemberAccesses(dc, channel.Id, timeRange);
            }
        }

        int IEmployerMemberAccessReportsRepository.GetDistinctMemberAccesses(Channel channel, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetDistinctMemberAccesses(dc, channel.Id, timeRange);
            }
        }

        int IEmployerMemberAccessReportsRepository.GetMemberAccesses(MemberAccessReason reason, Channel channel, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetMemberAccessesByReason(dc, reason, channel.Id, timeRange);
            }
        }

        int IEmployerMemberAccessReportsRepository.GetEmployerAccesses(Guid employerId, MemberAccessReason reason, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetMemberAccessesByEmployer(dc, employerId, reason, timeRange);
            }
        }

        IDictionary<Guid, int> IEmployerMemberAccessReportsRepository.GetEmployerAccesses(IEnumerable<Guid> employerIds, MemberAccessReason reason, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                var contacts = GetAllMemberUnlockingsByEmployer(dc, new SplitList<Guid>(employerIds).ToString(), reason, timeRange).ToDictionary(c => c.Item1, c => c.Item2);
                return (from e in employerIds select new { EmployerId = e, Count = contacts.ContainsKey(e) ? contacts[e] : 0 }).ToDictionary(e => e.EmployerId, e => e.Count);
            }
        }

        int IEmployerMemberAccessReportsRepository.GetMemberViewings(Guid memberId, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                if (timeRange == null)
                {
                    var count = GetMemberViewsByMember(dc, memberId);
                    return count == null ? 0 : (int) count.Value;
                }
                
                return GetMemberViewsByMemberAndDateRange(dc, memberId, timeRange);
            }
        }

        int IEmployerMemberAccessReportsRepository.GetMemberAccesses(Guid memberId)
        {
            using (var dc = CreateDataContext(true))
            {
                var count = GetMemberAccessesByMember(dc, memberId);
                return count == null ? 0 : (int)count.Value;
            }
        }

        int IEmployerMemberAccessReportsRepository.GetMemberAccesses(Guid memberId, MemberAccessReason reason)
        {
            using (var dc = CreateDataContext(true))
            {
                var count = GetMemberAccessesByMemberAndReason(dc, memberId, reason);
                return count == null ? 0 : (int)count.Value;
            }
        }

        int IEmployerMemberAccessReportsRepository.GetMemberSearchResults(Guid memberId, int rank)
        {
            using (var dc = CreateDataContext(true))
            {
                return GetMemberSearchResults(dc, memberId, rank);
            }
        }

        protected override EmployersDataContext CreateDataContext(IDbConnection connection)
        {
            return new EmployersDataContext(connection);
        }
    }
}