using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Framework.Text.Synonyms
{
    public class SynonymsCommand
        : ISynonymsCommand
    {
        // This includes trimmed characters and any other characters LENS doesn't handle,
        // like comma and colon.
        private static readonly char[] DisallowedCharacters = new[] { '\"', '(', ')', ',', ':', ';' };

        private class State
        {
            public Dictionary<string, SynonymGroup> SynonymGroups;
            public IList<string> Terms;
        }

        private State _state;

        private readonly ISynonymsRepository _repository;
        private readonly string[] _ignoredSynonyms;

        public SynonymsCommand(ISynonymsRepository repository, string[] ignoredSynonyms)
        {
            _repository = repository;
            _ignoredSynonyms = ignoredSynonyms;
            EnsureInitialised();
        }

        void ISynonymsCommand.CreateSynonyms(SynonymGroup synonymGroup)
        {
            synonymGroup.Prepare();
            synonymGroup.Validate();
            _repository.CreateSynonyms(synonymGroup);

            // Reset.

            _state = null;
        }

        IList<string> ISynonymsCommand.GetTerms()
        {
            return EnsureInitialised().Terms;
        }

        IList<string> ISynonymsCommand.GetSynonyms(string term)
        {
            SynonymGroup synonymGroup;
            EnsureInitialised().SynonymGroups.TryGetValue(term, out synonymGroup);
            return synonymGroup == null ? new List<string>() : synonymGroup.Terms;
        }

        private State EnsureInitialised()
        {
            var state = _state;
            if (state != null)
                return state;

            // Load all synonyms up front.

            var synonyms = _repository.GetSynonyms();
            var synonymGroupMap = new Dictionary<string, SynonymGroup>(StringComparer.CurrentCultureIgnoreCase);
            var allTerms = new List<string>();

            foreach (var terms in synonyms)
            {
                foreach (var term in terms.Terms)
                {
                    ValidateTerm(term);

                    try
                    {
                        synonymGroupMap.Add(term, terms);
                    }
                    catch (ArgumentException ex)
                    {
                        throw new ApplicationException(string.Format("A search term, '{0}' is listed in two groups of equivalent terms: {1} and {2}.", term, synonymGroupMap[term].Id, terms.Id), ex);
                    }

                    allTerms.Add(term.ToLower());
                }
            }

            foreach (var ignored in _ignoredSynonyms)
                allTerms.Remove(ignored);

            state = new State {SynonymGroups = synonymGroupMap, Terms = allTerms};
            _state = state;
            return state;
        }

        private static void ValidateTerm(string term)
        {
            // For simplicity InsertSynonyms() simply trims quotes and brackets from the query string.
            // This should work OK (ie. not return false positives) as long as those characters don't appear
            // inside any equivalent terms. Validate that here.

            var index = term.IndexOfAny(DisallowedCharacters);
            if (index != -1)
                throw new ApplicationException(string.Format("Equivalent term '{0}' contains a disallowed character, '{1}' at index {2}.", term, term[index], index));
        }
    }
}