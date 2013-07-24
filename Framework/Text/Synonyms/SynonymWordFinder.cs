using System.Collections.Generic;

namespace LinkMe.Framework.Text.Synonyms
{
    public class SynonymWordFinder
        : IWordFinder
    {
        private readonly ISynonymsCommand _synonymsCommand;

        public SynonymWordFinder(ISynonymsCommand synonymsCommand)
        {
            _synonymsCommand = synonymsCommand;
        }

        IEnumerable<string> IWordFinder.GetRelated(string originalText)
        {
            return _synonymsCommand.GetSynonyms(originalText);
        }
    }
}