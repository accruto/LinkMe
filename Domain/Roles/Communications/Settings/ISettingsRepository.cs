using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Roles.Communications.Settings
{
    public interface ISettingsRepository
    {
        void CreateDefinition(Definition definition);
        Definition GetDefinition(Guid id);
        Definition GetDefinition(string name);
        IList<Definition> GetDefinitions();
        IList<Definition> GetDefinitions(UserType userType);

        Category GetCategory(Guid id);
        Category GetCategory(string name);
        IList<Category> GetCategories();
        IList<Category> GetCategories(UserType userType);

        RecipientSettings GetSettings(Guid recipientId);
        RecipientSettings GetSettings(Guid recipientId, Guid definitionId, Guid? categoryId);

        void DeleteSettings(Guid recipientId);
        void SetFrequency(Guid recipientId, Guid categoryId, Frequency frequency);
        void SetLastSentTime(Guid recipientId, Guid definitionId, DateTime? lastSentTime);

        void CreateNonMemberSettings(NonMemberSettings settings);
        void UpdateNonMemberSettings(NonMemberSettings settings);
        NonMemberSettings GetNonMemberSettings(string emailAddress);
    }
}
