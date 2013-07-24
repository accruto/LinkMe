using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LinkMe.Framework.Text.Synonyms;

namespace LinkMe.Domain.Test.Synonyms
{
    public static class SynonymsTestExtensions
    {
        /// <summary>
        /// Loads synonyms from comma delimited file.
        /// </summary>
        public static void LoadTestSynonyms(this ISynonymsCommand synonymsCommand, string filePath)
        {
            var synonyms = new Dictionary<Guid, IList<string>>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(new []{","}, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        IList<string> list;
                        var id = new Guid(parts[1]);
                        if (!synonyms.TryGetValue(id, out list))
                        {
                            list = new List<string>();
                            synonyms[id] = list;
                        }

                        list.Add(parts[0]);
                    }
                }
            }

            // Group together.

            var synonymsGroups = from s in synonyms
                                 select new SynonymGroup {Id = s.Key, Terms = s.Value};
            foreach (var synonymGroup in synonymsGroups)
                synonymsCommand.CreateSynonyms(synonymGroup);
        }
    }
}
