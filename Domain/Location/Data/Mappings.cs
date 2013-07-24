using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Location.Queries;

namespace LinkMe.Domain.Location.Data
{
    public interface ILocationReferenceEntity
    {
        Guid id { get; set; }
        string unstructuredLocation { get; set; }
        int? namedLocationId { get; set; }
        int countrySubdivisionId { get; set; }
        int? localityId { get; set; }
    }

    public interface IAddressEntity<TLocationReferenceEntity>
        where TLocationReferenceEntity : class, ILocationReferenceEntity
    {
        Guid id { get; set; }
        string line1 { get; set; }
        string line2 { get; set; }
        TLocationReferenceEntity LocationReferenceEntity { get; set; }
    }

    public interface IHaveAddressEntity<TAddressEntity, TLocationReferenceEntity>
        where TAddressEntity : class, IAddressEntity<TLocationReferenceEntity>
        where TLocationReferenceEntity : class, ILocationReferenceEntity
    {
        TAddressEntity AddressEntity { get; set; }
    }

    public interface IHaveLocationReferenceEntity<TLocationReferenceEntity>
        where TLocationReferenceEntity : class, ILocationReferenceEntity
    {
        TLocationReferenceEntity LocationReferenceEntity { get; set; }
    }

    public interface IHaveLocationReferenceEntities<TLocationReferenceEntity>
        where TLocationReferenceEntity : class
    {
        Table<TLocationReferenceEntity> LocationReferenceEntities { get; }
    }

    public interface IHaveAddressEntities<TAddressEntity, TLocationReferenceEntity>
        : IHaveLocationReferenceEntities<TLocationReferenceEntity>
        where TLocationReferenceEntity : class
        where TAddressEntity : class
    {
        Table<TAddressEntity> AddressEntities { get; }
    }

    public static class Mappings
    {
        public static void CheckDeleteAddress<TAddressEntity, TLocationReferenceEntity>(this IHaveAddressEntities<TAddressEntity, TLocationReferenceEntity> haveAddressEntities, IHaveAddress haveAddress, IHaveAddressEntity<TAddressEntity, TLocationReferenceEntity> entity)
            where TAddressEntity : class, IAddressEntity<TLocationReferenceEntity>
            where TLocationReferenceEntity : class, ILocationReferenceEntity
        {
            // If there was an address but there is none now then delete.

            if (entity.AddressEntity != null && haveAddress.Address == null)
            {
                if (entity.AddressEntity.LocationReferenceEntity != null)
                    haveAddressEntities.LocationReferenceEntities.DeleteOnSubmit(entity.AddressEntity.LocationReferenceEntity);
                haveAddressEntities.AddressEntities.DeleteOnSubmit(entity.AddressEntity);
                entity.AddressEntity = null;
            }
        }

        public static void CheckDeleteLocation<TLocationReferenceEntity>(this IHaveLocationReferenceEntities<TLocationReferenceEntity> haveLocationReferenceEntities, IHaveLocation haveLocation, IHaveLocationReferenceEntity<TLocationReferenceEntity> entity)
            where TLocationReferenceEntity : class, ILocationReferenceEntity
        {
            if (entity.LocationReferenceEntity != null && haveLocation.Location == null)
            {
                haveLocationReferenceEntities.LocationReferenceEntities.DeleteOnSubmit(entity.LocationReferenceEntity);
                entity.LocationReferenceEntity = null;
            }
        }

        public static LocationReference Map(this ILocationReferenceEntity entity, ILocationQuery locationQuery)
        {
            return new LocationReference(
                entity.unstructuredLocation,
                entity.namedLocationId == null ? null : locationQuery.GetNamedLocation(entity.namedLocationId.Value),
                locationQuery.GetCountrySubdivision(entity.countrySubdivisionId),
                entity.localityId == null ? null : locationQuery.GetLocality(entity.localityId.Value))
                {
                    Id = entity.id
                };
        }

        public static T MapTo<T>(this LocationReference location)
            where T : ILocationReferenceEntity, new()
        {
            var t = new T {id = location.Id};
            location.MapTo(t);
            return t;
        }

        public static void MapTo(this LocationReference location, ILocationReferenceEntity entity)
        {
            // Need to ensure that objects match what is in the database.

            if (entity.id != Guid.Empty)
                location.Id = entity.id;

            entity.unstructuredLocation = location.UnstructuredLocation;
            entity.namedLocationId = (location.NamedLocation == null ? (int?) null : location.NamedLocation.Id);
            entity.countrySubdivisionId = location.CountrySubdivision.Id;
            entity.localityId = location.Locality == null ? (int?) null : location.Locality.Id;
        }

        public static Address Map<TLocationReferenceEntity>(this IAddressEntity<TLocationReferenceEntity> entity, ILocationQuery locationQuery)
            where TLocationReferenceEntity : class, ILocationReferenceEntity
        {
            return new Address
           {
               Id = entity.id,
               Line1 = entity.line1,
               Line2 = entity.line2,
               Location = entity.LocationReferenceEntity == null ? null : entity.LocationReferenceEntity.Map(locationQuery),
           };
        }

        public static TAddressEntity MapTo<TAddressEntity, TLocationReferenceEntity>(this Address address)
            where TAddressEntity : IAddressEntity<TLocationReferenceEntity>, new()
            where TLocationReferenceEntity : class, ILocationReferenceEntity, new()
        {
            var entity = new TAddressEntity {id = address.Id};
            address.MapTo(entity);
            return entity;
        }

