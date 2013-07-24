using System.Collections.Generic;
using LinkMe.Framework.Text.Synonyms;

namespace LinkMe.Domain.Test.Search
{
    public static class SynonymsExtensions
    {
        public static void CreateTestSynonyms(this ISynonymsCommand synonymsCommand)
        {
            synonymsCommand.CreateSynonyms(new SynonymGroup {Terms = new List<string>{"CEO", "Chief Executive Officer"}});
            synonymsCommand.CreateSynonyms(new SynonymGroup {Terms = new List<string>{"programmer", "developer"}});
            synonymsCommand.CreateSynonyms(new SynonymGroup {Terms = new List<string>{"admin", "administrator"}});
            synonymsCommand.CreateSynonyms(new SynonymGroup {Terms = new List<string>{"reception", "receptionist"}});
            synonymsCommand.CreateSynonyms(new SynonymGroup {Terms = new List<string>{"PA", "personal assistant"}});
        }
    }
}
