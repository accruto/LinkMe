using System.Collections;
using System.Runtime.Serialization;
using System.Reflection;
using System.IO;
using System.Xml;

namespace LinkMe.Framework.Type.Exceptions
{
	#region PrimitiveTypeNotFoundException

	[System.Serializable]
	public class PrimitiveTypeNotFoundException
		:	TypeException
	{
		public PrimitiveTypeNotFoundException()
			:	base()
		{
		}

		public PrimitiveTypeNotFoundException(System.Type source, string method, string typeName, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(typeName);
		}

		public PrimitiveTypeNotFoundException(string source, string method, string typeName, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(typeName);
		}

		public PrimitiveTypeNotFoundException(System.Type source, string method, string typeName)
			:	base(source, method)
		{
			Set(typeName);
		}

		public PrimitiveTypeNotFoundException(string source, string method, string typeName)
			:	base(source, method)
		{
			Set(typeName);
		}

		protected PrimitiveTypeNotFoundException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string TypeName
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.TypeName, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string typeName)
		{
			SetPropertyValue(Constants.Exceptions.TypeName, typeName == null ? "<null>" : typeName);
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.TypeName, PrimitiveType.String)
		};
	}

	#endregion

	#region PrimitiveTypeXsiTypeNotFoundException

	[System.Serializable]
	public class PrimitiveTypeXsiTypeNotFoundException
		:	TypeException
	{
		public PrimitiveTypeXsiTypeNotFoundException()
			:	base()
		{
		}

		public PrimitiveTypeXsiTypeNotFoundException(System.Type source, string method, string xsiTypeName, string ns)
			:	base(source, method)
		{
			Set(xsiTypeName, ns);
		}

		public PrimitiveTypeXsiTypeNotFoundException(string source, string method, string xsiTypeName, string ns)
			:	base(source, method)
		{
			Set(xsiTypeName, ns);
		}

		protected PrimitiveTypeXsiTypeNotFoundException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string XsiTypeName
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.XsiTypeName, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string xsiTypeName, string xsiTypeNamespace)
		{
			string type = xsiTypeName == null ? "<null>" : xsiTypeName;
			if (xsiTypeNamespace != null && xsiTypeNamespace.Length > 0)
			{
				type = xsiTypeNamespace + ":" + type;
			}

			SetPropertyValue(Constants.Exceptions.XsiTypeName, type);
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.XsiTypeName, PrimitiveType.String)
		};
	}

	#endregion
}
