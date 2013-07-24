using System.Collections;
using System.Diagnostics;
using System.Management;
using System.Threading;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Wmi;

namespace LinkMe.Framework.Instrumentation.Connection.Wmi
{
	public class WmiConnection
        : RepositoryConnection,
        IRepositoryUpdate
	{
        public WmiConnection(string wmiNamespace)
            : base(wmiNamespace)
        {
        }

        #region IRepositoryUpdate

		public void RegisterCallback(IRepositoryCallback callback)
		{
			const string method = "RegisterCallback";

			if (callback == null)
				throw new NullParameterException(GetType(), method, "callback");

			// Initialise the callback if not done yet.

			if (_lock == null)
			{
				lock (this)
				{
					if (_lock == null)
					{
						InitialiseCallback();
					}
				}
			}
			Debug.Assert(_lock != null, "_lock != null");

			// Add the callback to the list.

			_lock.AcquireWriterLock(Timeout.Infinite);

			try
			{
				_callbacks.Add(callback);
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
		}

		#endregion

		protected override ICatalogueConnection CreateCatalogueConnection()
		{
            var scope = new ManagementScope(InitialisationString);
            scope.Connect();
            return new WmiCatalogueConnection(scope);
        }

		private void InitialiseCallback()
		{
			_lock = new ReaderWriterLock();
			_callbacks = new ArrayList();

            var scope = new ManagementScope(InitialisationString);
			scope.Connect();

			// Event enable/disable notifications - these are stored as static properties on the
			// LinkMe_InstrumentationEventStatusChange class.

			var query = new WqlEventQuery("__ClassModificationEvent",
				"TargetClass isa \"" + Constants.Wmi.EventStatusChange.Class + "\"");
			WmiUtil.StartEventWatcher(scope, query, HandleEventStatusChange);

			// Watch for modifications of the LinkMe_InstrumentationEvent class to pick up changes to default
			// event status.

			query = new WqlEventQuery("__InstanceModificationEvent", "TargetInstance isa \""
				+ Constants.Wmi.EventType.Class + "\"");
			WmiUtil.StartEventWatcher(scope, query, HandleEventModification);

			// Watch for creation of new LinkMe_InstrumentationSource objects.

			query = new WqlEventQuery("__InstanceCreationEvent", "TargetInstance isa \""
				+ Constants.Wmi.Source.Class + "\"");
			WmiUtil.StartEventWatcher(scope, query, HandleSourceCreation);

			// Watch for deletion of LinkMe_InstrumentationSource objects.

			query = new WqlEventQuery("__InstanceDeletionEvent", "TargetInstance isa \""
				+ Constants.Wmi.Source.Class + "\"");
			WmiUtil.StartEventWatcher(scope, query, HandleSourceDeletion);

			// TODO: Need to work out how to stop - implement IDisposable? In practice only Instrumentation
			// runtime registeres a callback and it never stops until the AppDomain is shut down, anyway.
		}

		#region Event handlers

		private void HandleEventStatusChange(object sender, EventArrivedEventArgs e)
		{
			string fullName;
			CatalogueElements elementType;

			try
			{
				// Read the details of the change.

				var config = (ManagementBaseObject) WmiUtil.GetPropertyValue(
					e.NewEvent, "TargetClass");
				Debug.Assert(config != null, "config != null");

				fullName = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.EventStatusChange.FullNameProperty);
				elementType = (CatalogueElements) WmiUtil.GetPropertyValue(config, Constants.Wmi.EventStatusChange.ElementTypeProperty);
			}
			catch (System.Exception ex)
			{
				HandleNotificationException(ex);
				return;
			}

			// Work through the callbacks.

			_lock.AcquireReaderLock(Timeout.Infinite);

			try
			{
				foreach (IRepositoryCallback callback in _callbacks)
				{
					try
					{
						callback.EventStatusChanged(elementType, fullName);
					}
					catch
					{
					}
				}
			}
			finally
			{
				_lock.ReleaseReaderLock();
			}
		}

		private void HandleEventModification(object sender, EventArrivedEventArgs e)
		{
			string name;
			bool isEnabled;

			try
			{
				// Read the details of the change.

				var config = (ManagementBaseObject) WmiUtil.GetPropertyValue(
					e.NewEvent, "TargetInstance");
				Debug.Assert(config != null, "config != null");

				name = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.NameProperty);
				isEnabled = (bool) WmiUtil.GetPropertyValue(config, Constants.Wmi.EventType.IsEnabledProperty);
			}
			catch (System.Exception ex)
			{
				HandleNotificationException(ex);
				return;
			}

			// Work through the callbacks.

			_lock.AcquireReaderLock(Timeout.Infinite);

			try
			{
				foreach (IRepositoryCallback callback in _callbacks)
				{
					try
					{
						callback.EventChanged(name, isEnabled);
					}
					catch
					{
					}
				}
			}
			finally
			{
				_lock.ReleaseReaderLock();
			}
		}

		private void HandleSourceCreation(object sender, EventArrivedEventArgs e)
		{
			string fullyQualifiedReference;

			try
			{
				// Read the details of the change.

				fullyQualifiedReference = GetSourceFullyQualifiedReference(e);
			}
			catch (System.Exception ex)
			{
				HandleNotificationException(ex);
				return;
			}

			// Work through the callbacks.

			_lock.AcquireReaderLock(Timeout.Infinite);

			try
			{
				foreach (IRepositoryCallback callback in _callbacks)
				{
					try
					{
						callback.SourceCreated(fullyQualifiedReference);
					}
					catch
					{
					}
				}
			}
			finally
			{
				_lock.ReleaseReaderLock();
			}
		}

		private void HandleSourceDeletion(object sender, EventArrivedEventArgs e)
		{
			string fullyQualifiedReference;

			try
			{
				// Read the details of the change.

				fullyQualifiedReference = GetSourceFullyQualifiedReference(e);
			}
			catch (System.Exception ex)
			{
				HandleNotificationException(ex);
				return;
			}

			// Work through the callbacks.

			_lock.AcquireReaderLock(Timeout.Infinite);

			try
			{
				foreach (IRepositoryCallback callback in _callbacks)
				{
					try
					{
						callback.SourceDeleted(fullyQualifiedReference);
					}
					catch
					{
					}
				}
			}
			finally
			{
				_lock.ReleaseReaderLock();
			}
		}

		#endregion

		private static string GetSourceFullyQualifiedReference(EventArrivedEventArgs e)
		{
			var config = (ManagementBaseObject) WmiUtil.GetPropertyValue(e.NewEvent, "TargetInstance");
			Debug.Assert(config != null, "config != null");

			var name = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.NameProperty);
			var parent = (string) WmiUtil.GetPropertyValue(config, Constants.Wmi.ParentProperty);
			return CatalogueName.GetQualifiedReferenceUnchecked(parent, name);
		}

		private void HandleNotificationException(System.Exception ex)
		{
			// Let callbacks handle this error.

			foreach (IRepositoryCallback callback in _callbacks)
			{
				try
				{
					callback.HandleNotificationException(ex);
				}
				catch
				{
				}
			}
		}

		private ReaderWriterLock _lock;
		private ArrayList _callbacks;
	}
}
