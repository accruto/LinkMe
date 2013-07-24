using System;
using System.Collections;

namespace com.bgt.lens
{
	/// <summary>
	/// Summary description for LensErrors.
	/// </summary>
	public class LensErrors
	{

		private static string[,] mErrors = {
			{ErrorIndex.INTERNAL_ERR,"Internal error."},
			{ErrorIndex.SESSION_EXCEEDED,"Client sessions exceeded the limit."},
			{ErrorIndex.UNKNOWN_HOST, "Unknown host or unable to resolve lens server host name."},
			{ErrorIndex.UNABLE_TO_CONNECT, "Unable to connect with lens server, lens server may not be running."},
			{ErrorIndex.SESSION_NOT_OPEN, "Session with lens server is not open"},
			{ErrorIndex.WRITE_ERROR, "Unable to send the message to lens server."},
			{ErrorIndex.READ_ERROR, "Unable to read the message from lens server."},
			{ErrorIndex.TIMEOUT_ERROR, "Timed out while reading the message from lens server."},
			{ErrorIndex.INVALID_HEADER, "Server returned an invalid message header."},
			{ErrorIndex.INVALID_MESSAGE, "Invalid message length."},
			{ErrorIndex.COMMUNICATION_ERROR, "Error occured while communicating to lens server."},
			{ErrorIndex.INVALID_TIMEOUT, "Invalid timeout value supplied."},
			{ErrorIndex.INVALID_SESSION_COUNT, "Invalid session count value supplied."},
			{ErrorIndex.FILE_NOT_EXISTS, "File not exists."},
			{ErrorIndex.IO_READ_ERROR, "Error occured while reading the file."},
			{ErrorIndex.IO_WRITE_ERROR, "Error occured while writing the file."},
			{ErrorIndex.INVALID_DOC_TYPE, "Invalid document type, should be < R | P >."},
			{ErrorIndex.INVALID_LENS_HOST, "Invalid lens host name/ip, please use setLensServer() method to set the global lens settings."},
			{ErrorIndex.INVALID_LENS_PORT, "Invalid lens host port, please use setLensServer() method to set the global lens settings."},
			{ErrorIndex.SESSION_REENTRY, "Session is busy, re-entry not allowed."},
			{ErrorIndex.TAG_TEXT_NOT_GENERATED, "Tagging/conversion failed, text not generated."},
		};

		private Hashtable mTable = new Hashtable(mErrors.Length);

		private static LensErrors instance = null;

		private LensErrors()
		{
			for(int i=0;i<mErrors.Length/2;i++)
			{
				mTable.Add(mErrors[i,0], mErrors[i,1]);
			}
		}

		public static LensErrors GetInstance()
		{
			if( null == instance)
			{
				instance = new LensErrors();
			}

			return instance;
		}

		public string GetError(string errorKey)
		{
			Object val = mTable[errorKey];
			if( null != val )
			{
				return val.ToString();
			}
			else
			{
				return "Internal error.";
			}
		}

	}
}
