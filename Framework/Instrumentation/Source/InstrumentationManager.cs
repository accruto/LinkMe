using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using LinkMe.Framework.Configuration;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;
using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Instrumentation.MessageComponents;
using LinkMe.Framework.Utility.Event;
using LinkMe.Framework.Utility.Unity;
using Microsoft.Practices.Unity;
using Catalogue=LinkMe.Framework.Instrumentation.Management.Catalogue;
using Exception=System.Exception;

namespace LinkMe.Framework.Instrumentation
{
	public static class InstrumentationManager
	{
		static InstrumentationManager()
		{
		    const string method = "InstrumentationManager";

			try
			{
				// Create members.

				Lock = new ReaderWriterLock();
				SourceStates = new Hashtable();
				Events = new Hashtable();
				EventDetailFactories = new EventDetailFactories();

				// Initialise.

				InitialiseDefaults();

				// Initialise internal message handler.

				InitialiseInternalMessageHandler();

                var message = GenerateInitialiseMessage();
                _internalMessageHandler.HandleEventMessage(message);
                
                // Now initialise everything.

                var container = new UnityContainer();
                container.AddConfiguration("linkme.instrumentation.container");

                InitialiseRootMessageHandler(container, message);
                InitialiseEventDetails();
                _catalogue = GetCatalogue(container);
                if ( _catalogue != null )
                    InitialiseEvents(_catalogue);
			}
            catch (Exception ex)
            {
                if (_internalMessageHandler != null)
    				_internalMessageHandler.HandleEventMessage(new EventMessage(null, Event.Error, typeof(InstrumentationManager), method, string.Format("Failed to initialise the Instrumentation module: {0}{1}", Environment.NewLine, ex)));
            }
			finally
			{
				Initialised = true;
			}
		}

		internal static SourceState GetSourceState(string fullyQualifiedReference)
		{
			SourceState sourceState = GetSourceStateFromCache(fullyQualifiedReference);

			if ( sourceState == null )
			{
				// Create the source.

				sourceState = CreateSourceState(fullyQualifiedReference);

				// Cache it so that next time it is available.

				Lock.AcquireWriterLock(Timeout.Infinite);
				try
				{
					SourceStates[fullyQualifiedReference] = sourceState;
				}
				finally
				{
					Lock.ReleaseWriterLock();
				}
			}

			return sourceState;
		}

		internal static void UpdateNamespace(string fullName)
		{
			Debug.Assert(!string.IsNullOrEmpty(fullName), "fullName != null && fullName.Length > 0");

			// Find the SourceStates for all Sources in the namespace (recursively).

			string prefix = fullName + ".";
			var statesToUpdate = new ArrayList();
			var nsCount = new Hashtable(); // Count of sources to update per namespace < string, int >

			Lock.AcquireReaderLock(Timeout.Infinite);
			try
			{
				foreach (SourceState sourceState in SourceStates.Values)
				{
					if (sourceState.FullyQualifiedReference.StartsWith(prefix))
					{
						statesToUpdate.Add(sourceState);

						// Maintain the count of states in this namespace that are being updated.

						string nsName = CatalogueName.CreateUnchecked(sourceState.FullyQualifiedReference).Namespace;
						object count = nsCount[nsName];
						nsCount[nsName] = (count == null ? 1 : (int)count + 1);
					}
				}
			}
			finally
			{
				Lock.ReleaseReaderLock();
			}

			if (statesToUpdate.Count == 0)
				return; // No Sources from this namespace have been created - nothing to update.

			// Get the catalogue and find the namespace on which event status was changed.

			var rootNs = _catalogue.GetSearcher().GetNamespace(fullName);
			if (rootNs == null)
				return; // The namespace must have been deleted after it was updated.

			// Update each source.

			foreach (SourceState sourceState in statesToUpdate)
			{
				CatalogueName catalogueName = CatalogueName.CreateUnchecked(sourceState.FullyQualifiedReference);
				var sourceCount = (int)nsCount[catalogueName.Namespace];

				// To minimise access to WMI use two different ways to search for the source. If there are only
				// 1 or 2 sources that need to be updated in the namespace then find them using the searcher -
				// if the repository is WMI it will read only those sources. If there are 3 or more then just
				// use the Sources collection, which will load all the sources in the namespace in one go.

				Source source;
				if (sourceCount < 3)
				{
					string relativeName = sourceState.FullyQualifiedReference.Substring(prefix.Length);
					source = rootNs.GetSearcher().GetSource(relativeName);
				}
				else
				{
					if (catalogueName.Namespace == fullName)
					{
						source = rootNs.Sources[catalogueName.RelativeQualifiedReference];
					}
					else
					{
					    Namespace leafNs = rootNs.GetSearcher().GetNamespace(catalogueName.Namespace.Substring(prefix.Length));
					    source = leafNs == null ? null : leafNs.Sources[catalogueName.RelativeQualifiedReference];
					}
				}

				if (source == null)
				{
					sourceState.SetEventStatus(_defaultEventStatus);
				}
				else
				{
					sourceState.SetEventStatus(CreateEventStatus(source.GetEffectiveEnabledEvents()));
				}
			}
		}

