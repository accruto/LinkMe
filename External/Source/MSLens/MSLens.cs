using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.IO;
using System.Threading;

namespace com.bgt.lens
{
	/// <summary>
	/// Summary description for MSLens.
	/// </summary>
	public class MSLens : LensSession
	{

		public const char RESUME_TYPE = 'R';
		public const char POSTING_TYPE = 'P';

		//by default it will be 1 minute
		private static ulong mTransactionTimeout = 60000;

		private static string gLensServerHost = null;
		private static uint gLensServerPort = 0;

		private string mLensServerHost = "localhost";
        private IPAddress mLensServerIp = null;
		private uint mLensServerPort = 2000;

		private static uint mActiveSessions = 0;
	    private TcpClient tcpClient = null;

		private bool mTimedout = false;
		private static bool mTimeoutEnabled = false;

		private Lock mLock = new Lock();

		private MSLens(string lensServerHost, uint lensServerPort)
		{
			this.mLensServerHost = lensServerHost;
			this.mLensServerPort = lensServerPort;
		}

		public static void SetLensServer(string inHost, uint inPort)
		{
			gLensServerHost = inHost;
			gLensServerPort = inPort;
		}

		public void SetTransactionTimeout(ulong timeout)
		{
			if (timeout <= 0) 
			{
				throw new LensException(ErrorIndex.INVALID_TIMEOUT);
			}
			mTransactionTimeout = timeout;
		}

