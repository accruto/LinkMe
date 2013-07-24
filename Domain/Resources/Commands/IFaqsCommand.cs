using System;

namespace LinkMe.Domain.Resources.Commands
{
    public interface IFaqsCommand
    {
        void MarkHelpful(Guid faqId);
        void MarkNotHelpful(Guid faqId);
    }
}
