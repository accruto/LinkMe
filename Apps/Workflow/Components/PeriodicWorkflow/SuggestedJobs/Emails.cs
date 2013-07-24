using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails.MemberAlerts;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Apps.Presentation.Domain.Search;
using LinkMe.Apps.Presentation.Query.Search.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Content.Templates;

namespace LinkMe.Workflow.Components.PeriodicWorkflow.SuggestedJobs
{
    public class SuggestedJobsEmailItem
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

    // Wrap ReadOnlyCollection<SuggestedJobsEmailItem> as the TemplateEngine can't deal with generics yet.
    public class SuggestedJobsEmailItemCollection
        : ReadOnlyCollection<SuggestedJobsEmailItem>
    {
        public SuggestedJobsEmailItemCollection(IList<SuggestedJobsEmailItem> list)
            : base(list)
        { }
    }

    public class SuggestedJobsEmail
        : MemberAlertEmail
    {
        private readonly int _totalMatches;
        private readonly SuggestedJobsEmailItemCollection _emailItems;
        private const int MaxMatches = 15;

        public static SuggestedJobsEmail Create(ICommunicationUser to, IEnumerable<JobAd> jobAds, int totalMatches)
        {
            var emailItems = CreateEmailItems(jobAds);
            var email = new SuggestedJobsEmail(to, totalMatches, emailItems);
            return email;
        }

        private SuggestedJobsEmail(ICommunicationUser to, int totalMatches, IList<SuggestedJobsEmailItem> emailItems)
            : base(to)
        {
            _totalMatches = totalMatches;
            _emailItems = new SuggestedJobsEmailItemCollection(emailItems);
        }

        protected override void AddProperties(TemplateProperties properties)
        {
            base.AddProperties(properties);
            properties.Add("TotalMatches", _totalMatches);
            properties.Add("Jobs", _emailItems);
            properties.Add("Timestamp", DateTime.Now.ToLongDateString());
            properties.Add("MaxMatches", MaxMatches);
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }

        private static IList<SuggestedJobsEmailItem> CreateEmailItems(IEnumerable<JobAd> jobAds)
        {
            var highlighter = new JobSearchHighlighter(null);
            var emailItems = jobAds.Select(jobAd =>CreateEmailItem(jobAd, highlighter))
                .ToList();

            return emailItems;
        }

        private static SuggestedJobsEmailItem CreateEmailItem(JobAd jobAd, JobSearchHighlighter highlighter)
        {
            var emailItem = new SuggestedJobsEmailItem
            {
                JobAdId = jobAd.Id.ToString("n"),
                Title = highlighter.HighlightTitle(jobAd.Title),
                Location = jobAd.GetLocationDisplayText()
            };

            if (jobAd.Description.Salary != null)
                emailItem.Salary = jobAd.Description.Salary.GetDisplayText();

            emailItem.PostedAge = GetAgeString(DateTime.Now - jobAd.CreatedTime);
            emailItem.PostedDate = jobAd.CreatedTime.ToShortDateString();

            emailItem.JobType = jobAd.Description.JobTypes.GetDisplayText(", ", false, false);

            if (jobAd.Description.Industries != null)
                emailItem.Industry = jobAd.Description.Industries.GetCriteriaIndustriesDisplayText();

            Summarize(jobAd, highlighter, out emailItem.Digest, out emailItem.Fragments);

            return emailItem;
        }

        private static string GetAgeString(TimeSpan age)
        {
            string rez;
            if (age.Days > 0)
            {
                string text = age.Days + (age.Days == 1 ? " day" : " days");

                if (age.Hours > 0)
                    text += string.Format(" {0} {1}", age.Hours, (age.Hours == 1 ? " hour" : " hours"));

                rez = text + " ago";
            }
            else if (age.Hours > 0)
                rez = age.Hours + (age.Hours == 1 ? " hour" : " hours") + " ago";
            else
                rez = "less than an hour ago";

            return rez;
        }

        private static void Summarize(JobAd jobAd, JobSearchHighlighter highlighter, out string digestHtml, out string fragmentsHtml)
        {
            // Show long summary without highlighting.

            string digestText;
            string bodyText;
            highlighter.Summarize(jobAd.Description.Summary, jobAd.Description.BulletPoints, jobAd.Description.Content, true, out digestText, out bodyText);

            digestHtml = HttpUtility.HtmlEncode(digestText).Replace("\x2022", "&#x2022;");
            fragmentsHtml = string.Empty;
        }
    }
}
