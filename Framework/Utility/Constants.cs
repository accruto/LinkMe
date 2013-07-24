using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LinkMe.Framework.Utility.Constants
{
	#region Xml

	public static class Xml
	{
		public const string RootNamespace				= "http://xmlns.linkme.com.au/Framework";
		public const string Namespace					= RootNamespace + "/Utility";
		public const string Prefix					    = "lmu";

		internal const string DescriptionElement		= "description";
		internal const string AssemblyElement			= "assembly";
		internal const string KeyFileElement			= "keyFile";
		internal const string ClassElement				= "class";
		internal const string DefaultElement			= "default";
		internal const string NameAttribute				= "name";
		internal const string ClassAttribute			= "class";
		internal const string VersionAttribute			= "version";
		internal const string IsNullAttribute			= "isNull";

		internal static class Exception
		{
			internal const string ExceptionElement = "Exception";
			internal const string MessageElement = "Message";
			internal const string SourceElement = "Source";
			internal const string MethodElement = "Method";
			internal const string TimeElement = "Time";
			internal const string StackTraceElement = "StackTrace";
			internal const string InnerExceptionElement = "InnerException";
			internal const string DetailsElement = "Details";
			internal const string PropertiesElement = "Properties";

			internal const string ClassAttribute = "class";
			internal const string IsSystemAttribute = "isSystem";
		}

		internal static class EventDetails
		{
			internal const string RootElement			= "EventDetails";
			internal const string EventDetailElement	= "EventDetail";
			internal const string ClassAttribute			= "class";
		}

		internal static class NetDetail
		{
			internal const string Name					= "NetDetail";
			internal const string AppDomainNameElement	= "AppDomainName";
            internal const string SessionIdElement      = "SessionId";
        }

		internal static class ProcessDetail
		{
			internal const string Name					= "ProcessDetail";
			internal const string ProcessIdElement		= "ProcessId";
			internal const string ProcessNameElement	= "ProcessName";
			internal const string MachineNameElement	= "MachineName";
            internal const string CommandLineArgsElement = "CommandLineArgs";
            internal const string ThreadIdElement = "ThreadId";
		}

		internal static class SecurityDetail
		{
			internal const string Name						= "SecurityDetail";
			internal const string ProcessUserNameElement	= "ProcessUserName";
			internal const string AuthenticationTypeElement	= "AuthenticationType";
			internal const string IsAuthenticatedElement	= "IsAuthenticated";
			internal const string UserNameElement			= "UserName";
		}

		internal static class ThreadDetail
		{
			internal const string Name					= "ThreadDetail";
			internal const string ThreadId				= "ThreadId";
			internal const string ApartmentState		= "ApartmentState";
			internal const string Culture				= "Culture";
			internal const string ThreadName			= "ThreadName";
			internal const string State					= "State";
			internal const string Priority				= "Priority";
			internal const string IsThreadPoolThread	= "IsThreadPoolThread";
			internal const string Sequence				= "Sequence";
		}
	}

	#endregion

	#region Xsi

	public static class Xsi
	{
		public const string Prefix						= "xsi";
		public const string Namespace						= System.Xml.Schema.XmlSchema.InstanceNamespace;
		public const string NilAttribute					= "nil";
		public const string TypeAttribute					= "type";
	}

	#endregion

	public static class Xmlns
	{
        public const string Prefix = "xmlns";
        public const string Namespace = "http://www.w3.org/2000/xmlns/";
	}

	#region Exception

	internal static class Exceptions
	{
		internal const string ResourceBaseName = "LinkMe.Framework.Utility.Exceptions.Exceptions";

		internal const string Parameter = "Parameter";
		internal const string ExpectedCount = "ExpectedCount";
		internal const string ParameterCount = "ParameterCount";
		internal const string ParameterType = "ParameterType";
		internal const string ParameterValue = "ParameterValue";
		internal const string MinValue = "MinValue";
		internal const string MaxValue = "MaxValue";
		internal const string ParameterLength = "ParameterLength";
		internal const string MinLength = "MinLength";
		internal const string MaxLength = "MaxLength";
		internal const string ExpectedType = "ExpectedType";
		internal const string ExpectedFormat = "ExpectedFormat";
		internal const string Type = "Type";
		internal const string Assembly = "Assembly";
		internal const string InterfaceType = "InterfaceType";
		internal const string AssemblyName = "AssemblyName";
		internal const string FullName = "FullName";
		internal const string Element = "Element";
		internal const string Attribute = "Attribute";
		internal const string QueryString = "QueryString";
		internal const string Scope = "Scope";
		internal const string PropertyName = "PropertyName";
		internal const string PropertyValue = "PropertyValue";
		internal const string ClassPath = "ClassPath";
		internal const string ClassText = "ClassText";
		internal const string FileName = "FileName";
		internal const string ServerAndNamespace = "ServerAndNamespace";
		internal const string CompilePhase = "CompilePhase";
		internal const string ObjectNumber = "ObjectNumber";
		internal const string FirstLine = "FirstLine";
		internal const string LastLine = "LastLine";
		internal const string NativeErrorCode = "NativeErrorCode";
		internal const string Facility = "Facility";
		internal const string Description = "Description";
		internal const string KeyName = "KeyName";
		internal const string ValueName = "ValueName";
		internal const string Path = "Path";
		internal const string ConnectionString = "ConnectionString";
		internal const string ServerVersion = "ServerVersion";
		internal const string CommandType = "CommandType";
		internal const string CommandText = "CommandText";
		internal const string CommandTimeout = "CommandTimeout";
		internal const string CommandParameters = "CommandParameters";
		internal const string Key = "Key";
		internal const string KeyValue = "KeyValue";
		internal const string Variable = "Variable";
		internal const string VariableValue = "VariableValue";
		internal const string XmlString = "XmlString";
		internal const string Prefix = "Prefix";
	}

	#endregion

	#region Serialization

	internal static class Serialization
	{
		internal const string Details = "Details";
		internal const string BinarySerializableStreamKey = "IBinarySerializable_stream";

		internal static class Exception
		{
			internal const string StackTrace			= "StackTrace";
			internal const string Method				= "Method";
			internal const string Time					= "Time";
			internal const string Details				= "Details";
			internal const string Properties			= "Properties";
			internal const string Class					= "Class";
		}

		internal static class NetDetail
		{
			internal const string AppDomainName = "AppDomainName";
		    internal const string SessionId = "SessionId";
		}

		internal static class ProcessDetail
		{
			internal const string ProcessId = "ProcessId";
			internal const string ProcessName = "ProcessName";
			internal const string MachineName = "MachineName";
		    internal const string CommandLineArgs = "CommandLineArgs";
		}

		internal static class SecurityDetail
		{
			internal const string ProcessUserName = "ProcessUserName";
			internal const string AuthenticationType = "AuthenticationType";
			internal const string IsAuthenticated = "IsAuthenticated";
			internal const string UserName = "UserName";
		}

		internal static class ThreadDetail
		{
			internal const string ThreadId = "ThreadId";
			internal const string ApartmentState = "ApartmentState";
			internal const string Culture = "Culture";
			internal const string ThreadName = "ThreadName";
			internal const string State = "State";
			internal const string Priority = "Priority";
			internal const string IsThreadPoolThread = "IsThreadPoolThread";
			internal const string Sequence = "Sequence";
		}

        internal static class GenericDetail
        {
            internal const string Name = "Name";
            internal const string Values = "Values";
        }
	}

	#endregion

	#region Reflection

	internal static class Reflection
	{
		internal const string InnerException = "_innerException";
	}

	#endregion

	internal static class SystemLog
	{
		internal const string LogName						= "Application";
		internal const string EventSource					= "LinkMe Utility";
	}

	internal static class Sql
	{
		internal const string MasterDatabaseName			= "master";
		internal const string ProviderRegex					= @"provider\s*=\s*SQLOLEDB(\.\d+)?\s*(;|$)";

		// The maximum number of SQL statements that should be executed as one SqlCommand. There doesn't seem
		// to be any hard limit.
		internal const int MaximumCommandBatchSize			= 50;
		// The timeout value to assign to a batch command. Caller code can change this before executing the
		// command.
		internal const int BatchCommandTimeout				= 300;
	}

    public static class Win32
    {
        public static class Messages
        {
            public const int WM_CLOSE = 0x10;
            public const int WM_SETCURSOR = 0x20;
            public const int WM_NOTIFY = 0x4E;
            public const int WM_KEYDOWN = 0x100;
            public const int WM_KEYUP = 0x101;
            public const int WM_LBUTTONDBLCLK = 0x203;
            public const int WM_REFLECT = 0x2000;

            public const int EM_SETCHARFORMAT = 0x444;
            public const int EM_SETREADONLY = 0xCF;
            public const int EM_EMPTYUNDOBUFFER = 0xCD;

            public const int CB_DELETESTRING = 0x0144;
            public const int CB_RESETCONTENT = 0x014B;
            public const int CB_SHOWDROPDOWN = 0x014F;

            public const int TVM_DELETEITEM = 0x1101;

            public const int LVM_SETCOLUMNWIDTH = 0x101E;
            public const int LVM_GETHEADER = 0x101F;

            public const int TCM_HITTEST = 0x130D;
        }

        public static class Notifications
        {
            public const int HDN_DIVIDERDBLCLICKA = -305;
            public const int HDN_DIVIDERDBLCLICKW = -325;
            public const int HDN_ENDTRACKA = -307;
            public const int HDN_ENDTRACKW = -327;

            private const int TVN_SELCHANGINGA = -401;
            private const int TVN_SELCHANGINGW = -450;

            public const int TCN_SELCHANGING = -552;

            public static readonly int TVN_SELCHANGING;

            static Notifications()
            {
                switch (Marshal.SystemDefaultCharSize)
                {
                    case 1: // ANSI
                        TVN_SELCHANGING = TVN_SELCHANGINGA;
                        break;

                    case 2: // Unicode
                        TVN_SELCHANGING = TVN_SELCHANGINGW;
                        break;

                    default: // Huh?
                        Debug.Fail("Unexpected value of SystemDefaultCharSize: "
                            + Marshal.SystemDefaultCharSize.ToString());
                        break;
                }
            }
        }

        public static class HResults
        {
            public const int S_OK = 0;
            public const int S_FALSE = 1;

            public const int E_NOINTERFACE = unchecked((int)0x80004002);
            public const int E_NOTIMPL = unchecked((int)0x80004001);

            internal const int CLASS_E_NOAGGREGATION = unchecked((int)0x80040110);
            internal const int CO_E_OBJISREG = unchecked((int)0x800401FC);
        }

        internal static class IIDs
        {
            internal const string IID_IUnknown = "00000000-0000-0000-C000-000000000046";
            internal const string IID_IDispatch = "00020400-0000-0000-C000-000000000046";
            internal const string IID_IClassFactory = "00000001-0000-0000-C000-000000000046";
            internal const string IID_IProvideClassInfo = "B196B283-BAB4-101A-B69C-00AA00341D07";

            internal const string IID_WbemClassObject = "{EB87E1BD-3233-11D2-AEC9-00C04FB68820}";
            internal const string IID_IWbemStatusCodeText = "EB87E1BC-3233-11D2-AEC9-00C04FB68820";
            internal const string IID_MofCompiler = "6DAF9757-2E37-11D2-AEC9-00C04FB68820";
            internal const string IID_IMofCompiler = "6DAF974E-2E37-11D2-AEC9-00C04FB68820";
        }

        public const int GW_CHILD = 5;

        public const int CFM_LINK = 32;
        public const int CFE_LINK = 32;

        public const int SCF_SELECTION = 1;

        internal const int LOCALE_USER_DEFAULT = 1024;

        internal const int IS_TEXT_UNICODE_ASCII16 = 1;
        internal const int IS_TEXT_UNICODE_REVERSE_ASCII16 = 0x10;
        internal const int IS_TEXT_UNICODE_STATISTICS = 2;
        internal const int IS_TEXT_UNICODE_REVERSE_STATISTICS = 0x20;
        internal const int IS_TEXT_UNICODE_CONTROLS = 4;
        internal const int IS_TEXT_UNICODE_REVERSE_CONTROLS = 0x40;
        internal const int IS_TEXT_UNICODE_SIGNATURE = 8;
        internal const int IS_TEXT_UNICODE_REVERSE_SIGNATURE = 0x80;
        internal const int IS_TEXT_UNICODE_ILLEGAL_CHARS = 0x100;
        internal const int IS_TEXT_UNICODE_ODD_LENGTH = 0x200;
        internal const int IS_TEXT_UNICODE_DBCS_LEADBYTE = 0x400;
        internal const int IS_TEXT_UNICODE_NULL_BYTES = 0x1000;
        internal const int IS_TEXT_UNICODE_UNICODE_MASK = 0xF;
        internal const int IS_TEXT_UNICODE_REVERSE_MASK = 0xF0;
        internal const int IS_TEXT_UNICODE_NOT_UNICODE_MASK = 0xF00;
        internal const int IS_TEXT_UNICODE_NOT_ASCII_MASK = 0xF000;

        internal const int LVSCW_AUTOSIZE = -1;
        public const int LVSCW_AUTOSIZE_USEHEADER = -2;

        internal const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100;
        internal const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x2000;
        internal const int FORMAT_MESSAGE_FROM_HMODULE = 0x800;
        internal const int FORMAT_MESSAGE_FROM_STRING = 0x400;
        internal const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;
        internal const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
        internal const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 0xFF;

        internal const int MAX_PATH = 260;
    }
}

