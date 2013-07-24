using System.Collections.Generic;

namespace LinkMe.Framework.Text
{
    public interface IWordFinder
    {
        IEnumerable<string> GetRelated(string term);
    }
}