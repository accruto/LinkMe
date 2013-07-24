using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Utility.Event
{
	[System.Serializable]
	public sealed class ProcessDetail
		:	IEventDetail,
			ISerializable,
			System.ICloneable,
			IXmlSerializable,
			IBinarySerializable,
			IInternable
	{
		#region Constructors

		static ProcessDetail()
		{
 			try
			{
				try
				{
					var currentProcess = Process.GetCurrentProcess();
					GenerateProcessId = currentProcess.Id;
                    GenerateCommandLineArgs = string.Join(" ", System.Environment.GetCommandLineArgs());

					// Get the process name using native API. Calling currentProcess.ProcessName is very expensive.

					var sb = new StringBuilder(Constants.Win32.MAX_PATH + 1);
					var result = Win32.SafeNativeMethods.GetModuleFileName(System.IntPtr.Zero, sb,
						Constants.Win32.MAX_PATH);
					if (result == 0)
						throw new System.ComponentModel.Win32Exception();

					GenerateProcessName = Path.GetFileName(sb.ToString());
				}
				catch (System.Exception ex)
				{
					LogInitialisationError(ex);
				}

				try
				{
					GenerateMachineName = System.Environment.MachineName;
				}
				catch (System.Exception ex)
				{
					LogInitialisationError(ex);
				}
			}
			catch ( System.Exception )
			{
			}
		}

		public ProcessDetail()
		{
		}

		private ProcessDetail(SerializationInfo info, StreamingContext streamingContext)
		{
			_processId = info.GetInt32(Constants.Serialization.ProcessDetail.ProcessId);
			_processName = info.GetString(Constants.Serialization.ProcessDetail.ProcessName);
			_machineName = info.GetString(Constants.Serialization.ProcessDetail.MachineName);
		    _commandLineArgs = info.GetString(Constants.Serialization.ProcessDetail.CommandLineArgs);
		}

		#endregion

		#region Properties

		public int ProcessId
		{
			get { return _processId; }
		}

		public string ProcessName
		{
			get { return _processName ?? string.Empty; }
		}

		public string MachineName
		{
			get { return _machineName ?? string.Empty; }
		}

	    public string CommandLineArgs
	    {
            get { return _commandLineArgs ?? string.Empty; }
	    }

		#endregion

		#region System.Object Members

		public override bool Equals(object other)
		{
			if ( !(other is ProcessDetail) )
				return false;
			var otherDetails = (ProcessDetail) other;
			return _processId == otherDetails._processId
				&& _processName == otherDetails._processName
				&& _machineName == otherDetails._machineName
                && _commandLineArgs == otherDetails._commandLineArgs;
		}

		public override int GetHashCode()
		{
			return _processId.GetHashCode()
				^ _processName.GetHashCode()
				^ _machineName.GetHashCode()
                ^ _commandLineArgs.GetHashCode();
		}

		#endregion

		#region IEventDetail Members

		string IEventDetail.Name
		{
			get { return typeof(ProcessDetail).Name; }
		}

        EventDetailValues IEventDetail.Values
        {
            get
            {
                return new EventDetailValues
                {
                    {Constants.Serialization.ProcessDetail.ProcessId, _processId},
                    {Constants.Serialization.ProcessDetail.ProcessName, _processName},
                    {Constants.Serialization.ProcessDetail.MachineName, _machineName},
                    {Constants.Serialization.ProcessDetail.CommandLineArgs, _commandLineArgs},
                };
            }
        }

		void IEventDetail.Populate()
		{
			_processId = GenerateProcessId;
			_processName = GenerateProcessName ?? string.Empty;
            _machineName = GenerateMachineName ?? string.Empty;
		    _commandLineArgs = GenerateCommandLineArgs ?? string.Empty;
		}
        
		#endregion

		#region ISerializable Members

		public void GetObjectData(SerializationInfo info, StreamingContext streamingContext)
		{
			info.AddValue(Constants.Serialization.ProcessDetail.ProcessId, _processId);
			info.AddValue(Constants.Serialization.ProcessDetail.ProcessName, _processName);
			info.AddValue(Constants.Serialization.ProcessDetail.MachineName, _machineName);
            info.AddValue(Constants.Serialization.ProcessDetail.CommandLineArgs, _commandLineArgs);
		}

		#endregion

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void WriteOuterXml(XmlWriter writer)
		{
			var adaptor = new XmlWriteAdaptor(writer, Constants.Xml.Namespace);
			adaptor.WriteStartElement(Constants.Xml.ProcessDetail.Name);
			WriteXml(adaptor);
			adaptor.WriteEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			var adaptor = new XmlWriteAdaptor(writer, Constants.Xml.Namespace);
			WriteXml(adaptor);
		}

		private void WriteXml(XmlWriteAdaptor adaptor)
		{
			adaptor.WriteNamespace(Constants.Xml.Prefix, Constants.Xml.Namespace);

			adaptor.WriteElement(Constants.Xml.ProcessDetail.ProcessIdElement, XmlConvert.ToString(_processId));
			adaptor.WriteElement(Constants.Xml.ProcessDetail.ProcessNameElement, _processName);
			adaptor.WriteElement(Constants.Xml.ProcessDetail.MachineNameElement, _machineName);
            adaptor.WriteElement(Constants.Xml.ProcessDetail.CommandLineArgsElement, _commandLineArgs);
        }

		public void ReadOuterXml(XmlReader reader)
		{
			var adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
			if ( adaptor.IsReadingElement(Constants.Xml.ProcessDetail.Name) )
			{
				ReadXml(adaptor);
				adaptor.ReadEndElement();
			}
		}

		public void ReadXml(XmlReader reader)
		{
			var adaptor = new XmlReadAdaptor(reader, Constants.Xml.Namespace);
			ReadXml(adaptor);
		}

		private void ReadXml(XmlReadAdaptor adaptor)
		{
			_processId = XmlConvert.ToInt32(adaptor.ReadElementString(Constants.Xml.ProcessDetail.ProcessIdElement));
			_processName = adaptor.ReadElementString(Constants.Xml.ProcessDetail.ProcessNameElement);
			_machineName = adaptor.ReadElementString(Constants.Xml.ProcessDetail.MachineNameElement);
		    _commandLineArgs = adaptor.ReadElementString(Constants.Xml.ProcessDetail.CommandLineArgsElement);
		}

		#endregion

		#region IBinarySerializable Members

		void IBinarySerializable.Write(BinaryWriter writer)
		{
			writer.Write(_processId);
			writer.Write(_processName);
			writer.Write(_machineName);
            writer.Write(_commandLineArgs);
		}

		void IBinarySerializable.Read(BinaryReader reader)
		{
			_processId = reader.ReadInt32();
			_processName = reader.ReadString();
			_machineName = reader.ReadString();
		    _commandLineArgs = reader.ReadString();
		}

		#endregion

		#region ICloneable Members

		object System.ICloneable.Clone()
		{
			return MemberwiseClone();
		}

		#endregion

		#region IInternable Members

		public void Intern(Interner interner)
		{
			const string method = "Intern";

			if (interner == null)
				throw new NullParameterException(GetType(), method, "interner");

			_processName = interner.Intern(_processName);
			_machineName = interner.Intern(_machineName);
		}

		#endregion

		private static void LogInitialisationError(System.Exception ex)
		{
			try
			{
				if ( !EventLog.Exists(Constants.SystemLog.LogName, ".") )
				{
                    var data = new EventSourceCreationData(Constants.SystemLog.EventSource, Constants.SystemLog.LogName);
					EventLog.CreateEventSource(data);
				}

				var eventLog = new EventLog(Constants.SystemLog.LogName, ".", Constants.SystemLog.EventSource);

				string exceptionMessage;
				try
				{
					exceptionMessage = ex.ToString();
				}
				catch (System.Exception)
				{
					exceptionMessage = string.Format("An exception of type {0} was thrown.", ex);
				}

				eventLog.WriteEntry(typeof(ProcessDetail).FullName + " failed to initialise:" + System.Environment.NewLine
					+ exceptionMessage, EventLogEntryType.Error);
			}
			catch ( System.Exception )
			{
			}
		}

		#region Member Variables

		private static readonly int GenerateProcessId;
		private static readonly string GenerateProcessName;
		private static readonly string GenerateMachineName;
        private static readonly string GenerateCommandLineArgs;

		private int _processId;
		private string _processName = string.Empty;
		private string _machineName = string.Empty;
	    private string _commandLineArgs = string.Empty;

	    #endregion
	}

	public class ProcessDetailFactory
		:	IEventDetailFactory
	{
		public string Name
		{
			get { return typeof(ProcessDetail).Name; }
		}

		public IEventDetail CreateInstance()
		{
			return new ProcessDetail();
		}
	}
}
