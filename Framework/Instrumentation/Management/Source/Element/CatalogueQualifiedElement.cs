using System.Collections;
using System.Diagnostics;
using LinkMe.Framework.Configuration;

namespace LinkMe.Framework.Instrumentation.Management
{
	public abstract class CatalogueQualifiedElement
		:	QualifiedElement
	{
		#region Constructors

		protected CatalogueQualifiedElement(IQualifiedElement parent, string name)
			:	base(parent, name)
		{
		}

		#endregion

		#region Collections

		protected override Collection CreateCollection(string name)
		{
			switch ( name )
			{
				case Constants.Collections.Namespaces:
					return new Namespaces(this);

				case Constants.Collections.Sources:
					return new Sources(this);

				case Constants.Collections.EventTypes:
					return new EventTypes(this);

				default:
					Debug.Fail("Unrecognised collection name: '" + name + "'.");
					return null;
			}
		}

		protected override void PopulateCollection(string name)
		{
			switch ( name )
			{
				case Constants.Collections.Namespaces:
					((INamespaceParent) this).Namespaces.AddIfNotLoaded(((ICatalogueElement) this).Catalogue.CatalogueConnection.GetNamespaces((INamespaceParent) this));
					break;

				case Constants.Collections.Sources:
					((ISourceParent) this).Sources.AddIfNotLoaded(((ICatalogueElement) this).Catalogue.CatalogueConnection.GetSources((ISourceParent) this));
					break;

				case Constants.Collections.EventTypes:
					((Catalogue) this).EventTypes.AddIfNotLoaded(((ICatalogueElement) this).Catalogue.CatalogueConnection.GetEventTypes((Catalogue) this));
					break;

				default:
					Debug.Fail("Unrecognised collection name: '" + name + "'.");
					break;
			}
		}

		#endregion

		internal void PushEnabledEventsFromRootToThis(string[] events)
		{
			// Build the list of parent namespaces up to the root.

			var namespaces = new ArrayList();

			for (var parent = Parent as Namespace; parent != null; parent = parent.Parent as Namespace)
			{
				namespaces.Add(parent);
			}

			// Wherever these events are enabled on a parent namespace push that "enabled" status down to the
			// child namespaces and sources, starting from the top.

			for (int index = namespaces.Count -1; index >= 0; index--)
			{
				((Namespace)namespaces[index]).PushEnabledEventsToChildren(events);
			}		
		}
	}
}
