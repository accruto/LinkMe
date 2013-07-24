using System;
using LinkMe.Domain;
using LinkMe.Framework.Utility.Wcf;
using LinkMe.Workflow.CandidateStatusWorkflow;

namespace LinkMe.Apps.Agents.Domain.Roles.Candidates.Commands
{
    public class CandidateStatusCommand
        : ICandidateStatusCommand
    {
        private readonly IChannelManager<IService> _serviceFactory;

        public CandidateStatusCommand(IChannelManager<IService> serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        void ICandidateStatusCommand.ConfirmStatus(Guid candidateId, CandidateStatus status)
        {
            var service = _serviceFactory.Create();
            try
            {
                switch (status)
                {
                    case CandidateStatus.ActivelyLooking:
                        service.OnActivelyLookingConfirmed(candidateId);
                        break;

                    case CandidateStatus.AvailableNow:
                        service.OnAvailableNowConfirmed(candidateId);
                        break;
                }

                _serviceFactory.Close(service);
            }
            catch (Exception)
            {
                _serviceFactory.Abort(service);
                throw;
            }
        }
    }
}
