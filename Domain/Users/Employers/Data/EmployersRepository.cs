using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Data;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Users.Employers.Data
{
    public class EmployersRepository
        : Repository, IEmployersRepository
    {
        private readonly IIndustriesQuery _industriesQuery;

        private static readonly DataLoadOptions EmployerLoadOptions = DataOptions.CreateLoadOptions<EmployerEntity, EmployerEntity>(e => e.RegisteredUserEntity, e => e.EmployerIndustryEntities);

        private static readonly Func<EmployersDataContext, Guid, EmployerEntity> GetEmployerEntityQuery
            = CompiledQuery.Compile((EmployersDataContext dc, Guid id)
                => (from e in dc.EmployerEntities
                    where e.id == id
                    select e).SingleOrDefault());

        private static readonly Func<EmployersDataContext, Guid, IIndustriesQuery, Employer> GetEmployerQuery
            = CompiledQuery.Compile((EmployersDataContext dc, Guid id, IIndustriesQuery industriesQuery)
                => (from e in dc.EmployerEntities
                    join u in dc.RegisteredUserEntities on e.id equals u.id
                    where e.id == id
                    select Mappings.Map(e, u, industriesQuery)).SingleOrDefault());

        private static readonly Func<EmployersDataContext, string, IIndustriesQuery, IQueryable<Employer>> GetEmployersQuery
            = CompiledQuery.Compile((EmployersDataContext dc, string ids, IIndustriesQuery industriesQuery)
                => from e in dc.EmployerEntities
                   join u in dc.RegisteredUserEntities on e.id equals u.id
                   join i in dc.SplitGuids(SplitList<Guid>.Delimiter, ids) on e.id equals i.value
                   select Mappings.Map(e, u, industriesQuery));

        private static readonly Func<EmployersDataContext, string, IIndustriesQuery, IQueryable<Employer>> GetEmployersByEmailAddressQuery
            = CompiledQuery.Compile((EmployersDataContext dc, string emailAddress, IIndustriesQuery industriesQuery)
                => from e in dc.EmployerEntities
                   join u in dc.RegisteredUserEntities on e.id equals u.id
                   where u.emailAddress == emailAddress
                   select Mappings.Map(e, u, industriesQuery));

        private static readonly Func<EmployersDataContext, IQueryable<Guid>> GetEmployerIds
            = CompiledQuery.Compile((EmployersDataContext dc)
                => from e in dc.EmployerEntities
                   select e.id);

        public EmployersRepository(IDataContextFactory dataContextFactory, IIndustriesQuery industriesQuery)
            : base(dataContextFactory)
        {
            _industriesQuery = industriesQuery;
        }

        void IEmployersRepository.CreateEmployer(Employer employer)
        {
            using (var dc = CreateContext())
            {
                dc.EmployerEntities.InsertOnSubmit(employer.Map());

                dc.SubmitChanges();
            }
        }

        void IEmployersRepository.UpdateEmployer(Employer employer)
        {
            using (var dc = CreateContext())
            {
                var entity = GetEmployerEntity(dc, employer.Id);
                if (entity != null)
                {
                    // Delete any industries and devices already there.

                    if (entity.EmployerIndustryEntities != null)
                    {
                        dc.EmployerIndustryEntities.DeleteAllOnSubmit(entity.EmployerIndustryEntities);
                        entity.EmployerIndustryEntities = null;
                    }

                    employer.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        Employer IEmployersRepository.GetEmployer(Guid id)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetEmployer(dc, id);
            }
        }

        IList<Employer> IEmployersRepository.GetEmployers(IEnumerable<Guid> ids)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetEmployers(dc, ids).ToList();
            }
        }

        IList<Employer> IEmployersRepository.GetEmployers(string emailAddress)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetEmployers(dc, emailAddress).ToList();
            }
        }

        IList<Guid> IEmployersRepository.GetEmployerIds()
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetEmployerIds(dc).ToList();
            }
        }

        private Employer GetEmployer(EmployersDataContext dc, Guid id)
        {
            dc.LoadOptions = EmployerLoadOptions;
            return GetEmployerQuery(dc, id, _industriesQuery);
        }

        private IEnumerable<Employer> GetEmployers(EmployersDataContext dc, IEnumerable<Guid> ids)
        {
            dc.LoadOptions = EmployerLoadOptions;
            return GetEmployersQuery(dc, new SplitList<Guid>(ids.Distinct()).ToString(), _industriesQuery);
        }

        private IEnumerable<Employer> GetEmployers(EmployersDataContext dc, string emailAddress)
        {
            dc.LoadOptions = EmployerLoadOptions;
            return GetEmployersByEmailAddressQuery(dc, emailAddress, _industriesQuery);
        }

        private static EmployerEntity GetEmployerEntity(EmployersDataContext dc, Guid id)
        {
            dc.LoadOptions = EmployerLoadOptions;
            return GetEmployerEntityQuery(dc, id);
        }

        private EmployersDataContext CreateContext()
        {
            return CreateContext(c => new EmployersDataContext(c));
        }
    }
}