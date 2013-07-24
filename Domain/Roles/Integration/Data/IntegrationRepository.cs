using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Integration.Data
{
    public class IntegrationRepository
        : Repository, IIntegrationRepository
    {
        private static readonly Func<IntegrationDataContext, Guid, AtsIntegratorEntity> GetAtsIntegratorEntity
            = CompiledQuery.Compile((IntegrationDataContext dc, Guid id)
                => (from a in dc.AtsIntegratorEntities
                    where a.id == id
                    select a).SingleOrDefault());

        private static readonly Func<IntegrationDataContext, Guid, IntegratorUser> GetIntegratorUser
            = CompiledQuery.Compile((IntegrationDataContext dc, Guid id)
                => (from u in dc.IntegratorUserEntities
                    where u.id == id
                    select u.Map()).SingleOrDefault());

        private static readonly Func<IntegrationDataContext, string, IntegratorUser> GetIntegratorUserByLoginId
            = CompiledQuery.Compile((IntegrationDataContext dc, string loginId)
                => (from u in dc.IntegratorUserEntities
                    where u.username == loginId
                    select u.Map()).SingleOrDefault());

        private static readonly Func<IntegrationDataContext, IQueryable<IntegratorUser>> GetIntegratorUsers
            = CompiledQuery.Compile((IntegrationDataContext dc)
                => from u in dc.IntegratorUserEntities
                   select u.Map());

        public IntegrationRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IIntegrationRepository.CreateIntegrationSystem(IntegrationSystem system)
        {
            using (var dc = CreateContext())
            {
                dc.AtsIntegratorEntities.InsertOnSubmit(system.Map());
                dc.SubmitChanges();
            }
        }

        T IIntegrationRepository.GetIntegrationSystem<T>(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetAtsIntegratorEntity(dc, id).MapTo<T>();
            }
        }

        void IIntegrationRepository.CreateIntegratorUser(IntegratorUser user)
        {
            using (var dc = CreateContext())
            {
                dc.IntegratorUserEntities.InsertOnSubmit(user.Map());
                dc.SubmitChanges();
            }
        }

        IntegratorUser IIntegrationRepository.GetIntegratorUser(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetIntegratorUser(dc, id);
            }
        }

        IntegratorUser IIntegrationRepository.GetIntegratorUser(string loginId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetIntegratorUserByLoginId(dc, loginId);
            }
        }

        IList<IntegratorUser> IIntegrationRepository.GetIntegratorUsers()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetIntegratorUsers(dc).ToList();
            }
        }

        private IntegrationDataContext CreateContext()
        {
            return CreateContext(c => new IntegrationDataContext(c));
        }
    }
}
