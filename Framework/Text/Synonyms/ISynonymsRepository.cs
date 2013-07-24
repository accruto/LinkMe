using System.Collections.Generic;

namespace LinkMe.Framework.Text.Synonyms
{
    public interface ISynonymsRepository
    {
        void CreateSynonyms(SynonymGroup synonymGroup);
        IList<SynonymGroup> GetSynonyms();
    }
}