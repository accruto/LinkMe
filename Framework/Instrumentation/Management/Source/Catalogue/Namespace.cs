using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management.Connection;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Collections;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Instrumentation.Management
{
	public class Namespace
		:	CatalogueQualifiedElement,
			IElementReference,
			IElementIdentification,
			ISourceParent,
			INamespaceParent
	{
		#region Constructors

		internal Namespace(INamespaceParent parent, string name, string[] enabledEvents, string[] mixedEvents, Interner interner)
			:	base(parent, name)
		{
			// Check.

			const string method = ".ctor";
			if ( parent == null )
				throw new NullParameterException(typeof(Namespace), method, "parent");
			if ( !CatalogueName.IsName(name) )
				throw new InvalidParameterFormatException(typeof(Namespace), method, "name", name, Constants.Validation.CompleteNamePattern);

			// Collections.

			InitialiseCollections(2);

			_enabledEvents = Util.InternStringArray(interner, enabledEvents);
			_mixedEvents = Util.InternStringArray(interner, mixedEvents);

			CheckEventOverlap();
		}

		internal Namespace(INamespaceParent parent, string name)
			: this(parent, name, null, null, null)
		{
		}

		#endregion

		#region Properties

		public bool IsEnsured
		{
			get { return _isEnsured; }
			set { _isEnsured = value; }
		}

		// The events listed in EnabledEvents are enabled for all child sources and all child namespaces,
		// recursively. They should NOT also be listed in EnabledEvents for those children.

		// The events listed in MixedEvents are enabled for at least one child source somewhere under this
		// namespace (that is, they are included in EnabledEvents for child source or namespace). This is not
		// used by the Instrumentation runtime, only by EventStatusEditor.

		// Events not listed in either EnabledEvents or MixedEvents may still be enabled on a parent namespace
		// (at any level). Events not included in EnabledEvents at any level are disabled. This way it
		// EventStatusEditor does not have to load all children recursively - it only has to look at this
		// namespace and all parent namespaces up to the root.

		public ReadOnlyStringList EnabledEvents
		{
			get
			{
				return (_enabledEvents == null ? ReadOnlyStringList.Empty : new ReadOnlyStringList(_enabledEvents));
			}
		}

		public ReadOnlyStringList MixedEvents
		{
			get
			{
				return (_mixedEvents == null ? ReadOnlyStringList.Empty : new ReadOnlyStringList(_mixedEvents));
			}
		}

		internal string[] EnabledEventsInternal
		{
			get { return _enabledEvents; }
			set { _enabledEvents = value; }
		}

		internal string[] MixedEventsInternal
		{
			get { return _mixedEvents; }
			set { _mixedEvents = value; }
		}

		#endregion

		#region Relationships

		public Catalogue Catalogue
		{
			get { return ((ICatalogueElement) base.Parent).Catalogue; }
		}

		CatalogueElements ICatalogueElement.Element
		{
			get { return CatalogueElements.Namespace; }
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
			get { return "[Namespace]" + FullName; }
		}

		string IElementIdentification.ElementKey
		{
			get { return Name; }
		}

		string IElementIdentification.ElementType
		{
			get { return CatalogueElements.Namespace.ToString(); }
		}

		string IElementReference.FullyQualifiedReference
		{
			get { return FullName; }
		}

		public new INamespaceParent Parent
		{
			get { return (INamespaceParent) base.Parent; }
		}

        public Searcher GetSearcher()
        {
            return new Searcher(this);
        }

        #endregion

		#region Collections

		public Sources Sources
		{
			get { return (Sources) Collection(Constants.Collections.Sources); }
		}

		public void Add(Source source, bool updateParentState)
		{
			Namespaces.CheckAddElement(source);
			Sources.CheckAddElement(source);

			Sources.Add(source);

			if (updateParentState)
			{
				UpdateStatesOnSourceAdded(source);
			}
		}

		public void AddIfNotLoaded(Source source)
		{
			Sources.AddIfNotLoaded(source);
		}

		public void Remove(Source source, bool updateParentState)
		{
			Sources.Remove(source);

			if (updateParentState)
			{
				UpdateStatesOnSourceRemoved(source);
			}
		}

		public void Replace(Source oldSource, Source newSource, bool updateParentState)
		{
			// Check first.

			if ( ((IElementIdentification) oldSource).ElementKey != ((IElementIdentification) newSource).ElementKey )
			{
				Namespaces.CheckAddElement(newSource);
			}

			Sources.Replace(oldSource, newSource);

			if (updateParentState)
			{
				UpdateStatesOnSourceRemoved(oldSource);
				UpdateStatesOnSourceAdded(newSource);
			}
		}

		public Namespaces Namespaces
		{
			get { return (Namespaces) Collection(Constants.Collections.Namespaces); }
		}

		public void Add(Namespace ns, bool updateParentState)
		{
			Namespaces.CheckAddElement(ns);
			Sources.CheckAddElement(ns);

			Namespaces.Add(ns);

			if (updateParentState)
			{
				UpdateStatesOnNamespaceAdded(ns);
			}
		}

		public void AddIfNotLoaded(Namespace ns)
		{
			Namespaces.AddIfNotLoaded(ns);
		}

		public void Remove(Namespace ns, bool updateParentState)
		{
			Namespaces.Remove(ns);

			if (updateParentState)
			{
				UpdateStatesOnNamespaceRemoved(ns);
			}
		}

		#endregion

		#region Operations

		public Namespace Clone(INamespaceParent parent, string name)
		{
			Namespace ns = parent.Catalogue.CreateNamespace(parent, name);
			ns.CopyMembers(this, false);
			ns.CopyChildCollections(this);
			return ns;
		}

		public void CopyProperties(Namespace ns)
		{
			CopyMembers(ns, true);
		}

		public Namespace CloneProperties(INamespaceParent parent, string name)
		{
			Namespace ns = parent.Catalogue.CreateNamespace(parent, name);
			ns.CopyMembers(this, false);
			return ns;
		}

		// Check for defect 55905 - once this is fixed uncomment the "[Conditional("DEBUG")]" line below.
		// [Conditional("DEBUG")]
		internal void CheckEventOverlap()
		{
			// Check that there is no overlap between "enabled events" and "mixed events".

			string[] overlap = Util.GetArrayIntersection(_enabledEvents, _mixedEvents);
			if (overlap != null && overlap.Length > 0)
			{
				Trace.Fail(string.Format("The following events are listed in both EnabledEvents and MixedEvents"
					+ " for namespace '{0}': {1}", FullName, string.Join(", ", overlap)));
			}
		}

		private void CopyMembers(Namespace ns, bool updateProperty)
		{
			_isEnsured = ns._isEnsured;
			_enabledEvents = (ns._enabledEvents == null ? null : (string[]) ns._enabledEvents.Clone());
			_mixedEvents = (ns._mixedEvents == null ? null : (string[]) ns._mixedEvents.Clone());
			if (updateProperty)
				UpdateProperty();
		}

		private void CopyChildCollections(Namespace ns)
		{
			foreach ( Source source in ns.Sources )
				Sources.Add(source.Clone(this, source.Name));
			foreach ( Namespace childNs in ns.Namespaces )
				Namespaces.Add(childNs.Clone(this, childNs.Name));
		}

		#endregion

		#region Equality

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object other)
		{
			if ( !(other is Namespace) )
				return false;
			return Equals((Namespace) other);
		}

		public bool Equals(Namespace other)
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

		public static bool Equals(Namespace c1, Namespace c2)
		{
			return object.Equals(c1, c2);
		}

		public static bool operator==(Namespace c1, Namespace c2)
		{
			return Equals(c1, c2);
		}

		public static bool operator!=(Namespace c1, Namespace c2)
		{
			return !Equals(c1, c2);
		}

		private bool EqualDetails(Namespace other)
		{
			return (_isEnsured == other._isEnsured
				&& ReadOnlyStringList.Equals(EnabledEvents, other.EnabledEvents)
				&& ReadOnlyStringList.Equals(MixedEvents, other.MixedEvents));
		}

		private bool EqualChildren(Namespace other)
		{
			return Sources == other.Sources
				&& Namespaces == other.Namespaces;
		}

		internal ISourceParent EnsureSourceParent(string fullName)
		{
			// Check for the top.

			if ( fullName.Length == 0 )
				return this;

			// Extract the top name and look for it.

			string parentName;
			string relativeName;
			CatalogueName.SplitRootName(fullName, out parentName, out relativeName);

			// Ensure the container exists.

			Namespace ns = Namespaces[parentName];
			if ( ns == null )
			{
				// Not there so create one.

				ns = Catalogue.CreateNamespace(this, parentName);
				Namespaces.Add(ns);
			}

			// Keep going.

			return ns.EnsureSourceParent(relativeName);
		}

		#endregion

		#region Overrides

		protected override CommitStatus Create()
		{
			CheckEventOverlap();

			return Catalogue.CatalogueConnection.Create(this);
		}

		protected override CommitStatus Update()
		{
			CheckEventOverlap();

			ICatalogueConnection connection = Catalogue.CatalogueConnection;
			CommitStatus status = connection.Update(this);

			// Send a notification if any events have been enabled or disabled.

			if (_eventStatusChanged)
			{
				var catalogueUpdate = connection as ICatalogueUpdate;
				if (catalogueUpdate != null)
				{
					catalogueUpdate.EventStatusChanged(CatalogueElements.Namespace, FullName);
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
		/// Enables the specified events for the namespace, recursively, that is, adds them to EnabledEvents
		/// (if not already present) and removes them from MixedEvents (if present) for this namespace and
		/// also removes them from both EnabledEvents and MixedEvents for any child namespaces and sources.
		/// </summary>
		/// <param name="events">The names of events to enable.</param>
		public void EnableEvents(string[] events)
		{
			if (EnableEventsInternal(events))
			{
				// Update ancestors.

				var ns = Parent as Namespace;
				if (ns != null)
				{
					ns.UpdateEventStatesAndGoUpIfChanged(events);
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

			// Remove the events from MixedEvents (they may have been "mixed" before, but now they're "enabled").

			_mixedEvents = Util.RemoveRange(_mixedEvents, events);
			UpdateProperty();

			// Update children.

			ResetEventsForChildren(events);

			return true;
		}

		/// <summary>
		/// Disables the specified events for the namespace, recursively, that is, removes them from
		/// both EnabledEvents and MixedEvents (if present) both for this namespace and for any child
		/// namespaces and sources.
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

			// Disable the events on this namespace and all children, recrusively.

			ResetEvents(events);

			// Update event states on all ancestors.

			var parentNs = Parent as Namespace;
			if (parentNs != null)
			{
				parentNs.UpdateEventStatesAndGoUpAlways(events);
			}

			_eventStatusChanged = true;
		}

		internal void ResetEvents(string[] events)
		{
			bool changed = false;

			// Remove the events from EnabledEvents and MixedEvents.

			string[] newEnabled = Util.RemoveRange(_enabledEvents, events);
			if (newEnabled != _enabledEvents)
			{
				_enabledEvents = newEnabled;
				changed = true;
			}

			string[] newMixed = Util.RemoveRange(_mixedEvents, events);
			if (newMixed != _mixedEvents)
			{
				_mixedEvents = newMixed;
				changed = true;
			}

			// Reset children as well, unless all of these events were already disabled at this namespace.

			if (changed)
			{
				UpdateProperty();
				ResetEventsForChildren(events);
			}
		}

		/// <summary>
		/// If any of the specified events are in this namespace's EnabledEvents - remove them, but add them to
		/// EnabledEvents for all our children instead.
		/// </summary>
		internal void PushEnabledEventsToChildren(string[] events)
		{
			// Find which of the specified events are in our EnabledEvents.

			if (_enabledEvents == null)
				return;

			var enabled = new ArrayList();
			foreach (string eventName in events)
			{
				if (System.Array.IndexOf(_enabledEvents, eventName) >= 0)
				{
					enabled.Add(eventName);
				}
			}

			if (enabled.Count == 0)
				return; // None of the specified events are in our EnabledEvents.

			// Remove them from EnabledEvents.

			var enabledArray = (string[])enabled.ToArray(typeof(string));
			string[] newEnabled = Util.RemoveRange(_enabledEvents, enabledArray);
			Debug.Assert(newEnabled != _enabledEvents, "newEnabled != _enabledEvents");

			_enabledEvents = newEnabled;
			UpdateProperty();

			// Add them to EnabledEvents for all children, updating their children as well, but not their
			// ancestors.

			foreach (Namespace childNs in Namespaces)
			{
				childNs.EnableEventsInternal(enabledArray);
			}

			foreach (Source source in Sources)
			{
				source.EnableEventsInternal(enabledArray);
			}
		}

		internal void PushAllEnabledEventsToChildren()
		{
			if (_enabledEvents == null)
				return;

			string[] enabledArray = _enabledEvents;
			_enabledEvents = null;
			UpdateProperty();

			// Add them to EnabledEvents for all children, updating their children as well, but not their
			// ancestors.

			foreach (Namespace childNs in Namespaces)
			{
				childNs.EnableEventsInternal(enabledArray);
			}

			foreach (Source source in Sources)
			{
				source.EnableEventsInternal(enabledArray);
			}
		}

		public static bool AddEventStatesForChildNamespaces(INamespaceParent parent, string[] eventsToCheck,
			IList enabled, IList disabled, IList mixed)
		{
			IEnumerator enumerator = parent.Namespaces.GetEnumerator();

			if (enumerator.MoveNext())
			{
				// When processing the first namespace just set our states from its states.

				var childNs = (Namespace)enumerator.Current;
				ReadOnlyStringList childEnabled = childNs.EnabledEvents;
				ReadOnlyStringList childMixed = childNs.MixedEvents;

				foreach (string eventName in eventsToCheck)
				{
					if (childEnabled.Contains(eventName))
					{
						enabled.Add(eventName);
					}
					else if (childMixed.Contains(eventName))
					{
						mixed.Add(eventName);
					}
					else
					{
						disabled.Add(eventName);
					}
				}
			}
			else
				return false; // No namespaces to process.

			while (enumerator.MoveNext())
			{
				// For subsequent children look for states that change our state for an event from enabled or
				// disabled to mixed. Once an event is in "mixed" state that won't change.

				var childNs = (Namespace)enumerator.Current;
				ReadOnlyStringList childEnabled = childNs.EnabledEvents;
				ReadOnlyStringList childMixed = childNs.MixedEvents;

				int index = 0;
				while (index < enabled.Count)
				{
					var eventName = (string)enabled[index];
					if (!childEnabled.Contains(eventName))
					{
						// This event was enabled for all children so far, but not for this one - now it's mixed.

						enabled.RemoveAt(index);
						mixed.Add(eventName);
					}
					else
					{
						index++;
					}
				}

				index = 0;
				while (index < disabled.Count)
				{
					var eventName = (string)disabled[index];
					if (childEnabled.Contains(eventName) || childMixed.Contains(eventName))
					{
						// This event was disabled for all children so far, but not for this one - now it's mixed.

						disabled.RemoveAt(index);
						mixed.Add(eventName);
					}
					else
					{
						index++;
					}
				}
			}

			return true;
		}

		private static void AddEventStatesForSources(ISourceParent parent, IEnumerable<string> eventsToCheck,
			IList enabled, IList disabled, IList mixed, bool setFromFirstSource)
		{
			IEnumerator enumerator = parent.Sources.GetEnumerator();

			if (setFromFirstSource)
			{
				// When processing the first source just set our states from its states.

				if (enumerator.MoveNext())
				{
					var source = (Source)enumerator.Current;
					ReadOnlyStringList childEnabled = source.EnabledEvents;

					foreach (string eventName in eventsToCheck)
					{
						if (childEnabled.Contains(eventName))
						{
							enabled.Add(eventName);
						}
						else
						{
							disabled.Add(eventName);
						}
					}
				}
			}

			while (enumerator.MoveNext())
			{
				// For subsequent children look for states that change our state for an event from enabled or
				// disabled to mixed. Once an event is in "mixed" state that won't change.

				var source = (Source)enumerator.Current;
				ReadOnlyStringList childEnabled = source.EnabledEvents;

				int index = 0;
				while (index < enabled.Count)
				{
					var eventName = (string)enabled[index];
					if (!childEnabled.Contains(eventName))
					{
						// This event was enabled for all children so far, but not for this one - now it's mixed.

						enabled.RemoveAt(index);
						mixed.Add(eventName);
					}
					else
					{
						index++;
					}
				}

				index = 0;
				while (index < disabled.Count)
				{
					var eventName = (string)disabled[index];
					if (childEnabled.Contains(eventName))
					{
						// This event was disabled for all children so far, but not for this one - now it's mixed.

						disabled.RemoveAt(index);
						mixed.Add(eventName);
					}
					else
					{
						index++;
					}
				}
			}		
		}

		internal bool UpdateEventStates(string[] events)
		{
			Debug.Assert(events.Length > 0, "events.Length > 0");

			// The states of these events have changed for one of our child sources or namespaces. Read all
			// children (but not recrusively!) and work out the new states.

			var enabled = new ArrayList();
			var disabled = new ArrayList();
			var mixed = new ArrayList();

			// Process namespaces.

			bool haveNamespaces = AddEventStatesForChildNamespaces(this, events, enabled, disabled, mixed);

			// Process Sources, unless all events are already in "mixed" state. This is similar to Namespaces,
			// except that Sourcs have no "mixed" events.

			if (mixed.Count != events.Length)
			{
				AddEventStatesForSources(this, events, enabled, disabled, mixed, !haveNamespaces);
			}

			// We have the new states, apply them to this namespace.

			bool changed = false;

			if (enabled.Count > 0)
			{
				var enabledArray = (string[])enabled.ToArray(typeof(string));

				string[] newEnabled = Util.AddRangeUnique(_enabledEvents, enabledArray);
				if (newEnabled != _enabledEvents)
				{
					_enabledEvents = newEnabled;
					changed = true;

					// These enabled events might have been mixed before - update MixedEvents.

					_mixedEvents = Util.RemoveRange(_mixedEvents, enabledArray);

					// Enabled events also need to be reset on children - there should be no duplication of
					// events in the EnabledEvents property from any point in the tree up to the top.

					ResetEventsForChildren(newEnabled);
				}
			}

			if (mixed.Count > 0)
			{
				var mixedArray = (string[])mixed.ToArray(typeof(string));

				string[] newMixed = Util.AddRangeUnique(_mixedEvents, mixedArray);
				if (newMixed != _mixedEvents)
				{
					_mixedEvents = newMixed;
					changed = true;

					// These mixed events might have been enabled before - update EnabledEvents.

					_enabledEvents = Util.RemoveRange(_enabledEvents, mixedArray);
				}
			}

			if (disabled.Count > 0)
			{
				var disabledArray = (string[])disabled.ToArray(typeof(string));

				string[] newEnabled = Util.RemoveRange(_enabledEvents, disabledArray);
				if (newEnabled != _enabledEvents)
				{
					_enabledEvents = newEnabled;
					changed = true;
				}

				string[] newMixed = Util.RemoveRange(_mixedEvents, disabledArray);
				if (newMixed != _mixedEvents)
				{
					_mixedEvents = newMixed;
					changed = true;
				}
			}

			// If any event states were actually changed call UpdateProperty() and continue on to the parent.

			if (changed)
			{
				UpdateProperty();
			}

			return changed;
		}

		internal void UpdateEventStatesAndGoUpAlways(string[] events)
		{
			UpdateEventStates(events);

			var parent = Parent as Namespace;
			if (parent != null)
			{
				parent.UpdateEventStatesAndGoUpAlways(events);
			}
		}

		internal void UpdateEventStatesAndGoUpIfChanged(string[] events)
		{
			if (UpdateEventStates(events))
			{
				var parent = Parent as Namespace;
				if (parent != null)
				{
					parent.UpdateEventStatesAndGoUpIfChanged(events);
				}
			}
		}

		private void ResetEventsForChildren(string[] events)
		{
			foreach (Source source in Sources)
			{
				source.ResetEvents(events);
			}

			foreach (Namespace childNs in Namespaces)
			{
				childNs.ResetEvents(events);
			}
		}

		/// <summary>
		/// Returns the names of all events that are effectively enabled for this namespace after parent settings
		/// are taken into account. All events that are not in the returned array are either disabled or "mixed".
		/// </summary>
		private string[] GetEffectiveEnabledEvents()
		{
			ArrayList enabled = (_enabledEvents == null ? new ArrayList() : new ArrayList(_enabledEvents));

			for (var parent = Parent as Namespace; parent != null; parent = parent.Parent as Namespace)
			{
				// Go up the namespace tree, adding enabled events. Events that are not enabled at any
				// level are disabled.

				foreach (string enabledEvent in parent.EnabledEvents)
				{
					Debug.Assert(!enabled.Contains(enabledEvent), string.Format("Event '{0}' is enabled both"
						+ " on parent namespace '{1}' and on namespace '{2}' (or one of the namespaces in-between).",
						enabledEvent, parent.FullName, FullName));

					enabled.Add(enabledEvent);
				}
			}

			return (string[])enabled.ToArray(typeof(string));
		}

		private void UpdateStatesOnSourceAdded(Source source)
		{
			// If the source has some enabled events adding it may have changed our own event states.

			if (source.EnabledEventsInternal != null)
			{
				// Ensure that this source doesn't duplicate any enabled events alredy enabled on this
				// namespace or a parent namespace. At the same time ensure that events disabled on the
				// source, but enabled on this namespace (or a parent namespace) remain disabled, ie. they
				// become "mixed" on the namespace.

				string[] effectiveEnabled = GetEffectiveEnabledEvents();
				string[] disableOnSource = Util.RemoveRange(effectiveEnabled, source.EnabledEventsInternal);

				source.ResetEvents(effectiveEnabled);

				// If there are still events enabled explicitly on the source we need to update our state.

				if (source.EnabledEventsInternal != null)
				{
					UpdateEventStatesAndGoUpIfChanged(source.EnabledEventsInternal);
				}

				// Do this even if source.EnabledEventsInternal is null, because we need to update ancestors.

				if (disableOnSource != null && disableOnSource.Length > 0)
				{
					source.DisableEvents(disableOnSource);
				}
			}
		}

		private void UpdateStatesOnSourceRemoved(Source source)
		{
			// If the source had some enabled events removing it may have changed our own event states.

			if (source.EnabledEventsInternal != null)
			{
				UpdateEventStatesAndGoUpIfChanged(source.EnabledEventsInternal);
			}
		}

		private void UpdateStatesOnNamespaceAdded(Namespace ns)
		{
			// If the namespace has some enabled or mixed events adding it may have changed our own event states.

			if (ns._enabledEvents != null || ns._mixedEvents != null)
			{
				// Ensure that the child namespace doesn't duplicate any enabled events alredy enabled on this
				// namespace or a parent namespace. At the same time ensure that events disabled on the
				// child namespace, but enabled on this namespace (or a parent namespace) remain disabled, ie.
				// they become "mixed" on this namespace.

				string[] effectiveEnabled = GetEffectiveEnabledEvents();
				string[] disableOnChild = Util.RemoveRange(effectiveEnabled, ns._enabledEvents);

				ns.ResetEvents(effectiveEnabled);

				// If there are still events enabled explicitly or "mixed" on the child namespace we need to
				// update our state.

				string[] childEnabledAndMixed = Util.AddRangeUnique(ns._enabledEvents, ns._mixedEvents);
				if (childEnabledAndMixed != null && childEnabledAndMixed.Length > 0)
				{
					UpdateEventStatesAndGoUpIfChanged(childEnabledAndMixed);
				}

				// Do this even if ns._enabledEvents is null, because we need to update ancestors.

				if (disableOnChild != null && disableOnChild.Length > 0)
				{
					ns.DisableEvents(disableOnChild);
				}
			}
		}

		private void UpdateStatesOnNamespaceRemoved(Namespace ns)
		{
			// If the namespace had some enabled or mixed events removing it may have changed our own event states.

			if (ns._enabledEvents != null || ns._mixedEvents != null)
			{
				UpdateEventStatesAndGoUpIfChanged(Util.AddRangeUnique(ns._enabledEvents, ns._mixedEvents));
			}
		}

		#endregion

		#region Ensure

		internal INamespaceParent EnsureNamespaceParent(string fullName)
		{
			// Check for the top.

			if ( fullName.Length == 0 )
				return this;

			// Extract the top name and look for it.

			string namespaceName;
			string relativeName;
			CatalogueName.SplitRootName(fullName, out namespaceName, out relativeName);

			// Check for a namespace.

			Namespace ns = Namespaces[namespaceName];
			if ( ns == null )
			{
				// Not there so create one.

				ns = Catalogue.CreateNamespace(this, namespaceName);
				Namespaces.Add(ns);
			}

			// Keep going.

			return ns.EnsureNamespaceParent(relativeName);
		}

		#endregion

		#region Members

		private bool _isEnsured;
		private string[] _enabledEvents;
		private string[] _mixedEvents;
		private bool _eventStatusChanged;

		#endregion
	}

	public class Namespaces
		:	SortedElementCollection
	{
		internal Namespaces(Element parent)
			:	base(parent)
		{
		}

		public new Namespace this[int index]
		{
			get { return (Namespace) base[index]; }
		}

		public new Namespace this[string name]
		{
			get { return (Namespace) base[name]; }
		}

		internal new Namespace GetItem(string name, bool ignoreCase, bool allowLoading)
		{
			return (Namespace)base.GetItem(name, ignoreCase, allowLoading);
		}

		protected override string Name
		{
			get { return Constants.Collections.Namespaces; }
		}

		internal new void CheckAddElement(Element element)
		{
			base.CheckAddElement(element);
		}

		internal void Add(Namespace ns)
		{
			base.Add(ns);
		}

		internal void AddIfNotLoaded(Namespace ns)
		{
			base.AddIfNotLoaded(ns);
		}

		internal void AddIfNotLoaded(Namespace[] namespaces)
		{
			base.AddIfNotLoaded(namespaces);
		}

		internal void Remove(Namespace ns)
		{
			base.Remove(ns);
		}

		internal void Replace(Namespace oldNs, Namespace newNs)
		{
			base.Replace(oldNs, newNs);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object other)
		{
			if ( !(other is Namespaces) )
				return false;
			return base.Equals(other);
		}

		internal bool Equals(Namespaces other)
		{
			return base.Equals(other);
		}

		protected override Element CreateElement(Element parent, string key)
		{
			return ((ICatalogueElement) parent).Catalogue.CreateNamespace((INamespaceParent) parent, key);
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
