using System;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Domain.Roles.Networking.Commands
{
    public class NetworkingCommand
        : INetworkingCommand
    {
        private readonly INetworkingRepository _repository;

        public NetworkingCommand(INetworkingRepository repository)
        {
            _repository = repository;
        }

        void INetworkingCommand.CreateFirstDegreeLink(Guid fromId, Guid toId)
        {
            _repository.CreateFirstDegreeLink(fromId, toId);

            // Fire events.

            var handlers = FirstDegreeContactMade;
            if (handlers != null)
                handlers(this, new FirstDegreeContactEventArgs(fromId, toId));
        }

        void INetworkingCommand.DeleteFirstDegreeLink(Guid fromId, Guid toId)
        {
            _repository.DeleteFirstDegreeLink(fromId, toId);
        }

        void INetworkingCommand.IgnoreSecondDegreeLink(Guid fromId, Guid toId)
        {
            _repository.IgnoreSecondDegreeLink(fromId, toId);
        }

        [Publishes(PublishedEvents.FirstDegreeContactMade)]
        public event EventHandler<FirstDegreeContactEventArgs> FirstDegreeContactMade;
    }
}