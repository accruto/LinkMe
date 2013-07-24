using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Apps.Presentation;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Search;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Utility;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class ResumeSearchAlertEmail
        : EmployerMemberViewEmail
    {
        private const int MaximumResults = 20;
        private const string BlankCriteriaText = "(no criteria)";

        private readonly MemberSearchResults _results;
        private readonly EmployerMemberViews _views;
        private readonly string _criteriaText;
        private readonly Guid _savedSearchId;

        public ResumeSearchAlertEmail(ICommunicationUser to, MemberSearchCriteria criteria, IList<Industry> criteriaIndustries, MemberSearchResults results, EmployerMemberViews views, Guid savedSearchId)
            : base(to)
        {
            if (results == null)
                throw new ArgumentNullException("results");

            _results = results;
            _views = views;
            _criteriaText = GetCriteriaText(criteria, criteriaIndustries);
            _savedSearchId = savedSearchId;
        }
	
        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Criteria", _criteriaText);
            properties.Add("TotalMatches", _results.MemberIds.Count);

            properties.Add("Header", GetHeader(_results.MemberIds.Count));
            var newResults = new List<Guid>();
            var updatedResults = new List<Guid>();
            var emailViews = new EmployerMemberEmailViews();
            GetResults(newResults, updatedResults, emailViews);
            var newCount = newResults.Count;
            
            properties.Add("NewResults", newResults, typeof(IList));
            properties.Add("UpdatedResults", updatedResults, typeof(IList));
            properties.Add("Views", emailViews);

            properties.Add("NewCount", newCount);
            properties.Add("UpdatedCount", _results.MemberIds.Count - newCount);

            properties.Add("SavedSearchId", _savedSearchId.ToString("B")); // To help debugging.
        }

        private static string GetCriteriaText(MemberSearchCriteria criteria, IList<Industry> industries)
        {
            if (criteria == null)
                return string.Empty;

            var sb = new StringBuilder();
            AppendText(sb, criteria, industries);
            AppendText(sb, criteria);
            return sb.Length == 0 ? BlankCriteriaText : sb.ToString();
        }

        private static void AppendText(StringBuilder sb, MemberSearchCriteria criteria, IList<Industry> industries)
        {
            // Job Title.

            if (!string.IsNullOrEmpty(criteria.JobTitle.GetCriteriaJobTitleDisplayText()))
                sb.Append(criteria.JobTitle.GetCriteriaJobTitleDisplayText());

            // Keywords.

            if (criteria.KeywordsExpression != null)
            {
                var keywordExpression = criteria.KeywordsExpression.GetUserExpression();
                if (!string.IsNullOrEmpty(keywordExpression))
                {
                    if (sb.Length != 0)
                        sb.Append(", ");
                    sb.Append(TextUtil.TrimEndBracketsFromExpression(keywordExpression));
                }
            }

            // Location.

            if (criteria.Location != null)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(criteria.Location.IsCountry ? criteria.Location.Country.Name : criteria.Location.ToString());
            }

            // Salary.

            if (criteria.Salary != null)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(criteria.Salary.GetDisplayText());
            }

            // Industries.

            if (industries != null && industries.Count > 0)
            {
                var industriesText = industries.GetCriteriaIndustriesDisplayText();
                if (!string.IsNullOrEmpty(industriesText))
                {
                    if (sb.Length != 0)
                        sb.Append(", ");
                    sb.Append(industriesText);
                }
            }
        }

        private static void AppendText(StringBuilder sb, MemberSearchCriteria criteria)
        {
            // Desired job title.

            if (criteria.DesiredJobTitleExpression != null)
            {
                if (sb.Length != 0)
                    sb.Append(", ");

                sb.Append("Desired job title contains " + criteria.DesiredJobTitle);
            }

            // Ideal job type.

            if (criteria.JobTypes != JobTypes.All)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(criteria.JobTypes.GetDisplayText(" OR ", false, false));
            }

            // Candidate flags.

            if (criteria.CandidateStatusFlags.HasValue)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(string.Join(" OR ", criteria.CandidateStatusFlags.Value.GetDisplayTexts(CandidateStatusDisplay.Values, CandidateStatusDisplay.GetDisplayText).ToArray()));
            }

            // Ethnic flags.

            if (criteria.EthnicStatus.HasValue)
            {
                if (sb.Length != 0)
                    sb.Append(", ");
                sb.Append(string.Join(" OR ", criteria.EthnicStatus.Value.GetDisplayTexts(EthnicStatusDisplay.Values, EthnicStatusDisplay.GetDisplayText).ToArray()));
            }
        }

        private static string GetHeader(int totalMatches)
        {
            // Output all strong matches upto the maximum number.

            if (totalMatches > 0)
            {
                return totalMatches <= MaximumResults
                    ? string.Format("{0} result{1}", totalMatches, totalMatches == 1 ? "" : "s")
                    : string.Format("{0} results (first {1} shown)", totalMatches, MaximumResults);
            }

            return string.Empty;
        }

        private void GetResults(ICollection<Guid> newResults, ICollection<Guid> updatedResults, IDictionary<Guid, EmployerMemberEmailView> emailViews)
        {
            var matches = _results.MemberIds.Count;
            if (matches > 0)
                AppendResults(newResults, updatedResults, 0, Math.Min(matches, MaximumResults), emailViews);
        }

        private void AppendResults(ICollection<Guid> newResults, ICollection<Guid> updatedResults, int start, int count, IDictionary<Guid, EmployerMemberEmailView> emailViews)
        {
            for (var index = start; index < start + count; index++)
            {
                // Get the member for the result.

                var view = _views[_results.MemberIds[index]];
                if (view != null)
                    AppendResult(newResults, updatedResults, view, emailViews);
            }
        }

        private static void AppendResult(ICollection<Guid> newResults, ICollection<Guid> updatedResults, EmployerMemberView view, IDictionary<Guid, EmployerMemberEmailView> emailViews)
        {
            var emailView = GetEmailView(view);
            emailViews[view.Id] = emailView;
            if (emailView.IsNew)
                newResults.Add(emailView.CandidateId);
            else
                updatedResults.Add(emailView.CandidateId);
        }
    }
}