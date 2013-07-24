using System.IO;

using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Type.Exceptions;

namespace LinkMe.Framework.Type
{
	/// <summary>
	/// BinaryReader class is used to provide binary read functionality
	/// for primitive data types.
	/// </summary>
	public class BinaryReadAdaptor
	{
		/// <summary>
		/// The class constructor
		/// </summary>
		/// <param name="stream">Binary stream to read the data from</param>
		public BinaryReadAdaptor(BinaryReader reader)
		{
			m_reader = reader;
		}

		/// <summary>
		/// Exposes access to the underlying stream
		/// </summary>
		public BinaryReader Reader
		{
			get { return m_reader; }
		}

/*
 * 		/// <summary>
		/// Disposes the Binary Reader
		/// </summary>
		public void Close()
		{
			((IDisposable) m_reader).Dispose();
		}
*/

		/// <summary>
		/// Reads the String from current binary stream
		/// </summary>
		/// <returns>System.String</returns>
		public System.String ReadString()
		{
			return m_reader.ReadString();
		}

		/// <summary>
		/// Reads the Byte from current binary stream
		/// </summary>
		/// <returns>System.Byte</returns>
		public System.Byte ReadByte()
		{
			return m_reader.ReadByte();
		}

		/// <summary>
		/// Reads the Int16 from current binary stream
		/// </summary>
		/// <returns>System.Int16</returns>
		public System.Int16 ReadInt16()
		{
			return m_reader.ReadInt16();
		}

		/// <summary>
		/// Reads the Int32 from current binary stream
		/// </summary>
		/// <returns>System.Int32</returns>
		public System.Int32 ReadInt32()
		{
			return m_reader.ReadInt32();
		}

		/// <summary>
		/// Reads the Int64 from current binary stream
		/// </summary>
		/// <returns>System.Int64</returns>
		public System.Int64 ReadInt64()
		{
			return m_reader.ReadInt64();
		}

		/// <summary>
		/// Reads the Single from current binary stream
		/// </summary>
		/// <returns>System.Single</returns>
		public System.Single ReadSingle()
		{
			return m_reader.ReadSingle();
		}

		/// <summary>
		/// Reads the Double from current binary stream
		/// </summary>
		/// <returns>System.Double</returns>
		public System.Double ReadDouble()
		{
			return m_reader.ReadDouble();
		}

		/// <summary>
		/// Reads the Decimal from current binary stream
		/// </summary>
		/// <returns></returns>
		public System.Decimal ReadDecimal()
		{
			return m_reader.ReadDecimal();
		}

		/// <summary>
		/// Reads the Boolean from current binary stream
		/// </summary>
		/// <returns>Sytem.Boolean</returns>
		public System.Boolean ReadBoolean()
		{
			return m_reader.ReadBoolean();
		}

		/// <summary>
		/// Reads the Guid from current binary stream
		/// </summary>
		/// <returns>System.Guid</returns>
		public System.Guid ReadGuid()
		{
			return new System.Guid(m_reader.ReadBytes(16));
		}

		/// <summary>
		/// Reads the Date from current binary stream
		/// </summary>
		/// <returns>LinkMe.Framework.Type.Date</returns>
		public Date ReadDate()
		{
			return Date.Read(m_reader);
		}

		/// <summary>
		/// Reads the DateTime from current binary stream
		/// </summary>
		/// <returns>LinkMe.Framework.Type.DateTime</returns>
		public DateTime ReadDateTime()
		{
			return DateTime.Read(m_reader);
		}

		/// <summary>
		/// Reads the TimeOfDay from current binary stream
		/// </summary>
		/// <returns>LinkMe.Framework.Type.TimeOfDay</returns>
		public TimeOfDay ReadTimeOfDay()
		{
			return TimeOfDay.Read(m_reader);
		}

		/// <summary>
		/// Reads the TimeSpan from current binary stream
		/// </summary>
		/// <returns>LinkMe.Framework.Type.TimeSpan</returns>
		public TimeSpan ReadTimeSpan()
		{
			return TimeSpan.Read(m_reader);
		}

		public byte[] ReadBytes()
		{
			int count = m_reader.ReadInt32();
			return m_reader.ReadBytes(count);
		}

		/// <summary>
		/// Reads the Primitive from current binary stream
		/// </summary>
		/// <returns>System.Object</returns>
		public object ReadType(PrimitiveType type)
		{
			const string method = "ReadType"; 
			switch ( type )
			{
				case PrimitiveType.String:
					return ReadString();
				case PrimitiveType.Byte:
					return ReadByte();
				case PrimitiveType.Int16:
					return ReadInt16();
				case PrimitiveType.Int32:
					return ReadInt32();
				case PrimitiveType.Int64:
					return ReadInt64();
				case PrimitiveType.Single:
					return ReadSingle();
				case PrimitiveType.Double:
					return ReadDouble();
				case PrimitiveType.Decimal:
					return ReadDecimal();
				case PrimitiveType.Boolean:
					return ReadBoolean();
				case PrimitiveType.Guid:
					return ReadGuid();
				case PrimitiveType.Date:
					return ReadDate();
				case PrimitiveType.DateTime:
					return ReadDateTime();
				case PrimitiveType.TimeOfDay:
					return ReadTimeOfDay();
				case PrimitiveType.TimeSpan:
					return ReadTimeSpan();
				default:
					throw new InvalidParameterValueException(typeof(BinaryReader), method, "type", typeof(PrimitiveType), type);
			}
		}
	
		public object ReadObject()
		{
			const string method = "ReadObject";

			PrimitiveType type = (PrimitiveType) ReadInt32();
			switch ( type )
			{
				case PrimitiveType.None:
					return null;

				case PrimitiveType.String:
					return ReadString();

				case PrimitiveType.Byte:
					return ReadByte();

				case PrimitiveType.Int16:
					return ReadInt16();

				case PrimitiveType.Int32:
					return ReadInt32();

				case PrimitiveType.Int64:
					return ReadInt64();

				case PrimitiveType.Single:
					return ReadSingle();

				case PrimitiveType.Double:
					return ReadDouble();

				case PrimitiveType.Decimal:
					return ReadDecimal();

				case PrimitiveType.Boolean:
					return ReadBoolean();

				case PrimitiveType.Guid:
					return ReadGuid();

				case PrimitiveType.Date:
					return ReadDate();

				case PrimitiveType.DateTime:
					return ReadDateTime();

				case PrimitiveType.TimeOfDay:
					return ReadTimeOfDay();

				case PrimitiveType.TimeSpan:
					return ReadTimeSpan();

				default:
					throw new InvalidParameterValueException(typeof(BinaryReader), method, "type", typeof(PrimitiveType), type);
			}
		}

		public string ReadStringNullable()
		{
			if (m_reader.ReadBoolean())
				return m_reader.ReadString();
			else
				return null;
		}

		/// <summary>
		/// Store to hold BinaryReader instance to delegate the functionality 
		/// for MS.NET types
		/// </summary>
		private BinaryReader m_reader;
	}
}
