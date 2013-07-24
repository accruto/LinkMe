using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Web.Areas.Integration.Controllers
{
    public abstract class Report
    {
        private readonly IList<string> _errors = new List<string>();

        public IList<string> Errors
        {
            get { return _errors; }
        }
    }

    public class PostReport
        : Report
    {
        private readonly IList<Guid> _processedJobAdIds = new List<Guid>();

        public int Failed { get; set; }
        public int Posted { get; set; }
        public int Closed { get; set; }
        public int Updated { get; set; }

        public IList<Guid> ProcessedJobAdIds
        {
            get { return _processedJobAdIds; }
        }
    }

    public class CloseReport
        : Report
    {
        public int Failed { get; set; }
        public int Closed { get; set; }
    }

    public interface IJobAdPostsManager
    {
        CloseReport CloseJobAds(Guid integratorUserId, IEmployer jobPoster, IEnumerable<string> externalReferenceIds);
        PostReport PostJobAds(IntegratorUser integratorUser, IEmployer jobPoster, IEmployer integratorJobPoster, IEnumerable<JobAd> jobAds, bool closeAllOtherJobAds);
    }
}
