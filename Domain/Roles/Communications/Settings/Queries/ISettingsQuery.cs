using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Roles.Communications.Settings.Queries
{
    public interface ISettingsQuery
    {
        Category GetCategory(Guid id);
        Category GetCategory(string name);
        IList<Category> GetCategories();
        IList<Category> GetCategories(UserType userType);

        Definition GetDefinition(Guid id);
        Definition GetDefinition(string name);
        IList<Definition> GetDefinitions();
        IList<Definition> GetDefinitions(UserType userType);

        RecipientSettings GetSettings(Guid recipientId);
        RecipientSettings GetSettings(Guid recipientId, Guid definitionId, Guid? categoryId);
    }
}
