using System;

namespace com.bgt.lens
{
			/// <summary>
			///  This header encapsulates the constants used to identify various
			///	 message parts and the operations that can be performed on a message.
			///	 
			///	 	Currently the messages read from the socket should have an 16
			///	 	byte header followed by the data:
			///	 
			///	 	Header:
			///	 	----------------------------------------------------
			///	 	|  length  |  flags  | version |   id   |   type   |
			///	 	----------------------------------------------------
			///	 	  4 bytes    4 bytes   2 bytes   4 bytes  2 bytes
			///	 
			///	 	The length field contains the size of the message from the flags
			///	 	to the end of the type specific data.
			///	 
			///	 	The flags field contains bit values which indicate things like
			///	 	the presence of encryption or compression.
			///	 
			///	 	The version field indicates the current message layout version.
			///	 
			///	 	The id field is used to track messages. It is sender originated
			///	 	and will be copied into any reponses that are generated.
			///	 
			///	 	The type field indicates what kind of message this is: request,
			///	 	response, control, etc.
			///	 
			///	 	All values assumed to be in network byte order.
			/// </summary>
			public class Header
			{
				public const int		HEADER_SIZE = 16;
				public const int		INT_SIZE = 4;
				public const int		SHORT_SIZE = 2;
				public const short		VERSION = 1;

				private int length;
				private int flags;
				private int messageID;
				private short version;
				private short type;

				/// <summary>
				/// Default constructor, public access restricted.
				/// </summary>
				private Header() 
				{
					// dummy code to see if empty functions create problems
					length = 1;
				}
				
				/// <summary>
				/// Method to create a Header object
				/// </summary>
				/// <param name="length"></param>
				/// <param name="flags"></param>
				/// <param name="version"></param>
				/// <param name="id"></param>
				/// <param name="type"></param>
				/// <returns>Header</returns>
				public static Header Create(int length, int flags, short version, int id, short type)
				{
					Header newHeader = new Header();
					newHeader.length = length;
					newHeader.flags = flags;
					newHeader.messageID = id;
					newHeader.version = version;
					newHeader.type = type;

					return newHeader;
				}

				/// <summary>
				/// Method to pack the message header fields into a 16 byte array.
				/// </summary>
				/// <param name="length"></param>
				/// <param name="flags"></param>
				/// <param name="version"></param>
				/// <param name="id"></param>
				/// <param name="type"></param>
				/// <returns></returns>
				private byte[] PackHeader(int length, int flags, short version, int id, short type)
				{
					byte[] header = new byte[ HEADER_SIZE ];

					byte[] aLength = PackInteger( length );
					byte[] aFlags = PackInteger( flags );
					byte[] aVersion = PackShort( version );
					byte[] aId = PackInteger( id );
					byte[] aType = PackShort( type );

					int j=0;

					for(int i=0;i<aLength.Length;i++)
						header[j++] = aLength[i];
					for(int i=0;i<aFlags.Length;i++)
						header[j++] = aFlags[i];
					for(int i=0;i<aVersion.Length;i++)
						header[j++] = aVersion[i];
					for(int i=0;i<aId.Length;i++)
						header[j++] = aId[i];
					for(int i=0;i<aType.Length;i++)
						header[j++] = aType[i];

					return header;
				}
	
				/// <summary>
				/// Method to pack an int into a 4 byte array
				/// </summary>
				/// <param name="value"></param>
				/// <returns></returns>
				private byte[] PackInteger(int value)
				{
					byte[] buffer = new byte[4];

					buffer[3] = (byte)(value & 0xFF);
					buffer[2] = (byte)((value & 0xFF00) >> 8);
					buffer[1] = (byte)((value & 0xFF0000) >> 16);
					buffer[0] = (byte)((value & 0xFF000000) >> 24);

					return buffer;
				}

				/// <summary>
				/// Method to pack a short into a 2 byte array
				/// </summary>
				/// <param name="value"></param>
				/// <returns></returns>
				private byte[] PackShort(short value)
				{
					byte[] array = new byte[2];

					array[1] = (byte)(value & 0xFF);
					array[0] = (byte)((value & 0xFF00) >> 8);

					return array;
				}
	
				/// <summary>
				/// Method to get the composed message header files as 16 byte array
				/// </summary>
				/// <returns></returns>
				public byte[] GetBytes()
				{
					return PackHeader( GetLength(), GetFlags(), GetVersion(), GetMessageID(), GetMessageType() );
				}

				/// <summary>
				/// Method to get the length of the message
				/// </summary>
				/// <returns></returns>
				public int GetLength() 
				{
					return length;
				}

                public int GetDataLength()
                {
                    return length - 12;
                }

				/// <summary>
				/// Method to set the size of the message
				/// </summary>
				/// <param name="length"></param>
				public void SetLength(int length) 
				{
					this.length = length;
				}

				/// <summary>
				/// Method to get the user defined flags
				/// </summary>
				/// <returns></returns>
				public int GetFlags() 
				{
					return flags;
				}

				/// <summary>
				/// Method to set the user defined message flags
				/// </summary>
				/// <param name="flags"></param>
				public void SetFlags(int flags) 
				{
					this.flags = flags;
				}

				/// <summary>
				/// Method to get the message ID
				/// </summary>
				/// <returns></returns>
				public int GetMessageID() 
				{
					return messageID;
				}

				/// <summary>
				/// Method to set the message ID
				/// </summary>
				/// <param name="messageID"></param>
				public void SetMessageID(int messageID) 
				{
					this.messageID = messageID;
				}

				/// <summary>
				/// Method to get the BGT message version
				/// </summary>
				/// <returns></returns>
				public short GetVersion() 
				{
					return version;
				}

				/// <summary>
				/// Method to set the BGT message version
				/// </summary>
				/// <param name="version"></param>
				public void SetVersion(short version) 
				{
					this.version = version;
				}

				/// <summary>
				/// Method to get the type of the messsage
				/// </summary>
				/// <returns></returns>
				public short GetMessageType() 
				{
					return type;
				}

				/// <summary>
				/// Method to set the type of the message
				/// </summary>
				/// <param name="type"></param>
				public void SetMessageType(short type) 
				{
					this.type = type;
				}

				/// <summary>
				/// Method used while debugging, overrided the Object method.
				/// </summary>
				/// <returns>string</returns>
				public override string ToString()
				{
					return
						"length=" + GetLength() + " " +
						"flags=" + GetFlags() + " " +
						"version=" + GetVersion() + " " +
						"id=" + GetMessageID()+ " " +
						"type=" + GetType();
				}
			}
}
