using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using LinkMe.Framework.Utility;

namespace LinkMe.Utility.Utilities
{
	public sealed class StreamHelper
	{
		private const int DEFAULT_BUFFER_SIZE = 32768;

        private static readonly byte[] _gzipFileSignature = new[] { (byte)0x1F, (byte)0x8B, (byte)0x08 };

		private StreamHelper()
		{
		}

		public static string ReadManifestResourceFromAssembly(string manifestResourceName)
		{
			string manfestResource;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(manifestResourceName))
			{
				StreamReader reader = new StreamReader(stream);
				manfestResource = reader.ReadToEnd();
				reader.Close();
			}
			return manfestResource;
		}

		public static string ReadManifestResourceFromNamedAssembly(string assemblyName , string manifestResourceName)
		{
			string manfestResource;
			using (Stream stream = Assembly.Load(assemblyName).GetManifestResourceStream(manifestResourceName))
			{
				StreamReader reader = new StreamReader(stream);
				manfestResource = reader.ReadToEnd();
				reader.Close();
			}
			return manfestResource;
		}

		public static string ReadFileToString(string filename)
		{
			using (StreamReader reader = new StreamReader(filename))
			{
				return reader.ReadToEnd();
			}
		}

		public static byte[] ReadFile(string filename)
		{
			using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				return ReadFully(fs);
			}
		}

		public static byte[] ReadFully(Stream stream)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");

			// Get the length of the stream, if supported.

			long length = 0;
			if (stream.CanSeek)
			{
				try
				{
					length = stream.Length;
				}
				catch (NotSupportedException)
				{
					Debug.Fail("A stream of type '" + stream.GetType().FullName
						+ "' supports seeking, but does not support the Length property.");
				}
			}

			return ReadFully(stream, length);
		}

		// See http://www.developerfusion.co.uk/show/4696 for the reason why this
		// code is needed.
		public static byte[] ReadFully(Stream stream, long initialLength)
		{
            // Reset to the start of the stream, if supported.

            if (stream.CanSeek)
            {
                stream.Position = 0;
            }

			// If we've been passed an unhelpful initial length, just use 32K.

			if (initialLength < 1)
			{
				initialLength = DEFAULT_BUFFER_SIZE;
			}

			byte[] buffer = new byte[initialLength];
			int read = 0;

			int chunk;
			while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
			{
				read += chunk;

				// If we've reached the end of our buffer, check to see if there's
				// any more information.

				if (read == buffer.Length)
				{
					int nextByte = stream.ReadByte();

					// End of stream? If so, we're done
					if (nextByte == -1)
						return buffer;

					// Nope. Resize the buffer, put in the byte we've just
					// read, and continue.

					byte[] newBuffer = new byte[buffer.Length*2];
					Array.Copy(buffer, newBuffer, buffer.Length);
					newBuffer[read] = (byte) nextByte;
					buffer = newBuffer;
					read++;
				}
			}

			if (read == buffer.Length)
				return buffer;

			// Buffer is now too big. Shrink it.

			byte[] ret = new byte[read];
			Array.Copy(buffer, ret, read);
			return ret;
		}

		public static void CopyStream(Stream source, Stream destination)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (destination == null)
				throw new ArgumentNullException("destination");
			if (!source.CanRead)
				throw new ArgumentException("The source stream does not support reading.", "source");
			if (!destination.CanWrite)
				throw new ArgumentException("The destination stream does not support writing.", "destination");

			byte[] buffer = new byte[DEFAULT_BUFFER_SIZE];

			int bytesRead = source.Read(buffer, 0, buffer.Length);
			while (bytesRead > 0)
			{
				destination.Write(buffer, 0, bytesRead);
				bytesRead = source.Read(buffer, 0, buffer.Length);
			}
		}

        public static Stream DecompressIfGZipped(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanRead)
                throw new ArgumentException("The stream must support reading.");
            if (!stream.CanSeek)
                throw new ArgumentException("The stream must support seeking.");

            // Look for the GZIP file signature.

            var sigBuffer = new byte[_gzipFileSignature.Length];
            bool isCompressed = (stream.Read(sigBuffer, 0, sigBuffer.Length) == sigBuffer.Length
                && MiscUtils.ByteArraysEqual(sigBuffer, _gzipFileSignature));

            // Seek back to the start of the signature.
            stream.Position = Math.Max(stream.Position - _gzipFileSignature.Length, 0);

            return isCompressed ? new GZipStream(stream, CompressionMode.Decompress, true) : stream;
        }
	}
}
