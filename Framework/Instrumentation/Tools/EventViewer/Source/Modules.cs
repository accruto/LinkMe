using System;
using System.Collections.Generic;
using System.Xml;
using LinkMe.Framework.Configuration.Connection.Msmq;
using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Instrumentation.MessageComponents;
using LinkMe.Framework.Tools.Editors;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class Modules
	{
        private readonly SortedList<string, MessageReaderInfo> _messageReaders = new SortedList<string, MessageReaderInfo>();
		private readonly SortedList<string, MessageHandlerInfo> _messageHandlers = new SortedList<string, MessageHandlerInfo>();
		private readonly EditorManager _editorManager;

		public Modules(string modulesXmlFile)
		{
			const string eventViewerXPath = "configuration/" + EventViewerSettings.EventViewerXmlPrefix
					  + ":modules/" + EventViewerSettings.EventViewerXmlPrefix + ":eventViewer";

            _messageReaders.Add("MSMQ", new MessageReaderInfo<MsmqMessageReader, MsmqInitialise>("MSMQ", "MSMQ Queue"));
            _messageReaders.Add("EventFile", new MessageReaderInfo<EventFileMessageReader, EventFileInitialise>("EventFile", "Event File"));
            _messageReaders.Add("MSSQL", new MessageReaderInfo<MssqlMessageReader, MssqlInitialise>("MSSQL", "SQL Database"));

            _messageHandlers.Add("MSMQ", new MessageHandlerInfo<MsmqMessageHandler>("MSMQ", "MSMQ Queue"));
            _messageHandlers.Add("EventFile", new MessageHandlerInfo<EventFileMessageHandler>("EventFile", "Event File"));
            _messageHandlers.Add("MSSQL", new MessageHandlerInfo<MssqlMessageHandler>("MSSQL", "SQL Database"));

			try
			{
				var xmlDoc = new XmlDocument();
				xmlDoc.Load(modulesXmlFile);

				var xmlNsManager = new XmlNamespaceManager(xmlDoc.NameTable);
				xmlNsManager.AddNamespace(EventViewerSettings.EventViewerXmlPrefix,
					EventViewerSettings.EventViewerXmlNamespace);

				XmlNode xmlEventViewer = xmlDoc.SelectSingleNode(eventViewerXPath, xmlNsManager);
				if (xmlEventViewer == null)
					throw new ApplicationException("The XML file does not contain Event Viewer module settings.");

				// Editors (optional)

				XmlNode xmlEditors = xmlEventViewer.SelectSingleNode(
					EventViewerSettings.EventViewerXmlPrefix + ":editors", xmlNsManager);
				if (xmlEditors != null)
				{
					_editorManager = new EditorManager();
					_editorManager.ReadXmlSettings(xmlEditors, xmlNsManager,
						EventViewerSettings.EventViewerXmlPrefix + ":", modulesXmlFile);
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Failed to load event viewer modules"
					+ " from XML file '" + modulesXmlFile + "'.", ex);
			}
		}

		public ICollection<MessageReaderInfo> MessageReaders
		{
			get { return _messageReaders.Values; }
		}

		public ICollection<MessageHandlerInfo> MessageHandlers
		{
			get { return _messageHandlers.Values; }
		}

		public EditorManager EditorManager
		{
			get { return _editorManager; }
		}

		public IMessageReader CreateMessageReader(string name, string initialisationString)
		{
            return GetMessageReaderInfo(name).CreateInstance(initialisationString);
		}

		public IMessageHandler CreateMessageHandler(string name)
		{
			return GetMessageHandlerInfo(name).CreateInstance();
		}

        public string GetInitialisationString(string name)
        {
            return GetMessageReaderInfo(name).GetInitialisationString();
        }
       
        public string GetDisplayName(string name)
		{
			return GetMessageReaderInfo(name).DisplayName;
		}

		private MessageReaderInfo GetMessageReaderInfo(string name)
		{
			var info = _messageReaders[name];
			if (info == null)
			{
				throw new ApplicationException("No message reader class is registered with the name '"
					+ name + "'. Check the modules XML file.");
			}

			return info;
		}

		private MessageHandlerInfo GetMessageHandlerInfo(string name)
		{
			var info = _messageHandlers[name];
			if (info == null)
			{
				throw new ApplicationException("No message handler class is registered with the name '"
					+ name + "'. Check the modules XML file.");
			}

			return info;
		}
	}
}
