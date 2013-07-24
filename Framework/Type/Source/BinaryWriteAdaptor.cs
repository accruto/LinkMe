using System.IO;

using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Type.Exceptions;

namespace LinkMe.Framework.Type
{
	/// <summary>
	/// BinaryWriter class is used to provide binary write functionality
	/// for primitive data types.
	/// </summary>
	public class BinaryWriteAdaptor
	{
		/// <summary>
		/// The class constructor
		/// </summary>
		/// <param name="stream">Binary stream to write the data</param>
		public BinaryWriteAdaptor(BinaryWriter writer)
		{
			m_writer = writer;
		}

		/// <summary>
		/// Exposes access to the underlying stream
		/// </summary>
		public BinaryWriter Writer
		{
			get { return m_writer; }
		}

/*
 * 		/// <summary>
		/// Closes the underlying Binary Writer and stream
		/// </summary>
		public void Close()
		{
			((IDisposable) m_writer).Dispose();
		}
*/

		/// <summary>
		/// Clears all buffers for the current writer and causes any buffered data 
		/// to be written to the underlying device
		/// </summary>
		public void Flush()
		{
			m_writer.Flush();
		}

		/// <summary>
		/// Sets the position within the current stream.
		/// </summary>
		/// <param name="offset">A byte offset relative to origin</param>
		/// <param name="origin">A field of SeekOrigin indicating the reference 
		/// point from which the new position is to be obtained</param>
		/// <returns>The position with the current stream</returns>
		public long Seek(int offset, SeekOrigin origin)
		{
			return m_writer.Seek(offset, origin);
		}

		/// <summary>
		/// Writes a length-prefixed string to this stream in the current 
		/// encoding of the BinaryWriter, and advances the current position 
		/// of the stream in accordance with the encoding used and the specific 
		/// characters being written to the stream.
		/// </summary>
		/// <param name="value">The String value to write</param>
		public void Write(System.String value)
		{
			m_writer.Write(value);
		}

		/// <summary>
		/// Writes an unsigned Byte to the current stream and advances the 
		/// stream position by one byte
		/// </summary>
		/// <param name="value">The Byte value to write</param>
		public void Write(System.Byte value)
		{
			m_writer.Write(value);
		}

		/// <summary>
		/// Writes an Int16 value to the current stream and 
		/// advances the stream position by sixteen bytes.
		/// </summary>
		/// <param name="value">The Int16 value to write</param>
		public void Write(System.Int16 value)
		{
			m_writer.Write(value);
		}

		/// <summary>
		/// Writes an Int32 value to the current stream and 
		/// advances the stream position by thirty-two bytes.
		/// </summary>
		/// <param name="value">The Int32 value to write</param>
		public void Write(System.Int32 value)
		{
			m_writer.Write(value);
		}

		/// <summary>
		/// Writes an Int64 value to the current stream and 
		/// advances the stream position by sixty-four bytes.
		/// </summary>
		/// <param name="value">The Int64 value to write</param>
		public void Write(System.Int64 value)
		{
			m_writer.Write(value);
		}

		/// <summary>
		/// Writes a Single value to the current stream and 
		/// advances the stream position by four bytes.
		/// </summary>
		/// <param name="value">The Single value to write</param>
		public void Write(System.Single value)
		{
			m_writer.Write(value);
		}

		/// <summary>
		/// Writes a Double value to the current stream and 
		/// advances the stream position by eight bytes.
		/// </summary>
		/// <param name="value">The Double value to write</param>
		public void Write(System.Double value)
		{
			m_writer.Write(value);
		}
		/// <summary>
		/// Writes a decimal value to the current stream and 
		/// advances the stream position by eight bytes.
		/// </summary>
		/// <param name="value">The Decimal value to write</param>
		public void Write(System.Decimal value)
		{
			m_writer.Write(value);
		}
		/// <summary>
		/// Writes a one-byte Boolean value to the current stream
		/// </summary>
		/// <param name="value">The Boolean value to write (0 or 1)</param>
		public void Write(System.Boolean value)
		{
			m_writer.Write(value);
		}

		/// <summary>
		/// Writes a Guid value to the current stream
		/// </summary>
		/// <param name="value">The Guid value to write </param>
		public void Write(System.Guid value)
		{
			m_writer.Write(value.ToByteArray());
		}

		/// <summary>
		/// Writes a Date value to the current stream
		/// </summary>
		/// <param name="value">The Date value to write </param>
		public void Write(Date value)
		{
			value.Write(m_writer);
		}

		/// <summary>
		/// Writes a Datetime value to the current stream
		/// </summary>
		/// <param name="value">The DateTime value to write </param>
		public void Write(DateTime value)
		{
			value.Write(m_writer);
		}

		/// <summary>
		/// Writes a TimeOfDay value to the current stream
		/// </summary>
		/// <param name="value">The TimeOfDay value to write </param>
		public void Write(TimeOfDay value)
		{
			value.Write(m_writer);
		}

