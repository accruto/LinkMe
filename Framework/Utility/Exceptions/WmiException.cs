using System.Runtime.Serialization;

namespace LinkMe.Framework.Utility.Exceptions
{
	#region WmiQueryException

	[System.Serializable]
	public sealed class WmiQueryException
		:	UtilityException
	{
		public WmiQueryException()
			:	base(m_propertyInfos)
		{
		}

		public WmiQueryException(System.Type source, string method, string queryString, string scope, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(queryString, scope);
		}

		public WmiQueryException(System.Type source, string method, string queryString, string scope)
			:	base(m_propertyInfos, source, method)
		{
			Set(queryString, scope);
		}

		public WmiQueryException(string source, string method, string queryString, string scope, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(queryString, scope);
		}

		public WmiQueryException(string source, string method, string queryString, string scope)
			:	base(m_propertyInfos, source, method)
		{
			Set(queryString, scope);
		}

		private WmiQueryException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string QueryString
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.QueryString, string.Empty); }
		}

		public string Scope
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Scope, string.Empty); }
		}

		private void Set(string queryString, string scope)
		{
			SetPropertyValue(Constants.Exceptions.QueryString, queryString == null ? string.Empty : queryString);
			SetPropertyValue(Constants.Exceptions.Scope, scope == null ? string.Empty : scope);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.QueryString, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.Scope, System.TypeCode.String),
			};
	}

	#endregion

	#region WmiPropertyGetException

	[System.Serializable]
	public sealed class WmiGetClassException
		:	UtilityException
	{
		public WmiGetClassException()
			:	base(m_propertyInfos)
		{
		}

		public WmiGetClassException(System.Type source, string method, string classPath, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(classPath);
		}

		public WmiGetClassException(System.Type source, string method, string classPath)
			:	base(m_propertyInfos, source, method)
		{
			Set(classPath);
		}

		public WmiGetClassException(string source, string method, string classPath, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(classPath);
		}

		public WmiGetClassException(string source, string method, string classPath)
			:	base(m_propertyInfos, source, method)
		{
			Set(classPath);
		}

		private WmiGetClassException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string ClassPath
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ClassPath, string.Empty); }
		}

		private void Set(string classPath)
		{
			SetPropertyValue(Constants.Exceptions.ClassPath, classPath == null ? string.Empty : classPath);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.ClassPath, System.TypeCode.String),
			};
	}

	#endregion

	#region WmiPropertyGetException

	[System.Serializable]
	public sealed class WmiPropertyGetException
		:	UtilityException
	{
		public WmiPropertyGetException()
			:	base(m_propertyInfos)
		{
		}

		public WmiPropertyGetException(System.Type source, string method, string propertyName, string classPath, string classText, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(propertyName, classPath, classText);
		}

		public WmiPropertyGetException(System.Type source, string method, string propertyName, string classPath, string classText)
			:	base(m_propertyInfos, source, method)
		{
			Set(propertyName, classPath, classText);
		}

		public WmiPropertyGetException(string source, string method, string propertyName, string classPath, string classText, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(propertyName, classPath, classText);
		}

		public WmiPropertyGetException(string source, string method, string propertyName, string classPath, string classText)
			:	base(m_propertyInfos, source, method)
		{
			Set(propertyName, classPath, classText);
		}

		private WmiPropertyGetException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string PropertyName
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.PropertyName, string.Empty); }
		}

		public string ClassPath
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ClassPath, string.Empty); }
		}

		public string ClassText
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ClassText, string.Empty); }
		}

		private void Set(string propertyName, string classPath, string classText)
		{
			SetPropertyValue(Constants.Exceptions.PropertyName, propertyName == null ? string.Empty : propertyName);
			SetPropertyValue(Constants.Exceptions.ClassPath, classPath == null ? string.Empty : classPath);
			SetPropertyValue(Constants.Exceptions.ClassText, classText == null ? string.Empty : classText);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.PropertyName, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ClassPath, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ClassText, System.TypeCode.String)
			};
	}

	#endregion

	#region WmiPropertySetException

	[System.Serializable]
	public sealed class WmiPropertySetException
		:	UtilityException
	{
		public WmiPropertySetException()
			:	base(m_propertyInfos)
		{
		}

		public WmiPropertySetException(System.Type source, string method, string propertyName, string classPath, string classText, object propertyValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(propertyName, classPath, classText, propertyValue);
		}

		public WmiPropertySetException(System.Type source, string method, string propertyName, string classPath, string classText, object propertyValue)
			:	base(m_propertyInfos, source, method)
		{
			Set(propertyName, classPath, classText, propertyValue);
		}

		public WmiPropertySetException(string source, string method, string propertyName, string classPath, string classText, object propertyValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(propertyName, classPath, classText, propertyValue);
		}

		public WmiPropertySetException(string source, string method, string propertyName, string classPath, string classText, object propertyValue)
			:	base(m_propertyInfos, source, method)
		{
			Set(propertyName, classPath, classText, propertyValue);
		}

		private WmiPropertySetException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string PropertyName
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.PropertyName, string.Empty); }
		}

		public string ClassPath
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ClassPath, string.Empty); }
		}

		public string ClassText
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ClassText, string.Empty); }
		}

		public object PropertyValue
		{
			get { return GetPropertyValue(Constants.Exceptions.PropertyValue, null); }
		}

		private void Set(string propertyName, string classPath, string classText, object propertyValue)
		{
			SetPropertyValue(Constants.Exceptions.PropertyName, propertyName == null ? string.Empty : propertyName);
			SetPropertyValue(Constants.Exceptions.ClassPath, classPath == null ? string.Empty : classPath);
			SetPropertyValue(Constants.Exceptions.ClassText, classText == null ? string.Empty : classText);
			SetPropertyValue(Constants.Exceptions.PropertyValue, propertyValue == null ? "<null>" : propertyValue.ToString());
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.PropertyName, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ClassPath, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ClassText, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.PropertyValue, System.TypeCode.String),
			};
	}

	#endregion

	#region WmiMofFileCompileException

	[System.Serializable]
	public sealed class WmiMofFileCompileException
		:	UtilityException
	{
		public WmiMofFileCompileException()
			:	base(m_propertyInfos)
		{
		}

		public WmiMofFileCompileException(System.Type source, string method, string fileName,
			string serverAndNamespace, int compilePhase, int objectNumber, int firstLine, int lastLine,
			int nativeErrorCode, string facility, string description, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(fileName, serverAndNamespace, compilePhase, objectNumber, firstLine, lastLine,
				nativeErrorCode, facility, description);
		}

		public WmiMofFileCompileException(System.Type source, string method, string fileName,
			string serverAndNamespace, int compilePhase, int objectNumber, int firstLine, int lastLine,
			int nativeErrorCode, string facility, string description)
			:	base(m_propertyInfos, source, method)
		{
			Set(fileName, serverAndNamespace, compilePhase, objectNumber, firstLine, lastLine,
				nativeErrorCode, facility, description);
		}

		public WmiMofFileCompileException(string source, string method, string fileName,
			string serverAndNamespace, int compilePhase, int objectNumber, int firstLine, int lastLine,
			int nativeErrorCode, string facility, string description, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(fileName, serverAndNamespace, compilePhase, objectNumber, firstLine, lastLine,
				nativeErrorCode, facility, description);
		}

		public WmiMofFileCompileException(string source, string method, string fileName,
			string serverAndNamespace, int compilePhase, int objectNumber, int firstLine, int lastLine,
			int nativeErrorCode, string facility, string description)
			:	base(m_propertyInfos, source, method)
		{
			Set(fileName, serverAndNamespace, compilePhase, objectNumber, firstLine, lastLine,
				nativeErrorCode, facility, description);
		}

		private WmiMofFileCompileException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string FileName
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.FileName, string.Empty); }
		}

		public string ServerAndNamespace
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ServerAndNamespace, string.Empty); }
		}

		public int CompilePhase
		{
			get { return (int) GetPropertyValue(Constants.Exceptions.CompilePhase, 0); }
		}

		public int ObjectNumber
		{
			get { return (int) GetPropertyValue(Constants.Exceptions.ObjectNumber, 0); }
		}

		public int FirstLine
		{
			get { return (int) GetPropertyValue(Constants.Exceptions.FirstLine, 0); }
		}

		public int LastLine
		{
			get { return (int) GetPropertyValue(Constants.Exceptions.LastLine, 0); }
		}

		public int NativeErrorCode
		{
			get { return (int) GetPropertyValue(Constants.Exceptions.NativeErrorCode, 0); }
		}

		public string Facility
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Facility, string.Empty); }
		}

		public string Description
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Description, string.Empty); }
		}

		private void Set(string fileName, string serverAndNamespace, int compilePhase, int objectNumber,
			int firstLine, int lastLine, int nativeErrorCode, string facility, string description)
		{
			SetPropertyValue(Constants.Exceptions.FileName, fileName == null ? string.Empty : fileName);
			SetPropertyValue(Constants.Exceptions.ServerAndNamespace,
				serverAndNamespace == null ? string.Empty : serverAndNamespace);
			SetPropertyValue(Constants.Exceptions.CompilePhase, compilePhase);
			SetPropertyValue(Constants.Exceptions.ObjectNumber, objectNumber);
			SetPropertyValue(Constants.Exceptions.FirstLine, firstLine);
			SetPropertyValue(Constants.Exceptions.LastLine, lastLine);
			SetPropertyValue(Constants.Exceptions.NativeErrorCode, nativeErrorCode);
			SetPropertyValue(Constants.Exceptions.Facility, facility == null ? string.Empty : facility);
			SetPropertyValue(Constants.Exceptions.Description,
				description == null ? string.Empty : description);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.FileName, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.ServerAndNamespace, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.CompilePhase, System.TypeCode.Int32),
			new PropertyInfo(Constants.Exceptions.ObjectNumber, System.TypeCode.Int32),
			new PropertyInfo(Constants.Exceptions.FirstLine, System.TypeCode.Int32),
			new PropertyInfo(Constants.Exceptions.LastLine, System.TypeCode.Int32),
			new PropertyInfo(Constants.Exceptions.NativeErrorCode, System.TypeCode.Int32),
			new PropertyInfo(Constants.Exceptions.Facility, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.Description, System.TypeCode.String),
		};
	}

	#endregion
}
