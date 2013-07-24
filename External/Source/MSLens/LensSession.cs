using System;
using System.IO;

namespace com.bgt.lens
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public interface LensSession
	{
		/// <summary>
		/// Method to open the session with globaly supplied host-name and port.
		/// </summary>
		/// <exception cref="LensException" />
		void Open();

		/// <summary>
		/// Method to check if the sockect is open and connected
		/// </summary>
		/// <returns>bool</returns>
		bool IsOpen();

		/// <summary>
		/// Method to check if the lens server is active and ready to accept commands.
		/// </summary>
		/// <exception cref="LensException" />
		/// <returns>bool</returns>
		bool Ping();

		/// <summary>
		/// Method to send a message to Lens server.
		/// </summary>
		/// <param name="message">LensMessage</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage SendMessage(LensMessage message);

		/// <summary>
		/// Method to send a message to Lens server within the timeout period.
		/// </summary>
		/// <param name="message">LensMessage</param>
		/// <param name="timeout">long</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage SendMessage(LensMessage message, ulong timeout);

		/// <summary>
		/// Method to convert a binary file into ASCII text.
		/// </summary>
		/// <param name="file">FileInfo</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage ConvertFile(FileInfo file);

		/// <summary>
		/// Method to convert binary data into ASCII text.
		/// </summary>
		/// <param name="data">byte[]</param>
		/// <param name="docType">string</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage ConvertBinaryData(byte[] data, string docType);

		/// <summary>
		/// Method to tag a plain text file into BGT XML.
		/// </summary>
		/// <param name="file">FileInfo</param>
		/// <param name="type">char</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage TagFile(FileInfo file, char type);

		/// <summary>
		/// Method to tag a plain text into BGT XML.
		/// </summary>
		/// <param name="text">string</param>
		/// <param name="docType">string</param>
		/// <param name="type">char</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage TagText(string text, string docType, char type);

		/// <summary>
		/// Method to tag binary data into BGT XML.
		/// </summary>
		/// <param name="data">byte[]</param>
		/// <param name="docType">string</param>
		/// <param name="type">char</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage TagBinaryData(byte[] data, string docType, char type);

		/// <summary>
		/// Method to tag a file into BGT XML with RTF output enabled.
		/// </summary>
		/// <param name="file">FileInfo</param>
		/// <param name="type">char</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage TagFileWithRTF(FileInfo file, char type);

		/// <summary>
		/// Method to tag a plain text into BGT XML with RTF outout enabled.
		/// </summary>
		/// <param name="text">string</param>
		/// <param name="docType">string</param>
		/// <param name="type">char</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage TagTextWithRTF(string text, string docType, char type);

		/// <summary>
		/// Method to tag binary data into BGT XML with RTF output enabled.
		/// </summary>
		/// <param name="data">byte[]</param>
		/// <param name="docType">string</param>
		/// <param name="type">char</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage TagBinaryDataWithRTF(byte[] data, string docType, char type);

		/// <summary>
		/// Method to register a resume/posting file with a vendor
		/// </summary>
		/// <param name="file">FileInfo</param>
		/// <param name="vendor">string</param>
		/// <param name="ID">string</param>
		/// <param name="docKey">DocKey</param>
		/// <param name="type">char</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage RegisterFile(FileInfo file, string vendor, string ID, DocKey docKey, char type);

		/// <summary>
		/// Method to register a resume/posting text with a vendor
		/// </summary>
		/// <param name="text">string</param>
		/// <param name="vendor">string</param>
		/// <param name="ID">string</param>
		/// <param name="docKey">DocKey</param>
		/// <param name="type">char</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage RegisterText(string text,string vendor, string ID, DocKey docKey,  char type);

		/// <summary>
		/// Method to register a resume/posting binary data with a vendor
		/// </summary>
		/// <param name="data">byte[]</param>
		/// <param name="docType">string</param>
		/// <param name="vendor">string</param>
		/// <param name="ID">string</param>
		/// <param name="docKey">DocKey</param>
		/// <param name="type">char</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage RegisterBinaryData(byte[] data,string docType, string vendor, string ID, DocKey docKey,  char type);

		/// <summary>
		/// Method to un-register a resume/posting from a vendor based by document ID
		/// </summary>
		/// <param name="vendor">string</param>
		/// <param name="docID">string</param>
		/// <param name="docKey">DocKey</param>
		/// <param name="type">char</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage Unregister(string vendor, string docID, DocKey docKey, char type);

		/// <summary>
		/// Method to un-register a resume/posting from a vendor based by document key
		/// </summary>
		/// <param name="vendor">string</param>
		/// <param name="docKey">DocKey</param>
		/// <param name="type">char</param>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage Unregister(string vendor, DocKey docKey, char type);

		/// <summary>
		/// Method to get the status of the Lens system
		/// </summary>
		/// <exception cref="LensException" />
		/// <returns>LensMessage</returns>
		LensMessage GetInfo();

		/// <summary>
		/// Method to get the Lens port in which the currect session 
		/// is configured to connect
		/// </summary>
		/// <returns>int</returns>
		uint GetPort();

		/// <summary>
		/// Method to get the Lens host/IP in which the current session 
		/// is configured to connect
		/// </summary>
		/// <returns>string</returns>
		string GetHost();

		/// <summary>
		/// Method to set the transaction timeout for both send/receive messages
		/// </summary>
		/// <param name="timeout">ulong</param>
		void SetTransactionTimeout(ulong timeout);

		/// <summary>
		/// Method used to set the transaction timeout enabling/disabling
		/// If the timeout is enabled, all the send/receive methods will throw 
		/// LensException if the timeout event occurs. 
		/// By default the timeout will be disabled.
		/// </summary>
		/// <param name="enable">bool</param>
		void SetEnableTransactionTimeout(bool enable);

		/// <summary>
		/// Method to close the session and free up resources.
		/// </summary>
		/// <exception cref="LensException" />
		void Close();

	}
}
