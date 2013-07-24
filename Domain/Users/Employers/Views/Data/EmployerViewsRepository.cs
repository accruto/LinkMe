using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Users.Employers.Views.Data
{
    public class EmployerViewsRepository
        : Repository, IEmployerViewsRepository
    {
        private struct AccessCountsCriteria
        {
            public Guid EmployerId;
            public DateTime StartTime1;
            public DateTime EndTime1;
            public DateTime StartTime2;
            public DateTime EndTime2;
        }

        private static readonly Func<ViewsDataContext, Guid, IQueryable<Guid>> GetViewedMemberIds
            = CompiledQuery.Compile((ViewsDataContext dc, Guid employerId)
                => (from v in dc.MemberViewingEntities
                    where v.employerId == employerId
                    select v.memberId).Distinct());

        private static readonly Func<ViewsDataContext, Guid, string, IQueryable<Guid>> GetFilteredViewedMemberIds
            = CompiledQuery.Compile((ViewsDataContext dc, Guid employerId, string memberIds)
                => (from v in dc.MemberViewingEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, memberIds) on v.memberId equals i.value
                    where v.employerId == employerId
                    select v.memberId).Distinct());

        private static readonly Func<ViewsDataContext, Guid, Guid, bool> HasViewedMember
            = CompiledQuery.Compile((ViewsDataContext dc, Guid employerId, Guid memberId)
                => (from v in dc.MemberViewingEntities
                    where v.employerId == employerId
                    && v.memberId == memberId
                    select v).Any());

        private static readonly Func<ViewsDataContext, Guid, Guid, IQueryable<MemberViewing>> GetMemberViewings
            = CompiledQuery.Compile((ViewsDataContext dc, Guid employerId, Guid memberId)
                => from v in dc.MemberViewingEntities
                   where v.employerId == employerId
                   && v.memberId == memberId
                   select v.Map());

        private static readonly Func<ViewsDataContext, Guid, IQueryable<Guid>> GetAccessedMemberIds
            = CompiledQuery.Compile((ViewsDataContext dc, Guid employerId)
                => (from v in dc.MemberContactEntities
                    where v.employerId == employerId
                    select v.memberId).Distinct());

        private static readonly Func<ViewsDataContext, Guid, Guid, bool> HasAccessedMember
            = CompiledQuery.Compile((ViewsDataContext dc, Guid employerId, Guid memberId)
                => (from v in dc.MemberContactEntities
                    where v.employerId == employerId
                    && v.memberId == memberId
                    select v).Any());

        private static readonly Func<ViewsDataContext, Guid, Guid, IQueryable<MemberAccess>> GetMemberAccesses
            = CompiledQuery.Compile((ViewsDataContext dc, Guid employerId, Guid memberId)
                => from c in dc.MemberContactEntities
                   where c.employerId == employerId
                   && c.memberId == memberId
                   select c.Map());

        private static readonly Func<ViewsDataContext, Guid, string, IQueryable<Guid>> GetFilteredAccessedMemberIds
            = CompiledQuery.Compile((ViewsDataContext dc, Guid employerId, string memberIds)
                => (from v in dc.MemberContactEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, memberIds) on v.memberId equals i.value
                    where v.employerId == employerId
                    select v.memberId).Distinct());

        private static readonly Func<ViewsDataContext, AccessCountsCriteria, Guid, string, Tuple<int, int>> GetMemberAccessCounts
            = CompiledQuery.Compile((ViewsDataContext dc, AccessCountsCriteria criteria, Guid excludeMemberId, string reasons)
                => (from x in
                        (from c in dc.MemberContactEntities
                         join r in dc.SplitInts(SplitList<int>.Delimiter, reasons) on c.reason equals r.value
                         where c.employerId == criteria.EmployerId
                         && c.memberId != excludeMemberId
                         && !c.partOfBulk
                         select c)
                    group x by x.employerId into y
                    select new Tuple<int, int>(
                        (from u in y where u.time >= criteria.StartTime1 && u.time < criteria.EndTime1 select u.memberId).Distinct().Count(),
                        (from u in y where u.time >= criteria.StartTime2 && u.time < criteria.EndTime2 select u.memberId).Distinct().Count())).SingleOrDefault());

        private static readonly Func<ViewsDataContext, AccessCountsCriteria, string, string, Tuple<int, int>> GetBulkMemberAccessCounts
            = CompiledQuery.Compile((ViewsDataContext dc, AccessCountsCriteria criteria, string excludeMemberIds, string reasons)
                => (from x in (from c in dc.MemberContactEntities
                               join r in dc.SplitInts(SplitList<int>.Delimiter, reasons) on c.reason equals r.value
                               where c.employerId == criteria.EmployerId
                               && !(from i in dc.SplitGuids(SplitList<Guid>.Delimiter, excludeMemberIds) where i.value == c.memberId select c).Any()
                               && c.partOfBulk
                               select c)
                    group x by x.employerId into y
                    select new Tuple<int, int>(
                        (from u in y where u.time >= criteria.StartTime1 && u.time < criteria.EndTime1 select u.memberId).Distinct().Count(),
                        (from u in y where u.time >= criteria.StartTime2 && u.time < criteria.EndTime2 select u.memberId).Distinct().Count())).SingleOrDefault());

        public EmployerViewsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IEmployerViewsRepository.CreateMemberViewing(MemberViewing viewing)
        {
            using (var dc = CreateContext())
            {
                dc.MemberViewingEntities.InsertOnSubmit(viewing.Map());
                dc.SubmitChanges();
            }
        }

        bool IEmployerViewsRepository.HasViewedMember(Guid employerId, Guid memberId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return HasViewedMember(dc, employerId, memberId);
            }
        }

        IList<Guid> IEmployerViewsRepository.GetViewedMemberIds(Guid employerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetViewedMemberIds(dc, employerId).ToList();
            }
        }

        IList<Guid> IEmployerViewsRepository.GetViewedMemberIds(Guid employerId, IEnumerable<Guid> memberIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredViewedMemberIds(dc, employerId, new SplitList<Guid>(memberIds).ToString()).ToList();
            }
        }

        IList<MemberViewing> IEmployerViewsRepository.GetMemberViewings(Guid employerId, Guid memberId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberViewings(dc, employerId, memberId).ToList();
            }
        }

        void IEmployerViewsRepository.CreateMemberAccess(MemberAccess access)
        {
            using (var dc = CreateContext())
            {
                dc.MemberContactEntities.InsertOnSubmit(access.Map(false));
                dc.SubmitChanges();
            }
        }

        void IEmployerViewsRepository.CreateMemberAccesses(IEnumerable<MemberAccess> accesses)
        {
            using (var dc = CreateContext())
            {
                dc.MemberContactEntities.InsertAllOnSubmit(from a in accesses select a.Map(true));
                dc.SubmitChanges();
            }
        }

        bool IEmployerViewsRepository.HasAccessedMember(Guid employerId, Guid memberId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return HasAccessedMember(dc, employerId, memberId);
            }
        }

        IList<Guid> IEmployerViewsRepository.GetAccessedMemberIds(Guid employerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetAccessedMemberIds(dc, employerId).ToList();
            }
        }

        IList<Guid> IEmployerViewsRepository.GetAccessedMemberIds(Guid employerId, IEnumerable<Guid> memberIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredAccessedMemberIds(dc, employerId, new SplitList<Guid>(memberIds).ToString()).ToList();
            }
        }

        IList<MemberAccess> IEmployerViewsRepository.GetMemberAccesses(Guid employerId, Guid memberId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetMemberAccesses(dc, employerId, memberId).ToList();
            }
        }

        Tuple<int, int> IEmployerViewsRepository.GetMemberAccessCounts(Guid employerId, Guid excludeMemberId, IEnumerable<MemberAccessReason> reasons)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var today = DateTime.Today;
                var criteria = new AccessCountsCriteria
                {
                    EmployerId = employerId,
                    StartTime1 = today,
                    EndTime1 = today.AddDays(1),
                    StartTime2 = today.AddDays(-1 * (int)today.DayOfWeek),
                    EndTime2 = today.AddDays(1)
                };
                return GetMemberAccessCounts(dc, criteria, excludeMemberId, new SplitList<int>(from r in reasons select (int) r).ToString())
                    ?? new Tuple<int, int>(0, 0);
            }
        }

        Tuple<int, int> IEmployerViewsRepository.GetMemberAccessCounts(Guid employerId, IEnumerable<Guid> excludeMemberIds, IEnumerable<MemberAccessReason> reasons)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var today = DateTime.Today;
                var criteria = new AccessCountsCriteria
                {
                    EmployerId = employerId,
                    StartTime1 = today,
                    EndTime1 = today.AddDays(1),
                    StartTime2 = today.AddDays(-1 * (int)today.DayOfWeek),
                    EndTime2 = today.AddDays(1)
                };
                return GetBulkMemberAccessCounts(dc, criteria, new SplitList<Guid>(excludeMemberIds).ToString(), new SplitList<int>(from r in reasons select (int) r).ToString())
                       ?? new Tuple<int, int>(0, 0);
            }
        }

        private ViewsDataContext CreateContext()
        {
            return CreateContext(c => new ViewsDataContext(c));
        }
    }
}