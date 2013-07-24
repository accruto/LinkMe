namespace LinkMe.Domain.Roles.Communications.Settings.Queries
{
    public interface INonMemberSettingsQuery
    {
        NonMemberSettings GetNonMemberSettings(string emailAddress);
    }
}
