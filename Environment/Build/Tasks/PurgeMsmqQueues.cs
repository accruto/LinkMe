using System;
using System.Messaging;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks
{
    public class PurgeMsmqQueues
        : Task
    {
        public override bool Execute()
        {
            try
            {
                Log.LogMessage("Purging all private queues.");

                var queues = MessageQueue.GetPrivateQueuesByMachine(".");
                foreach (var queue in queues)
                    Purge(queue);

                Log.LogMessage("Purging the dead-letter queues.");

                Purge(new MessageQueue(@"FormatName:DIRECT=OS:.\SYSTEM$;DeadLetter"));
                Purge(new MessageQueue(@"FormatName:DIRECT=OS:.\SYSTEM$;DeadXact"));

                return true;
            }
            catch (Exception e)
            {
                Log.LogErrorFromException(e);
                return false;
            }
        }

        private void Purge(MessageQueue queue)
        {
            Log.LogMessage("Purging '{0}'.", queue.FormatName);

            var count = 0;
            try
            {
                var message = queue.Receive(TimeSpan.Zero);
                while (message != null)
                {
                    ++count;
                    message = queue.Receive(TimeSpan.Zero);
                }
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode != MessageQueueErrorCode.IOTimeout)
                    throw;

                Log.LogMessage("Purged {0} message{1} from '{2}'.", count, count == 1 ? "" : "s", queue.FormatName);
            }
        }
    }
}
