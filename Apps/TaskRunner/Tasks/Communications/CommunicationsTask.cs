using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Tasks;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.TaskRunner.Tasks.Communications
{
    public abstract class CommunicationsTask
        : Task
    {
        protected readonly IEmailsCommand _emailsCommand;

        protected CommunicationsTask(EventSource eventSource, IEmailsCommand emailsCommand)
            : base(eventSource)
        {
            _emailsCommand = emailsCommand;
        }
    }
}
