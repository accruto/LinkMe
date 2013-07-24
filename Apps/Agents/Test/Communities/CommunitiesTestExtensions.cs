using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Verticals;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Framework.Content;

namespace LinkMe.Apps.Agents.Test.Communities
{
    public enum TestCommunity
    {
        Aes,
        CorporateCulcha,
        Ahri,
        Talent2,
        ItWire,
        Aimpe,
        Rcsa,
        MonashGsb,
        Pga,
        Crossroads,
        Ping,
        Gta,
        Maanz,
        Scouts,
        Autopeople,
        Finsia,
        Sae,
        TheNursingCentre,
        Aat,
        Aime,
        NextStep,
        Otd,
        UniMelbArts,
        TheJunctionWorks,
        Vecci,
        ItcraLink,
        IndigenousCareers,

        // No longer enabled but used for some specific community tests.

        BusinessSpectator,
        LiveInAustralia,
        TheRedTentWoman,
    }

    public class CommunityTestData
        : VerticalTestData
    {
        public string EmailDomain
        {
            get { return GetValue("EmailDomain", null); }
        }

        public bool HasMembers
        {
            get { return GetBoolValue("HasMembers", true); }
        }

        public bool HasOrganisationalUnits
        {
            get { return GetBoolValue("HasOrganisationalUnits", false); }
        }

        public bool OrganisationsCanSearchAllMembers
        {
            get { return GetBoolValue("OrganisationsCanSearchAllMembers", false); }
        }

        public bool OrganisationsAreBranded
        {
            get { return GetBoolValue("OrganisationsAreBranded", false); }
        }
    }

    public static class CommunitiesTestExtensions
    {
        private static readonly IDictionary<string, CommunityTestData> Datas = new Dictionary<string, CommunityTestData>();
        private static readonly object DataLock = new object();

        public static Community CreateTestCommunity(this TestCommunity community, ICommunitiesCommand communitiesCommand, IVerticalsCommand verticalsCommand, IContentEngine contentEngine)
        {
            return communitiesCommand.CreateTestCommunity(verticalsCommand, contentEngine, community.ToString());
        }

        public static Community CreateTestCommunity(this TestCommunity community, ICommunitiesCommand communitiesCommand, IVerticalsCommand verticalsCommand)
        {
            return communitiesCommand.CreateTestCommunity(verticalsCommand, community.ToString());
        }

        public static Community CreateTestCommunity(this CommunityTestData data, ICommunitiesCommand communitiesCommand, IVerticalsCommand verticalsCommand, IContentEngine contentEngine)
        {
            data.CreateTestVertical(verticalsCommand, contentEngine);
            var community = new Community
            {
                Id = data.Id.Value,
                Name = data.Name,
                EmailDomain = data.EmailDomain,
                HasMembers = data.HasMembers,
                HasOrganisations = data.HasOrganisationalUnits,
                OrganisationsCanSearchAllMembers = data.OrganisationsCanSearchAllMembers,
                OrganisationsAreBranded = data.OrganisationsAreBranded,
            };
            communitiesCommand.CreateCommunity(community);
            return community;
        }

        public static Community CreateTestCommunity(this CommunityTestData data, ICommunitiesCommand communitiesCommand, IVerticalsCommand verticalsCommand)
        {
            return CreateTestCommunity(data, communitiesCommand, verticalsCommand, null);
        }

        public static CommunityTestData GetCommunityTestData(this TestCommunity community)
        {
            return GetCommunityTestData(community.ToString());
        }

        private static Community CreateTestCommunity(this ICommunitiesCommand communitiesCommand, IVerticalsCommand verticalsCommand, IContentEngine contentEngine, string name)
        {
            return GetCommunityTestData(name).CreateTestCommunity(communitiesCommand, verticalsCommand, contentEngine);
        }

        private static Community CreateTestCommunity(this ICommunitiesCommand communitiesCommand, IVerticalsCommand verticalsCommand, string name)
        {
            return GetCommunityTestData(name).CreateTestCommunity(communitiesCommand, verticalsCommand);
        }

        private static CommunityTestData GetCommunityTestData(string name)
        {
            CommunityTestData data;

            lock (DataLock)
            {
                if (Datas.TryGetValue(name, out data))
                    return data;
            }

            // Not found so load it.

            data = LoadData(name);
            if (data == null)
                return null;

            lock (DataLock)
            {
                if (!Datas.ContainsKey(name))
                    Datas[name] = data;
            }

            return data;
        }

        private static CommunityTestData LoadData(string name)
        {
            return VerticalsTestExtensions.LoadData<CommunityTestData>(name);
        }
    }
}