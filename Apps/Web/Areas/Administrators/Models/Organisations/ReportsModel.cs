using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Reports.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Web.Areas.Administrators.Models.Organisations
{
    public class ReportsModel
    {
        public Organisation Organisation { get; set; }
        public IList<EmployerReport> Reports { get; set; }
    }

    public class ReportModel
    {
        public Organisation Organisation { get; set; }
        public IAdministrator AccountManager { get; set; }
        public ContactDetails ContactDetails { get; set; }
        public EmployerReport Report { get; set; }
        public bool IncludeCredits { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}