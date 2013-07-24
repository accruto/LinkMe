using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class SuggestedCandidates
    {
        public JobAd JobAd { get; set; }
        public int TotalCandidates { get; set; }
        public IList<Guid> CandidateIds { get; set; }
    }

    public class SuggestedCandidatesEmail
        : EmployerMemberViewEmail
    {
        private readonly int _totalCandidates;
        private readonly int _totalJobs;
        private readonly IList<SuggestedCandidates> _suggestedCandidates;
        private readonly EmployerMemberViews _views;

        public SuggestedCandidatesEmail(string toAddress, IEmployer to, int totalCandidates, int totalJobs, IList<SuggestedCandidates> suggestedCandidates, EmployerMemberViews views)
            : base(GetEmployer(to, toAddress))
        {
            _totalCandidates = totalCandidates;
            _totalJobs = totalJobs;
            _suggestedCandidates = suggestedCandidates;
            _views = views;
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("TotalCandidates", _totalCandidates);
            properties.Add("TotalJobs", _totalJobs);
            properties.Add("SuggestedCandidates", _suggestedCandidates, typeof(IList));
            properties.Add("Views", GetEmailViews(_suggestedCandidates.SelectMany(s => s.CandidateIds), _views));
        }
    }
}