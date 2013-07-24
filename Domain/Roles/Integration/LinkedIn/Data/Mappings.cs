using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Domain.Roles.Integration.LinkedIn.Data
{
    internal partial class LinkedInDataContext
        : IHaveLocationReferenceEntities<LocationReferenceEntity>
    {
        Table<LocationReferenceEntity> IHaveLocationReferenceEntities<LocationReferenceEntity>.LocationReferenceEntities
        {
            get { return LocationReferenceEntities; }
        }
    }

    internal partial class LocationReferenceEntity
        : ILocationReferenceEntity
    {
    }

    internal partial class LinkedInProfileEntity
        : IHaveLocationReferenceEntity<LocationReferenceEntity>
    {
    }

    internal static class Mappings
    {
        public static LinkedInProfile Map(this LinkedInProfileEntity entity, IIndustriesQuery industryQuery, ILocationQuery locationQuery)
        {
            return new LinkedInProfile
            {
                Id = entity.linkedInId,
                UserId = entity.userId,
                CreatedTime = entity.createdTime,
                LastUpdatedTime = entity.lastUpdatedTime,
                FirstName = entity.firstName,
                LastName = entity.lastName,
                OrganisationName = entity.organisationName,
                Location = entity.LocationReferenceEntity == null ? null : entity.LocationReferenceEntity.Map(locationQuery),
                Industries = entity.LinkedInProfileIndustryEntities == null ? new List<Industry>() : entity.LinkedInProfileIndustryEntities.Map(industryQuery),
            };
        }

        public static LinkedInProfileEntity Map(this LinkedInProfile profile)
        {
            var entity = new LinkedInProfileEntity { linkedInId = profile.Id, createdTime = profile.CreatedTime };
            profile.MapTo(entity);
            return entity;
        }

        public static void MapTo(this LinkedInProfile profile, LinkedInProfileEntity entity)
        {
            entity.userId = profile.UserId;
            entity.lastUpdatedTime = profile.LastUpdatedTime;
            entity.firstName = profile.FirstName;
            entity.lastName = profile.LastName;
            entity.organisationName = profile.OrganisationName;
            if (profile.Industries == null)
                entity.LinkedInProfileIndustryEntities = new EntitySet<LinkedInProfileIndustryEntity>();
            else
                entity.LinkedInProfileIndustryEntities.AddRange((from i in profile.Industries select new LinkedInProfileIndustryEntity { linkedInId = entity.linkedInId, industryId = i.Id }));
            ((IHaveLocation)profile).MapTo(entity);
        }

        private static IList<Industry> Map(this IEnumerable<LinkedInProfileIndustryEntity> entities, IIndustriesQuery industriesQuery)
        {
            return (from e in entities
                    select industriesQuery.GetIndustry(e.industryId)).ToList();
        }
    }
}
