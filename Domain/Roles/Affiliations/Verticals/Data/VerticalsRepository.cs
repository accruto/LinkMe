using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Affiliations.Verticals.Data
{
    public class VerticalsRepository
        : Repository, IVerticalsRepository
    {
        private static readonly Func<VerticalsDataContext, bool, IQueryable<Vertical>> GetVerticals
            = CompiledQuery.Compile((VerticalsDataContext dc, bool includeDisabled)
                => from v in dc.VerticalEntities
                   where (v.enabled || includeDisabled)
                   orderby v.name
                   select v.Map());

        private static readonly Func<VerticalsDataContext, Guid, VerticalEntity> GetVerticalEntity
            = CompiledQuery.Compile((VerticalsDataContext dc, Guid id)
                => (from v in dc.VerticalEntities
                    where v.id == id
                    select v).SingleOrDefault());

        private static readonly Func<VerticalsDataContext, Guid, bool, Vertical> GetVertical
            = CompiledQuery.Compile((VerticalsDataContext dc, Guid id, bool includeDisabled)
                => (from v in dc.VerticalEntities
                    where v.id == id
                    && (v.enabled || includeDisabled)
                    select v.Map()).SingleOrDefault());

        private static readonly Func<VerticalsDataContext, string, Vertical> GetVerticalByName
            = CompiledQuery.Compile((VerticalsDataContext dc, string name)
                => (from v in dc.VerticalEntities
                    where v.name == name
                    && v.enabled
                    select v.Map()).SingleOrDefault());

        private static readonly Func<VerticalsDataContext, string, bool, Vertical> GetVerticalByHost
            = CompiledQuery.Compile((VerticalsDataContext dc, string host, bool includeDisabled)
                => (from v in dc.VerticalEntities
                    where (SqlMethods.Like(host, "%" + v.host) || SqlMethods.Like(host, "%" + v.secondaryHost) || SqlMethods.Like(host, "%" + v.tertiaryHost))
                    && (v.enabled || includeDisabled)
                    select v.Map()).SingleOrDefault());

        private static readonly Func<VerticalsDataContext, string, bool, Vertical> GetVerticalByUrl
            = CompiledQuery.Compile((VerticalsDataContext dc, string url, bool includeDisabled)
                => (from v in dc.VerticalEntities
                    where v.url == url
                    && (v.enabled || includeDisabled)
                    select v.Map()).SingleOrDefault());

        public VerticalsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IVerticalsRepository.CreateVertical(Vertical vertical)
        {
            using (var dc = CreateContext())
            {
                dc.VerticalEntities.InsertOnSubmit(vertical.Map());
                dc.SubmitChanges();
            }
        }

        void IVerticalsRepository.UpdateVertical(Vertical vertical)
        {
            using (var dc = CreateContext())
            {
                var entity = GetVerticalEntity(dc, vertical.Id);
                if (entity != null)
                {
                    vertical.MapTo(entity);
                    dc.SubmitChanges();
                }
            }
        }

        IList<Vertical> IVerticalsRepository.GetVerticals(bool includeDisabled)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetVerticals(dc, includeDisabled).ToList();
            }
        }

        Vertical IVerticalsRepository.GetVertical(Guid id, bool includeDisabled)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetVertical(dc, id, includeDisabled);
            }
        }

        Vertical IVerticalsRepository.GetVertical(string name)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetVerticalByName(dc, name);
            }
        }

        Vertical IVerticalsRepository.GetVerticalByHost(string host, bool includeDisabled)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetVerticalByHost(dc, host, includeDisabled);
            }
        }

        Vertical IVerticalsRepository.GetVerticalByUrl(string url, bool includeDisabled)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetVerticalByUrl(dc, url, includeDisabled);
            }
        }

        private VerticalsDataContext CreateContext()
        {
            return CreateContext(c => new VerticalsDataContext(c));
        }
    }
}
