using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Text.Synonyms;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;

namespace LinkMe.Query.Search.Members.Queries
{
    public class SuggestedMembersQuery
        : ISuggestedMembersQuery
    {
        private readonly ISynonymsCommand _synonymsCommand;
        private readonly IDictionary<string, string> _ignoredJobTitleWords;

        public SuggestedMembersQuery(ISynonymsCommand synonymsCommand, string ignoredJobTitleWords)
        {
            _synonymsCommand = synonymsCommand;
            _ignoredJobTitleWords = ignoredJobTitleWords.Split(new[] { ',' }).ToDictionary(s => s.ToLower(), s => s.ToLower());
        }

        MemberSearchCriteria ISuggestedMembersQuery.GetCriteria(JobAd jobAd)
        {
            var criteria = new MemberSearchCriteria
            {
                Salary = jobAd.Description.Salary,
                SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.Relevance },
            };

            var jobTitle = jobAd.Description.PositionTitle;
            if (TextUtil.TrimAndCheckEmpty(ref jobTitle))
                jobTitle = jobAd.Title;

            string newJobTitle;
            string jobTitleKeywords;
            GetJobTitleAndKeywords(jobTitle, out newJobTitle, out jobTitleKeywords);
            criteria.JobTitle = newJobTitle;
            criteria.IncludeSynonyms = true;

            criteria.SetKeywords(CombineKeywords(jobTitleKeywords, GetIndustryKeywords(jobAd.Description.Industries)));

            if (jobAd.Description.Location != null)
            {
                // set location; default distance will be set in Query
                criteria.Location = jobAd.Description.Location;
            }

            return criteria;
        }

        private void GetJobTitleAndKeywords(string jobTitle, out string newJobTitle, out string newKeywords)
        {
            newJobTitle = null;
            newKeywords = null;

            if (jobTitle == null)
                return;

            jobTitle = jobTitle.Trim();
            if (jobTitle.Length == 0)
                return;

            // Find the longest job title from the synonyms list inside the title, only matching on word boundaries
            // (eg. "account" should not be found inside "accounts").

            var indexOfLongestTitle = -1;
            var lengthOfLongestTitle = 0;
            var jobTitleLower = jobTitle.ToLower();

            foreach (var synonymLower in _synonymsCommand.GetTerms())
            {
                if (synonymLower.Length > lengthOfLongestTitle)
                {
                    var index = jobTitleLower.IndexOf(synonymLower);
                    if (index != -1 && AreWordBoundaries(jobTitle, index, index + synonymLower.Length - 1))
                    {
                        indexOfLongestTitle = index;
                        lengthOfLongestTitle = synonymLower.Length;
                    }
                }
            }

            if (indexOfLongestTitle != -1)
            {
                // Found a job title synonym - use it. Split the rest of the input into keywords, stripping ignored words.

                newJobTitle = jobTitle.Substring(indexOfLongestTitle, lengthOfLongestTitle);
                newKeywords = StripIgnoredWordsAndJoin(jobTitle.Remove(indexOfLongestTitle, lengthOfLongestTitle), " OR ");
            }
            else
            {
                // No job title synonym found. Try to split the input at the first " -" or " -" or " (".

                var index = -1;
                do
                {
                    index = jobTitle.IndexOfAny(new[] { '-', '(' }, index + 1);
                }
                while (index != -1 && !TextUtil.IsLeftWordBoundary(jobTitle, index) && !TextUtil.IsRightWordBoundary(jobTitle, index));

                if (index != -1)
                {
                    // Use the part before the " - " as the job title and the rest as keywords.

                    newJobTitle = StripIgnoredWordsAndJoin(jobTitle.Substring(0, index), " ");
                    newKeywords = StripIgnoredWordsAndJoin(jobTitle.Substring(index + 1), " OR ");
                }
                else
                {
                    // Use the whole input as the job title (but strip ignored words).

                    newJobTitle = StripIgnoredWordsAndJoin(jobTitle, " ");
                }
            }
        }

        private string StripIgnoredWordsAndJoin(string text, string separator)
        {
            var words = TextUtil.SplitIntoWords(text, null);
            if (words.Length == 0)
                return "";

            var sb = new StringBuilder();
            var uniqueWords = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);

            foreach (var word in words)
            {
                var realWord = word.Trim(',', '-', '/', '.', '!', ':', ';', '\'', '\"');

                // Ignore words with numbers or digits, certain specific words, repeated words.

                if (realWord.Length > 0 && !_ignoredJobTitleWords.ContainsKey(realWord) && IsRealWord(realWord) && !uniqueWords.ContainsKey(realWord))
                {
                    if (sb.Length > 0)
                        sb.Append(separator);
                    sb.Append(realWord);

                    uniqueWords.Add(realWord, null);
                }
            }

            return sb.ToString();
        }

        private static bool IsRealWord(string word)
        {
            // Ignore any "words" that contain characters other than letters, numbers and the symbols '-', '/', '.'.
            // Also ignore "words" that are just '-' or '/';

            for (var i = 0; i < word.Length; i++)
            {
                var c = word[i];
                if (!char.IsLetterOrDigit(c) && c != '-' && c != '/' && c != '.')
                    return false;
            }

            return true;
        }

        private static string CombineKeywords(string keywords1, string keywords2)
        {
            if (string.IsNullOrEmpty(keywords1))
                return keywords2;
            if (string.IsNullOrEmpty(keywords2))
                return keywords1;
            return "(" + keywords1 + ") (" + keywords2 + ")";
        }

        private static string GetIndustryKeywords(IEnumerable<Industry> industries)
        {
            if (industries == null)
                return null;

            var keywords = new StringBuilder();
            foreach (var industry in industries)
            {
                var keywordExpression = industry.KeywordExpression;
                if (!string.IsNullOrEmpty(keywordExpression))
                {
                    if (keywords.Length > 0)
                        keywords.Append(" ");
                    keywords.Append("(" + keywordExpression + ")");
                }
            }

            return keywords.Length == 0 ? null : keywords.ToString();
        }

        private static bool AreWordBoundaries(string text, int indexLeft, int indexRight)
        {
            return TextUtil.IsLeftWordBoundary(text, indexLeft) && TextUtil.IsRightWordBoundary(text, indexRight);
        }
    }
}
