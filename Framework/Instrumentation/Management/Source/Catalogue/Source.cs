using System.Collections;
using System.Diagnostics;
using System.IO;

using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management.Connection;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Collections;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Instrumentation.Management
{
	public class Source
		:	CatalogueQualifiedElement,
			ICatalogueElement,
			IElementReference,
			IElementIdentification
	{
		#region Constructors

		internal Source(IQualifiedElement parent, string name, string[] enabledEvents, Interner interner)
			:	base(parent, name)
		{
			// Check.

			const string method = ".ctor";
			if ( parent == null )
				throw new NullParameterException(typeof(Source), method, "parent");
			if ( !CatalogueName.IsName(name) )
				throw new InvalidParameterFormatException(typeof(Source), method, "name", name, Constants.Validation.CompleteNamePattern);

			// Assign.

			_enabledEvents = Util.InternStringArray(interner, enabledEvents);
		}

		internal Source(IQualifiedElement parent, string name)
			:	this(parent, name, null, null)
		{
		}

		#endregion

		#region Properties

		public string FullyQualifiedReference
		{
			get { return CatalogueName.GetQualifiedReference(Parent.FullName, Name); }
		}

		public string RelativeQualifiedReference
		{
			get { return Name; }
		}

		/// <summary>
		/// The list of events enabled for this source. Events not included in this list may still be enabled
		/// on a parent namespace (at any level). Events not included in EnabledEvents at any level are disabled.
		/// </summary>
		public ReadOnlyStringList EnabledEvents
		{
			get
			{
				return (_enabledEvents == null ? ReadOnlyStringList.Empty : new ReadOnlyStringList(_enabledEvents));
			}
		}

		internal string[] EnabledEventsInternal
		{
			get { return _enabledEvents; }
			set { _enabledEvents = value; }
		}

		#endregion

		#region Relationships

		public Catalogue Catalogue
		{
			get { return ((ICatalogueElement) base.Parent).Catalogue; }
		}

		CatalogueElements ICatalogueElement.Element
		{
			get { return CatalogueElements.Source; }
		}

		string IElementIdentification.CataloguePath
		{
			get
			{
				return ((IElementIdentification)Catalogue).CataloguePath + ", "
					+ ((IElementIdentification)this).ElementPath;
			}
		}

		string IElementIdentification.ElementPath
		{
			get { return "[Source]" + FullyQualifiedReference; }
		}

		string IElementIdentification.ElementKey
		{
			get { return RelativeQualifiedReference; }
		}

		string IElementIdentification.ElementType
		{
			get { return CatalogueElements.Source.ToString(); }
		}

		string IElementReference.FullyQualifiedReference
		{
			get { return FullyQualifiedReference; }
		}

		public new ISourceParent Parent
		{
			get { return (ISourceParent) base.Parent; }
		}

		#endregion

		#region Operations

		public Source Clone(ISourceParent parent, string name)
		{
			Source source = parent.Catalogue.CreateSource(parent, name);
			source.CopyMembers(this, false);
			CopyChildCollections(this);
			return source;
		}

		public void CopyProperties(Source source)
		{
			CopyMembers(source, true);
		}

		public Source CloneProperties(ISourceParent parent, string name)
		{
			Source source = parent.Catalogue.CreateSource(parent, name);
			source.CopyMembers(this, false);
			return source;
		}

		private void CopyMembers(Source source, bool updateProperty)
		{
			_enabledEvents = (source._enabledEvents == null ? null : (string[]) source._enabledEvents.Clone());
			if (updateProperty)
				UpdateProperty();
		}

		private static void CopyChildCollections(Source source)
		{
		}

		#endregion

		#region Equality

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object other)
		{
			if ( !(other is Source) )
				return false;
			return Equals((Source) other);
		}

		public bool Equals(Source other)
		{
			if ( other == null )
				return false;

			// Check base.

			if ( !base.Equals(other) )
				return false;

			// Check details.

			if ( !EqualDetails(other) )
				return false;

			// Check children.

			return EqualChildren(other);
		}

		public static bool Equals(Source c1, Source c2)
		{
			return object.Equals(c1, c2);
		}

		public static bool operator==(Source c1, Source c2)
		{
			return Equals(c1, c2);
		}

		public static bool operator!=(Source c1, Source c2)
		{
			return !Equals(c1, c2);
		}

		private bool EqualDetails(Source other)
		{
			return ReadOnlyStringList.Equals(EnabledEvents, other.EnabledEvents);
		}

		private static bool EqualChildren(Source other)
		{
			return true;
		}

		#endregion

		#region Overrides

		protected override CommitStatus Create()
		{
			return Catalogue.CatalogueConnection.Create(this);
		}

		protected override CommitStatus Update()
		{
			ICatalogueConnection connection = Catalogue.CatalogueConnection;
			CommitStatus status = connection.Update(this);

			// Send a notification if any events have been enabled or disabled.

			if (_eventStatusChanged)
			{
				var catalogueUpdate = connection as ICatalogueUpdate;
				if (catalogueUpdate != null)
				{
					catalogueUpdate.EventStatusChanged(CatalogueElements.Source, FullyQualifiedReference);
				}

				_eventStatusChanged = false;
			}

			return status;
		}

		protected override CommitStatus Delete()
		{
			return Catalogue.CatalogueConnection.Delete(this);
		}

		#endregion

		#region Helpers

		/// <summary>
		/// Returns the names of all events that are effectively enabled for this source after parent settings
		/// are taken into account. All events that are not in the returned array are disabled.
		/// </summary>
		public string[] GetEffectiveEnabledEvents()
		{
			ArrayList enabled = (_enabledEvents == null ? new ArrayList() : new ArrayList(_enabledEvents));

			for (var parent = Parent as Namespace; parent != null; parent = parent.Parent as Namespace)
			{
				// Go up the namespace tree, adding enabled events. Events that are not enabled at any
				// level are disabled.

				foreach (string enabledEvent in parent.EnabledEvents)
				{
					Debug.Assert(!enabled.Contains(enabledEvent), string.Format("Event '{0}' is enabled both"
						+ " on parent namespace '{1}' and on source '{2}' (or one of the namespaces in-between).",
						enabledEvent, parent.FullName, FullName));

					enabled.Add(enabledEvent);
				}
			}

			return (string[])enabled.ToArray(typeof(string));
		}

		/// <summary>
		/// Enables the specified events for the source, that is, adds them to EnabledEvents (if not already
		/// present) and updates EnabledEvents and MixedEvents on the parent namespace.
		/// </summary>
		/// <param name="events">The names of events to enable.</param>
		public void EnableEvents(string[] events)
		{
			if (EnableEventsInternal(events))
			{
				// Update ancestors.

				var parent = Parent as Namespace;
				if (parent != null)
				{
					parent.UpdateEventStatesAndGoUpIfChanged(events);
				}

				_eventStatusChanged = true;
			}
		}

		/// <summary>
		/// Enable the specified events without updating ancestors.
		/// </summary>
		internal bool EnableEventsInternal(string[] events)
		{
			string[] newEnabled = Util.AddRangeUnique(_enabledEvents, events);
			if (newEnabled == _enabledEvents)
				return false; // No change.

			_enabledEvents = newEnabled;
			UpdateProperty();

			return true;
		}

		/// <summary>
		/// Disables the specified events for the source, that is, removes them from EnabledEvents (if present)
		/// and updates EnabledEvents and MixedEvents on the parent namespace.
		/// </summary>
		/// <param name="events">The names of events to disable.</param>
		public void DisableEvents(string[] events)
		{
			// Work out whether all the events being disabled are explicitly enabled on this source. If so,
			// all we need to do is removed them from EnabledEvents. If not then we need to disable them on the
			// parent namespaces where they are enabled and enable them for all the other children of those
			// namespaces (so that the net effect is to disable them only on this source).

			if (!Util.ArrayContainsEntireArray(_enabledEvents, events))
			{
				PushEnabledEventsFromRootToThis(events);
			}

			// Remove the events from our own EnabledEvents list (if present).

			string[] newEnabled = Util.RemoveRange(_enabledEvents, events);

			if (newEnabled != _enabledEvents)
			{
				_enabledEvents = newEnabled;
				UpdateProperty();
			}

			// Update event states on all ancestors.

			var parentNs = Parent as Namespace;
			if (parentNs != null)
			{
				parentNs.UpdateEventStatesAndGoUpAlways(events);
			}

			_eventStatusChanged = true;
		}

		/// <summary>
		/// Removes the specified events from EnabledEvents, so that the parent namespace settings are used
		/// for those events.
		/// </summary>
		/// <param name="events">The names of events to reset.</param>
		internal void ResetEvents(string[] events)
		{
			string[] newEnabled = Util.RemoveRange(_enabledEvents, events);
			if (newEnabled != _enabledEvents)
			{
				_enabledEvents = newEnabled;
				UpdateProperty();
			}
		}

		#endregion

		#region Members

		private string[] _enabledEvents;
		private bool _eventStatusChanged;

		#endregion
	}

	public class Sources
		:	SortedElementCollection
	{
		internal Sources(Element parent)
			:	base(parent)
		{
		}

		public new Source this[int index]
		{
			get { return (Source) base[index]; }
		}

		public new Source this[string relativeQualifiedReference]
		{
			get { return (Source) base[relativeQualifiedReference]; }
		}

		internal new Source GetItem(string relativeQualifiedReference, bool ignoreCase, bool allowLoading)
		{
			return (Source)base.GetItem(relativeQualifiedReference, ignoreCase, allowLoading);
		}

		protected override string Name
		{
			get { return Constants.Collections.Sources; }
		}

		internal new void CheckAddElement(Element element)
		{
			base.CheckAddElement(element);
		}

		internal void Add(Source source)
		{
			base.Add(source);
		}

		internal void AddIfNotLoaded(Source source)
		{
			base.AddIfNotLoaded(source);
		}

		internal void AddIfNotLoaded(Source[] sources)
		{
			base.AddIfNotLoaded(sources);
		}

		internal void Remove(Source source)
		{
			base.Remove(source);
		}

		internal void Replace(Source oldSource, Source newSource)
		{
			base.Replace(oldSource, newSource);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object other)
		{
			if ( !(other is Sources) )
				return false;
			return base.Equals(other);
		}

		internal bool Equals(Sources other)
		{
			return base.Equals(other);
		}

		protected override Element CreateElement(Element parent, string key)
		{
			var catalogueName = new CatalogueName(key);
			return ((ICatalogueElement) parent).Catalogue.CreateSource((ISourceParent) parent, catalogueName.Name);
		}

		internal void Write(BinaryWriter writer)
		{
			((IBinarySerializable) this).Write(writer);
		}

		internal void Read(BinaryReader reader)
		{
			((IBinarySerializable) this).Read(reader);
		}
	}
}
