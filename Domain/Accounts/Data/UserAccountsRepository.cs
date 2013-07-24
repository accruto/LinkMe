using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Accounts.Data
{
    public class UserAccountsRepository
        : Repository, IUserAccountsRepository
    {
        private static readonly Func<AccountsDataContext, Guid, RegisteredUserEntity> GetRegisteredUserEntity
            = CompiledQuery.Compile((AccountsDataContext dc, Guid userId)
                => (from u in dc.RegisteredUserEntities
                    where u.id == userId
                    select u).SingleOrDefault());

        private static readonly Func<AccountsDataContext, Guid, bool> IsEnabled
            = CompiledQuery.Compile((AccountsDataContext dc, Guid userId)
                => (from u in dc.RegisteredUserEntities
                    where u.id == userId
                    && (u.flags & (int)UserFlags.Disabled) == 0
                    select u).Any());

        private static readonly Func<AccountsDataContext, Guid, bool> IsActive
            = CompiledQuery.Compile((AccountsDataContext dc, Guid userId)
                => (from u in dc.RegisteredUserEntities
                    where u.id == userId
                    && (u.flags & (int)UserFlags.Disabled) == 0
                    && (u.flags & (int)UserFlags.Activated) == (int)UserFlags.Activated
                    select u).Any());

        private static readonly Func<AccountsDataContext, string, IQueryable<Guid>> GetEnabledAccountIds
            = CompiledQuery.Compile((AccountsDataContext dc, string ids)
                => (from u in dc.RegisteredUserEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on u.id equals i.value
                    where (u.flags & (int)UserFlags.Disabled) == 0
                    select u.id));

        private static readonly Func<AccountsDataContext, Guid, DateTime> GetCreatedTime
            = CompiledQuery.Compile((AccountsDataContext dc, Guid userId)
                => (from u in dc.RegisteredUserEntities
                    where u.id == userId
                    select u.createdTime).SingleOrDefault());

        private static readonly Func<AccountsDataContext, DateTimeRange, IQueryable<Guid>> GetCreatedAccountIds
            = CompiledQuery.Compile((AccountsDataContext dc, DateTimeRange dateTimeRange)
                => (from u in dc.RegisteredUserEntities
                    where u.createdTime >= dateTimeRange.Start && u.createdTime < dateTimeRange.End
                    select u.id));

        private static readonly Func<AccountsDataContext, string, DateTimeRange, IQueryable<Guid>> GetFilteredCreatedAccountIds
            = CompiledQuery.Compile((AccountsDataContext dc, string ids, DateTimeRange dateTimeRange)
                => (from u in dc.RegisteredUserEntities
                    join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on u.id equals i.value
                    where u.createdTime >= dateTimeRange.Start && u.createdTime < dateTimeRange.End
                    select u.id));

        private static readonly Func<AccountsDataContext, Guid, IQueryable<UserAccountAction>> GetEnablements
            = CompiledQuery.Compile((AccountsDataContext dc, Guid userId)
                => from e in dc.UserEnablementEntities
                   where e.userId == userId
                   select e.Map());

        private static readonly Func<AccountsDataContext, Guid, IQueryable<UserAccountAction>> GetDisablements
            = CompiledQuery.Compile((AccountsDataContext dc, Guid userId)
                => from e in dc.UserDisablementEntities
                   where e.userId == userId
                   select e.Map());

        private static readonly Func<AccountsDataContext, Guid, IQueryable<UserAccountAction>> GetActivations
            = CompiledQuery.Compile((AccountsDataContext dc, Guid userId)
                => from e in dc.UserActivationEntities
                   where e.userId == userId
                   select e.Map());

        private static readonly Func<AccountsDataContext, Guid, IQueryable<UserAccountAction>> GetDeactivations
            = CompiledQuery.Compile((AccountsDataContext dc, Guid userId)
                => from e in dc.UserDeactivationEntities
                   where e.userId == userId
                   select e.Map());

        public UserAccountsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IUserAccountsRepository.EnableUserAccount(Guid userId, Guid enabledById, DateTime time)
        {
            using (var dc = CreateContext())
            {
                var entity = GetRegisteredUserEntity(dc, userId);
                if (entity != null)
                {
                    entity.flags = (short)((UserFlags)entity.flags).ResetFlag(UserFlags.Disabled);
                    dc.UserEnablementEntities.InsertOnSubmit(new UserEnablementEntity { id = Guid.NewGuid(), userId = userId, enabledById = enabledById, time = time });
                    dc.SubmitChanges();
                }
            }
        }

        void IUserAccountsRepository.DisableUserAccount(Guid userId, Guid disabledById, DateTime time)
        {
            using (var dc = CreateContext())
            {
                var entity = GetRegisteredUserEntity(dc, userId);
                if (entity != null)
                {
                    entity.flags = (short)((UserFlags)entity.flags).SetFlag(UserFlags.Disabled);
                    dc.UserDisablementEntities.InsertOnSubmit(new UserDisablementEntity { id = Guid.NewGuid(), userId = userId, disabledById = disabledById, time = time });
                    dc.SubmitChanges();
                }
            }
        }

        void IUserAccountsRepository.ActivateUserAccount(Guid userId, Guid activatedById, DateTime time)
        {
            using (var dc = CreateContext())
            {
                var entity = GetRegisteredUserEntity(dc, userId);
                if (entity != null)
                {
                    entity.flags = (short) ((UserFlags) entity.flags).SetFlag(UserFlags.Activated);
                    dc.UserActivationEntities.InsertOnSubmit(new UserActivationEntity { id = Guid.NewGuid(), userId = userId, activatedById = activatedById, time = time });
                    dc.SubmitChanges();
                }
            }
        }

        void IUserAccountsRepository.DeactivateUserAccount(Guid userId, Guid deactivatedById, DateTime time)
        {
            using (var dc = CreateContext())
            {
                DeactivateUserAccount(dc, userId, deactivatedById, time, null, null);
            }
        }

        void IUserAccountsRepository.DeactivateUserAccount(Guid userId, Guid deactivatedById, DateTime time, DeactivationReason reason, string comments)
        {
            using (var dc = CreateContext())
            {
                DeactivateUserAccount(dc, userId, deactivatedById, time, reason, comments);
            }
        }

        bool IUserAccountsRepository.IsEnabled(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return IsEnabled(dc, userId);
            }
        }

        bool IUserAccountsRepository.IsActive(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return IsActive(dc, userId);
            }
        }

        IList<Guid> IUserAccountsRepository.GetEnabledAccountIds(IEnumerable<Guid> userIds)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetEnabledAccountIds(dc, new SplitList<Guid>(userIds).ToString()).ToList();
            }
        }

        DateTime? IUserAccountsRepository.GetCreatedTime(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                var dt = GetCreatedTime(dc, userId);
                return dt == default(DateTime) ? (DateTime?) null : dt;
            }
        }

        IList<Guid> IUserAccountsRepository.GetCreatedAccountIds(DateTimeRange dateTimeRange)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetCreatedAccountIds(dc, dateTimeRange).ToList();
            }
        }

        IList<Guid> IUserAccountsRepository.GetCreatedAccountIds(IEnumerable<Guid> userIds, DateTimeRange dateTimeRange)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetFilteredCreatedAccountIds(dc, new SplitList<Guid>(userIds).ToString(), dateTimeRange).ToList();
            }
        }

        IList<UserAccountAction> IUserAccountsRepository.GetEnablements(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetEnablements(dc, userId).ToList();
            }
        }

        IList<UserAccountAction> IUserAccountsRepository.GetDisablements(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDisablements(dc, userId).ToList();
            }
        }

        IList<UserAccountAction> IUserAccountsRepository.GetActivations(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetActivations(dc, userId).ToList();
            }
        }

        IList<UserAccountAction> IUserAccountsRepository.GetDeactivations(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetDeactivations(dc, userId).ToList();
            }
        }

        private static void DeactivateUserAccount(AccountsDataContext dc, Guid userId, Guid deactivatedById, DateTime time, DeactivationReason? reason, string comments)
        {
            var entity = GetRegisteredUserEntity(dc, userId);
            if (entity != null)
            {
                entity.flags = (short) ((UserFlags) entity.flags).ResetFlag(UserFlags.Activated);
                dc.UserDeactivationEntities.InsertOnSubmit(new UserDeactivationEntity { id = Guid.NewGuid(), userId = userId, deactivatedById = deactivatedById, time = time, reason = Mappings.GetReason(reason), comments = comments });
                dc.SubmitChanges();
            }
        }

        private AccountsDataContext CreateContext()
        {
            return CreateContext(c => new AccountsDataContext(c));
        }
    }
}