		internal static void UpdateSource(string fullyQualifiedReference)
		{
			Debug.Assert(!string.IsNullOrEmpty(fullyQualifiedReference),
				"fullyQualifiedReference != null && fullyQualifiedReference.Length > 0");

			// Find the SourceState object for this Source. The existing object must be updated (it cannot be
			// overwritten), because user code may already hold a reference to it via an EventSource.

			SourceState sourceState = GetSourceStateFromCache(fullyQualifiedReference);
			if (sourceState == null)
				return; // This Source hasn't been created - nothing to update.

			// Find the Source in the Catalogue.

			var source = _catalogue.GetSearcher().GetSource(fullyQualifiedReference);

		    BitArray eventStatus = source == null ? _defaultEventStatus : CreateEventStatus(source.GetEffectiveEnabledEvents());

			sourceState.SetEventStatus(eventStatus);
		}

		internal static void UpdateEvent(string eventName, bool isEnabled)
		{
			var eventInfo = (EventInfo) Events[eventName];
			if (eventInfo == null)
				return; // The changed event must have been created after Instrumentation initialised.

			// Update the EventInfo.

			eventInfo.IsEnabled = isEnabled;

		    // Update the default event status.

			_defaultEventStatus[eventInfo.Index] = isEnabled;
		}

		internal static void SourceCreated(string fullyQualifiedReference)
		{
			SourceState sourceState = GetSourceStateFromCache(fullyQualifiedReference);
			if ( sourceState == null )
				return; // Not cached, so nothing to update.

			// Update the cached SourceState properties from the catalogue (do not create a new SourceState
			// object, since user code may already hold references to it via EventSources).

			Source source = _catalogue.GetSearcher().GetSource(fullyQualifiedReference);
			if (source == null)
				return; // Strange - the Source must have been created and immediately deleted.

			// Update the Message Handler and event status.

			sourceState.SetMessageHandler(CreateMessageHandler(fullyQualifiedReference));
			sourceState.SetEventStatus(CreateEventStatus(source.GetEffectiveEnabledEvents()));
		}		

		internal static void SourceDeleted(string fullyQualifiedReference)
		{
			SourceState sourceState = GetSourceStateFromCache(fullyQualifiedReference);
			if ( sourceState == null )
				return; // Not cached, so nothing to update.

			// Set the cached SourceState properties to the default values.

			sourceState.SetMessageHandler(CreateMessageHandler(fullyQualifiedReference));
			sourceState.SetEventStatus(_defaultEventStatus);
		}

		internal static SourceState GetSourceState(string ns, string name)
		{
			return GetSourceState(CatalogueName.GetQualifiedReference(ns, name));
		}

		/// <summary>
		/// Returns an EventSource instance based on the passed type.
		/// </summary>
		/// <param name="type">The System.Type for the EventSource.</param>
		/// <returns>EventSource Instance</returns>
		internal static SourceState GetSourceState(System.Type type)
		{
			return GetSourceState(type.Namespace, type.Name);
		}

		/// <summary>
		/// Returns the root message handler.
		/// </summary>
		/// <returns>The root message handler</returns>
		public static IMessageHandler GetMessageHandler()
		{
			// Always return the root message handler.

			return _rootMessageHandler;
		}

