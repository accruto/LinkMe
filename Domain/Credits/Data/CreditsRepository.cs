using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Domain.Credits.Data
{
    public class CreditsRepository
        : Repository, ICreditsRepository
    {
        private class ExerciseCriteria
        {
            public Guid CreditId;
            public string CreditIds;
            public Guid OwnerId;
            public string OwnerIds;
            public Guid ExercisedOnId;
            public string ExercisedOnIds;
        }

        private static readonly Func<CreditsDataContext, IQueryable<Credit>> GetCredits
            = CompiledQuery.Compile((CreditsDataContext dc)
                => from d in dc.CreditEntities
                   orderby d.displayName
                   select d.Map());

        private static readonly Func<CreditsDataContext, Guid, CreditAllocationEntity> GetAllocationEntity
            = CompiledQuery.Compile((CreditsDataContext dc, Guid id)
                => (from p in dc.CreditAllocationEntities
                    where p.id == id
                    select p).SingleOrDefault());

        private static readonly Func<CreditsDataContext, Guid, Allocation> GetAllocation
            = CompiledQuery.Compile((CreditsDataContext dc, Guid id)
                => (from p in dc.CreditAllocationEntities
                    where p.id == id
                    select p.Map()).SingleOrDefault());

        private static readonly Func<CreditsDataContext, string, IQueryable<Allocation>> GetAllAllocations
            = CompiledQuery.Compile((CreditsDataContext dc, string ids)
                => from p in dc.CreditAllocationEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on p.id equals i.value
                   select p.Map());

        private static readonly Func<CreditsDataContext, Guid, IQueryable<Allocation>> GetAllocationsByOwnerId
            = CompiledQuery.Compile((CreditsDataContext dc, Guid ownerId)
                => from p in dc.CreditAllocationEntities
                   where p.ownerId == ownerId
                   orderby p.expiryDate
                   select p.Map());

        private static readonly Func<CreditsDataContext, Guid, Guid, IQueryable<Allocation>> GetCreditAllocationsByOwnerId
            = CompiledQuery.Compile((CreditsDataContext dc, Guid ownerId, Guid creditId)
                => from p in dc.CreditAllocationEntities
                   where p.ownerId == ownerId
                   && p.creditId == creditId
                   orderby p.expiryDate
                   select p.Map());

        private static readonly Func<CreditsDataContext, string, IQueryable<Tuple<Guid, Allocation>>> GetAllAllocationsByOwnerId
            = CompiledQuery.Compile((CreditsDataContext dc, string ownerIds)
                => from a in dc.CreditAllocationEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ownerIds) on a.ownerId equals i.value
                   orderby a.expiryDate
                   select new Tuple<Guid, Allocation>(a.ownerId, a.Map()));

        private static readonly Func<CreditsDataContext, Guid, IQueryable<Allocation>> GetAllocationsByReferenceId
            = CompiledQuery.Compile((CreditsDataContext dc, Guid referenceId)
                => from p in dc.CreditAllocationEntities
                   where p.referenceId == referenceId
                   orderby p.expiryDate
                   select p.Map());

        private static readonly Func<CreditsDataContext, Guid, DateTime, IQueryable<Allocation>> GetActiveAllocations
            = CompiledQuery.Compile((CreditsDataContext dc, Guid ownerId, DateTime expiryDate)
                => from a in dc.CreditAllocationEntities
                   where a.ownerId == ownerId
                   && (a.expiryDate == null || a.expiryDate.Value >= expiryDate)
                   && a.deallocatedTime == null
                   orderby a.expiryDate
                   select a.Map());

        private static readonly Func<CreditsDataContext, string, string, DateTime, IQueryable<Tuple<Guid, Allocation>>> GetAllActiveAllCreditAllocations
            = CompiledQuery.Compile((CreditsDataContext dc, string ownerIds, string creditIds, DateTime expiryDate)
                => from a in dc.CreditAllocationEntities
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ownerIds) on a.ownerId equals i.value
                   join c in dc.SplitGuids(SplitList<Guid>.Delimiter, creditIds) on a.creditId equals c.value
                   where (a.expiryDate == null || a.expiryDate.Value >= expiryDate)
                   && a.deallocatedTime == null
                   orderby a.expiryDate
                   select new Tuple<Guid, Allocation>(a.ownerId, a.Map()));

        private static readonly Func<CreditsDataContext, Guid, Guid, DateTime, IQueryable<Allocation>> GetActiveCreditAllocations
            = CompiledQuery.Compile((CreditsDataContext dc, Guid ownerId, Guid creditId, DateTime expiryDate)
                => from a in dc.CreditAllocationEntities
                   where a.ownerId == ownerId
                   && a.creditId == creditId
                   && (a.expiryDate == null || a.expiryDate.Value >= expiryDate)
                   && a.deallocatedTime == null
                   orderby a.expiryDate
                   select a.Map());

        private static readonly Func<CreditsDataContext, Guid, DateTime, IQueryable<Tuple<Guid, Allocation>>> GetExpiringAllocations
            = CompiledQuery.Compile((CreditsDataContext dc, Guid creditId, DateTime expiryDate)
                => from a in dc.CreditAllocationEntities
                   where (from q in dc.CreditAllocationEntities
                          where q.ownerId == a.ownerId
                          select q.expiryDate ?? DateTime.MaxValue).Max() <= expiryDate
                   && a.creditId == creditId
                   && a.expiryDate != null && a.expiryDate.Value > DateTime.Now
                   select new Tuple<Guid, Allocation>(a.ownerId, a.Map()));

        private static readonly Func<CreditsDataContext, ExerciseCriteria, bool> HasExercisedCredit
            = CompiledQuery.Compile((CreditsDataContext dc, ExerciseCriteria criteria)
                => (from p in dc.CandidateAccessPurchaseEntities
                    where p.candidateId == criteria.ExercisedOnId
                    &&
                    (
                        (
                            // Supports old contacts before allocations were introduced.

                            p.allocationId == null
                            && p.searcherId == criteria.OwnerId
                            && (from c in dc.CreditEntities where c.id == criteria.CreditId && c.name == "ContactCredit" select c).Any()
                        )
                        ||
                        (
                            // Supports new contacts after allocations were introduced.

                            (from a in dc.CreditAllocationEntities
                             join o in dc.SplitGuids(SplitList<Guid>.Delimiter, criteria.OwnerIds) on a.ownerId equals o.value
                             where a.id == p.allocationId && a.creditId == criteria.CreditId
                             select a).Any()
                        )
                    )
                    select p).Any());

        private static readonly Func<CreditsDataContext, Guid, ExercisedCredit> GetExercisedCredit
            = CompiledQuery.Compile((CreditsDataContext dc, Guid id)
                => (from p in dc.CandidateAccessPurchaseEntities
                    where p.id == id
                    orderby p.purchaseTime descending
                    let creditId = p.allocationId == null
                        ? (from c in dc.CreditEntities where c.name == "ContactCredit" select c.id).Single()
                        : (from a in dc.CreditAllocationEntities where a.id == p.allocationId.Value select a.creditId).Single()
                    select p.Map(creditId)).SingleOrDefault());

        private static readonly Func<CreditsDataContext, Guid, IQueryable<ExercisedCredit>> GetExercisedCreditsByAllocationId
            = CompiledQuery.Compile((CreditsDataContext dc, Guid allocationId)
                => from p in dc.CandidateAccessPurchaseEntities
                   join a in dc.CreditAllocationEntities on p.allocationId equals a.id
                   where a.id == allocationId
                   orderby p.purchaseTime descending
                   select p.Map(a.creditId));

        private static readonly Func<CreditsDataContext, ExerciseCriteria, IQueryable<ExercisedCredit>> GetExercisedCreditsByCriteria
            = CompiledQuery.Compile((CreditsDataContext dc, ExerciseCriteria criteria)
                => from p in dc.CandidateAccessPurchaseEntities
                   where
                   (
                        p.allocationId == null
                        && p.searcherId == criteria.OwnerId
                        && (from c in dc.CreditEntities where c.id == criteria.CreditId && c.name == "ContactCredit" select c).Any()
                   )
                   ||
                   (
                        (from a in dc.CreditAllocationEntities
                         join o in dc.SplitGuids(SplitList<Guid>.Delimiter, criteria.OwnerIds) on a.ownerId equals o.value
                         where a.id == p.allocationId && a.creditId == criteria.CreditId
                         select a).Any()
                   )
                   orderby p.purchaseTime descending
                   select p.Map(criteria.CreditId));

        private static readonly Func<CreditsDataContext, Guid, DateTimeRange, IQueryable<ExercisedCredit>> GetExercisedCreditsByExerciserId
            = CompiledQuery.Compile((CreditsDataContext dc, Guid exercisedById, DateTimeRange timeRange)
                => from p in dc.CandidateAccessPurchaseEntities
                   where p.searcherId == exercisedById
                   && p.purchaseTime >= timeRange.Start && p.purchaseTime < timeRange.End
                   let creditId = p.allocationId == null
                       ? (from c in dc.CreditEntities where c.name == "ContactCredit" select c.id).Single()
                       : (from a in dc.CreditAllocationEntities where a.id == p.allocationId select a.creditId).Single()
                   orderby p.purchaseTime descending
                   select p.Map(creditId));

        private static readonly Func<CreditsDataContext, Guid, DateTimeRange, IQueryable<ExercisedCredit>> GetExercisedCreditsByOwnerId
            = CompiledQuery.Compile((CreditsDataContext dc, Guid ownerId, DateTimeRange timeRange)
                => from p in dc.CandidateAccessPurchaseEntities
                   join a in dc.CreditAllocationEntities on p.allocationId equals a.id
                   where a.ownerId == ownerId
                   && p.purchaseTime >= timeRange.Start && p.purchaseTime < timeRange.End
                   orderby p.purchaseTime descending
                   select p.Map(a.creditId));

        private static readonly Func<CreditsDataContext, ExerciseCriteria, IQueryable<Guid>> HasExercisedCredits
            = CompiledQuery.Compile((CreditsDataContext dc, ExerciseCriteria criteria)
                => (from p in dc.CandidateAccessPurchaseEntities
                    where
                    (
                        p.allocationId == null
                        && p.searcherId == criteria.OwnerId
                        && (from c in dc.CreditEntities
                            join i in dc.SplitGuids(SplitList<Guid>.Delimiter, criteria.CreditIds) on c.id equals i.value
                            where c.name == "ContactCredit"
                            select c).Any()
                    )
                    ||
                    (
                        (from a in dc.CreditAllocationEntities
                         join o in dc.SplitGuids(SplitList<Guid>.Delimiter, criteria.OwnerIds) on a.ownerId equals o.value
                         join i in dc.SplitGuids(SplitList<Guid>.Delimiter, criteria.CreditIds) on a.creditId equals i.value
                         where a.id == p.allocationId
                         select a).Any()
                    )
                    select p.candidateId.Value).Distinct());

        private static readonly Func<CreditsDataContext, ExerciseCriteria, IQueryable<Guid>> HasFilteredExercisedCredits
            = CompiledQuery.Compile((CreditsDataContext dc, ExerciseCriteria criteria)
                => (from p in dc.CandidateAccessPurchaseEntities
                    join r in dc.SplitGuids(SplitList<Guid>.Delimiter, criteria.ExercisedOnIds) on p.candidateId equals r.value
                    where
                    (
                        p.allocationId == null
                        && p.searcherId == criteria.OwnerId
                        && (from c in dc.CreditEntities
                            join i in dc.SplitGuids(SplitList<Guid>.Delimiter, criteria.CreditIds) on c.id equals i.value
                            where c.name == "ContactCredit" select c).Any()
                    )
                    ||
                    (
                        (from a in dc.CreditAllocationEntities
                         join o in dc.SplitGuids(SplitList<Guid>.Delimiter, criteria.OwnerIds) on a.ownerId equals o.value
                         join i in dc.SplitGuids(SplitList<Guid>.Delimiter, criteria.CreditIds) on a.creditId equals i.value
                         where a.id == p.allocationId
                         select a).Any()
                    )
                    select p.candidateId.Value).Distinct());

        private static readonly Func<CreditsDataContext, ExerciseCriteria, IQueryable<ExercisedCredit>> GetFilteredExercisedCredits
            = CompiledQuery.Compile((CreditsDataContext dc, ExerciseCriteria criteria)
                => from p in dc.CandidateAccessPurchaseEntities
                   join r in dc.SplitGuids(SplitList<Guid>.Delimiter, criteria.ExercisedOnIds) on p.candidateId equals r.value
                   where 
                   (
                        p.allocationId == null
                        && p.searcherId == criteria.OwnerId
                        && (from c in dc.CreditEntities where c.id == criteria.CreditId && c.name == "ContactCredit" select c).Any()
                   )
                   ||
                   (
                        (from a in dc.CreditAllocationEntities
                         join o in dc.SplitGuids(SplitList<Guid>.Delimiter, criteria.OwnerIds) on a.ownerId equals o.value
                         where a.id == p.allocationId && a.creditId == criteria.CreditId
                         select a).Any()
                   )
                   orderby p.purchaseTime descending
                   select p.Map(criteria.CreditId));

        public CreditsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IList<Credit> ICreditsRepository.GetCredits()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from c in GetCredits(dc) where c != null select c).ToList();
            }
        }

        void ICreditsRepository.CreateAllocation(Allocation allocation)
        {
            using (var dc = CreateContext())
            {
                dc.CreditAllocationEntities.InsertOnSubmit(allocation.Map());
                dc.SubmitChanges();
            }
        }

        void ICreditsRepository.UpdateAllocation(Allocation allocation)
        {
            using (var dc = CreateContext())
            {
                var entity = GetAllocationEntity(dc, allocation.Id);
                allocation.MapTo(entity);
                dc.SubmitChanges();
            }
        }

        void ICreditsRepository.UpdateAllocations(IEnumerable<Allocation> allocations)
        {
            using (var dc = CreateContext())
            {
                foreach (var allocation in allocations)
                {
                    var entity = GetAllocationEntity(dc, allocation.Id);
                    allocation.MapTo(entity);
                }
                dc.SubmitChanges();
            }
        }

        Allocation ICreditsRepository.GetAllocation(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetAllocation(dc, id);
            }
        }

        IList<Allocation> ICreditsRepository.GetAllocations(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetAllAllocations(dc, new SplitList<Guid>(ids).ToString()).ToList();
            }
        }

        IList<Allocation> ICreditsRepository.GetAllocationsByOwnerId(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetAllocationsByOwnerId(dc, ownerId).ToList();
            }
        }

        IList<Allocation> ICreditsRepository.GetAllocationsByOwnerId(Guid ownerId, Guid creditId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCreditAllocationsByOwnerId(dc, ownerId, creditId).ToList();
            }
        }

        IDictionary<Guid, IList<Allocation>> ICreditsRepository.GetAllocationsByOwnerId(IEnumerable<Guid> ownerIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var allocations = GetAllAllocationsByOwnerId(dc, new SplitList<Guid>(ownerIds).ToString()).ToList();

                return (from o in ownerIds
                        select new { OwnerId = o, Allocations = from a in allocations where a.Item1 == o select a.Item2 }
                        ).ToDictionary(a => a.OwnerId, a => (IList<Allocation>)a.Allocations.ToList());
            }
        }

        IList<Allocation> ICreditsRepository.GetAllocationsByReferenceId(Guid referenceId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetAllocationsByReferenceId(dc, referenceId).ToList();
            }
        }

        IList<Allocation> ICreditsRepository.GetActiveAllocations(Guid ownerId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetActiveAllocations(dc, ownerId, DateTime.Today).ToList();
            }
        }

        IList<Allocation> ICreditsRepository.GetActiveAllocations(Guid ownerId, Guid creditId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetActiveCreditAllocations(dc, ownerId, creditId, DateTime.Today).ToList();
            }
        }

        IDictionary<Guid, IList<Allocation>> ICreditsRepository.GetActiveAllocations(IEnumerable<Guid> ownerIds, IEnumerable<Guid> creditIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var allocations = GetAllActiveAllCreditAllocations(dc, new SplitList<Guid>(ownerIds).ToString(), new SplitList<Guid>(creditIds).ToString(), DateTime.Today).ToList();

                return (from o in ownerIds
                        select new { OwnerId = o, Allocations = from a in allocations where a.Item1 == o select a.Item2 }
                        ).ToDictionary(a => a.OwnerId, a => (IList<Allocation>)a.Allocations.ToList());
            }
        }

        IDictionary<Guid, IList<Allocation>> ICreditsRepository.GetExpiringAllocations(Guid creditId, DateTime expiryDate)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return (from a in GetExpiringAllocations(dc, creditId, expiryDate)
                        group a by a.Item1).ToDictionary(g => g.Key, g => (IList<Allocation>)(from a in g select a.Item2).ToList());
            }
        }

        void ICreditsRepository.CreateExercisedCredit(ExercisedCredit exercisedCredit)
        {
            using (var dc = CreateContext())
            {
                dc.CandidateAccessPurchaseEntities.InsertOnSubmit(exercisedCredit.Map());
                dc.SubmitChanges();
            }
        }

        void ICreditsRepository.CreateExercisedCredits(IEnumerable<ExercisedCredit> credits)
        {
            using (var dc = CreateContext())
            {
                dc.CandidateAccessPurchaseEntities.InsertAllOnSubmit(from c in credits select c.Map());
                dc.SubmitChanges();
            }
        }

        bool ICreditsRepository.HasExercisedCredit(Guid creditId, HierarchyPath hierarchyPath, Guid exercisedOnId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var criteria = new ExerciseCriteria
                {
                    CreditId = creditId,
                    OwnerId = hierarchyPath.First(),
                    OwnerIds = new SplitList<Guid>(hierarchyPath).ToString(),
                    ExercisedOnId = exercisedOnId
                };
                return HasExercisedCredit(dc, criteria);
            }
        }

        IList<Guid> ICreditsRepository.HasExercisedCredits(Guid[] creditIds, HierarchyPath hierarchyPath)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var criteria = new ExerciseCriteria
                {
                    CreditIds = new SplitList<Guid>(creditIds).ToString(),
                    OwnerId = hierarchyPath.First(),
                    OwnerIds = new SplitList<Guid>(hierarchyPath).ToString(),
                };
                return HasExercisedCredits(dc, criteria).ToList();
            }
        }

        IList<Guid> ICreditsRepository.HasExercisedCredits(Guid[] creditIds, HierarchyPath hierarchyPath, IEnumerable<Guid> exercisedOnIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var criteria = new ExerciseCriteria
                {
                    CreditIds = new SplitList<Guid>(creditIds).ToString(),
                    OwnerId = hierarchyPath.First(),
                    OwnerIds = new SplitList<Guid>(hierarchyPath).ToString(),
                    ExercisedOnIds = new SplitList<Guid>(exercisedOnIds).ToString()
                };
                return HasFilteredExercisedCredits(dc, criteria).ToList();
            }
        }

        ExercisedCredit ICreditsRepository.GetExercisedCredit(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetExercisedCredit(dc, id);
            }
        }

        IList<ExercisedCredit> ICreditsRepository.GetExercisedCredits(Guid creditId, HierarchyPath hierarchyPath)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var criteria = new ExerciseCriteria
                {
                    CreditId = creditId,
                    OwnerId = hierarchyPath.First(),
                    OwnerIds = new SplitList<Guid>(hierarchyPath).ToString(),
                };
                return GetExercisedCreditsByCriteria(dc, criteria).ToList();
            }
        }

        IList<ExercisedCredit> ICreditsRepository.GetExercisedCredits(Guid creditId, HierarchyPath hierarchyPath, IEnumerable<Guid> exercisedOnIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var criteria = new ExerciseCriteria
                {
                    CreditId = creditId,
                    OwnerId = hierarchyPath.First(),
                    OwnerIds = new SplitList<Guid>(hierarchyPath).ToString(),
                    ExercisedOnIds = new SplitList<Guid>(exercisedOnIds).ToString()
                };
                return GetFilteredExercisedCredits(dc, criteria).ToList();
            }
        }

        IList<ExercisedCredit> ICreditsRepository.GetExercisedCredits(Guid allocationId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetExercisedCreditsByAllocationId(dc, allocationId).ToList();
            }
        }

        IList<ExercisedCredit> ICreditsRepository.GetExercisedCreditsByExerciserId(Guid exercisedById, DateTimeRange timeRange)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetExercisedCreditsByExerciserId(dc, exercisedById, timeRange).ToList();
            }
        }

        IList<ExercisedCredit> ICreditsRepository.GetExercisedCreditsByOwnerId(Guid ownerId, DateTimeRange timeRange)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetExercisedCreditsByOwnerId(dc, ownerId, timeRange).ToList();
            }
        }

        private CreditsDataContext CreateContext()
        {
            return CreateContext(c => new CreditsDataContext(c));
        }
    }
}
