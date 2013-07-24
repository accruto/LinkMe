using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Applications.Commands
{
    public interface ITinyUrlCommand
    {
        void CreateMappings(IEnumerable<TinyUrlMapping> mappings);
    }
}