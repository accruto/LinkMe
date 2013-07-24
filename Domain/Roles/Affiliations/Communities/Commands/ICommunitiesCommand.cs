namespace LinkMe.Domain.Roles.Affiliations.Communities.Commands
{
    public interface ICommunitiesCommand
    {
        void CreateCommunity(Community community);
        void UpdateCommunity(Community community);
    }
}