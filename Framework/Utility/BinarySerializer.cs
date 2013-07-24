using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using LinkMe.Framework.Utility.Net;

namespace LinkMe.Framework.Utility
{
	/// <summary>
	/// Provides static methods for serializing objects that support the IBinarySerializable interface.
	/// </summary>
	public sealed class BinarySerializer
	{
		/// <summary>
		/// Private construtor so that an instance cannot be created.
		/// </summary>
		private BinarySerializer()
		{
		}

		public static void Serialize(object source, BinaryWriter writer)
		{
			// Ensure the source supports the correct interface.

			IBinarySerializable serializable = source as IBinarySerializable;
			if ( serializable == null )
				return;

			writer.Write(source.GetType().AssemblyQualifiedName);
			serializable.Write(writer);
		}

		public static Stream Serialize(object source)
		{
			// Create a stream.

			MemoryStream stream = new MemoryStream();
			Serialize(source, new BinaryWriter(stream));

			return stream;
		}

		public static object Deserialize(BinaryReader reader)
		{
			// Read the class details first.

			string assemblyQualifiedName = reader.ReadString();
			ClassInfo classInfo = new ClassInfo(assemblyQualifiedName);

			return Deserialize(classInfo.GetNetType(), reader);
		}

		public static object Deserialize(System.Type type, BinaryReader reader)
		{
			// Grab the appropriate constructor.

			const BindingFlags constructorFlags = BindingFlags.Instance | BindingFlags.ExactBinding | BindingFlags.Public | BindingFlags.NonPublic;
			ConstructorInfo constructor = type.GetConstructor(constructorFlags, null, new System.Type[] {}, null);
			if ( constructor == null )
				throw new System.ApplicationException("Cannot find constructor for class '" + type.FullName + "'.");

			IBinarySerializable serializable = constructor.Invoke(new object[] {}) as IBinarySerializable;
			if ( serializable != null )
				serializable.Read(reader);
			return serializable;
		}

		/// <summary>
		/// Implements the ISerializable.GetObjectData() method for an object that implements IBinarySerializable.
		/// </summary>
		/// <param name="value">The object instance to be serialized.</param>
		/// <param name="info">The SerializationInfo object passed to the serialized object's
		/// ISerializable.GetObjectData() method.</param>
		public static void WriteObjectDataForBinarySerializable(IBinarySerializable value, SerializationInfo info)
		{
			const string method = "WriteObjectDataForBinarySerializable";

			if (value == null)
				throw new Exceptions.NullParameterException(typeof(BinarySerializer), method, "value");
			if (info == null)
				throw new Exceptions.NullParameterException(typeof(BinarySerializer), method, "info");

			MemoryStream stream = new MemoryStream();

			using (BinaryWriter writer = new BinaryWriter(stream))
			{
				value.Write(writer);
			}

			info.AddValue(Constants.Serialization.BinarySerializableStreamKey, stream.ToArray());
		}

		public static void ReadObjectDataForBinarySerializable(IBinarySerializable value, SerializationInfo info)
		{
			const string method = "ReadObjectDataForBinarySerializable";

			if (value == null)
				throw new Exceptions.NullParameterException(typeof(BinarySerializer), method, "value");
			if (info == null)
				throw new Exceptions.NullParameterException(typeof(BinarySerializer), method, "info");

			byte[] buffer = (byte[])info.GetValue(Constants.Serialization.BinarySerializableStreamKey, typeof(byte[]));
			Debug.Assert(buffer != null, "buffer != null");

			using (BinaryReader reader = new BinaryReader(new MemoryStream(buffer)))
			{
				value.Read(reader);
			}
		}
	}
}
