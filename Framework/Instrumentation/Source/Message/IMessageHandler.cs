namespace LinkMe.Framework.Instrumentation.Message
{
	/// <summary>
	/// Interface IMessageHandler
	/// </summary>
	public interface IMessageHandler
	{
		/// <summary>
		/// Gets the Message handler for a particualr event source.
		/// </summary>
		/// <param name="source">The name of the event source.</param>
		/// <returns>Returns the IMessage Handler reference.</returns>
		IMessageHandler GetMessageHandler(string source);

		/// <summary>
		/// Handles the passed Event Message.
		/// </summary>
		/// <param name="message">The Event Message object.</param>
		void HandleEventMessage(EventMessage message);
		
		/// <summary>
		/// Handles the collection of event Messages.
		/// </summary>
        /// <param name="messages">The collection of event messages.</param>
		void HandleEventMessages(EventMessages messages);
	}

    public interface IMessageNotifyHandler
    {
        void HandleEventMessage(EventMessageIdentifier identifier);
        void HandleEventMessages(EventMessageIdentifiers identifiers);
    }
}