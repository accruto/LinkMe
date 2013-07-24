using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace LinkMe.Framework.Utility.Net
{
	[System.Serializable]
	public class ClassInfo
		:	System.ICloneable,
			IInternable,
			IBinarySerializable,
			ISerializable
	{
		private string m_fullName = string.Empty;
		private string m_assemblyName = string.Empty;
		private System.Type m_type = null;

		public ClassInfo()
		{
		}

		public ClassInfo(string fullName, string assemblyName)
		{
			m_fullName = fullName == null || fullName.Length == 0 ? string.Empty : fullName;
			m_assemblyName = assemblyName == null || assemblyName.Length == 0 ? string.Empty : assemblyName;

			if (m_fullName.Length == 0 ^ m_assemblyName.Length == 0)
			{
				throw new Exceptions.InvalidClassInfoParametersException(GetType(),
					ConstructorInfo.ConstructorName, m_assemblyName, m_fullName);
			}
		}

		public ClassInfo(System.Type type)
			:	this(type == null ? string.Empty : type.FullName, type == null ? string.Empty : type.Assembly.FullName)
		{
			m_type = type;
		}

		public ClassInfo(string text)
		{
			m_fullName = string.Empty;
			m_assemblyName = string.Empty;

			if ( text != null )
			{
				int index = text.IndexOf(',');
				if ( index == -1 )
				{
					m_fullName = text;
				}
				else
				{
					// Everything upto the ',' is the class name, everything else is the assembly.

					m_fullName = text.Substring(0, index);
					m_assemblyName = text.Substring(index + 2);
				}
			}
		}

		// This deserialization constructor should not be be called by derived classes - they should override
		// Write(BinaryWriter) instead.
		private ClassInfo(SerializationInfo info, StreamingContext context)
		{
			BinarySerializer.ReadObjectDataForBinarySerializable(this, info);
		}

		#region ICloneable Members

		object System.ICloneable.Clone()
		{
			return Clone();
		}

		#endregion

		#region IInternable Members

		void IInternable.Intern(Interner interner)
		{
			m_assemblyName = interner.Intern(m_assemblyName);
			m_fullName = interner.Intern(m_fullName);
		}

		#endregion

		#region IBinarySerializable Members

		public void Write(BinaryWriter writer)
		{
			writer.Write(m_fullName);
			writer.Write(m_assemblyName);
		}

		public void Read(BinaryReader reader)
		{
			m_fullName = reader.ReadString();
			m_assemblyName = reader.ReadString();
		}

		#endregion

		#region ISerializable Members

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			BinarySerializer.WriteObjectDataForBinarySerializable(this, info);
		}

		#endregion

		public string FullName
		{
			get { return m_fullName; }
		}

		public string Namespace
		{
			get
			{
				int position = m_fullName.LastIndexOf('.');
				return position == -1 ? string.Empty : m_fullName.Substring(0, position);
			}
		}

		public string Name
		{
			get
			{
				int position = m_fullName.LastIndexOf('.');
				return position == -1 ? m_fullName : m_fullName.Substring(position + 1);
			}
		}

		public string AssemblyName
		{
			get { return m_assemblyName; }
		}

		public bool IsEmpty
		{
			get { return (m_fullName.Length == 0 && m_assemblyName.Length == 0); }
		}

		#region Operators

		public static bool operator==(ClassInfo c1, ClassInfo c2)
		{
			return object.Equals(c1, c2);
		}

		public static bool operator!=(ClassInfo c1, ClassInfo c2)
		{
			return !object.Equals(c1, c2);
		}

		#endregion

		#region Static methods

		public static System.Type GetTypeFromAssemblyQualifiedName(string qualifiedName)
		{
			const string method = "GetTypeFromAssemblyQualifiedName";

			if ( qualifiedName == null )
				throw new Exceptions.NullParameterException(typeof(ClassInfo), method, "qualifiedName");

			int index = qualifiedName.IndexOf(',');
			if ( index == -1 )
			{
				throw new Exceptions.InvalidParameterFormatException(typeof(ClassInfo), method, "qualifiedName",
					qualifiedName, "<TypeName>, <AssemblyFullName>");
			}

			string assemblyName = qualifiedName.Substring(index + 2);
			string typeName = qualifiedName.Substring(0, index);
			Assembly assembly = Assembly.Load(assemblyName);
			return assembly.GetType(typeName, true);
		}

		public static string ToString(System.Type type)
		{
			const string method = "ToString";

			if ( type == null)
				throw new Exceptions.NullParameterException(typeof(ClassInfo), method, "type");

			return type.FullName + ", " + type.Assembly.FullName;
		}

		#endregion

		public ClassInfo Clone()
		{
			ClassInfo clone = new ClassInfo();
			clone.m_fullName = m_fullName;
			clone.m_assemblyName = m_assemblyName;
			clone.m_type = m_type;
			return clone;
		}

		public object CreateInstance(params object[] args)
		{
			return CreateInstanceInternal<object>(BindingFlags.Instance | BindingFlags.Public, args);
		}

		public T CreateInstance<T>(params object[] args)
            where T : class
		{
			return CreateInstanceInternal<T>(BindingFlags.Instance | BindingFlags.Public, args);
		}

		public object CreateInstanceNonPublic(params object[] args)
		{
			return CreateInstanceInternal<object>(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, args);
		}

		public T CreateInstanceNonPublic<T>(params object[] args)
            where T : class
        {
			return CreateInstanceInternal<T>(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, args);
		}

		public System.Type GetNetType()
		{
			const string method = "GetNetType";

			if (m_type != null)
				return m_type;

			if (IsEmpty)
				return null;

			// Get the assembly.

			try
			{
				Assembly assembly = Assembly.Load(m_assemblyName);
				if ( assembly != null )
				{
					m_type = assembly.GetType(m_fullName);
					return m_type;
				}
				else
					return null;
			}
			catch (System.Exception ex)
			{
				throw new Exceptions.CannotLoadTypeException(GetType(), method, m_fullName, m_assemblyName, ex);
			}
		}

		public bool SupportsInterface<T>()
            where T : class
		{
			const string method = "SupportsInterface";
			if (!typeof(T).IsInterface)
                throw new Exceptions.TypeNotAnInterfaceException(GetType(), method, typeof(T));

			// Get the type.

			System.Type type = GetNetType();
			if (type == null)
				return false;

			// Check whether it implements the interface.

            return typeof(T).IsAssignableFrom(type);
		}

		public override string ToString()
		{
			return (m_assemblyName.Length == 0 ? m_fullName : m_fullName + ", " + m_assemblyName);
		}

		public override bool Equals(object other)
		{
			ClassInfo classInfo = other as ClassInfo;
			if (classInfo == null)
				return false;

			return m_fullName == classInfo.m_fullName && m_assemblyName == classInfo.m_assemblyName;
		}

		public override int GetHashCode()
		{
			return m_fullName.GetHashCode() ^ m_assemblyName.GetHashCode();
		}

		private T CreateInstanceInternal<T>(BindingFlags flags, params object[] args)
            where T : class
		{
			const string method = "CreateInstanceInternal";

			// Get the type.

			System.Type type = GetNetType();
			if (type == null)
				throw new Exceptions.CannotCreateTypeInstanceException(GetType(), method, m_fullName, m_assemblyName);

			// Check if the type supports the requested interface.

			if (typeof(T) != typeof(object) && !SupportsInterface<T>())
				throw new Exceptions.TypeDoesNotImplementInterfaceException(GetType(), method, type, typeof(T));

			// Create the instance.

			try
			{
				return System.Activator.CreateInstance(type, flags, null, args, null, null) as T;
			}
			catch (System.Exception ex)
			{
				throw new Exceptions.CannotCreateTypeInstanceException(GetType(), method,
					type.FullName, type.Assembly.Location, ex);
			}
		}
	}
}
