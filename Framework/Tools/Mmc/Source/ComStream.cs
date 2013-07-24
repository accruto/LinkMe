using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using STATSTG = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace LinkMe.Framework.Tools.Mmc
{
	internal class ComStream
		:	Stream
	{
		public ComStream(IStream stream)
			:	base()
		{
			m_stream = stream;		
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanSeek
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return true; }
		}

		public override long Length
		{
			get
			{ 
				STATSTG statstg;
				m_stream.Stat(out statstg, 1 /*STATFLAG_NONAME*/);
				return statstg.cbSize;
			}
		}

		public override long Position
		{
			get { return Seek(0, SeekOrigin.Current); }
			set { Seek(value, SeekOrigin.Begin); }
		}

		public override void Close()
		{
			m_stream.Commit(0);
			Marshal.ReleaseComObject(m_stream);
			m_stream = null;
			GC.SuppressFinalize(this);
		}

		public override void Flush()
		{
			m_stream.Commit(0);
		}

		public unsafe override long Seek(long offset, SeekOrigin origin)
		{
			long position = 0;
			IntPtr address = new IntPtr(&position);
			m_stream.Seek(offset, (int) origin, address);
			return position;
		}
		
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			if ( offset != 0 )
				throw new NotSupportedException("Only a zero offset is supported.");

			int bytesRead;
			IntPtr address = new IntPtr(&bytesRead);
			m_stream.Read(buffer, count, address);
			return bytesRead;
		}

		public override void SetLength(long value)
		{
			m_stream.SetSize(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if ( offset != 0 )
				throw new NotSupportedException("Only a zero offset is supported.");
			m_stream.Write(buffer, count, IntPtr.Zero);
		}

		private IStream m_stream;
	}
}
