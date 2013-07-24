using System;
using System.Collections;
using System.Diagnostics;
using System.Management;
using System.Text;
using LinkMe.Framework.Utility.Collections;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Wmi;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;

namespace LinkMe.Framework.Instrumentation.Connection.Wmi
{
	public class WmiCatalogueConnection
		:	CatalogueConnection,
			ICatalogueSearch,
			ICatalogueUpdate
	{
		#region Nested types

		private class NamespaceDetails
		{
			internal string[] _enabledEvents;
			internal string[] _mixedEvents;
		}

		#endregion

		public WmiCatalogueConnection(ManagementScope scope)
		{
			if (scope == null)
				throw new NullParameterException(GetType(), ".ctor", "scope");

			_scope = scope;
		}

		#region Namespace

		public override Namespace[] GetNamespaces(INamespaceParent parent)
		{
			// Search.

			var namespaces = new ArrayList();
			var searcher = new ManagementObjectSearcher(_scope, GetChildQuery(Constants.Wmi.Namespace.Class, parent.FullName));

			using (ManagementObjectCollection configs = searcher.Get())
			{
				foreach ( ManagementObject config in configs )
					namespaces.Add(CreateNamespace(parent, config));
			}

			return (Namespace[]) namespaces.ToArray(typeof(Namespace));
		}

		public override CommitStatus Create(Namespace ns)
		{
			UpdateNamespace(ns);
			return CommitStatus.Continue;
		}

		public override CommitStatus Delete(Namespace ns)
		{
			DeleteNamespace(ns);
			return CommitStatus.DoNotCommitChildren;
		}

		public override CommitStatus Update(Namespace ns)
		{
			UpdateNamespace(ns);
			return CommitStatus.Continue;
		}

		private static Namespace CreateNamespace(INamespaceParent parent, ManagementBaseObject config)
		{
			// Get the properties.

			var name = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.NameProperty);

			var enabledEvents = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.Namespace.EnabledEventsProperty);
			var mixedEvents = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.Namespace.MixedEventsProperty);

            string[] enabledEventsArray = SplitString(enabledEvents, Constants.Module.ListSeparator);
            string[] mixedEventsArray = SplitString(mixedEvents, Constants.Module.ListSeparator);

			return parent.Catalogue.CreateNamespace(parent, name, enabledEventsArray, mixedEventsArray);
		}

		private void UpdateNamespace(Namespace ns)
		{
			// Try to get an existing source.

			ManagementObject config = WmiUtil.GetObject(_scope, GetPath(ns));
			if ( config == null )
			{
				// Not found, so create a new instance.

				config = CreateInstance(Constants.Wmi.Namespace.Class);
				WmiUtil.SetPropertyValue(config, Constants.Wmi.ParentProperty, ns.Parent.FullName);
				WmiUtil.SetPropertyValue(config, Constants.Wmi.NameProperty, ns.Name);
			}

			// Update the rest of the properties and save.

            string enabledEvents = JoinString(ns.EnabledEvents, Constants.Module.ListSeparator);
            string mixedEvents = JoinString(ns.MixedEvents, Constants.Module.ListSeparator);

			WmiUtil.SetPropertyValue(config, Constants.Wmi.Namespace.EnabledEventsProperty, enabledEvents);
			WmiUtil.SetPropertyValue(config, Constants.Wmi.Namespace.MixedEventsProperty, mixedEvents);
			config.Put();
		}

		private void DeleteNamespace(Namespace ns)
		{
			// Get the configuration for this source.

			ManagementObject config = WmiUtil.GetObject(_scope, GetPath(ns));
			if ( config != null )
				DeleteNamespace(config);
		}

		private void DeleteNamespace(ManagementObject config)
		{
			// Delete all child namespaces first.

			var searcher = new ManagementObjectSearcher(_scope, GetChildQuery(Constants.Wmi.Namespace.Class, config));
			
			using (ManagementObjectCollection childConfigs = searcher.Get())
			{
				foreach ( ManagementObject childConfig in childConfigs )
					DeleteNamespace(childConfig);
			}

			// Delete sources.

			searcher = new ManagementObjectSearcher(_scope, GetChildQuery(Constants.Wmi.Source.Class, config));

			using (ManagementObjectCollection childConfigs = searcher.Get())
			{
				foreach ( ManagementObject childConfig in childConfigs )
					DeleteSource(childConfig);
			}

			// Delete the namespace itself.

			config.Delete();
		}

		private static string GetPath(Namespace ns)
		{
			return GetPath(Constants.Wmi.Namespace.Class, ns.Parent.FullName, ns.Name);
		}

		#endregion

		#region Source

		public override Source[] GetSources(ISourceParent parent)
		{
			// Search.

			var sources = new ArrayList();
			var searcher = new ManagementObjectSearcher(_scope, GetChildQuery(Constants.Wmi.Source.Class, parent.FullName));
			
			using (ManagementObjectCollection configs = searcher.Get())
			{
				foreach ( ManagementObject config in configs )
					sources.Add(CreateSource(parent, config));
			}

			return (Source[]) sources.ToArray(typeof(Source));
		}

		public override CommitStatus Create(Source source)
		{
			UpdateSource(source);
			return CommitStatus.Continue;
		}

		public override CommitStatus Delete(Source source)
		{
			DeleteSource(source);
			return CommitStatus.DoNotCommitChildren;
		}

		public override CommitStatus Update(Source source)
		{
			UpdateSource(source);
			return CommitStatus.Continue;
		}

		private static Source CreateSource(ISourceParent parent, ManagementBaseObject config)
		{
			// Get the properties.

			var name = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.NameProperty);
			var enabledEvents = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.Source.EnabledEventsProperty);
            string[] enabledEventsArray = SplitString(enabledEvents, Constants.Module.ListSeparator);
			return parent.Catalogue.CreateSource(parent, name, enabledEventsArray);
		}

		private void UpdateSource(Source source)
		{
			// Try to get an existing source.

			ManagementObject config = WmiUtil.GetObject(_scope, GetPath(source));
			if ( config == null )
			{
				// Not found, so create a new instance.

				config = CreateInstance(Constants.Wmi.Source.Class);
				WmiUtil.SetPropertyValue(config, Constants.Wmi.ParentProperty, source.Parent == null ? string.Empty : source.Parent.FullName);
				WmiUtil.SetPropertyValue(config, Constants.Wmi.NameProperty, source.Name);
			}

			// Update the rest of the properties and save.

            string enabledEvents = JoinString(source.EnabledEvents, Constants.Module.ListSeparator);

			WmiUtil.SetPropertyValue(config, Constants.Wmi.Source.EnabledEventsProperty, enabledEvents);
			config.Put();
		}

		private void DeleteSource(Source source)
		{
			// Get the configuration for this source.

			ManagementObject config = WmiUtil.GetObject(_scope, GetPath(source));
			if ( config != null )
				DeleteSource(config);
		}

		private static void DeleteSource(ManagementObject config)
		{
			// Delete the source itself.

			config.Delete();
		}

		private static string GetPath(Source source)
		{
		    if ( source.Parent == null )
				return GetPath(Constants.Wmi.Source.Class, string.Empty, source.Name);
		    return GetPath(Constants.Wmi.Source.Class, source.Parent.FullName, source.Name);
		}

	    #endregion

		#region Event

		public override EventType[] GetEventTypes(Catalogue catalogue)
		{
			// Iterate over all instances.

			var events = new ArrayList();
			var searcher = new ManagementObjectSearcher(_scope, GetQuery(Constants.Wmi.EventType.Class));

			using (ManagementObjectCollection configs = searcher.Get())
			{
				foreach ( ManagementObject config in configs )
					events.Add(CreateEventType(catalogue, config));
			}

			return (EventType[]) events.ToArray(typeof(EventType));
		}

		public override CommitStatus Create(EventType eventType)
		{
			UpdateEventType(eventType);
			return CommitStatus.Continue;
		}

		public override CommitStatus Delete(EventType eventType)
		{
			DeleteEventType(eventType);
			return CommitStatus.DoNotCommitChildren;
		}

		public override CommitStatus Update(EventType eventType)
		{
			UpdateEventType(eventType);
			return CommitStatus.Continue;
		}

        private static EventType CreateEventType(Catalogue catalogue, ManagementBaseObject config)
		{
			// Get the properties.

			var name = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.NameProperty);
			var isEnabled = (bool) WmiUtil.GetPropertyValue(config, Constants.Wmi.EventType.IsEnabledProperty);

			return catalogue.CreateEventType(catalogue, name, isEnabled);
		}

        private void UpdateEventType(EventType eventType)
		{
			// Try to get an existing source.

			ManagementObject config = WmiUtil.GetObject(_scope, GetPath(eventType));
			if ( config == null )
			{
				// Not found, so create a new instance.

				config = CreateInstance(Constants.Wmi.EventType.Class);
				WmiUtil.SetPropertyValue(config, Constants.Wmi.NameProperty, eventType.Name);
			}

			// Update the rest of the properties and save.

			WmiUtil.SetPropertyValue(config, Constants.Wmi.EventType.IsEnabledProperty, eventType.IsEnabled);
			config.Put();
		}

	    private void DeleteEventType(IElement eventType)
		{
			// Get the configuration for this source.

			ManagementObject config = WmiUtil.GetObject(_scope, GetPath(eventType));
			if ( config != null )
				DeleteEventType(config);
		}

		private static void DeleteEventType(ManagementObject config)
		{
			config.Delete();
		}

		private static string GetPath(IElement eventType)
		{
			return GetPath(Constants.Wmi.EventType.Class, eventType.Name);
		}

		#endregion

		#region ICatalogueSearch Members

		public Namespace GetNamespace(INamespaceParent parent, string relativeName, bool ignoreCase)
		{
			// WMI is very slow, so do everything possible to minimise WMI access. We need to load all the
			// namespaces in the hierarchy (from the parent to the one being sought), because any of them
			// may have enabled events. Get them all in one query.

			// Build a query that gets all the namespaces we need (if they exist).

			string[] nsParts = relativeName.Split('.');
			string parentFullName = parent.FullName;
			string queryString = BuildQueryStringForNamespaces(nsParts, parentFullName);

			// WQL is case-insensitive, so if ignoreCase is false use the Hashtable created below to filter.

		    Hashtable namespaces = ignoreCase ? new Hashtable(StringComparer.InvariantCultureIgnoreCase) : new Hashtable();

			var searcher = new ManagementObjectSearcher(_scope, new ObjectQuery(queryString));

			// Read the namespaces returned and store them temporarily in a Hasthable. We cannot create
			// Namespace objects as we go, because they may be returned out of order (ie. children before parents).

			using (ManagementObjectCollection configs = searcher.Get())
			{
				foreach (ManagementObject config in configs)
				{
					var details = new NamespaceDetails();

					var nsName = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.NameProperty);
					var nsParentName = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.ParentProperty);
					var enabledEvents = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.Namespace.EnabledEventsProperty);
					var mixedEvents = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.Namespace.MixedEventsProperty);

                    details._enabledEvents = SplitString(enabledEvents, Constants.Module.ListSeparator);
                    details._mixedEvents = SplitString(mixedEvents, Constants.Module.ListSeparator);

					namespaces.Add(CatalogueName.GetFullNameUnchecked(nsParentName, nsName), details);
				}
			}

			// Now that we have all the details create the hierarchy of Namespace objects.

			Catalogue catalogue = parent.Catalogue;
			Namespace ns = null;
			foreach (string nsName in nsParts)
			{
				string nsFullName = CatalogueName.GetFullNameUnchecked(parentFullName, nsName);
				var details = (NamespaceDetails)namespaces[nsFullName];
				if (details == null)
					return null; // One of the namespaces in the path was not found in WMI.

				ns = catalogue.CreateNamespace(parent, nsName, details._enabledEvents, details._mixedEvents);
				parent.AddIfNotLoaded(ns);

				parentFullName = nsFullName;
				parent = ns;
			}

			return ns;
		}

		public Source GetSource(ISourceParent parent, string name, bool ignoreCase)
		{
			string query = "SELECT * FROM " + Constants.Wmi.Source.Class
				+ " WHERE Parent = '" + parent.FullName + "' AND Name = '" + name + "'";

			var searcher = new ManagementObjectSearcher(_scope, new ObjectQuery(query));

			using (ManagementObjectCollection configs = searcher.Get())
			{
				Debug.Assert(configs.Count <= 1, "configs.Count <= 1");

				IEnumerator enumerator = configs.GetEnumerator();
				if (!enumerator.MoveNext())
					return null; // Source not found.

				Source source = CreateSource(parent, (ManagementObject)enumerator.Current);

				// WQL is case-isensitive, so check the case below, if needed.

				if (!ignoreCase && source.Name != name)
					return null;

				parent.AddIfNotLoaded(source);

				return source;
			}
		}

		#endregion

		#region ICatalogueUpdate Members

		public void EventStatusChanged(CatalogueElements elementType, string fullName)
		{
			// Store the changes in static properties on the LinkMe_InstrumentationEventStatusChange class.

			var config = new ManagementClass(_scope,
				new ManagementPath(Constants.Wmi.EventStatusChange.Class), null);

			WmiUtil.SetPropertyValue(config, Constants.Wmi.EventStatusChange.FullNameProperty, fullName);
			WmiUtil.SetPropertyValue(config, Constants.Wmi.EventStatusChange.ElementTypeProperty, (int)elementType);

			config.Put();
		}

		#endregion

		private static string BuildQueryStringForNamespaces(string[] nsParts, string parentFullName)
		{
			Debug.Assert(nsParts.Length > 0, "nsParts.Length > 0");

			var queryBuilder = new StringBuilder("SELECT * FROM "
				+ Constants.Wmi.Namespace.Class + " WHERE ");

			queryBuilder.Append("(Parent = '");
			queryBuilder.Append(parentFullName);
			queryBuilder.Append("' AND Name = '");
			queryBuilder.Append(nsParts[0]);
			queryBuilder.Append("')");

			var parentBuilder = new StringBuilder(parentFullName);
			if (parentFullName.Length > 0)
			{
				parentBuilder.Append('.');
			}
			parentBuilder.Append(nsParts[0]);

			for (int index = 1; index < nsParts.Length; index++)
			{
				queryBuilder.Append(" OR (Parent = '");
				queryBuilder.Append(parentBuilder.ToString());
				queryBuilder.Append("' AND Name = '");
				queryBuilder.Append(nsParts[index]);
				queryBuilder.Append("')");

				parentBuilder.Append('.');
				parentBuilder.Append(nsParts[index]);
			}

			return queryBuilder.ToString();
		}

		internal static string[] SplitString(string input, char separator)
		{
			return (string.IsNullOrEmpty(input) ? null : input.Split(separator));
		}

		private static string JoinString(ReadOnlyStringList input, char separator)
		{
			return (input == null || input.Count == 0 ? null : input.Join(separator.ToString()));
		}

	    private static string GetPath(string className, string name)
		{
			return className + "." + Constants.Wmi.NameProperty + "=\"" + name + "\"";
		}

		private static string GetPath(string className, string parentNamespace, string name)
		{
			return className + "." + Constants.Wmi.ParentProperty + "=\"" + parentNamespace + "\","
				+ Constants.Wmi.NameProperty + "=\"" + name + "\"";
		}

		private static ObjectQuery GetQuery(string className)
		{
			return new ObjectQuery("SELECT * FROM " + className);
		}

		private static ObjectQuery GetChildQuery(string className, ManagementBaseObject config)
		{
			var parent = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.ParentProperty);
			var name = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.NameProperty);
			return GetChildQuery(className, CatalogueName.GetFullName(parent, name));
		}

		private static ObjectQuery GetChildQuery(string className, string fullName)
		{
			return new ObjectQuery("SELECT * FROM " + className + " WHERE " + Constants.Wmi.ParentProperty + "=\"" + fullName + "\"");
		}

		private ManagementObject CreateInstance(string className)
		{
			var managementClass = new ManagementClass(_scope, new ManagementPath(className), null);
			return managementClass.CreateInstance();
		}

		private readonly ManagementScope _scope;
	}
}