		internal static IMessageHandler GetInternalMessageHandler()
		{
			// Always return the root message handler.
			return _internalMessageHandler;
		}

		internal static EventInfo GetEventInfo(string name)
		{
			// Look for the event info and return it, if found.

			var eventInfo = (EventInfo) Events[name];
			if ( eventInfo != null )
				return eventInfo;

			// Create a default and add it.

			var newEventInfo = new EventInfo(name, false, 0);
			if ( !Initialised )
				Events[name] = newEventInfo;

			return newEventInfo;
		}

		internal static IEventDetailFactory GetEventDetailFactory(string name)
		{
			// Look for the event info and return its factory if found.

			return EventDetailFactories[name];
		}

		#region Initialise

		private static void InitialiseDefaults()
		{
			_internalMessageHandler = new NullMessageHandler();
			_rootMessageHandler = new NullMessageHandler();
			_allEvents = new string[0];
			_defaultEventStatus = new BitArray(1, false);
		}

		private static void InitialiseInternalMessageHandler()
		{
			// Create by default an EventLog message handler for raising internal events.

			_internalMessageHandler = new EventLogMessageHandler();
        }

		private static Catalogue GetCatalogue(IUnityContainer container)
		{
			const string method = "GetCatalogue";

		    IRepositoryReader repositoryReader = null;
			Catalogue catalogue = null;

			try
			{
				// Access the current catalogue.

			    repositoryReader = container.Resolve<IRepositoryReader>();
                catalogue = repositoryReader.Read();

				_errorGettingCatalogue = null;
			}
			catch ( Exception ex )
			{
				_errorGettingCatalogue = ex;
				_internalMessageHandler.HandleEventMessage(new EventMessage(null, Event.Error, typeof(InstrumentationManager), method,
					string.Format("Failed to initialise the current catalogue for the Instrumentation module: {0}{1}", Environment.NewLine, ex)));
			}

			try
			{
				if (catalogue != null)
				{
                    // Register for callbacks.

                    var repositoryUpdate = repositoryReader as IRepositoryUpdate;
                    if (repositoryUpdate != null)
                        repositoryUpdate.RegisterCallback(RepositoryCallback);
				}
			}
			catch ( Exception ex )
			{
				_internalMessageHandler.HandleEventMessage(new EventMessage(null, Event.Warning, typeof(InstrumentationManager), method,
					string.Format("Failed to register an Instrumentation repository callback for repository"
					+ " in process {0}, running as user '{1}\\{2}'. Instrumentation will still be initialised,"
					+ " but configuration changes will have no effect until the process is restarted.{3}{4}",
					Process.GetCurrentProcess().Id, Environment.UserDomainName, Environment.UserName,
					Environment.NewLine, ex)));
			}

			return catalogue;
		}

        private static EventMessage GenerateInitialiseMessage()
        {
            const string method = "GenerateInitialiseMessage";

            // Log a message indicating what is going on so that even if something fails there is some information to determine what is going on.

            var sb = new StringBuilder();
            sb.AppendLine("Initialising instrumentation:");

            try
            {
                // Try to get as many details as possible to add directly to the message.

                GetProcessDetail(sb);
                GetSecurityDetail(sb);
                GetNetDetail(sb);
                GetDiagnosticDetail(sb);
            }
            catch (Exception)
            {
            }

            return new EventMessage(null, Event.Information, typeof(InstrumentationManager), method, sb.ToString());
        }

	    private static void GetDiagnosticDetail(StringBuilder sb)
	    {
            sb.AppendLine().AppendLine(Separator).AppendLine("Diagnostic details").AppendLine(Separator);
            var detail = new DiagnosticDetail();
            ((IEventDetail)detail).Populate();
            sb.Append("Stack Trace: ").AppendLine(detail.StackTrace);
        }

	    private static void GetProcessDetail(StringBuilder sb)
	    {
            sb.AppendLine().AppendLine(Separator).AppendLine("Process details").AppendLine(Separator);
            var detail = new ProcessDetail();
            ((IEventDetail)detail).Populate();
            sb.Append("Machine Name: ").AppendLine(detail.MachineName);
            sb.Append("Process Name: ").AppendLine(detail.ProcessName);
            sb.Append("Process Id: ").AppendLine(detail.ProcessId.ToString());
	    }

