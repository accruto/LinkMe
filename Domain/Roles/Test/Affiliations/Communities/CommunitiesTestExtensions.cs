using System;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Verticals;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Domain.Roles.Test.Affiliations.Communities
{
    public static class CommunitiesTestExtensions
    {
        private const string CommunityNameFormat = "Community{0}";
        private const string CommunityShortNameFormat = "Comm{0}";

        public static Community CreateTestCommunity(this ICommunitiesCommand communitiesCommand, int index)
        {
            var community = new Community
            {
                Id = Guid.NewGuid(),
                Name = string.Format(CommunityNameFormat, index),
                ShortName = string.Format(CommunityShortNameFormat, index),
                EmailDomain = null,
                HasMembers = true,
                HasOrganisations = false,
                OrganisationsCanSearchAllMembers = false,
                OrganisationsAreBranded = false,
            };

            communitiesCommand.CreateCommunity(community);
            return community;
        }

        public static Vertical GetVertical(this IVerticalsCommand verticalsCommand, Community community)
        {
            return verticalsCommand.GetVertical(community.Id);
        }

        public static ReadOnlyUrl GetCommunityUrl(this IVerticalsCommand verticalsCommand, Community community, string path)
        {
            return verticalsCommand.GetVertical(community).GetVerticalUrl(path);
        }

        public static ReadOnlyUrl GetCommunityUrl(this IVerticalsCommand verticalsCommand, Community community, bool secure, string path)
        {
            return verticalsCommand.GetVertical(community).GetVerticalUrl(secure, path);
        }

        public static ReadOnlyUrl GetCommunityHostUrl(this IVerticalsCommand verticalsCommand, Community community, bool secure, string path)
        {
            return verticalsCommand.GetVertical(community).GetVerticalHostUrl(secure, path);
        }

        public static ReadOnlyUrl GetCommunityHostUrl(this IVerticalsCommand verticalsCommand, Community community, string path)
        {
            return verticalsCommand.GetVertical(community).GetVerticalHostUrl(path);
        }

        public static ReadOnlyUrl GetCommunitySecondaryHostUrl(this IVerticalsCommand verticalsCommand, Community community, string path)
        {
            return verticalsCommand.GetVertical(community).GetVerticalSecondaryHostUrl(path);
        }

        public static ReadOnlyUrl GetCommunityTertiaryHostUrl(this IVerticalsCommand verticalsCommand, Community community, string path)
        {
            return verticalsCommand.GetVertical(community).GetVerticalTertiaryHostUrl(path);
        }

        public static ReadOnlyUrl GetCommunityPathUrl(this IVerticalsCommand verticalsCommand, Community community, bool secure, string path)
        {
            return verticalsCommand.GetVertical(community).GetVerticalPathUrl(secure, path);
        }

        public static ReadOnlyUrl GetCommunityPathUrl(this IVerticalsCommand verticalsCommand, Community community, string path)
        {
            return verticalsCommand.GetVertical(community).GetVerticalPathUrl(path);
        }
    }
}