using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
    internal class CombinedMessageHandler
        : BaseMessageHandler
    {
        private readonly IMessageHandler[] _registeredMessageHandlers;
        private readonly IMessageHandler _messageHandler;

        public CombinedMessageHandler(IMessageHandler[] registeredMessageHandlers, IMessageHandler messageHandler)
        {
            const string method = ".ctor";
            if (registeredMessageHandlers == null)
                throw new NullParameterException(typeof(CombinedMessageHandler), method, "registeredMessageHandlers");
            if (messageHandler == null)
                throw new NullParameterException(typeof(CombinedMessageHandler), method, "messageHandler");

            _registeredMessageHandlers = registeredMessageHandlers;
            _messageHandler = messageHandler;
        }

        protected override void HandleEventMessage(EventMessage message)
        {
            // Pass to the real message handler first.

            _messageHandler.HandleEventMessage(message);

            // Now pass to registered message handlers.

            foreach (var registeredMessageHandler in _registeredMessageHandlers)
                registeredMessageHandler.HandleEventMessage(message);
        }
    }
}
