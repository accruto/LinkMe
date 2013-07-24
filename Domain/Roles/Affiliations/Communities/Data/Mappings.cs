using System;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Roles.Affiliations.Communities.Data
{
    internal static class Mappings
    {
        [Flags]
        private enum CommunityFlags
        {
            HasMembers = 0x1,
            HasOrganisationalUnits = 0x2,
            OrganisationsCanSearchAllMembers = 0x4,
            OrganisationsAreBranded = 0x8,
        }

        public static Community Map(this CommunityEntity entity)
        {
            var flags = (CommunityFlags)entity.flags;
            return new Community
            {
                Id = entity.id,
                Name = entity.name,
                ShortName = entity.shortName,
                HasMembers = flags.IsFlagSet(CommunityFlags.HasMembers),
                HasOrganisations = flags.IsFlagSet(CommunityFlags.HasOrganisationalUnits),
                OrganisationsCanSearchAllMembers = flags.IsFlagSet(CommunityFlags.OrganisationsCanSearchAllMembers),
                OrganisationsAreBranded = flags.IsFlagSet(CommunityFlags.OrganisationsAreBranded),
                EmailDomain = entity.emailDomain,
            };
        }

        public static CommunityEntity Map(this Community community)
        {
            var entity = new CommunityEntity {id = community.Id};
            community.MapTo(entity);
            return entity;
        }

        public static void MapTo(this Community community, CommunityEntity entity)
        {
            var flags = new CommunityFlags();
            flags = flags.SetFlag(CommunityFlags.HasMembers, community.HasMembers);
            flags = flags.SetFlag(CommunityFlags.HasOrganisationalUnits, community.HasOrganisations);
            flags = flags.SetFlag(CommunityFlags.OrganisationsCanSearchAllMembers, community.OrganisationsCanSearchAllMembers);
            flags = flags.SetFlag(CommunityFlags.OrganisationsAreBranded, community.OrganisationsAreBranded);

            entity.name = community.Name;
            entity.shortName = community.ShortName;
            entity.flags = (byte) flags;
            entity.emailDomain = community.EmailDomain;
        }
    }
}
