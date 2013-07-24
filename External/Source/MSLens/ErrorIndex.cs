using System;
using System.Reflection;
using System.Resources;

namespace com.bgt.lens
{
			/// <summary>
			/// Summary description for ErrorIndex.
			/// </summary>
			public abstract class ErrorIndex
			{
				public const string INTERNAL_ERR					   = "000"; // Internal program error

				public const string UNKNOWN_HOST                = "200"; // Unknown host exception
				public const string UNABLE_TO_CONNECT           = "201"; // Unable to connect to host
				public const string SESSION_NOT_OPEN            = "202"; // Session not opened
				public const string WRITE_ERROR                 = "203"; // Unable to write message
				public const string READ_ERROR                  = "204"; // Unable to read message
				public const string TIMEOUT_ERROR               = "205"; // Timed out while read/write
				public const string FILE_NOT_EXISTS             = "206"; // File does not exists.
				public const string IO_READ_ERROR               = "207"; // IO error occurs while read
				public const string COMMUNICATION_ERROR         = "208"; // Error occured while communicating
				public const string IO_WRITE_ERROR              = "209"; // IO error occurs while write
				public const string SESSION_EXCEEDED            = "210"; // MAX Session count reached
				public const string SESSION_REENTRY             = "211"; // Invalid session state, session is already busy

				public const string INVALID_HEADER              = "300"; // Invalid message header
				public const string INVALID_MESSAGE             = "301"; // Invalid message
				public const string INVALID_TIMEOUT             = "302"; // Invalid timeout value
				public const string INVALID_SESSION_COUNT       = "303"; // Invalid session count value
				public const string INVALID_DOC_TYPE            = "304"; // Invalid document type, <resume|posting>
				public const string INVALID_LENS_HOST           = "305"; // Invalid Lens machine name/ip
				public const string INVALID_LENS_PORT           = "306"; // Invalid Lens port

				public const string TAG_TEXT_NOT_GENERATED      = "401"; // Tagging failed.


				// Load the Locale specific LensError messages, currently only English is implemented
				//private static ResourceManager bundle = new ResourceManager("ErrorIndex",Assembly.GetExecutingAssembly());
				private static LensErrors mErrors = LensErrors.GetInstance();

				/// <summary>
				/// Method to get the description of an error message
				/// </summary>
				/// <param name="errorID"></param>
				/// <returns></returns>
				public static string GetMessage(string errorID)
				{
					//return bundle.GetString(errorID);
					return mErrors.GetError(errorID);
				}
			}
}
