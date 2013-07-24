using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Communications.Settings
{
    public class RecipientSettings
    {
        public Guid Id { get; set; }
        public IList<CategorySettings> CategorySettings { get; set; }
        public IList<DefinitionSettings> DefinitionSettings { get; set; }
    }
}
