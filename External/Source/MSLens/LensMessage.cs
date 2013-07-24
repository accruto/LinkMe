using System;
using System.IO;
using System.Text;
using System.Net.Sockets;

namespace com.bgt.lens
{
	/// <summary>
	/// <code>LensMessage</code> object is composed of a <code>Header</code> and a message body
	/// Communication between Lens and its clients are done through plain/secured Socket communications.
	/// Any request to the Lens server has to be sent through this message object, Lens server
	/// will also reply back to each request using thos message object.
	///
	/// By default the constructor is restricted for public access, clients has to create
	/// the instances using any of the <code>Create()</code> methods.
	///
	/// Example:
	///
	///       String messageData = "&lt;bgtcmd&gt;&lt;info&gt;&lt;/info&gt;&lt;/bgtcmd&gt;";
	///       LensMessage infoMessage = LensMessage.Create(messageData, LensMessage.XML_TYPE);
	///       session.sendMessage(infoMessage);
	/// </summary>
			public class LensMessage
			{
				public const short ERROR_TYPE = 2;
				public const short PING_TYPE = 6;
				public const short TAG_TYPE = 8;
				public const short XML_TYPE = 9;
				public const short LOGIN_TYPE = 10;
				public const short CONVERT_TYPE = 11;
				public const short TAG_RTF_TYPE = 12;

				private Header header = null;
				private byte[] messageData = null;

				private static int MESSAGE_ID = 1;

				/// <summary>
				/// Default constructor
				/// </summary>
				/// <param name="header"></param>
				/// <param name="messageData"></param>
				private LensMessage(Header header, string messageData) 
				{
					this.header = header;
					this.messageData = ToBytes(messageData);
				}

				/// <summary>
				/// Constructor to create Message with binary data
				/// </summary>
				/// <param name="header"></param>
				/// <param name="messageData"></param>
				private LensMessage(Header header, byte[] messageData) 
				{
					this.header = header;
					this.messageData = messageData;
				}

				/// <summary>
				/// Method to create a LensMessage object with pre-created message header.
				/// </summary>
				/// <param name="header"></param>
				/// <param name="messageData"></param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(Header header, String messageData)
				{
					return new LensMessage( header, messageData );
				}

				/// <summary>
				/// Method to create a LensMessage object with binary data 
				/// and pre-created message header.
				/// </summary>
				/// <param name="header"></param>
				/// <param name="messageData"></param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(Header header, byte[] messageData)
				{
					return new LensMessage( header, messageData );
				}

				/// <summary>
				/// Method to create a LensMessage object with default message id and flags
				/// </summary>
				/// <param name="messageData"></param>
				/// <param name="messageType">
				/// <see cref="ERROR_TYPE"/>
				/// <see cref="PING_TYPE"/>
				/// <see cref="TAG_TYPE"/>
				/// <see cref="LOGIN_TYPE"/>
				/// </param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(string messageData, short messageType)
				{
					return Create(messageData, ++MESSAGE_ID, messageType);
				}

				/// <summary>
				/// Method to create a LensMessage object with binary message data, 
				/// default message id and flags
				/// </summary>
				/// <param name="messageData"></param>
				/// <param name="messageType">
				/// <see cref="ERROR_TYPE"/>
				/// <see cref="PING_TYPE"/>
				/// <see cref="TAG_TYPE"/>
				/// <see cref="LOGIN_TYPE"/>
				/// </param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(byte[] messageData, short messageType)
				{
					Header header = Header.Create(messageData.Length + 12,0,Header.VERSION,++MESSAGE_ID,messageType);
					return new LensMessage(header, messageData);
				}

				/// <summary>
				/// Method to create a LensMessage object with default flags
				/// </summary>
				/// <param name="messageData"></param>
				/// <param name="messageID"></param>
				/// <param name="messageType">
				/// <see cref="ERROR_TYPE"/>
				/// <see cref="PING_TYPE"/>
				/// <see cref="TAG_TYPE"/>
				/// <see cref="LOGIN_TYPE"/>
				/// </param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(String messageData, int messageID, short messageType)
				{
					return Create(messageData, ++MESSAGE_ID, 0,messageType);
				}

