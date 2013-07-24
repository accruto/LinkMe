using System;
using System.Text;
using System.Web;
using LinkMe.Domain;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    public abstract class CriteriaTests
        : SearchTests
    {
        [TestMethod]
        public void TestCriteriaDisplay()
        {
            TestDisplay();
        }

        protected abstract void TestDisplay();

        protected void TestDisplay(MemberSearchCriteria criteria)
        {
            // Get the page and check that all fields are set properly.

            Get(GetSearchUrl(criteria));
            AssertCriteria(criteria);

            // Look for the hash in the page and resubmit it.

            var hash = AssertHash(criteria);
            var url = GetSearchUrl().AsNonReadOnly();
            url.QueryString.Add(new QueryString(hash));
            Get(url);
            AssertCriteria(criteria);

            // Look for the hash in the partial results and resubmit it.

            Get(GetPartialSearchUrl(criteria));
            hash = AssertHash(criteria);

            url = GetSearchUrl().AsNonReadOnly();
            url.QueryString.Add(new QueryString(hash));
            Get(url);
            AssertCriteria(criteria);
        }

        private string AssertHash(MemberSearchCriteria criteria)
        {
            var hash = GetPageHash();
            Assert.AreEqual(GetHash(criteria), hash);
            return hash;
        }

        private static string GetHash(MemberSearchCriteria criteria)
        {
            // This simulates the workings of new QueryStringGenerator(new MemberSearchCriteriaConverter()).GenerateQueryString(criteria);
            // Don't use that method here though so it gets tested independently.

            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(criteria.ExactPhrase) || !string.IsNullOrEmpty(criteria.AnyKeywords) || !string.IsNullOrEmpty(criteria.WithoutKeywords))
            {
                if (!string.IsNullOrEmpty(criteria.ExactPhrase))
                    Append(sb, "ExactPhrase", HttpUtility.UrlEncode(criteria.ExactPhrase));
                if (!string.IsNullOrEmpty(criteria.AllKeywords))
                    Append(sb, "AllKeywords", HttpUtility.UrlEncode(criteria.AllKeywords));
                if (!string.IsNullOrEmpty(criteria.WithoutKeywords))
                    Append(sb, "WithoutKeywords", HttpUtility.UrlEncode(criteria.WithoutKeywords));

                if (!string.IsNullOrEmpty(criteria.AnyKeywords))
                {
                    foreach (var anyKeywords in criteria.AnyKeywords.Split(new[] { ' ' }))
                        Append(sb, "AnyKeywords", HttpUtility.UrlEncode(anyKeywords));
                }
            }
            else
            {
                var keywords = criteria.GetKeywords();
                if (!string.IsNullOrEmpty(keywords))
                    Append(sb, "Keywords", HttpUtility.UrlEncode(keywords));
            }

            if (criteria.CandidateStatusFlags != null)
            {
                if (criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.AvailableNow))
                    Append(sb, "AvailableNow", "true");
                if (criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.ActivelyLooking))
                    Append(sb, "ActivelyLooking", "true");
                if (criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.OpenToOffers))
                    Append(sb, "OpenToOffers", "true");
                if (criteria.CandidateStatusFlags.Value.IsFlagSet(CandidateStatusFlags.Unspecified))
                    Append(sb, "Unspecified", "true");
            }

            if (criteria.JobTypes != JobTypes.All)
            {
                if (criteria.JobTypes.IsFlagSet(JobTypes.FullTime))
                    Append(sb, "FullTime", "true");
                if (criteria.JobTypes.IsFlagSet(JobTypes.PartTime))
                    Append(sb, "PartTime", "true");
                if (criteria.JobTypes.IsFlagSet(JobTypes.Contract))
                    Append(sb, "Contract", "true");
                if (criteria.JobTypes.IsFlagSet(JobTypes.Temp))
                    Append(sb, "Temp", "true");
                if (criteria.JobTypes.IsFlagSet(JobTypes.JobShare))
                    Append(sb, "JobShare", "true");
            }

            if (!string.IsNullOrEmpty(criteria.EducationKeywords))
                Append(sb, "EducationKeywords", HttpUtility.UrlEncode(criteria.EducationKeywords));

            if (!string.IsNullOrEmpty(criteria.JobTitle))
            {
                Append(sb, "JobTitle", HttpUtility.UrlEncode(criteria.JobTitle));
                if (criteria.JobTitlesToSearch != JobsToSearch.RecentJobs)
                    Append(sb, "JobTitlesToSearch", criteria.JobTitlesToSearch);
            }

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                Append(sb, "CandidateName", HttpUtility.UrlEncode(criteria.Name));
                if (criteria.IncludeSimilarNames)
                    Append(sb, "IncludeSimilarNames", "true");
            }

            if (!string.IsNullOrEmpty(criteria.DesiredJobTitle))
                Append(sb, "DesiredJobTitle", HttpUtility.UrlEncode(criteria.DesiredJobTitle));

            if (!string.IsNullOrEmpty(criteria.CompanyKeywords))
            {
                Append(sb, "CompanyKeywords", HttpUtility.UrlEncode(criteria.CompanyKeywords));
                if (criteria.CompaniesToSearch != JobsToSearch.AllJobs)
                    Append(sb, "CompaniesToSearch", criteria.CompaniesToSearch);
            }

            if (criteria.Location != null)
            {
                Append(sb, "CountryId", criteria.Location.Country.Id);

                var location = criteria.Location.ToString();
                if (!string.IsNullOrEmpty(location))
                    Append(sb, "Location", HttpUtility.UrlEncode(location));
                if (criteria.Distance != null && criteria.Distance.Value != 50)
                    Append(sb, "Distance", criteria.Distance.Value);
            }

            if (criteria.Recency != null)
                Append(sb, "Recency", criteria.Recency.Value.Days);

            if (criteria.Salary != null)
            {
                if (criteria.Salary.LowerBound != null)
                    Append(sb, "SalaryLowerBound", criteria.Salary.LowerBound.Value);
                if (criteria.Salary.UpperBound != null)
                    Append(sb, "SalaryUpperBound", criteria.Salary.UpperBound.Value);
            }

            if (criteria.EthnicStatus != null)
            {
                if (criteria.EthnicStatus.Value.IsFlagSet(EthnicStatus.Aboriginal))
                    Append(sb, "Aboriginal", "true");
                if (criteria.EthnicStatus.Value.IsFlagSet(EthnicStatus.TorresIslander))
                    Append(sb, "TorresIslander", "true");
            }

            if (criteria.VisaStatusFlags != null)
            {
                if (criteria.VisaStatusFlags.Value.IsFlagSet(VisaStatusFlags.Citizen))
                    Append(sb, "Citizen", "true");
                if (criteria.VisaStatusFlags.Value.IsFlagSet(VisaStatusFlags.UnrestrictedWorkVisa))
                    Append(sb, "UnrestrictedWorkVisa", "true");
                if (criteria.VisaStatusFlags.Value.IsFlagSet(VisaStatusFlags.RestrictedWorkVisa))
                    Append(sb, "RestrictedWorkVisa", "true");
                if (criteria.VisaStatusFlags.Value.IsFlagSet(VisaStatusFlags.NoWorkVisa))
                    Append(sb, "NoWorkVisa", "true");
            }

            if (criteria.HasViewed != null)
                Append(sb, "HasViewed", criteria.HasViewed.Value.ToString().ToLower());
            if (criteria.IsUnlocked != null)
                Append(sb, "IsUnlocked", criteria.IsUnlocked.Value.ToString().ToLower());

            if (criteria.CommunityId != null)
                Append(sb, "CommunityId", criteria.CommunityId.Value);

            return sb.Length == 0 ? null : sb.ToString();
        }

        private static void Append(StringBuilder sb, string name, object value)
        {
            if (sb.Length != 0)
                sb.Append("&");
            sb.Append(name).Append("=").Append(value);
        }

        private string GetPageHash()
        {
            // Looking for: window.location.hash = "#<%= hash %>";

            var startIndex = Browser.CurrentPageText.IndexOf("window.location.hash = \"#");
            if (startIndex == -1)
                return null;

            startIndex += "window.location.hash = \"#".Length;
            var endIndex = Browser.CurrentPageText.IndexOf("\";", startIndex, StringComparison.Ordinal);
            return endIndex == -1
                ? Browser.CurrentPageText.Substring(startIndex)
                : Browser.CurrentPageText.Substring(startIndex, endIndex - startIndex);
        }
    }
}