        public static void MapTo<TAddressEntity, TLocationReferenceEntity>(this IHaveAddress haveAddress, IHaveAddressEntity<TAddressEntity, TLocationReferenceEntity> entity)
            where TAddressEntity : class, IAddressEntity<TLocationReferenceEntity>, new()
            where TLocationReferenceEntity : class, ILocationReferenceEntity, new()
        {
            if (haveAddress.Address != null)
            {
                if (entity.AddressEntity == null)
                    entity.AddressEntity = haveAddress.Address.MapTo<TAddressEntity, TLocationReferenceEntity>();
                else
                    haveAddress.Address.MapTo(entity.AddressEntity);
            }
        }

        public static void MapTo<TLocationReferenceEntity>(this IHaveLocation haveLocation, IHaveLocationReferenceEntity<TLocationReferenceEntity> entity)
            where TLocationReferenceEntity : class, ILocationReferenceEntity, new()
        {
            if (haveLocation.Location != null)
            {
                if (entity.LocationReferenceEntity == null)
                    entity.LocationReferenceEntity = haveLocation.Location.MapTo<TLocationReferenceEntity>();
                else
                    haveLocation.Location.MapTo(entity.LocationReferenceEntity);
            }
        }

        public static void MapTo<TLocationReferenceEntity>(this Address address, IAddressEntity<TLocationReferenceEntity> entity)
            where TLocationReferenceEntity : class, ILocationReferenceEntity, new()
        {
            // Need to ensure that objects match what is in the database.

            if (entity.id != Guid.Empty)
                address.Id = entity.id;

            entity.line1 = address.Line1;
            entity.line2 = address.Line2;

            // Try to update in place.

            if (address.Location != null)
            {
                if (entity.LocationReferenceEntity == null)
                    entity.LocationReferenceEntity = address.Location.MapTo<TLocationReferenceEntity>();
                else
                    address.Location.MapTo(entity.LocationReferenceEntity);
            }
        }

        internal static Country Map(this CountryEntity entity)
        {
            return new Country
            {
                Id = entity.id,
                Name = entity.displayName,
                IsoCode = entity.isoCode,
            };
        }

        internal static LocationAbbreviation Map(this LocationAbbreviationEntity entity)
        {
            return new LocationAbbreviation
            {
                Id = entity.id,
                Abbreviation = entity.abbreviation,
                Name = entity.displayName,
                IsPrefix = entity.prefix,
                IsSuffix = entity.suffix,
            };
        }

        internal static RelativeLocation Map(this RelativeLocationEntity entity)
        {
            return new RelativeLocation
            {
                Id = entity.id,
                Name = entity.displayName,
                IsPrefix = entity.prefix,
                IsSuffix = entity.suffix
            };
        }

        internal static CountrySubdivision Map(this CountrySubdivisionEntity entity, IDictionary<int, Country> countries)
        {
            return new CountrySubdivision
            {
                Id = entity.id,
                Name = entity.GeographicalAreaEntity.NamedLocationEntity.displayName,
                Country = countries[entity.countryId],
                ShortName = entity.shortDisplayName,
                UrlName = entity.urlName,
                CircleRadiusKm = entity.circleRadiusKm,
                CircleCentreId = entity.circleCentreId,
                Aliases = new List<CountrySubdivisionAlias>(),
            };
        }

        internal static CountrySubdivisionAlias Map(this CountrySubdivisionAliasEntity entity)
        {
            return new CountrySubdivisionAlias
            {
                Id = entity.id,
                Name = entity.displayName,
            };
        }

        internal static Region Map(this RegionEntity entity, IDictionary<int, IList<int>> regionLocalities, IDictionary<int, Locality> localities)
        {
            var allCountries = !regionLocalities.ContainsKey(entity.id)
                ? new List<Country>()
                : (from rl in regionLocalities[entity.id]
                   let l = localities[rl]
                   from c in l.CountrySubdivisions
                   select c.Country).ToList();

            return new Region
            {
                Id = entity.id,
                Name = entity.GeographicalAreaEntity.NamedLocationEntity.displayName,
                Country = allCountries.Count == 0
                    ? null
                    : allCountries.All(c => c == allCountries.First()) ? allCountries.First() : null,
                UrlName = entity.urlName,
                IsMajorCity = entity.isMajorCity,
                Aliases = new List<RegionAlias>(),
            };
        }

        internal static RegionAlias Map(this RegionAliasEntity entity)
        {
            return new RegionAlias
            {
                Id = entity.id,
                Name = entity.displayName,
            };
        }

        internal static Locality Map(this LocalityEntity entity, IDictionary<int, CountrySubdivision> subdivisions)
        {
            return new Locality
            {
                Id = entity.id,
                Name = entity.GeographicalAreaEntity.NamedLocationEntity.displayName,
                Centroid = new GeoCoordinates(entity.centroidLatitude, entity.centroidLongitude),
                CountrySubdivisions = (from ls in entity.LocalityCountrySubdivisionEntities
                                       select subdivisions[ls.countrySubdivisionId]).ToList()
            };
        }

        internal static PostalCode Map(this PostalCodeEntity entity, IDictionary<int, Locality> localities)
        {
            return new PostalCode
            {
                Id = entity.id,
                Name = entity.NamedLocationEntity.displayName,
                Locality = localities[entity.localityId],
            };
        }

        internal static PostalSuburb Map(this PostalSuburbEntity entity, IDictionary<int, CountrySubdivision> subdivisions, IDictionary<int, PostalCode> postalCodes)
        {
            return new PostalSuburb
            {
                Id = entity.id,
                Name = entity.NamedLocationEntity.displayName,
                CountrySubdivision = subdivisions[entity.countrySubdivisionId],
                PostalCode = postalCodes[entity.postcodeId]
            };
        }
    }
}