        private static void GetSecurityDetail(StringBuilder sb)
        {
            sb.AppendLine().AppendLine(Separator).AppendLine("Security details").AppendLine(Separator);
            var detail = new SecurityDetail();
            ((IEventDetail)detail).Populate();
            sb.Append("Process User Name: ").AppendLine(detail.ProcessUserName);
            sb.Append("Authentication Type: ").AppendLine(detail.AuthenticationType);
            sb.Append("Is Authenticated: ").AppendLine(detail.IsAuthenticated.ToString());
            sb.Append("User Name: ").AppendLine(detail.UserName);
        }

        private static void GetNetDetail(StringBuilder sb)
        {
            sb.AppendLine().AppendLine(Separator).AppendLine("Net details").AppendLine(Separator);
            var detail = new NetDetail();
            ((IEventDetail)detail).Populate();
            sb.Append("AppDomain Name: ").AppendLine(detail.AppDomainName);
        }

        private static void InitialiseRootMessageHandler(IUnityContainer container, EventMessage message)
		{
			const string method = "InitialiseRootMessageHandler";

			try
			{
				// Try to get the root message handler name from the settings.

			    _rootMessageHandler = container.Resolve<IMessageHandler>();

                try
                {
                    // Give the root message handler the initialisation message so it can log it as well.

                    _rootMessageHandler.HandleEventMessage(message);
                }
                catch (Exception ex)
                {
                    _internalMessageHandler.HandleEventMessage(new EventMessage(null, Event.Warning, typeof(InstrumentationManager), method, "Cannot handle the initialisation message: " + ex.Message));
                }
			}
			catch ( Exception ex )
			{
				_internalMessageHandler.HandleEventMessage(new EventMessage(null, Event.Error, typeof(InstrumentationManager), method,
					"The root message handler cannot be set: "
					+ Environment.NewLine + ex));
			}
		}

		private static void InitialiseEventDetails()
		{
			const string method = "InitialiseEventDetails";

			try
			{
				// Grab all the event details.

			    EventDetailFactories.Add(new MessageSecurityDetailFactory());
                EventDetailFactories.Add(new MessageProcessDetailFactory());
                EventDetailFactories.Add(new MessageDiagnosticDetailFactory());
                EventDetailFactories.Add(new MessageHttpDetailFactory());
                EventDetailFactories.Add(new MessageNetDetailFactory());
                EventDetailFactories.Add(new MessageThreadDetailFactory());
			}
			catch ( Exception ex)
			{
				_internalMessageHandler.HandleEventMessage(new EventMessage(null, Event.Error, typeof(InstrumentationManager), method,
					"Failed to initialise the Instrumentation event groups: "
					+ Environment.NewLine + ex));
			}
		}

		private static void InitialiseEvents(Catalogue catalogue)
		{
			const string method = "InitialiseEvents";

		    try
			{
				// Grab all the events giving them an appropriate index.

				int index = 0;
				foreach ( EventType eventType in catalogue.EventTypes )
				{
					var eventInfo = (EventInfo) Events[eventType.Name];
					if ( eventInfo == null )
					{
						eventInfo = new EventInfo(eventType.Name, eventType.IsEnabled, ++index);
						Events[eventType.Name] = eventInfo;
					}
					else
					{
						eventInfo.IsEnabled = eventType.IsEnabled;
						eventInfo.Index = ++index;
					}
				}

				// Initialise the default status.

			    _defaultEventStatus = new BitArray(Events.Count + 1, false);

				// Index 0 is for unknown events and its status is always false.

				int eventIndex = 0;
				_defaultEventStatus[0] = false;
				_allEvents = new string[Events.Count];
				foreach ( EventInfo eventInfo in Events.Values )
				{
					_allEvents[eventIndex++] = eventInfo.Name;

					if ( eventInfo.IsEnabled )
						_defaultEventStatus[eventInfo.Index] = true;
				}
			}
			catch ( Exception ex)
			{
				_internalMessageHandler.HandleEventMessage(new EventMessage(null, Event.Error, typeof(InstrumentationManager), method,
					"Failed to initialise Instrumentation events: "
					+ Environment.NewLine + ex));
			}
			finally
			{
				// Ensure that there are defaults.

				if ( _defaultEventStatus == null )
					_defaultEventStatus = new BitArray(Events.Count + 1, false);
				if ( _allEvents == null )
				{
					_allEvents = new string[Events.Count];
					for (int index = 0; index < _allEvents.Length; ++index)
						_allEvents[index] = string.Empty;
				}
			}
		}