		/// <summary>
		/// Writes a TimeSpan value to the current stream
		/// </summary>
		/// <param name="value">The TimeSpan value to write </param>
		public void Write(TimeSpan value)
		{
			value.Write(m_writer);
		}

		public void Write(object value)
		{
			Write(value, false);
		}

		public void Write(object value, bool allowNull)
		{
			const string method = "Write";

			if ( value == null )
			{
				if ( allowNull )
				{
					Write((int) PrimitiveType.None);
				}
				else
					throw new NullParameterException(GetType(), method, "value");
			}
			else if ( value is System.String )
			{
				Write((int) PrimitiveType.String);
				Write((string) value);
			}
			else if ( value is System.Byte )
			{
				Write((int) PrimitiveType.Byte);
				Write((System.Byte) value);
			}
			else if ( value is System.Int16 )
			{
				Write((int) PrimitiveType.Int16);
				Write((System.Int16) value);
			}
			else if ( value is System.Int32 )
			{
				Write((int) PrimitiveType.Int32);
				Write((System.Int32) value);
			}
			else if ( value is System.Int64 )
			{
				Write((int) PrimitiveType.Int64);
				Write((System.Int64) value);
			}
			else if ( value is System.Single )
			{
				Write((int) PrimitiveType.Single);
				Write((System.Single) value);
			}
			else if ( value is System.Double )
			{
				Write((int) PrimitiveType.Double);
				Write((System.Double) value);
			}
			else if ( value is System.Decimal )
			{
				Write((int) PrimitiveType.Decimal);
				Write((System.Decimal) value);
			}
			else if ( value is System.Boolean )
			{
				Write((int) PrimitiveType.Boolean);
				Write((System.Boolean) value);
			}
			else if ( value is System.Guid )
			{
				Write((int) PrimitiveType.Guid);
				Write((System.Guid) value);
			}
			else if ( value is Date )
			{
				Write((int) PrimitiveType.Date);
				Write((Date) value);
			}
			else if ( value is DateTime )
			{
				Write((int) PrimitiveType.DateTime);
				Write((DateTime) value);
			}
			else if ( value is TimeOfDay )
			{
				Write((int) PrimitiveType.TimeOfDay);
				Write((TimeOfDay) value);
			}
			else if ( value is TimeSpan )
			{
				Write((int) PrimitiveType.TimeSpan);
				Write((TimeSpan) value);
			}
			else
			{
				throw new InvalidParameterValueException(typeof(BinaryWriter), method, "value", value.GetType(), value);
			}
		}

		public void Write(object value, PrimitiveType type)
		{
			const string method = "Write";

			if ( value == null )
				throw new NullParameterException(GetType(), method, "value");

			switch ( type )
			{
				case PrimitiveType.String:
					Write((string) value);
					return;

				case PrimitiveType.Byte:
					Write((System.Byte) value);
					return;

				case PrimitiveType.Int16:

					Write((System.Int16) value);
					return;

				case PrimitiveType.Int32:
					Write((System.Int32) value);
					return;

				case PrimitiveType.Int64:
					Write((System.Int64) value);
					return;

				case PrimitiveType.Single:
					Write((System.Single) value);
					return;

				case PrimitiveType.Double:
					Write((System.Double) value);
					return;

				case PrimitiveType.Decimal:
					Write((System.Decimal) value);
					return;

				case PrimitiveType.Boolean:
					Write((System.Boolean) value);
					return;

				case PrimitiveType.Guid:
					Write((System.Guid) value);
					return;

				case PrimitiveType.Date:
					Write((Date) value);
					return;

				case PrimitiveType.DateTime:
					Write((DateTime) value);
					return;

				case PrimitiveType.TimeOfDay:
					Write((TimeOfDay) value);
					return;

				case PrimitiveType.TimeSpan:
					Write((TimeSpan) value);
					return;

				default:
					throw new InvalidParameterValueException(typeof(BinaryWriter), method, "type", typeof(PrimitiveType), type);
			}
		}

		public void Write(byte[] value)
		{
			m_writer.Write(value.Length);
			m_writer.Write(value, 0, value.Length);
		}

		public void WriteNullable(string value)
		{
			if (value == null)
			{
				m_writer.Write(false);
			}
			else
			{
				m_writer.Write(true);
				m_writer.Write(value);
			}
		}

		public void WriteNullable(IBinarySerializable value)
		{
			if (value == null)
			{
				m_writer.Write(false);
			}
			else
			{
				m_writer.Write(true);
				value.Write(m_writer);
			}
		}

		/// <summary>
		/// Store to hold System.IO.BinaryWriter instance to delegate the functionality
		/// for MS.NET types
		/// </summary>
		private BinaryWriter m_writer;
	}
}
