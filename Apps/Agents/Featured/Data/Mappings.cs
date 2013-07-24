namespace LinkMe.Apps.Agents.Featured.Data
{
    internal static class Mappings
    {
        public static FeaturedStatistics Map(this FeaturedStatisticsEntity entity)
        {
            return new FeaturedStatistics
            {
                CreatedJobAds = entity.createdJobAds,
                Members = entity.members,
                MemberSearches = entity.memberSearches,
                MemberAccesses = entity.memberAccesses,
            };
        }

        public static void MapTo(this FeaturedStatistics statistics, FeaturedStatisticsEntity entity)
        {
            entity.createdJobAds = statistics.CreatedJobAds;
            entity.members = statistics.Members;
            entity.memberSearches = statistics.MemberSearches;
            entity.memberAccesses = statistics.MemberAccesses;
        }

        public static FeaturedEmployer Map(this FeaturedEmployerEntity entity)
        {
            return new FeaturedEmployer
            {
                Id = entity.id,
                Name = entity.name,
                LogoUrl = entity.logoUrl,
                LogoOrder = entity.logoOrder,
            };
        }

        public static FeaturedItem Map(this FeaturedJobAdEntity entity)
        {
            return new FeaturedItem
            {
                Id = entity.id,
                Url = entity.url,
                Title = entity.title,
            };
        }

        public static FeaturedJobAdEntity Map(this FeaturedItem jobAd)
        {
            return new FeaturedJobAdEntity
            {
                id = jobAd.Id,
                url = jobAd.Url,
                title = jobAd.Title,
            };
        }

        public static FeaturedItem Map(this FeaturedCandidateSearchEntity entity)
        {
            return new FeaturedItem
            {
                Id = entity.id,
                Url = entity.url,
                Title = entity.title,
            };
        }
    }
}