		#endregion

		private static SourceState GetSourceStateFromCache(string fullyQualifiedReference)
		{
			SourceState sourceState;

			// Lock

			Lock.AcquireReaderLock(Timeout.Infinite);
			try
			{
				// Check whether the event source has been cached.

				sourceState = (SourceState) SourceStates[fullyQualifiedReference];
			}
			finally
			{
				Lock.ReleaseReaderLock();
			}

			return sourceState;
		}

		private static SourceState CreateSourceState(string fullyQualifiedReference)
		{
			const string method = "CreateSourceState";

			// If an error occurred in getting the current catalogue during initialisation do not try
			// to get it again.

			if (_errorGettingCatalogue != null)
				return CreateState(fullyQualifiedReference);

			try
			{
				// Connect to the catalogue.

				Source source = _catalogue.GetSearcher().GetSource(fullyQualifiedReference);

				if ( source != null )
					return CreateState(fullyQualifiedReference, source.GetEffectiveEnabledEvents());
			    return CreateState(fullyQualifiedReference);
			}
			catch ( Exception ex )
			{
				// Log the error, but make sure there are no more than 10 of these.

				if (_createSourceErrors < Constants.Errors.MaximumErrorsToLog)
				{
					_createSourceErrors++;
					string message = string.Format("Failed to create a SourceState for source '{0}':{1}{2}",
						fullyQualifiedReference, Environment.NewLine, ex);

					if (_createSourceErrors >= Constants.Errors.MaximumErrorsToLog)
					{
						message += Environment.NewLine + Environment.NewLine
							+ "No further source creation errors will be logged.";
					}

					_internalMessageHandler.HandleEventMessage(new EventMessage(null, Event.Warning, typeof(InstrumentationManager),
						method, message));
				}

				// Create a default source state.

				return CreateState(fullyQualifiedReference);
			}
		}

		private static SourceState CreateState(string fullyQualifiedReference, IEnumerable<string> enabledEvents)
		{
			return new SourceState(fullyQualifiedReference, CreateMessageHandler(fullyQualifiedReference), CreateEventStatus(enabledEvents));
		}

		private static SourceState CreateState(string fullyQualifiedReference)
		{
			// Pass _defaultEventStatus to this SourceState - do not clone it, so that changes to the default
			// event status at runtime can be picked by existing EventSources.

			return new SourceState(fullyQualifiedReference, CreateMessageHandler(fullyQualifiedReference), _defaultEventStatus);
		}

		private static BitArray CreateEventStatus(IEnumerable<string> enabledEvents)
		{
			// Create the array.

			var array = new BitArray(Events.Count + 1, false);

			// Determine which are enabled.

			foreach ( string enabledEvent in enabledEvents )
			{
				var eventInfo = (EventInfo) Events[enabledEvent];
				if ( eventInfo != null )
					array[eventInfo.Index] = true;
			}

			return array;
		}

        private static IMessageHandler CreateMessageHandler(string fullyQualifiedReference)
        {
            return _rootMessageHandler.GetMessageHandler(fullyQualifiedReference);
        }

		#region Member Variables

        private static readonly string Separator = new string('-', 30);
	    private static readonly Catalogue _catalogue;
        private static int _createSourceErrors;
        private static readonly bool Initialised;
        private static readonly ReaderWriterLock Lock;
        private static readonly Hashtable SourceStates;
        private static readonly Hashtable Events;
		private static readonly EventDetailFactories EventDetailFactories;
		private static string[] _allEvents;
		private static BitArray _defaultEventStatus;
		private static IMessageHandler _internalMessageHandler;
		private static IMessageHandler _rootMessageHandler;
		private static readonly RepositoryCallback RepositoryCallback = new RepositoryCallback();
		private static Exception _errorGettingCatalogue;

		#endregion
	}
}
