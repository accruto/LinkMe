using System.Collections;
using System.Collections.Generic;
using System.Threading;
using LinkMe.Framework.Instrumentation.MessageComponents;

namespace LinkMe.Framework.Instrumentation.Message
{
    public abstract class EventMessageIdentifier
    {
    }

    public class EventMessageIdentifiers
        : IEnumerable<EventMessageIdentifier>
    {
        private readonly IList<EventMessageIdentifier> m_identifiers = new List<EventMessageIdentifier>();

        IEnumerator<EventMessageIdentifier> IEnumerable<EventMessageIdentifier>.GetEnumerator()
        {
            return m_identifiers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_identifiers.GetEnumerator();
        }

        public int Count
        {
            get { return m_identifiers.Count; }
        }

        public void Add(EventMessageIdentifier identifier)
        {
            m_identifiers.Add(identifier);
        }
    }

	/// <summary>
	/// Represents a component that can read messages from a repository and write them to an IMessageHandler.
	/// </summary>
	public interface IMessageReader
	{
		/// <summary>
		/// Raised when all the existing messages in the repository have been read by the Start() method.
		/// If the reader is monitoring the repository and new messages are added this event should be raised
		/// again once all the new messages have been read.
		/// </summary>
		event System.EventHandler ExistingMessagesRead;
		/// <summary>
		/// Raised when an exception occurs on the thread that reads the messages.
		/// </summary>
		event ThreadExceptionEventHandler ReaderThreadException;

		/// <summary>
		/// Initialise the message reader to send messages to the specified IMessageHandler.
		/// </summary>
		void SetMessageHandler(IMessageHandler handler);
		/// <summary>
		/// Read messages in the repository, blocking until all messages have been read.
		/// </summary>
		void ReadAll();
		/// <summary>
		/// Start reading messages in the repository asynchronously. After all existing messages have
		/// been read fire the ExistingMessagesRead event and then continue monitoring the repository for
		/// new messages (if supported).
		/// </summary>
		void Start(Filters filters);
		/// <summary>
		/// Stop reading messages from the repository. This method should block until the reader has truly
		/// stopped and is ready to start reading again.
		/// </summary>
		void Stop();
		/// <summary>
		/// Stop reading messages from the repository, but remember the "position" of the last read message.
		/// </summary>
		void Pause();
		/// <summary>
		/// Continue reading messages from the position where the reader was paused.
		/// </summary>
		void Resume();
	}

    public interface IMessageNotifyReader
    {
        /// <summary>
        /// Reads a single message for the given identifier.
        /// </summary>
        EventMessage GetMessage(EventMessageIdentifier identifier);
        /// <summary>
        /// Reads a range of messages from the start identifier to the end identiifer. The returned
        /// message are not ordered - it is up to the caller to order them.
        /// </summary>
        EventMessages GetMessages(EventMessageIdentifier startIdentifier, EventMessageIdentifier endIdentifier);
        EventMessageIdentifier GetIdentifier(EventMessage message);
    }

	/// <summary>
	/// This interface is implemented by message readers that support removing message from the repository
	/// (eg. MSMQ).
	/// </summary>
	public interface IRemoveMessages
	{
		/// <summary>
		/// Set to true to delete the messages from the repository once they have been read.
		/// </summary>
		bool RemoveWhenReading { get; set; }
		/// <summary>
		/// Remove all messages from the repository.
		/// </summary>
		void RemoveAll();
	}

	/// <summary>
	/// This interface is implemented by message readers that support retrieving a count of the total number of
	/// messages in the repository without retrieving the messages themselves.
	/// </summary>
	public interface IMessageCount
	{
		/// <summary>
		/// Gets the total number of messages in the repository.
		/// </summary>
		int GetMessageCount();
	}
}
