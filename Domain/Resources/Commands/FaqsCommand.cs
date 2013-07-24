using System;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Domain.Resources.Commands
{
    public class FaqsCommand
        : IFaqsCommand
    {
        private readonly IResourcesRepository _repository;

        public FaqsCommand(IResourcesRepository repository)
        {
            _repository = repository;
        }

        void IFaqsCommand.MarkHelpful(Guid faqId)
        {
            _repository.MarkHelpful(faqId);

            var handlers = FaqMarked;
            if (handlers != null)
                handlers(this, new ResourceEventArgs(faqId));
        }

        void IFaqsCommand.MarkNotHelpful(Guid faqId)
        {
            _repository.MarkNotHelpful(faqId);

            var handlers = FaqMarked;
            if (handlers != null)
                handlers(this, new ResourceEventArgs(faqId));
        }

        [Publishes(PublishedEvents.FaqMarked)]
        public event EventHandler<ResourceEventArgs> FaqMarked;
    }
}
