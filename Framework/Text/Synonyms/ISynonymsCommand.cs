using System.Collections.Generic;

namespace LinkMe.Framework.Text.Synonyms
{
    public interface ISynonymsCommand
    {
        void CreateSynonyms(SynonymGroup synonymGroup);
        IList<string> GetTerms();
        IList<string> GetSynonyms(string term);
    }
}