		public void SetEnableTransactionTimeout(bool enable) 
		{
			mTimeoutEnabled = enable;
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static LensSession CreateSession()
		{
			if( gLensServerHost == null )
				throw new LensException(ErrorIndex.INVALID_LENS_HOST);

			if( gLensServerPort <= 0 )
				throw new LensException(ErrorIndex.INVALID_LENS_PORT);

			return CreateSession(gLensServerHost, gLensServerPort);
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static LensSession CreateSession(string inHost, uint inPort)
		{
			return new MSLens(inHost, inPort);
			//return null;
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Open()
		{
			// Escalate it to overloaded <code>open(String,int)</code> method

            // Look up the IP address once.

            if (mLensServerIp == null)
            {
                if (!IPAddress.TryParse(mLensServerHost, out mLensServerIp))
                {
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(mLensServerHost);
                    mLensServerIp = ipHostInfo.AddressList[0];
                }

                Debug.Assert(mLensServerIp != null, "mLensServerIp != null");
            }

            Open(mLensServerIp, mLensServerPort);
		}

        private void Open(IPAddress lensServerIp, uint lensServerPort)
		{

			try 
			{
                IPEndPoint remoteEP = new IPEndPoint(lensServerIp, (int)lensServerPort);

                if (tcpClient == null || !tcpClient.Connected) 
				{
                    tcpClient = new TcpClient(AddressFamily.InterNetwork);
                    tcpClient.Connect(remoteEP);

					++mActiveSessions;
				}
			}
			catch(SocketException sex)
			{
				throw new LensException(ErrorIndex.UNABLE_TO_CONNECT, sex);
			}
			catch (IOException iox) 
			{
				throw new LensException(ErrorIndex.UNABLE_TO_CONNECT, iox);
			}
			catch (Exception ex) 
			{
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
		}

		public bool Ping()
		{
			LensMessage message = LensMessage.Create("",LensMessage.PING_TYPE);
			if (mTimeoutEnabled) 
			{
				SendMessage(message, mTransactionTimeout);
			}
			else 
			{
				SendMessage(message, 0);
			}
			return true;
		}

		public LensMessage SendMessage(LensMessage message)
		{
			if (mTimeoutEnabled) 
			{
				return SendMessage(message, mTransactionTimeout);
			}
			else 
			{
				return SendMessage(message, 0);
			}
		}

		public LensMessage SendMessage(LensMessage message, ulong timeout)
		{

			mLock.Acquire();

			Timer sendTimer = null;
			mTimedout = false;

			try 
			{

				if (!IsOpen()) 
				{
					throw new LensException(ErrorIndex.SESSION_NOT_OPEN);
				}

				if (timeout < 0 ) 
				{
					throw new LensException(ErrorIndex.INVALID_TIMEOUT);
				}
				else
				{
					sendTimer = new Timer(new TimerCallback(this.TimedOut),null,TimeSpan.FromSeconds(Convert.ToDouble(timeout)),TimeSpan.FromSeconds(Convert.ToDouble(timeout)));
				}

                message.WriteToSocket(tcpClient);

				return ReadMessage();
			}
			catch(LensException lex)
			{
				throw lex;
			}
			catch(SocketException sex)
			{
				if( mTimedout)
				{
					throw new LensException(ErrorIndex.UNABLE_TO_CONNECT, sex);
				}
				throw new LensException(ErrorIndex.WRITE_ERROR, sex);
			}
			catch (IOException iex) 
			{
				if (mTimedout) 
				{
					throw new LensException(ErrorIndex.TIMEOUT_ERROR);
				}
				throw new LensException(ErrorIndex.WRITE_ERROR, iex);
			}
			catch (Exception ex) 
			{
				if (mTimedout) 
				{
					throw new LensException(ErrorIndex.TIMEOUT_ERROR);
				}
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
			finally 
			{
				try 
				{
					if( sendTimer != null )
					{
						sendTimer.Dispose();
					}
				}
				catch (Exception ex) 
				{
					Console.WriteLine(ex.StackTrace + "\n");
				}
				mLock.Release();
			}

		}

		private void TimedOut(object state)
		{
			mTimedout = true;
			try 
			{
				// Close the socket connection with server
				// IOException will be thrown if the <code>ReadMessage(long)</code>
				// or <code>SendMessage()</code> is blockig the socket
				Close();
			}
			catch (LensException ex) 
			{
				//!!!! Bad practice, register an error callback handler and pass the error to the caller. !!!
				Console.WriteLine(ex.StackTrace + "\n");
			}
		}
		
		/// <summary>
		/// Method to pack an int into a 4 byte array
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private int UnpackInt(byte[] buffer)
		{
			int result = buffer[3]; // = (byte)(value & 0xFF);
			int tmp = buffer[2];
			tmp  = tmp << 8;
			result = result | tmp; //			= (byte)((value & 0xFF00) >> 8);

			tmp = buffer[1];
			tmp = tmp << 16;
			result = result | tmp; //			= (byte)((value & 0xFF0000) >> 16);

			tmp = buffer[0];
			tmp = tmp << 24;
			result = result | tmp; //			= (byte)((value & 0xFF0000) >> 16);

			return result;
		}

		/// <summary>
		/// Method to pack a short into a 2 byte array
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private short UnpackShort(byte []array)
		{
			uint   result = array[1];
			uint  tmp = array[0];
			tmp = tmp << 8;
			result =  result |  tmp;

			return (short)result;
		}

		private Header ReadHeader()
		{
			int length;
			int flags;
			short version;
			int id;
			short type;

			byte[] bLength = new byte[4];
			byte[] bFlags = new byte[4];
			byte[] bVersion = new byte[2];
			byte[] bID = new byte[4];
			byte[] bType = new byte[2];
            
/*
			mSocket.Receive(bLength);
			mSocket.Receive(bFlags);
			mSocket.Receive(bVersion);
			mSocket.Receive(bID);
			mSocket.Receive(bType);
*/
		    tcpClient.GetStream().Read(bLength, 0, bLength.Length);
            tcpClient.GetStream().Read(bFlags, 0, bFlags.Length);
            tcpClient.GetStream().Read(bVersion, 0, bVersion.Length);
            tcpClient.GetStream().Read(bID, 0, bID.Length);
            tcpClient.GetStream().Read(bType, 0, bType.Length);
			
			length = UnpackInt(bLength); // Convert.ToInt16(LensMessage.toString(bLength));
			flags = UnpackInt(bFlags); // Convert.ToInt16(LensMessage.toString(bFlags));
			version = UnpackShort(bVersion); // Convert.ToInt16(LensMessage.toString(bVersion));
			id = UnpackInt(bID); // Convert.ToInt16(LensMessage.toString(bID));
			type = UnpackShort(bType); // Convert.ToInt16(LensMessage.toString(bType));

			// Construct a new <code>Header</code> and send back to the caller
			return Header.Create(length, flags, version, id, type);

		}

		private LensMessage ReadMessage()
		{
			Header header = null;
			byte[] buffer = null;
			
			if (!IsOpen()) 
			{
				throw new LensException(ErrorIndex.SESSION_NOT_OPEN);
			}

			// Read the message header first
			try 
			{
				header = ReadHeader();
			}
			catch(SocketException sex)
			{
				if (mTimedout) 
				{
					throw new LensException(ErrorIndex.TIMEOUT_ERROR);
				}
				throw new LensException(ErrorIndex.INVALID_HEADER, sex);
			}
			catch (IOException ex) 
			{
				if (mTimedout) 
				{
					throw new LensException(ErrorIndex.TIMEOUT_ERROR);
				}
				throw new LensException(ErrorIndex.INVALID_HEADER, ex);
			}

            if (header.GetLength() == 0)
            {
                throw new LensException(ErrorIndex.INVALID_MESSAGE,
                    new ApplicationException("The message length returned from LENS is 0."));
            }

            //System.Threading.Thread.Sleep(2000);//Delay needed for slower Systems
			//Read the message body
			try 
			{
				// Shouldn't we checkig the validity of the message size??
                // EP 15/01/07: Yes, we should! Checked below.

                try
                {
                    buffer = new byte[header.GetDataLength()];
                    int index = 0;

                    while (index < buffer.Length)
                    {
                        int i = tcpClient.GetStream().ReadByte();
                        if(i == -1)
                            break;

                        buffer[index++] = (byte) i;
                    }
                }
                catch (SocketException sex)
                {
                    if (mTimedout)
                    {
                        throw new LensException(ErrorIndex.TIMEOUT_ERROR);
                    }
                    throw new LensException(ErrorIndex.READ_ERROR, sex);
                }
                catch (IOException iex)
                {
                    if (mTimedout)
                    {
                        throw new LensException(ErrorIndex.TIMEOUT_ERROR);
                    }
                    throw new LensException(ErrorIndex.READ_ERROR, iex);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(string.Format("Failed to read message body. Header values:"
                        + " length = {0}, flags = {1}, version = {2}, ID = {3}, type = {4}",
                        header.GetLength(), header.GetFlags(), header.GetVersion(), header.GetMessageID(),
                        header.GetMessageType()), ex);
                }
			}
			catch (Exception ex) 
			{
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
			//Compose a LensMessage and send back to caller
			return LensMessage.Create(header, buffer);
		}
        
        private object locker = new object();

	    public void Close()
        {
            try
            {
                if (IsOpen()) 
				{
					--mActiveSessions;
                    tcpClient.GetStream().Close();
                    tcpClient.Close();
				}
			}
			catch(SocketException sex)
			{
				throw new LensException(ErrorIndex.COMMUNICATION_ERROR, sex);
			}
			catch (IOException iex) 
			{
				throw new LensException(ErrorIndex.COMMUNICATION_ERROR, iex);
			}
			catch (Exception ex) 
			{
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
			finally 
			{
                tcpClient = null; 
			}
		}

		public bool IsOpen() 
		{
            return (tcpClient != null && tcpClient.Connected);
		}

		public LensMessage ConvertFile(FileInfo file)
		{
			try
			{
				if( !file.Exists )
					throw new LensException(ErrorIndex.FILE_NOT_EXISTS);

				String docType = LensMessage.GetDocType(file.Name);

				byte[] data = LensMessage.ReadFile(file.FullName);

				return ConvertBinaryData(data, docType);
			}
			catch(SocketException sex)
			{
				throw new LensException(ErrorIndex.IO_READ_ERROR, sex);
			}
			catch(LensException lex)
			{
				throw lex;
			}
			catch(IOException iex)
			{
				throw new LensException(ErrorIndex.IO_READ_ERROR, iex);
			}
			catch(Exception ex)
			{
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
		}

		public LensMessage ConvertBinaryData(byte[] data, string docType)
		{
			try
			{
				byte[] hint = null;
				byte[] body = null;

				if (docType != null)
					hint = LensMessage.ToBytes(docType);

				if (hint == null || hint.Length < 1) 
				{
					body = new byte[data.Length + 1];
				}
				else
				{
					body = new byte[hint.Length + data.Length + 1];
				}

				int index = 0;
				if(hint != null)
				{
					for(int i=0;i<hint.Length;i++)
					{
						body[index++] = hint[i];
					}
				}
				body[index++] = Convert.ToByte('\0');

				for(int i=0;i<data.Length;i++)
				{
					body[index++] = data[i];
				}

				//String messageData = LensMessage.toString(body);
				LensMessage convertMessage = LensMessage.Create(body,LensMessage.CONVERT_TYPE);
				return SendMessage(convertMessage);
			}
			catch(LensException lex)
			{
				throw lex;
			}
			catch(Exception ex)
			{
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
		}

		public LensMessage TagFile(FileInfo file, char type)
		{
			try
			{

				if(!file.Exists)
					throw new LensException(ErrorIndex.FILE_NOT_EXISTS);

				string docType = "";
				int extIndex = file.Name.LastIndexOf(".");
				if( extIndex != -1 )
				{
					string fname = file.Name;
					docType = fname.Substring(extIndex,file.Name.Length - extIndex);
					docType = file.Name.Substring(extIndex, file.Name.Length - extIndex );
				}

				byte[] data = LensMessage.ReadFile(file);

				return TagBinaryData(data, docType, type);
			}
			catch(SocketException sex)
			{
				throw new LensException(ErrorIndex.IO_READ_ERROR, sex);
			}
			catch(LensException lex)
			{
				throw lex;
			}
			catch(IOException iex)
			{
				throw new LensException(ErrorIndex.IO_READ_ERROR, iex);
			}
			catch(Exception ex)
			{
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
		}

		public LensMessage TagText(string text, string docType, char type)
		{
			return TagBinaryData( LensMessage.ToBytes( text ) , docType, type);
		}

		public LensMessage TagBinaryData(byte[] data, string docType, char type)
		{
			try
			{
				switch(type)
				{
					case MSLens.RESUME_TYPE:
					case MSLens.POSTING_TYPE:
						break;
					default:
						throw new LensException(ErrorIndex.INVALID_DOC_TYPE);
				}

				byte[] body = null;

				int extraLen = 0;
				if( docType != null && docType.Length > 0 )
				{
					extraLen += docType.Length;
				}

				body = new byte[data.Length + 2 + extraLen];

				int index = 0;
				body[index++] = (byte) type;

				if( extraLen > 0 )
				{
					char[] docTypeBytes = docType.ToCharArray();
					for(int i=0;i<docTypeBytes.Length;i++)
					{
						body[index++] = Convert.ToByte( docTypeBytes[i] );
					}
				}
				body[index++] = Convert.ToByte( '\0' );

				for(int i=0;i<data.Length;i++)
				{
					body[index++] = data[i];
				}

				//String messageData = LensMessage.toString(body);
				LensMessage tagMessage = LensMessage.Create(body,LensMessage.TAG_TYPE);
				return SendMessage(tagMessage);
			}
			catch(LensException lex)
			{
				throw lex;
			}
			catch(Exception ex)
			{
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
		}

		public LensMessage TagFileWithRTF(FileInfo file, char type)
		{
			try
			{
				if(!file.Exists)
					throw new LensException(ErrorIndex.FILE_NOT_EXISTS);

				String docType = "";
				int extIndex = file.Name.LastIndexOf(".");
				if( extIndex != -1 )
				{
					docType = file.Name.Substring(extIndex+1, file.Name.Length);
				}

				byte[] data = LensMessage.ReadFile(file);

				return TagBinaryDataWithRTF(data, docType, type);
			}
			catch(LensException lex)
			{
				throw lex;
			}
			catch(SocketException sex)
			{
				throw new LensException(ErrorIndex.IO_READ_ERROR, sex);
			}
			catch(IOException iex)
			{
				throw new LensException(ErrorIndex.IO_READ_ERROR, iex);
			}
			catch(Exception ex)
			{
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
		}

		public LensMessage TagTextWithRTF(string text, string docType, char type)
		{
			return TagBinaryDataWithRTF(LensMessage.ToBytes(text), docType, type);
		}

		public LensMessage TagBinaryDataWithRTF(byte[] data, string docType, char type)
		{
			try
			{
				switch(type)
				{
					case MSLens.RESUME_TYPE:
					case MSLens.POSTING_TYPE:
						break;
					default:
						throw new LensException(ErrorIndex.INVALID_DOC_TYPE);
				}

				byte[] body = null;

				int extraLen = 0;
				if( docType != null && docType.Length > 0 )
				{
					extraLen += docType.Length;
				}

				body = new byte[data.Length + 2 + extraLen];

				int index = 0;
				body[index++] = (byte) type;

				if( extraLen > 0 )
				{
					char[] docTypeBytes = docType.ToCharArray();
					for(int i=0;i<docTypeBytes.Length;i++)
					{
						body[index++] = (byte) docTypeBytes[i];
					}
				}
				body[index++] = (byte)'\0';

				for(int i=0;i<data.Length;i++)
				{
					body[index++] = data[i];
				}

				//String messageData = LensMessage.toString(body);
				LensMessage tagMessage = LensMessage.Create(body,LensMessage.TAG_RTF_TYPE);
				return SendMessage(tagMessage);
			}
			catch(LensException lex)
			{
				throw lex;
			}
			catch(Exception ex)
			{
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
		}


		public LensMessage RegisterFile(FileInfo file, string vendor, string ID, DocKey docKey, char type)
		{
			try
			{
				if( !file.Exists )
					throw new LensException(ErrorIndex.FILE_NOT_EXISTS);

				byte[] data = LensMessage.ReadFile(file);
				String docType = LensMessage.GetDocType(file.Name);

				return RegisterBinaryData(data, docType, vendor, ID, docKey, type);
			}
			catch(LensException lex)
			{
				throw lex;
			}
			catch(SocketException sex)
			{
				throw new LensException(ErrorIndex.IO_READ_ERROR, sex);
			}
			catch(IOException iex)
			{
				throw new LensException(ErrorIndex.IO_READ_ERROR, iex);
			}
			catch(Exception ex)
			{
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
		}

		public LensMessage RegisterText(string text, string vendor, string ID, DocKey docKey, char type )
		{
			return RegisterBinaryData(LensMessage.ToBytes(text), "", vendor, ID, docKey, type);
		}

		public LensMessage RegisterBinaryData(byte[] data, string docType, string vendor, string ID, DocKey docKey, char type )
		{
			try
			{

				LensMessage tagMessage = TagBinaryData(data, docType, type);
				String result = tagMessage.GetMessageData();

				int spos,epos;
				String cmd = "";

				try 
				{
					switch (type) 
					{
						case MSLens.RESUME_TYPE:
							spos = result.IndexOf("<resume");
							epos = result.LastIndexOf("</resume>") + 9;

							cmd = "<bgtcmd><register type='resume' vendor='";
							cmd += vendor;
							cmd += "' id='";
							cmd += ID;
							cmd += "'>";
							cmd += result.Substring(spos, epos - spos + 1);
							cmd += "</register></bgtcmd>";

							break;
						case MSLens.POSTING_TYPE:
							spos = result.IndexOf("<posting");
							epos = result.LastIndexOf("</posting>") + 10;

							cmd = "<bgtcmd><register type='posting' vendor='";
							cmd += vendor;
							cmd += "' id='";
							cmd += ID;
							cmd += "'>";
							cmd += result.Substring(spos, epos);
							cmd += "</register></bgtcmd>";

							break;
						default:
							throw new LensException(ErrorIndex.INVALID_DOC_TYPE);
					}
				}
				catch (LensException lex) 
				{
					throw lex;
				}
				catch(IndexOutOfRangeException)
				{
					throw new LensException(ErrorIndex.TAG_TEXT_NOT_GENERATED);
					//return LensMessage.CreateErrorMessage("Tag text not generated.");
				}

				LensMessage regMessage = SendMessage(LensMessage.Create(cmd,LensMessage.XML_TYPE));
				result = regMessage.GetMessageData();

				// this is cheating, but we just need to extract the key value
				int pos = result.IndexOf( "key='" );
				if (pos != -1) 
				{
					pos += 5; // skip key='
					string key = result.Substring(pos, result.IndexOf("'", pos));
					docKey.SetKey(ulong.Parse(key));
				}
				else 
				{
					docKey.SetKey(0);
				}


				return regMessage;
			}
			catch(LensException lex)
			{
				throw lex;
			}
			catch(Exception ex)
			{
				throw new LensException(ErrorIndex.INTERNAL_ERR, ex);
			}
		}

		public LensMessage Unregister(string vendor, string docID, DocKey docKey, char type)
		{
			String message = null;
			switch(type)
			{
				case MSLens.RESUME_TYPE:
					message = "<bgtcmd><unregister type = 'resume' vendor='"+ vendor + "' id='" + docID  + "'/></bgtcmd>";
					break;
				case MSLens.POSTING_TYPE:
					message = "<bgtcmd><unregister type = 'posting' vendor='"+ vendor + "' id='" + docID  + "'/></bgtcmd>";
					break;
				default:
					throw new LensException(ErrorIndex.INVALID_DOC_TYPE);
			}
			LensMessage unregMessage = LensMessage.Create(message, LensMessage.XML_TYPE);
			LensMessage outMessage = SendMessage(unregMessage);
			String result = outMessage.GetMessageData();

			// this is cheating, but we just need to extract the key value
			int pos = result.IndexOf( "key='" );
			if( pos != -1 )
			{
				pos += 5;	// skip key='
				String key = result.Substring( pos, result.IndexOf( "'", pos ) );
				docKey.SetKey(ulong.Parse(key));
			}
			else
			{
				docKey.SetKey(0);
			}

			return outMessage;
		}
		public LensMessage Unregister(string vendor, DocKey docKey, char type)
		{
			string message = null;
			switch(type)
			{
				case MSLens.RESUME_TYPE:
					message = "<bgtcmd><unregister type = 'resume' vendor='"+ vendor + "' key='" + docKey.GetKey()  + "'/></bgtcmd>";
					break;
				case MSLens.POSTING_TYPE:
					message = "<bgtcmd><unregister type = 'posting' vendor='"+ vendor + "' key='" + docKey.GetKey()  + "'/></bgtcmd>";
					break;
				default:
					throw new LensException(ErrorIndex.INVALID_DOC_TYPE);
			}
			LensMessage unregMessage = LensMessage.Create(message, LensMessage.XML_TYPE);
			return SendMessage(unregMessage);
		}

		public LensMessage GetInfo()
		{
			String message = "<bgtcmd><info></info></bgtcmd>";
			LensMessage inMessage = LensMessage.Create(message, LensMessage.XML_TYPE);
			return SendMessage(inMessage);
		}

		public uint GetPort() 
		{
			return mLensServerPort;
		}

		public string GetHost() 
		{
			return mLensServerHost;
		}


	}

	class Lock
	{
		private bool mBusy = false;

		public Lock()
		{

		}
		public void Acquire()
		{
			if( mBusy )
				throw new LensException(ErrorIndex.SESSION_REENTRY);
			mBusy = true;
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Release()
		{
			mBusy = false;
		}
	}
}
