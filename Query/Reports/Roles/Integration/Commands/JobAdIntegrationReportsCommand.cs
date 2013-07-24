using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Query.Reports.Roles.Integration.Commands
{
    public class JobAdIntegrationReportsCommand
        : IJobAdIntegrationReportsCommand
    {
        private readonly IIntegrationReportsRepository _repository;

        public JobAdIntegrationReportsCommand(IIntegrationReportsRepository repository)
        {
            _repository = repository;
        }

        void IJobAdIntegrationReportsCommand.CreateJobAdIntegrationEvent(JobAdIntegrationEvent evt)
        {
            evt.Prepare();
            evt.Validate();
            _repository.CreateJobAdIntegrationEvent(evt);
        }
    }
}
