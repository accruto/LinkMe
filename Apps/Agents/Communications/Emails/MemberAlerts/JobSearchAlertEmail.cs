using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search;
using LinkMe.Apps.Presentation.Query.Search.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Content.Templates;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberAlerts
{
    public class JobSearchAlertEmailResult
    {
        public string JobAdId;
        public string Title;
        public string Location;
        public string Salary = string.Empty;
        public string JobType;
        public string Industry = string.Empty;
        public string PostedAge;
        public string PostedDate;
        public string Digest;
        public string Fragments;
    }

    public class JobSearchAlertEmailResults
        : ReadOnlyCollection<JobSearchAlertEmailResult>
    {
        public JobSearchAlertEmailResults(IList<JobSearchAlertEmailResult> list)
            : base(list)
        {
        }
    }

    public class JobSearchAlertEmail
        : MemberAlertEmail
    {
        private readonly int _matches;
        private readonly JobSearchAlertEmailResults _jobSearchResults;
        private readonly string _criteriaText;
        private readonly string _criteriaHtml;
        private readonly ReadOnlyQueryString _criteriaQueryString;
        private readonly Guid _savedSearchId;

        public JobSearchAlertEmail(ICommunicationUser to, int matches, IList<JobSearchAlertEmailResult> jobSearchResults, JobAdSearchCriteria criteria, Guid savedSearchId)
            : base(to)
        {
            if (jobSearchResults == null)
                throw new ArgumentNullException("jobSearchResults");

            _matches = matches;
            _jobSearchResults = new JobSearchAlertEmailResults(jobSearchResults);
            _criteriaText = criteria.GetDisplayText();
            _criteriaHtml = criteria.GetDisplayHtml();
            _criteriaQueryString = criteria.GetQueryString();
            _savedSearchId = savedSearchId;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("Matches", _matches);
            properties.Add("Results", _jobSearchResults);
            properties.Add("CriteriaText", _criteriaText);
            properties.Add("CriteriaHtml", _criteriaHtml);
            properties.Add("CriteriaQueryString", _criteriaQueryString);
            properties.Add("SavedSearchId", _savedSearchId);
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }
    }
}