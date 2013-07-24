using System;
using LinkMe.Framework.Configuration;

namespace LinkMe.Framework.Instrumentation.Management
{
	#region CatalogueElements

	[Flags]
	public enum CatalogueElements
	{
		None				= 0x0000,
		Source				= 0x0001,
		Event				= 0x0002,
		Catalogue			= 0x0004,
		Namespace			= 0x0020,
		All = Source
			| Event
			| Catalogue
			| Namespace
	}

	#endregion

	#region ICatalogueElement

	public interface ICatalogueElement
		:	IElement
	{
		Catalogue Catalogue { get; }
		CatalogueElements Element { get; }
	}

	#endregion

	#region ISourceParent

	public interface ISourceParent
		:	IQualifiedElement,
		ICatalogueElement
	{
		Sources Sources { get; }
		void Add(Source source, bool updateParentState);
		void AddIfNotLoaded(Source source);
		void Remove(Source source, bool updateParentState);
		void Replace(Source oldSource, Source newSource, bool updateParentState);
	}

	#endregion

	#region INamespaceParent

	public interface INamespaceParent
		:	IQualifiedElement,
		ICatalogueElement
	{
		Namespaces Namespaces { get; }
		void Add(Namespace ns, bool updateParentState);
		void AddIfNotLoaded(Namespace ns);
		void Remove(Namespace ns, bool updateParentState);
	}

	#endregion
}

namespace LinkMe.Framework.Instrumentation.Management.Connection
{
	#region ICatalogueWriter

	public interface ICatalogueWriter
	{
		void Write(Namespace ns);
		void Write(Source source);
        void Write(EventType eventType);
	}

	#endregion

	#region ICatalogueReader

	public interface ICatalogueReader
	{
		void Read(Catalogue catalogue);
	}

	#endregion

	#region ICatalogueConnection

	public interface ICatalogueConnection
	{
		CommitStatus Commit(Catalogue catalogue);

		// Namespaces

		Namespace[] GetNamespaces(INamespaceParent parent);
		CommitStatus Create(Namespace ns);
		CommitStatus Delete(Namespace ns);
		CommitStatus Update(Namespace ns);

		// Sources

		Source[] GetSources(ISourceParent parent);
		CommitStatus Create(Source source);
		CommitStatus Delete(Source source);
		CommitStatus Update(Source source);

		// EventTypes

		EventType[] GetEventTypes(Catalogue catalogue);
        CommitStatus Create(EventType eventType);
        CommitStatus Delete(EventType eventType);
        CommitStatus Update(EventType eventType);
	}

	#endregion

    #region IRepositoryWriter

	public interface IRepositoryWriter
    {
		void Write(Catalogue catalogue);
		void Write(Namespace ns, bool iterate);
		void Write(Source source);
        void Write(EventType eventType);
		void Close();
	}

	#endregion

	#region IRepositoryReader

	public interface IRepositoryReader
    {
		Catalogue Read();
	}

	#endregion

	#region IRepositoryConnection
	
	public interface IRepositoryConnection
		:	IRepositoryWriter,
			IRepositoryReader
	{
		Catalogue Connect();
	}

	#endregion

	#region IRepositoryCallback

	/// <summary>
	/// This interface is called by a repository when certain changes are committed to the repository. The
	/// client provides an implementation and registers it via the IRepositoryUpdate.RegisterCallback() method.
	/// </summary>
	public interface IRepositoryCallback
	{
		/// <summary>
		/// Called when the enabled/disabled status of a Source or Namespace changes.
		/// </summary>
		/// <param name="elementType">The type of element (Source or Namespace) that has changed.</param>
		/// <param name="fullName">The full name of the Namespace or fully qualified reference of the Source
		/// that has changed.</param>
		/// <remarks>The implementer should ensure that no exceptions are thrown from this method.</remarks>
		void EventStatusChanged(CatalogueElements elementType, string fullName);
		/// <summary>
		/// Called when an Event object changes.
		/// </summary>
		/// <param name="eventName">The name of the event that has changed.</param>
		/// <param name="isEnabled">The new value of Event.IsEnabled.</param>
		void EventChanged(string eventName, bool isEnabled);
		/// <summary>
		/// Called when a new Source object is created.
		/// </summary>
		/// <param name="fullyQualifiedReference">The fully qualified reference of the new Source.</param>
		void SourceCreated(string fullyQualifiedReference);
		/// <summary>
		/// Called when a Source object is deleted.
		/// </summary>
		/// <param name="fullyQualifiedReference">The fully qualified reference of the deleted Source.</param>
		void SourceDeleted(string fullyQualifiedReference);
		/// <summary>
		/// Called when the repository receives a notification of a change, but an exception is thrown in
		/// processing it.
		/// </summary>
		/// <param name="ex">The exception that was thrown.</param>
		/// <remarks>This method is called only when the repository itself throws an exception. Exceptions
		/// raised from methods on this interface are silently ignored.</remarks>
		void HandleNotificationException(Exception ex);
	}

	#endregion

	#region IRepositoryUpdate

	public interface IRepositoryUpdate
	{
		void RegisterCallback(IRepositoryCallback callback);
	}

	#endregion

	#region ICatalogueUpdate

	public interface ICatalogueUpdate
	{
		void EventStatusChanged(CatalogueElements elementType, string fullName);
	}

	#endregion

    #region ICatalogueSearch

    public interface ICatalogueSearch
    {
        Namespace GetNamespace(INamespaceParent parent, string relativeName, bool ignoreCase);
        Source GetSource(ISourceParent parent, string name, bool ignoreCase);
    }

    #endregion

}
