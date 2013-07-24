using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Framework.Utility.Data.Linq;
using LinkMe.Framework.Utility.Sql;

namespace LinkMe.Framework.Text.Synonyms.Data
{
    public class SynonymsRepository
        : ISynonymsRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        private static readonly Func<SynonymsDataContext, IQueryable<EquivalentTermEntity>> GetEquivalentTermEntities
            = CompiledQuery.Compile((SynonymsDataContext dc)
                                    => from e in dc.EquivalentTermEntities
                                       select e);

        public SynonymsRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        void ISynonymsRepository.CreateSynonyms(SynonymGroup synonymGroup)
        {
            using (var dc = new SynonymsDataContext(_connectionFactory.CreateConnection()))
            {
                dc.EquivalentTermEntities.InsertAllOnSubmit(synonymGroup.Map());
                dc.SubmitChanges();
            }
        }

        IList<SynonymGroup> ISynonymsRepository.GetSynonyms()
        {
            using (var dc = new SynonymsDataContext(_connectionFactory.CreateConnection()).AsReadOnly())
            {
                var synonymGroupMap = new Dictionary<Guid, SynonymGroup>();

                foreach (var entity in GetEquivalentTermEntities(dc))
                {
                    SynonymGroup synonymGroup;
                    if (!synonymGroupMap.TryGetValue(entity.equivalentGroupId, out synonymGroup))
                    {
                        synonymGroup = new SynonymGroup { Id = entity.equivalentGroupId, Terms = new List<string>() };
                        synonymGroupMap.Add(entity.equivalentGroupId, synonymGroup);
                    }

                    synonymGroup.Terms.Add(entity.searchTerm);
                }

                return synonymGroupMap.Values.ToList();
            }
        }
    }
}