				/// <summary>
				/// Method to create a LensMessage object with binary data and default flags
				/// </summary>
				/// <param name="messageData"></param>
				/// <param name="messageID"></param>
				/// <param name="messageType">
				/// <see cref="ERROR_TYPE"/>
				/// <see cref="PING_TYPE"/>
				/// <see cref="TAG_TYPE"/>
				/// <see cref="LOGIN_TYPE"/>
				/// </param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(byte[] messageData, int messageID, short messageType)
				{
					return Create(messageData, ++MESSAGE_ID, 0,messageType);
				}

				/// <summary>
				/// Method to create a LensMessage object.
				/// The flags field contains bit values which indicate things like the 
				/// presence of encryption or compression
				/// </summary>
				/// <param name="messageData"></param>
				/// <param name="messageID"></param>
				/// <param name="flags"></param>
				/// <param name="messageType">
				/// <see cref="ERROR_TYPE"/>
				/// <see cref="PING_TYPE"/>
				/// <see cref="TAG_TYPE"/>
				/// <see cref="LOGIN_TYPE"/>
				/// </param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(String messageData, int messageID, int flags, short messageType)
				{
					Header header = Header.Create(ToBytes(messageData).Length + 12, flags, Header.VERSION, messageID, messageType);
					LensMessage message = new LensMessage( header, messageData );
					return message;
				}

				/// <summary>
				/// Method to create a LensMessage object with binary message data.
				/// The flags field contains bit values which indicate things like
				/// the presence of encryption or compression
				/// </summary>
				/// <param name="messageData"></param>
				/// <param name="messageID"></param>
				/// <param name="flags"></param>
				/// <param name="messageType">
				/// <see cref="ERROR_TYPE"/>
				/// <see cref="PING_TYPE"/>
				/// <see cref="TAG_TYPE"/>
				/// <see cref="LOGIN_TYPE"/>
				/// </param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(byte[] messageData, int messageID, int flags, short messageType)
				{
					Header header = Header.Create(messageData.Length + 12, flags, Header.VERSION, messageID, messageType);
					LensMessage message = new LensMessage( header, messageData );
					return message;
				}

				/// <summary>
				/// Method to create a LensMessage object with binary message data.
				/// The flags field contains bit values which indicate things like
				/// the presence of encryption or compression
				/// </summary>
				/// <param name="messageData"></param>
				/// <param name="messageID"></param>
				/// <param name="flags"></param>
				/// <param name="version"></param>
				/// <param name="messageType">
				/// <see cref="ERROR_TYPE"/>
				/// <see cref="PING_TYPE"/>
				/// <see cref="TAG_TYPE"/>
				/// <see cref="LOGIN_TYPE"/>
				/// </param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(String messageData, int messageID, int flags, short version, short messageType)
				{
					Header header = Header.Create(messageData.Length + 12, flags, version, messageID, messageType);
					LensMessage message = new LensMessage( header, messageData );
					return message;
				}

				/// <summary>
				/// Method to create a LensMessage object with binary message data.
				/// The flags field contains bit values which indicate things like
				/// the presence of encryption or compression
				/// </summary>
				/// <param name="messageData"></param>
				/// <param name="messageID"></param>
				/// <param name="flags"></param>
				/// <param name="version"></param>
				/// <param name="messageType">
				/// <see cref="ERROR_TYPE"/>
				/// <see cref="PING_TYPE"/>
				/// <see cref="TAG_TYPE"/>
				/// <see cref="LOGIN_TYPE"/>
				/// </param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(byte[] messageData, int messageID, int flags, short version, short messageType)
				{
					Header header = Header.Create(messageData.Length + 12, flags, version, messageID, messageType);
					LensMessage message = new LensMessage( header, messageData );
					return message;
				}

				/// <summary>
				/// Method to create a lens XML message type, content of the file will be
				/// considered as a BGT Lens XML command
				/// </summary>
				/// <param name="file">FileInfo</param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(FileInfo file)
				{
					try
					{
						byte[] data = ReadFile(file);
						return Create(data, LensMessage.XML_TYPE);
					}
					catch(IOException iex)
					{
						throw new LensException(ErrorIndex.IO_READ_ERROR,iex);
					}
				}

				/// <summary>
				/// Method to create a lens with the File content as message data
				/// </summary>
				/// <param name="file"></param>
				/// <param name="messageType"></param>
				/// <returns>LensMessage</returns>
				public static LensMessage Create(FileInfo file, short messageType)
				{
					try
					{
						byte[] data = ReadFile(file);
						return Create(data, messageType);
					}
					catch(IOException iex)
					{
						throw new LensException(ErrorIndex.IO_READ_ERROR,iex);
					}
				}

