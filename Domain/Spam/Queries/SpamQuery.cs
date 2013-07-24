using System;
using System.Linq;

namespace LinkMe.Domain.Spam.Queries
{
    public class SpamQuery
        : ISpamQuery
    {
        private readonly ISpamRepository _repository;

        public SpamQuery(ISpamRepository repository)
        {
            _repository = repository;
        }

        bool ISpamQuery.IsSpam(string text)
        {
            var patterns = _repository.GetSpamText();
            var pattern = patterns.FirstOrDefault(p => text.IndexOf(p, StringComparison.CurrentCultureIgnoreCase) != -1);
            return pattern != null;
        }

        bool ISpamQuery.IsSpammer(Spammer possibleSpammer)
        {
            return _repository.IsSpammer(possibleSpammer);
        }
    }
}