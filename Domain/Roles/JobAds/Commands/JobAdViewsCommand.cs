using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public class JobAdViewsCommand
        : IJobAdViewsCommand
    {
        private readonly IJobAdViewsRepository _repository;

        public JobAdViewsCommand(IJobAdViewsRepository repository)
        {
            _repository = repository;
        }

        void IJobAdViewsCommand.ViewJobAd(Guid? viewerId, Guid jobAdId)
        {
            var viewing = new JobAdViewing
            {
                ViewerId = viewerId,
                JobAdId = jobAdId
            };

            viewing.Prepare();
            viewing.Validate();
            _repository.CreateJobAdViewing(viewing);
        }
    }
}
