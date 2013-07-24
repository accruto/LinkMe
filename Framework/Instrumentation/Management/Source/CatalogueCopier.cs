namespace LinkMe.Framework.Instrumentation.Management
{
	public enum ReadOnlyOption
	{
		Ignore,
		Clear,
		Set
	}

	public static class CatalogueCopier
	{
	    public static void Copy(Catalogue toCatalogue, Catalogue fromCatalogue, ReadOnlyOption readOnlyOption, bool isReferenced)
		{
			// Iterate.

			foreach ( EventType eventType in fromCatalogue.EventTypes )
				CopyElement(toCatalogue, eventType);
			
			// Clone the source namespace, so that CopyElement can modify it by calling PushAllEnabledEventsToChildren().

			foreach ( Namespace ns in fromCatalogue.Namespaces )
				CopyElement(toCatalogue, ns.Clone(fromCatalogue, ns.Name), true);
		}

		public static void Copy(Catalogue toCatalogue, Catalogue fromCatalogue, ReadOnlyOption readOnlyOption)
		{
            Copy(toCatalogue, fromCatalogue, readOnlyOption, false);
		}

        public static void Copy(Catalogue toCatalogue, EventType eventType, ReadOnlyOption readOnlyOption, bool isReferenced)
		{
			CopyElement(toCatalogue, eventType);
		}

        public static void Copy(Catalogue toCatalogue, EventType eventType, ReadOnlyOption readOnlyOption)
		{
			Copy(toCatalogue, eventType, readOnlyOption, false);
		}

		public static void Copy(Catalogue toCatalogue, Namespace ns, ReadOnlyOption readOnlyOption, bool isReferenced, bool iterate)
		{
			INamespaceParent parent = toCatalogue.EnsureNamespaceParent(ns.Parent.FullName);

			// Clone the source namespace, so that CopyElement can modify it by calling PushAllEnabledEventsToChildren().

			CopyElement(parent, ns.Clone(ns.Parent, ns.Name), iterate);
		}

		public static void Copy(Catalogue toCatalogue, Namespace ns, ReadOnlyOption readOnlyOption)
		{
			Copy(toCatalogue, ns, readOnlyOption, false, true);
		}

		public static void Copy(Catalogue toCatalogue, Source source, ReadOnlyOption readOnlyOption, bool isReferenced)
		{
			ISourceParent parent = toCatalogue.EnsureSourceParent(source.Parent.FullName);
			CopyElement(parent, source);
		}

		public static void Copy(Catalogue toCatalogue, Source source, ReadOnlyOption readOnlyOption)
		{
			Copy(toCatalogue, source, readOnlyOption, false);
		}

		private static void CopyElement(INamespaceParent parent, Namespace ns, bool iterate)
		{
			bool mergeEventStates = false;

			// Check whether the namespace already exists.

			Namespace thisNamespace = parent.Namespaces[ns.Name];
			if ( thisNamespace == null )
			{
				// Doesn't exist so create new clone.

				thisNamespace = ns.CloneProperties(parent, ns.Name);
				parent.Namespaces.Add(thisNamespace);
			}
			else
			{
				if (thisNamespace.IsEnsured)
				{
					// Already exists, so copy details if "ensured".
					thisNamespace.CopyProperties(ns);
				}
				else
				{
					// If "ns" has any children then the event states on "thisNamespace" need to be merged from
					// "ns", since we may be adding sources.

					mergeEventStates = (ns.Sources.Count > 0 || ns.Namespaces.Count > 0);
				}
			}

			string[] eventsToUpdate = null;
			if (mergeEventStates)
			{
				// The status of all events that are enabled or mixed on either target or source needs to be
				// merged. To do this first push the "enabled" status right down to the source level then,
				// after the children of the source namespace are added to this (target) namespace update
				// the event states on the namespace from the states of the children.

				eventsToUpdate = Util.AddRangeUnique(
					Util.AddRangeUnique(thisNamespace.EnabledEventsInternal, thisNamespace.MixedEventsInternal),
					Util.AddRangeUnique(ns.EnabledEventsInternal, ns.MixedEventsInternal));

				if (eventsToUpdate != null && eventsToUpdate.Length != 0)
				{
					thisNamespace.PushAllEnabledEventsToChildren();
					ns.PushAllEnabledEventsToChildren();
				}
			}

			// Iterate.

			if ( iterate )
			{
				foreach ( Source source in ns.Sources )
					CopyElement(thisNamespace, source);
				foreach ( Namespace childNamespace in ns.Namespaces )
					CopyElement(thisNamespace, childNamespace, true);
			}

			if (mergeEventStates && eventsToUpdate != null && eventsToUpdate.Length > 0)
			{
				 // Don't go up to the parent at all in this case - we're traversing from bottom up already.

				thisNamespace.UpdateEventStates(eventsToUpdate);
			}
		}

		private static void CopyElement(ISourceParent parent, Source source)
		{
			// Check whether the source already exists.

			Source thisSource = parent.Sources[source.RelativeQualifiedReference];
			if ( thisSource == null )
			{
				// Doesn't exist so create new clone.

				thisSource = source.CloneProperties(parent, source.Name);
				parent.Sources.Add(thisSource);
			}
			else
			{
				// Already exists so copy details.

				thisSource.CopyProperties(source);
			}
		}

        private static void CopyElement(Catalogue catalogue, EventType eventType)
		{
			// Check whether the event already exists.

			EventType thisEventType = catalogue.EventTypes[eventType.Name];
			if ( thisEventType == null )
			{
				// Doesn't exist so create new clone.

				thisEventType = eventType.CloneProperties(catalogue, eventType.Name);
				catalogue.Add(thisEventType);
			}
			else
			{
				// Already exists so copy details.

				thisEventType.CopyProperties(eventType);
			}
		}
	}
}
