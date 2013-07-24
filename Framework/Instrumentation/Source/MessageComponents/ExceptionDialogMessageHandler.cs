using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
    public class ExceptionDialogMessageHandler
        : BaseMessageHandler
    {
        private bool m_dialogShown = false;

        protected override void HandleEventMessage(EventMessage message)
        {
            // Look for errors that have an exception.

            switch (message.Event)
            {
                case Constants.Events.CriticalError:
                case Constants.Events.Error:
                    // Only show one Exception dialog at a time, otherwise things can quickly get out of hand!

                    try
                    {
                        if (!m_dialogShown && message.Exception != null)
                        {
                            m_dialogShown = true;
                            try
                            {
                                new ExceptionDialog(message.Exception, message.Message).ShowDialog();
                            }
                            finally
                            {
                                m_dialogShown = false;
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        // Avoid an infinite loop - don't let exceptions escape from here.
                        System.Diagnostics.Debug.Fail(ex.Message, ex.ToString());
                    }
                    break;
            }
        }
    }
}
