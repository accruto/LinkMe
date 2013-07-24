namespace LinkMe.Apps.Agents.Users.Members.Queries
{
    public interface IVisitorStatusQuery
    {
        VisitorStatus GetVisitorStatus(int sessionViews, int sessionApplications, int occasionalPrompts, int casualPrompts);
    }
}