				/// <summary>
				/// Method to create a LensMessage with BG Error XML format
				/// </summary>
				/// <param name="message"></param>
				/// <returns></returns>
				public static LensMessage CreateErrorMessage(String message)
				{
					String cmd = "<bgtres> " + message + "</bgtres>";
					return Create(cmd, LensMessage.XML_TYPE);
				}

				/// <summary>
				/// Method to read the entire content of a file into a byte array
				/// </summary>
				/// <param name="filePath"></param>
				/// <returns></returns>
				public static byte[] ReadFile(string filePath)
				{
					FileStream fin = new FileStream(filePath, FileMode.Open);
					byte[] data = new byte[fin.Length];
					fin.Read(data,0,data.Length);
					return data;
				}

				public static byte[] ReadFile(FileInfo file)
				{
					return ReadFile(file.FullName);
				}

				/// <summary>
				/// Method to convert a string into byte array
				/// </summary>
				/// <param name="str"></param>
				/// <returns></returns>
				public static byte[] ToBytes(string str)
				{
					//ASCIIEncoding encoding = new ASCIIEncoding();
					UTF8Encoding encoding = new UTF8Encoding();

					return encoding.GetBytes(str);
				}

				/// <summary>
				/// Method to convert a byte arry into a String object.
				/// </summary>
				/// <param name="barr"></param>
				/// <returns></returns>
				public static string ToString(byte[] barr)
				{
					//ASCIIEncoding encoding = new ASCIIEncoding();
					UTF8Encoding encoding = new UTF8Encoding();

					return encoding.GetString(barr);
				}

				/// <summary>
				/// Method to extract the type (extension) of a file
				/// </summary>
				/// <param name="file"></param>
				/// <returns></returns>
				public static String GetDocType(string file)
				{
					String docType = "";
					int extIndex = file.LastIndexOf(".");
					if( extIndex != -1 )
					{
						docType = file.Substring(extIndex+1, file.Length - (extIndex +1));
					}
					return docType;
				}

				/// <summary>
				/// Method to serialize and write the message into the specified stream
				/// </summary>
				/// <param name="stream"></param>
				public void WriteToStream( Stream stream )
				{
					stream.Write( header.GetBytes(), 0, Header.HEADER_SIZE );
					stream.Write( messageData, 0, messageData.Length );
				}

                public void WriteToSocket(TcpClient tcpClient) {
                    tcpClient.GetStream().Write(header.GetBytes(), 0, header.GetBytes().Length);
                    tcpClient.GetStream().Write(messageData, 0, messageData.Length);
                }
				/// <summary>
				/// Method to serialize and write the message
				/// into a TCP socket.
				/// </summary>
				/// <param name="inSocket"></param>
				public void WriteToSocket( Socket inSocket)
				{
					inSocket.Send( header.GetBytes() );
					inSocket.Send( messageData );
				}

				/// <summary>
				/// Method to retrive message body
				/// </summary>
				/// <returns>string</returns>
				public string GetMessageData() 
				{
					if( messageData == null)
					{
						return null;
					}
					return LensMessage.ToString(messageData);
				}

				/// <summary>
				/// Method to retrive message body as byte array.
				/// </summary>
				/// <returns>byte[]</returns>
				public byte[] GetMessageDataAsByteArray()
				{
					return messageData;
				}

				/// <summary>
				/// Method to get the size of the message
				/// </summary>
				/// <returns>int</returns>
				public int GetSize() 
				{
					return messageData.Length;
				}

				/// <summary>
				/// Method to get the type of the message (tag, xml, ping, login, .. etc)
				/// </summary>
				/// <returns>short</returns>
				public short GetMessageType() 
				{
					return header.GetMessageType();
				}

				/// <summary>
				/// Method to get the message id
				/// </summary>
				/// <returns>int</returns>
				public int GetMessageID() 
				{
					return header.GetMessageID();
				}

				/// <summary>
				/// Method to get the user defined flags
				/// </summary>
				/// <returns>int</returns>
				public int GetFlags() 
				{
					return header.GetFlags();
				}

				/// <summary>
				/// Method to get the BGT message API version
				/// </summary>
				/// <returns>short</returns>
				public short GetVersion() 
				{
					return header.GetVersion();
				}
			}
}
