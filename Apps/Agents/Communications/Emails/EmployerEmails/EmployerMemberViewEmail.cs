using System;
using System.Collections.Generic;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public abstract class EmployerMemberViewEmail
        : EmployerEmail
    {
        private const string EmployerHidden = "<Employer hidden>";
        private static readonly TimeSpan SearchResultFreshDays = new TimeSpan(28, 0, 0, 0);

        protected EmployerMemberViewEmail(ICommunicationUser to, ICommunicationUser from)
            : base(to, from)
        {
        }

        protected EmployerMemberViewEmail(ICommunicationUser to)
            : base(to)
        {
        }

        protected EmployerMemberEmailViews GetEmailViews(IEnumerable<Guid> ids, EmployerMemberViews views)
        {
            var emailViews = new EmployerMemberEmailViews();
            foreach (var id in ids)
                emailViews[id] = GetEmailView(views[id]);
            return emailViews;
        }

        protected static EmployerMemberEmailView GetEmailView(EmployerMemberView view)
        {
            var result = new EmployerMemberEmailView
            {
                CandidateId = view.Id,
                IsNew = view.CreatedTime.Date > DateTime.Today - SearchResultFreshDays,
            };

            // Only set everything else if there is a current job.

            if (view.Resume != null && !view.Resume.Jobs.IsNullOrEmpty())
            {
                // Add the location if it is set.

                var location = view.Address.Location.ToString();
                if (!string.IsNullOrEmpty(location))
                    result.Location = location;

                // Current job.

                var currentJob = view.Resume.Jobs[0];
                result.CurrentJobTitle = currentJob.Title;
                result.CurrentEmployer = currentJob.Company ?? EmployerHidden;

                // Previous job.

                if (view.Resume.Jobs.Count > 1)
                {
                    var previousJob = view.Resume.Jobs[1];
                    result.PreviousJobTitle = previousJob.Title;
                    result.PreviousEmployer = previousJob.Company ?? EmployerHidden;
                }

                // Salary.

                if (view.DesiredSalary != null)
                    result.IdealSalary = view.DesiredSalary.GetDisplayText();
            }
            return result;
        }
    }
}
