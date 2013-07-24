using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Communications.Settings.Commands
{
    public class NonMemberSettingsCommand
        : INonMemberSettingsCommand
    {
        private readonly ISettingsRepository _repository;

        public NonMemberSettingsCommand(ISettingsRepository repository)
        {
            _repository = repository;
        }

        void INonMemberSettingsCommand.CreateNonMemberSettings(NonMemberSettings settings)
        {
            settings.Prepare();
            settings.Validate();
            _repository.CreateNonMemberSettings(settings);
        }

        void INonMemberSettingsCommand.UpdateNonMemberSettings(NonMemberSettings settings)
        {
            settings.Validate();
            _repository.UpdateNonMemberSettings(settings);
        }
    }
}
