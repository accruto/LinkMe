namespace LinkMe.Domain.Roles.Communications.Settings.Queries
{
    public class NonMemberSettingsQuery
        : INonMemberSettingsQuery
    {
        private readonly ISettingsRepository _repository;

        public NonMemberSettingsQuery(ISettingsRepository repository)
        {
            _repository = repository;
        }

        NonMemberSettings INonMemberSettingsQuery.GetNonMemberSettings(string emailAddress)
        {
            return _repository.GetNonMemberSettings(emailAddress);
        }
    }
}
