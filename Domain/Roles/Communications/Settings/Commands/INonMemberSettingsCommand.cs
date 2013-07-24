namespace LinkMe.Domain.Roles.Communications.Settings.Commands
{
    public interface INonMemberSettingsCommand
    {
        void CreateNonMemberSettings(NonMemberSettings settings);
        void UpdateNonMemberSettings(NonMemberSettings settings);
    }
}
