using System.Collections.Generic;

namespace LinkMe.Framework.Text
{
    public interface ISpellChecker
    {
        IList<string> GetSuggestions(IDictionary<string, int> dictionary, string term);
    }
}