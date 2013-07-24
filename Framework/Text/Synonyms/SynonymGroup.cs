using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Framework.Text.Synonyms
{
    /// <summary>
    /// A collection of search terms that are equivalent in meaning (synonyms).
    /// </summary>
    public class SynonymGroup
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public IList<string> Terms { get; set; }
    }
}