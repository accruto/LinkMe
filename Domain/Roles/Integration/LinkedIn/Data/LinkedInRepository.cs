using System;
using System.Data.Linq;
using System.Linq;
using LinkMe.Domain.Data;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Domain.Roles.Integration.LinkedIn.Data
{
    public class LinkedInRepository
        : Repository, ILinkedInRepository
    {
        private readonly IIndustriesQuery _industriesQuery;
        private readonly ILocationQuery _locationQuery;

        private static readonly DataLoadOptions ProfileLoadOptions = DataOptions.CreateLoadOptions<LinkedInProfileEntity, LinkedInProfileEntity>(p => p.LocationReferenceEntity, p => p.LinkedInProfileIndustryEntities);

        private static readonly Func<LinkedInDataContext, string, LinkedInProfileEntity> GetProfileEntity
            = CompiledQuery.Compile((LinkedInDataContext dc, string linkedInId)
                => (from p in dc.LinkedInProfileEntities
                    where p.linkedInId == linkedInId
                    select p).SingleOrDefault());

        private static readonly Func<LinkedInDataContext, Guid, LinkedInProfileEntity> GetProfileEntityByUserId
            = CompiledQuery.Compile((LinkedInDataContext dc, Guid userId)
                => (from p in dc.LinkedInProfileEntities
                    where p.userId  == userId
                    select p).SingleOrDefault());

        private static readonly Func<LinkedInDataContext, string, IIndustriesQuery, ILocationQuery, LinkedInProfile> GetProfileQuery
            = CompiledQuery.Compile((LinkedInDataContext dc, string linkedInId, IIndustriesQuery industriesQuery, ILocationQuery locationQuery)
                => (from p in dc.LinkedInProfileEntities
                    where p.linkedInId == linkedInId
                    select p.Map(industriesQuery, locationQuery)).SingleOrDefault());

        private static readonly Func<LinkedInDataContext, Guid, IIndustriesQuery, ILocationQuery, LinkedInProfile> GetProfileByUserIdQuery
            = CompiledQuery.Compile((LinkedInDataContext dc, Guid userId, IIndustriesQuery industriesQuery, ILocationQuery locationQuery)
                => (from p in dc.LinkedInProfileEntities
                    where p.userId == userId
                    select p.Map(industriesQuery, locationQuery)).SingleOrDefault());

        public LinkedInRepository(IDataContextFactory dataContextFactory, IIndustriesQuery industriesQuery, ILocationQuery locationQuery)
            : base(dataContextFactory)
        {
            _industriesQuery = industriesQuery;
            _locationQuery = locationQuery;
        }

        void ILinkedInRepository.UpdateProfile(LinkedInProfile profile)
        {
            using (var dc = CreateContext())
            {
                var entity = GetProfileEntity(dc, profile.Id);
                if (entity == null)
                {
                    dc.LinkedInProfileEntities.InsertOnSubmit(profile.Map());
                }
                else
                {
                    dc.CheckDeleteLocation(profile, entity);
                    if (entity.LinkedInProfileIndustryEntities != null && entity.LinkedInProfileIndustryEntities.Count > 0)
                        dc.LinkedInProfileIndustryEntities.DeleteAllOnSubmit(entity.LinkedInProfileIndustryEntities);
                    profile.MapTo(entity);
                }

                dc.SubmitChanges();
            }
        }

        void ILinkedInRepository.DeleteProfile(Guid userId)
        {
            using (var dc = CreateContext())
            {
                var entity = GetProfileEntityByUserId(dc, userId);
                if (entity != null)
                {
                    if (entity.LinkedInProfileIndustryEntities != null)
                        dc.LinkedInProfileIndustryEntities.DeleteAllOnSubmit(entity.LinkedInProfileIndustryEntities);
                    dc.LinkedInProfileEntities.DeleteOnSubmit(entity);
                    dc.SubmitChanges();
                }
            }
        }

        LinkedInProfile ILinkedInRepository.GetProfile(string linkedInId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetProfile(dc, linkedInId);
            }
        }

        LinkedInProfile ILinkedInRepository.GetProfile(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetProfile(dc, userId);
            }
        }

        private LinkedInProfile GetProfile(LinkedInDataContext dc, string linkedInId)
        {
            dc.LoadOptions = ProfileLoadOptions;
            return GetProfileQuery(dc, linkedInId, _industriesQuery, _locationQuery);
        }

        private LinkedInProfile GetProfile(LinkedInDataContext dc, Guid userId)
        {
            dc.LoadOptions = ProfileLoadOptions;
            return GetProfileByUserIdQuery(dc, userId, _industriesQuery, _locationQuery);
        }

        private LinkedInDataContext CreateContext()
        {
            return CreateContext(c => new LinkedInDataContext(c));
        }
    }
}
