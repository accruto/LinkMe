using System;
using System.Data.Linq;

namespace LinkMe.Domain.Criterias.Data
{
    public interface ICriteriaEntity
    {
        Guid id { get; set; }
        string name { get; set; }
        object value { get; set; }
    }

    public interface ICriteriaSetEntity<TCriteriaEntity>
        where TCriteriaEntity : class, ICriteriaEntity
    {
        Guid id { get; set; }
        string type { get; set; }
        EntitySet<TCriteriaEntity> Entities { get; set; }
    }

    public static class Mappings
    {
        public static TCriteriaSetEntity MapTo<TCriteriaSetEntity, TCriteriaEntity, TCriteria>(this TCriteria criteria, ICriteriaPersister criteriaPersister, bool persistantIsString)
            where TCriteriaSetEntity : ICriteriaSetEntity<TCriteriaEntity>, new()
            where TCriteriaEntity : class, ICriteriaEntity, new()
            where TCriteria : Criteria
        {
            return new TCriteriaSetEntity
            {
                id = criteria.Id,
                type = criteriaPersister.GetCriteriaType(criteria),
                Entities = criteria.MapTo<TCriteriaEntity, TCriteria>(criteria.Id, criteriaPersister, persistantIsString)
            };
        }

        public static TCriteriaSetEntity MapTo<TCriteriaSetEntity, TCriteriaEntity, TCriteria>(this TCriteria criteria, string type, ICriteriaPersister criteriaPersister, bool persistantIsString)
            where TCriteriaSetEntity : ICriteriaSetEntity<TCriteriaEntity>, new()
            where TCriteriaEntity : class, ICriteriaEntity, new()
            where TCriteria : Criteria
        {
            return new TCriteriaSetEntity
            {
                id = criteria.Id,
                type = type,
                Entities = criteria.MapTo<TCriteriaEntity, TCriteria>(criteria.Id, criteriaPersister, persistantIsString)
            };
        }

        public static EntitySet<TCriteriaEntity> MapTo<TCriteriaEntity, TCriteria>(this TCriteria criteria, Guid id, ICriteriaPersister criteriaPersister, bool persistantIsString)
            where TCriteriaEntity : class, ICriteriaEntity, new()
            where TCriteria : Criteria
        {
            var entities = new EntitySet<TCriteriaEntity>();

            criteriaPersister.OnSaving(criteria);

            // Create the set entity and then attach all items that need to be persisted.

            foreach (var item in criteria.GetPersistantItems(persistantIsString))
                entities.Add(new TCriteriaEntity { id = id, name = item.Name, value = item.Value });

            criteriaPersister.OnSaved(criteria);
            return entities;
        }

        public static TCriteria MapTo<TCriteriaSetEntity, TCriteriaEntity, TCriteria>(this TCriteriaSetEntity entity, ICriteriaPersister criteriaPersister, bool persistantIsString)
            where TCriteriaSetEntity : ICriteriaSetEntity<TCriteriaEntity>
            where TCriteriaEntity : class, ICriteriaEntity
            where TCriteria : Criteria
        {
            return entity.MapTo<TCriteriaSetEntity, TCriteriaEntity, TCriteria>(entity.type, criteriaPersister, persistantIsString);
        }

        public static TCriteria MapTo<TCriteriaSetEntity, TCriteriaEntity, TCriteria>(this TCriteriaSetEntity entity, string type, ICriteriaPersister criteriaPersister, bool persistantIsString)
            where TCriteriaSetEntity : ICriteriaSetEntity<TCriteriaEntity>
            where TCriteriaEntity : class, ICriteriaEntity
            where TCriteria : Criteria
        {
            // Create first.

            var criteria = criteriaPersister.CreateCriteria<TCriteria>(type);
            criteria.Id = entity.id;

            criteriaPersister.OnLoading(criteria);

            foreach (var criteriaEntity in entity.Entities)
            {
                try
                {
                    criteria.SetPersistantItem(criteriaEntity.name, criteriaEntity.value, persistantIsString);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Could not set the criteria item with name '" + criteriaEntity.name + "'.", ex);
                }
            }

            criteriaPersister.OnLoaded(criteria);

            return criteria;
        }
    }
}