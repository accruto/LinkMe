using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Users.Administrators.Data
{
    public class AdministratorsRepository
        : Repository, IAdministratorsRepository
    {
        private static readonly Func<AdministratorsDataContext, Guid, AdministratorEntity> GetAdministratorEntity
            = CompiledQuery.Compile((AdministratorsDataContext dc, Guid id)
                => (from e in dc.AdministratorEntities
                    where e.id == id
                    select e).SingleOrDefault());

        private static readonly Func<AdministratorsDataContext, Guid, Administrator> GetAdministrator
            = CompiledQuery.Compile((AdministratorsDataContext dc, Guid id)
                => (from e in dc.AdministratorEntities
                    join u in dc.RegisteredUserEntities on e.id equals u.id
                    where e.id == id
                    select Mappings.Map(e, u)).SingleOrDefault());

        private static readonly Func<AdministratorsDataContext, IQueryable<Administrator>> GetAdministrators
            = CompiledQuery.Compile((AdministratorsDataContext dc)
                => from e in dc.AdministratorEntities
                   join u in dc.RegisteredUserEntities on e.id equals u.id
                   orderby u.firstName, u.lastName
                   select Mappings.Map(e, u));

        private static readonly Func<AdministratorsDataContext, IQueryable<Administrator>> GetEnabledAdministrators
            = CompiledQuery.Compile((AdministratorsDataContext dc)
                => from e in dc.AdministratorEntities
                   join u in dc.RegisteredUserEntities on e.id equals u.id
                   where (u.flags & (int) UserFlags.Disabled) == 0
                   orderby u.firstName, u.lastName
                   select Mappings.Map(e, u));

        public AdministratorsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IAdministratorsRepository.CreateAdministrator(Administrator administrator)
        {
            using (var dc = CreateContext())
            {
                dc.AdministratorEntities.InsertOnSubmit(administrator.Map());
                dc.SubmitChanges();
            }
        }

        void IAdministratorsRepository.UpdateAdministrator(Administrator administrator)
        {
            using (var dc = CreateContext())
            {
                var entity = GetAdministratorEntity(dc, administrator.Id);
                if (entity != null)
                {
                    administrator.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        Administrator IAdministratorsRepository.GetAdministrator(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetAdministrator(dc, id);
            }
        }

        IList<Administrator> IAdministratorsRepository.GetAdministrators(bool enabledOnly)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return enabledOnly
                    ? GetEnabledAdministrators(dc).ToList()
                    : GetAdministrators(dc).ToList();
            }
        }

        private AdministratorsDataContext CreateContext()
        {
            return CreateContext(c => new AdministratorsDataContext(c));
        }
    }
}