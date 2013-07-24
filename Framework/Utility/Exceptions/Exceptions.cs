using System.Runtime.Serialization;


namespace LinkMe.Framework.Utility.Exceptions
{
	#region InvalidParameterException

	[System.Serializable]
	public abstract class InvalidParameterException
		:	UtilityException
	{
		protected InvalidParameterException(PropertyInfo[] propertyInfos, System.Type source, string method, string parameter, System.Exception innerException)
			:	base(propertyInfos, source, method, innerException)
		{
			Set(parameter);
		}

		protected InvalidParameterException(PropertyInfo[] propertyInfos, string source, string method, string parameter, System.Exception innerException)
			:	base(propertyInfos, source, method, innerException)
		{
			Set(parameter);
		}

		protected InvalidParameterException(PropertyInfo[] propertyInfos, System.Type source, string method, string parameter)
			:	base(propertyInfos, source, method)
		{
			Set(parameter);
		}

		protected InvalidParameterException(PropertyInfo[] propertyInfos, string source, string method, string parameter)
			:	base(propertyInfos, source, method)
		{
			Set(parameter);
		}

		protected InvalidParameterException(PropertyInfo[] propertyInfos, SerializationInfo info, StreamingContext context)
			:	base(propertyInfos, info, context)
		{
		}

		protected InvalidParameterException(PropertyInfo[] propertyInfos)
			:	base(propertyInfos)
		{
		}

		public string Parameter
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Parameter, string.Empty); }
		}

		private void Set(string parameter)
		{
			SetPropertyValue(Constants.Exceptions.Parameter, parameter == null ? string.Empty : parameter);
		}
	}

	#endregion

	#region NullParameterException

	[System.Serializable]
	public sealed class NullParameterException
		:	InvalidParameterException
	{
		public NullParameterException()
			:	base(m_propertyInfos)
		{
		}

		public NullParameterException(System.Type source, string method, string parameter)
			:	base(m_propertyInfos, source, method, parameter)
		{
		}

		public NullParameterException(string source, string method, string parameter)
			:	base(m_propertyInfos, source, method, parameter)
		{
		}

		private NullParameterException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Parameter, System.TypeCode.String)
			};
	}

	#endregion 

	#region InvalidParameterCountException

	[System.Serializable]
	public sealed class InvalidParameterCountException
		:	UtilityException
	{
		public InvalidParameterCountException()
			:	base(m_propertyInfos)
		{
		}

		public InvalidParameterCountException(System.Type source, string method, int expectedCount, int parameterCount)
			:	base(m_propertyInfos, source, method)
		{
			Set(expectedCount, parameterCount);
		}

		public InvalidParameterCountException(string source, string method, int expectedCount, int parameterCount)
			:	base(m_propertyInfos, source, method)
		{
			Set(expectedCount, parameterCount);
		}

		private InvalidParameterCountException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public int ExpectedCount
		{
			get { return (int) GetPropertyValue(Constants.Exceptions.ExpectedCount, 0); }
		}

		public int ParameterCount
		{
			get { return (int) GetPropertyValue(Constants.Exceptions.ParameterCount, 0); }
		}

		private void Set(int expectedCount, int parameterCount)
		{
			SetPropertyValue(Constants.Exceptions.ExpectedCount, expectedCount);
			SetPropertyValue(Constants.Exceptions.ParameterCount, parameterCount);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.ExpectedCount, System.TypeCode.Int32),
				new PropertyInfo(Constants.Exceptions.ParameterCount, System.TypeCode.Int32),
		};
	}

	#endregion

	#region InvalidParameterTypeException

	[System.Serializable]
	public sealed class InvalidParameterTypeException
		:	InvalidParameterException
	{
		public InvalidParameterTypeException()
			:	base(m_propertyInfos)
		{
		}

		public InvalidParameterTypeException(System.Type source, string method, string parameter, System.Type expectedType, object parameterValue)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(expectedType, parameterValue);
		}

		public InvalidParameterTypeException(string source, string method, string parameter, System.Type expectedType, object parameterValue)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(expectedType, parameterValue);
		}

		private InvalidParameterTypeException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string ExpectedType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ExpectedType, string.Empty); }
		}

		public string ParameterType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParameterType, string.Empty); }
		}

		private void Set(System.Type expectedType, object parameterValue)
		{
			SetPropertyValue(Constants.Exceptions.ExpectedType, expectedType == null ? "<null>" : expectedType.AssemblyQualifiedName);
			SetPropertyValue(Constants.Exceptions.ParameterType, parameterValue == null ? "<null>" : parameterValue.GetType().AssemblyQualifiedName);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Parameter, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ExpectedType, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ParameterType, System.TypeCode.String),
			};
	}

	#endregion

	#region InvalidParameterValueException

	[System.Serializable]
	public sealed class InvalidParameterValueException
		:	InvalidParameterException
	{
		public InvalidParameterValueException()
			:	base(m_propertyInfos)
		{
		}

		public InvalidParameterValueException(System.Type source, string method, string parameter, System.Type parameterType, object parameterValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, parameter, innerException)
		{
			Set(parameterType, parameterValue);
		}

		public InvalidParameterValueException(string source, string method, string parameter, System.Type parameterType, object parameterValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, parameter, innerException)
		{
			Set(parameterType, parameterValue);
		}

		public InvalidParameterValueException(System.Type source, string method, string parameter, System.Type parameterType, object parameterValue)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterType, parameterValue);
		}

		public InvalidParameterValueException(string source, string method, string parameter, System.Type parameterType, object parameterValue)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterType, parameterValue);
		}

		private InvalidParameterValueException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string ParameterType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParameterType, string.Empty); }
		}

		public string ParameterValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParameterValue, string.Empty); }
		}

		private void Set(System.Type parameterType, object parameterValue)
		{
			SetPropertyValue(Constants.Exceptions.ParameterType, parameterType == null ? "<null>" : parameterType.AssemblyQualifiedName);
			SetPropertyValue(Constants.Exceptions.ParameterValue, parameterValue == null ? "<null>" : parameterValue.ToString());
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Parameter, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ParameterType, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ParameterValue, System.TypeCode.String),
			};
	}

	#endregion

    #region InvalidParameterValueException

    [System.Serializable]
    public sealed class ParameterValueNotFoundException
        : InvalidParameterException
    {
        public ParameterValueNotFoundException()
            : base(m_propertyInfos)
        {
        }

        public ParameterValueNotFoundException(System.Type source, string method, string key, string parameter, System.Exception innerException)
            : base(m_propertyInfos, source, method, parameter, innerException)
        {
            Set(key);
        }

        public ParameterValueNotFoundException(string source, string method, string key, string parameter, System.Exception innerException)
            : base(m_propertyInfos, source, method, parameter, innerException)
        {
            Set(key);
        }

        public ParameterValueNotFoundException(System.Type source, string method, string key, string parameter)
            : base(m_propertyInfos, source, method, parameter)
        {
            Set(key);
        }

        public ParameterValueNotFoundException(string source, string method, string key, string parameter)
            : base(m_propertyInfos, source, method, parameter)
        {
            Set(key);
        }

        private ParameterValueNotFoundException(SerializationInfo info, StreamingContext context)
            : base(m_propertyInfos, info, context)
        {
        }

		public string Key
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Key, string.Empty); }
		}

        private void Set(string key)
        {
            SetPropertyValue(Constants.Exceptions.Key, key ?? "<null>");
        }

        private static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Parameter, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.Key, System.TypeCode.String),
			};
    }

    #endregion

    #region ParameterOutOfRangeException

	[System.Serializable]
	public sealed class ParameterOutOfRangeException
		:	InvalidParameterException
	{
		public ParameterOutOfRangeException()
			:	base(m_propertyInfos)
		{
		}

		public ParameterOutOfRangeException(System.Type source, string method, string parameter, object parameterValue, object minValue, object maxValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, parameter, innerException)
		{
			Set(parameterValue, minValue, maxValue);
		}

		public ParameterOutOfRangeException(string source, string method, string parameter, object parameterValue, object minValue, object maxValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, parameter, innerException)
		{
			Set(parameterValue, minValue, maxValue);
		}

		public ParameterOutOfRangeException(System.Type source, string method, string parameter, object parameterValue, object minValue, object maxValue)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, minValue, maxValue);
		}

		public ParameterOutOfRangeException(string source, string method, string parameter, object parameterValue, object minValue, object maxValue)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, minValue, maxValue);
		}

		private ParameterOutOfRangeException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string ParameterValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParameterValue, string.Empty); }
		}

		public string MinValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.MinValue, string.Empty); }
		}

		public string MaxValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.MaxValue, string.Empty); }
		}

		private void Set(object parameterValue, object minValue, object maxValue)
		{
			SetPropertyValue(Constants.Exceptions.ParameterValue, parameterValue == null ? "<null>" : parameterValue.ToString());
			SetPropertyValue(Constants.Exceptions.MinValue, minValue == null ? "<null>" : minValue.ToString());
			SetPropertyValue(Constants.Exceptions.MaxValue, maxValue == null ? "<null>" : maxValue.ToString());
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Parameter, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ParameterValue, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.MinValue, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.MaxValue, System.TypeCode.String),
			};
	}

	#endregion

	#region MinimumValueParameterException

	[System.Serializable]
	public sealed class MinimumValueParameterException
		:	InvalidParameterException
	{
		public MinimumValueParameterException()
			:	base(m_propertyInfos)
		{
		}

		public MinimumValueParameterException(System.Type source, string method, string parameter, object parameterValue, object minValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, parameter, innerException)
		{
			Set(parameterValue, minValue);
		}

		public MinimumValueParameterException(string source, string method, string parameter, object parameterValue, object minValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, parameter, innerException)
		{
			Set(parameterValue, minValue);
		}

		public MinimumValueParameterException(System.Type source, string method, string parameter, object parameterValue, object minValue)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, minValue);
		}

		public MinimumValueParameterException(string source, string method, string parameter, object parameterValue, object minValue)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, minValue);
		}

		private MinimumValueParameterException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string ParameterValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParameterValue, string.Empty); }
		}

		public string MinValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.MinValue, string.Empty); }
		}

		private void Set(object parameterValue, object minValue)
		{
			SetPropertyValue(Constants.Exceptions.ParameterValue, parameterValue == null ? "<null>" : parameterValue.ToString());
			SetPropertyValue(Constants.Exceptions.MinValue, minValue == null ? "<null>" : minValue.ToString());
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Parameter, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ParameterValue, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.MinValue, System.TypeCode.String),
		};
	}

	#endregion

	#region MaximumValueParameterException

	[System.Serializable]
	public sealed class MaximumValueParameterException
		:	InvalidParameterException
	{
		public MaximumValueParameterException()
			:	base(m_propertyInfos)
		{
		}

		public MaximumValueParameterException(System.Type source, string method, string parameter, object parameterValue, object maxValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, parameter, innerException)
		{
			Set(parameterValue, maxValue);
		}

		public MaximumValueParameterException(string source, string method, string parameter, object parameterValue, object maxValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, parameter, innerException)
		{
			Set(parameterValue, maxValue);
		}

		public MaximumValueParameterException(System.Type source, string method, string parameter, object parameterValue, object maxValue)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, maxValue);
		}

		public MaximumValueParameterException(string source, string method, string parameter, object parameterValue, object maxValue)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, maxValue);
		}

		private MaximumValueParameterException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string ParameterValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParameterValue, string.Empty); }
		}

		public string MaxValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.MaxValue, string.Empty); }
		}

		private void Set(object parameterValue, object maxValue)
		{
			SetPropertyValue(Constants.Exceptions.ParameterValue, parameterValue == null ? "<null>" : parameterValue.ToString());
			SetPropertyValue(Constants.Exceptions.MaxValue, maxValue == null ? "<null>" : maxValue.ToString());
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Parameter, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ParameterValue, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.MaxValue, System.TypeCode.String),
		};
	}

	#endregion

	#region ParameterStringLengthOutOfRangeException

	[System.Serializable]
	public class ParameterStringLengthOutOfRangeException
		:	InvalidParameterException
	{
		public ParameterStringLengthOutOfRangeException(System.Type source, string method, string parameter,
			string parameterValue, int minLength, int maxLength)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, minLength, maxLength);
		}

		public ParameterStringLengthOutOfRangeException(string source, string method, string parameter,
			string parameterValue, int minLength, int maxLength)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, minLength, maxLength);
		}

		protected ParameterStringLengthOutOfRangeException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		protected ParameterStringLengthOutOfRangeException()
			:	base(m_propertyInfos)
		{
		}

		public string ParameterValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParameterValue, string.Empty); }
		}

		public string MinValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.MinValue, string.Empty); }
		}

		public string MaxValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.MaxValue, string.Empty); }
		}

		private void Set(string parameterValue, int minLength, int maxLength)
		{
			SetPropertyValue(Constants.Exceptions.ParameterValue, parameterValue == null ? "<null>" : parameterValue);
			SetPropertyValue(Constants.Exceptions.ParameterLength, parameterValue == null ? -1 : parameterValue.Length);
			SetPropertyValue(Constants.Exceptions.MinLength, minLength);
			SetPropertyValue(Constants.Exceptions.MaxLength, maxLength);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.Parameter, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.ParameterValue, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.MinLength, System.TypeCode.Int32),
			new PropertyInfo(Constants.Exceptions.MaxLength, System.TypeCode.Int32),
		};
	}

	#endregion

	#region ParameterStringTooShortException

	[System.Serializable]
	public class ParameterStringTooShortException
		:	InvalidParameterException
	{
		public ParameterStringTooShortException(System.Type source, string method, string parameter,
			string parameterValue, int minLength)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, minLength);
		}

		public ParameterStringTooShortException(string source, string method, string parameter,
			string parameterValue, int minLength)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, minLength);
		}

		protected ParameterStringTooShortException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		protected ParameterStringTooShortException()
			:	base(m_propertyInfos)
		{
		}

		public string ParameterValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParameterValue, string.Empty); }
		}

		public string MinValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.MinValue, string.Empty); }
		}

		private void Set(string parameterValue, int minLength)
		{
			SetPropertyValue(Constants.Exceptions.ParameterValue, parameterValue == null ? "<null>" : parameterValue);
			SetPropertyValue(Constants.Exceptions.ParameterLength, parameterValue == null ? -1 : parameterValue.Length);
			SetPropertyValue(Constants.Exceptions.MinLength, minLength);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.Parameter, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.ParameterValue, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.ParameterLength, System.TypeCode.Int32),
			new PropertyInfo(Constants.Exceptions.MinLength, System.TypeCode.Int32)
		};
	}

	#endregion

	#region InvalidParameterFormatException

	[System.Serializable]
	public sealed class InvalidParameterFormatException
		:	InvalidParameterException
	{
		public InvalidParameterFormatException()
			:	base(m_propertyInfos)
		{
		}

		public InvalidParameterFormatException(System.Type source, string method, string parameter, object parameterValue, string expectedFormat, System.Exception innerException)
			:	base(m_propertyInfos, source, method, parameter, innerException)
		{
			Set(parameterValue, expectedFormat);
		}

		public InvalidParameterFormatException(string source, string method, string parameter, object parameterValue, string expectedFormat, System.Exception innerException)
			:	base(m_propertyInfos, source, method, parameter, innerException)
		{
			Set(parameterValue, expectedFormat);
		}

		public InvalidParameterFormatException(System.Type source, string method, string parameter, object parameterValue, string expectedFormat)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, expectedFormat);
		}

		public InvalidParameterFormatException(string source, string method, string parameter, object parameterValue, string expectedFormat)
			:	base(m_propertyInfos, source, method, parameter)
		{
			Set(parameterValue, expectedFormat);
		}

		private InvalidParameterFormatException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string ParameterValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParameterValue, string.Empty); }
		}

		public string ExpectedFormat
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ExpectedFormat, string.Empty); }
		}

		private void Set(object parameterValue, string expectedFormat)
		{
			SetPropertyValue(Constants.Exceptions.ParameterValue, parameterValue == null ? "<null>" : parameterValue.ToString());
			SetPropertyValue(Constants.Exceptions.ExpectedFormat, expectedFormat == null ? string.Empty : expectedFormat);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Parameter, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ParameterValue, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ExpectedFormat, System.TypeCode.String),
			};
	}

	#endregion

	#region TypeNotAnInterfaceException

	[System.Serializable]
	public sealed class TypeNotAnInterfaceException
		:	UtilityException
	{
		public TypeNotAnInterfaceException()
			:	base(m_propertyInfos)
		{
		}

		public TypeNotAnInterfaceException(System.Type source, string method, System.Type type)
			:	base(m_propertyInfos, source, method)
		{
			Set(type);
		}

		public TypeNotAnInterfaceException(string source, string method, System.Type type)
			:	base(m_propertyInfos, source, method)
		{
			Set(type);
		}

		private TypeNotAnInterfaceException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Type
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.Type, string.Empty); }
		}

		private void Set(System.Type type)
		{
			SetPropertyValue(Constants.Exceptions.Type, type == null ? "<null>" : type.AssemblyQualifiedName);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.Type, System.TypeCode.String),
		};
	}

	#endregion

	#region TypeDoesNotImplementInterfaceException

	[System.Serializable]
	public sealed class TypeDoesNotImplementInterfaceException
		:	UtilityException
	{
		public TypeDoesNotImplementInterfaceException()
			:	base(m_propertyInfos)
		{
		}

		public TypeDoesNotImplementInterfaceException(System.Type source, string method, System.Type type, System.Type interfaceType)
			:	base(m_propertyInfos, source, method)
		{
			Set(type, interfaceType);
		}

		public TypeDoesNotImplementInterfaceException(string source, string method, System.Type type, System.Type interfaceType)
			:	base(m_propertyInfos, source, method)
		{
			Set(type, interfaceType);
		}

		private TypeDoesNotImplementInterfaceException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Type
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.Type, string.Empty); }
		}

		public string InterfaceType
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.InterfaceType, string.Empty); }
		}

		private void Set(System.Type type, System.Type interfaceType)
		{
			SetPropertyValue(Constants.Exceptions.Type, type == null ? "<null>" : type.AssemblyQualifiedName);
			SetPropertyValue(Constants.Exceptions.InterfaceType, interfaceType == null ? "<null>" : interfaceType.AssemblyQualifiedName);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Type, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.InterfaceType, System.TypeCode.String),
			};
	}

	#endregion

	#region TypeNotExpectedException

	[System.Serializable]
	public sealed class TypeNotExpectedException
		:	UtilityException
	{
		public TypeNotExpectedException()
			:	base(m_propertyInfos)
		{
		}

		public TypeNotExpectedException(System.Type source, string method, System.Type type, System.Type expectedType, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(type, expectedType);
		}

		public TypeNotExpectedException(string source, string method, System.Type type, System.Type expectedType, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(type, expectedType);
		}

		public TypeNotExpectedException(System.Type source, string method, System.Type type, System.Type expectedType)
			:	base(m_propertyInfos, source, method)
		{
			Set(type, expectedType);
		}

		public TypeNotExpectedException(string source, string method, System.Type type, System.Type expectedType)
			:	base(m_propertyInfos, source, method)
		{
			Set(type, expectedType);
		}

		public TypeNotExpectedException(System.Type source, string method, object value, System.Type expectedType, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(value, expectedType);
		}

		public TypeNotExpectedException(string source, string method, object value, System.Type expectedType, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(value, expectedType);
		}

		public TypeNotExpectedException(System.Type source, string method, object value, System.Type expectedType)
			:	base(m_propertyInfos, source, method)
		{
			Set(value, expectedType);
		}

		public TypeNotExpectedException(string source, string method, object value, System.Type expectedType)
			:	base(m_propertyInfos, source, method)
		{
			Set(value, expectedType);
		}

		private TypeNotExpectedException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Type
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.Type, string.Empty); }
		}

		public string ExpectedType
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.ExpectedType, string.Empty); }
		}

		private void Set(System.Type type, System.Type expectedType)
		{
			SetPropertyValue(Constants.Exceptions.Type, type == null ? "<null>" : type.AssemblyQualifiedName);
			SetPropertyValue(Constants.Exceptions.ExpectedType, expectedType == null ? "<null>" : expectedType.AssemblyQualifiedName);
		}

		private void Set(object value, System.Type expectedType)
		{
			SetPropertyValue(Constants.Exceptions.Type, value == null ? "<null>" : value.GetType().AssemblyQualifiedName);
			SetPropertyValue(Constants.Exceptions.ExpectedType, expectedType == null ? "<null>" : expectedType.AssemblyQualifiedName);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Type, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.ExpectedType, System.TypeCode.String),
		};
	}

	#endregion

	#region InvalidClassInfoParametersException

	[System.Serializable]
	public sealed class InvalidClassInfoParametersException
		:	UtilityException
	{
		public InvalidClassInfoParametersException()
			:	base(m_propertyInfos)
		{
		}

		public InvalidClassInfoParametersException(System.Type source, string method, string assemblyName, string fullName)
			:	base(m_propertyInfos, source, method)
		{
			Set(assemblyName, fullName);
		}

		public InvalidClassInfoParametersException(string source, string method, string assemblyName, string fullName)
			:	base(m_propertyInfos, source, method)
		{
			Set(assemblyName, fullName);
		}

		private InvalidClassInfoParametersException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string AssemblyName
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.AssemblyName, string.Empty); }
		}

		public string FullName
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.FullName, string.Empty); }
		}

		private void Set(string assemblyName, string fullName)
		{
			SetPropertyValue(Constants.Exceptions.AssemblyName, assemblyName == null ? string.Empty : assemblyName);
			SetPropertyValue(Constants.Exceptions.FullName, fullName == null ? string.Empty : fullName);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.AssemblyName, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.FullName, System.TypeCode.String),
			};
	}

	#endregion

	#region CannotOpenRegistryKeyException

	[System.Serializable]
	public sealed class CannotOpenRegistryKeyException
		:	UtilityException
	{
		public CannotOpenRegistryKeyException()
			:	base(m_propertyInfos)
		{
		}

		public CannotOpenRegistryKeyException(System.Type source, string method, string keyName,
			System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(keyName);
		}

		public CannotOpenRegistryKeyException(System.Type source, string method, string keyName)
			:	base(m_propertyInfos, source, method)
		{
			Set(keyName);
		}

		public CannotOpenRegistryKeyException(string source, string method, string keyName,
			System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(keyName);
		}

		public CannotOpenRegistryKeyException(string source, string method, string keyName)
			:	base(m_propertyInfos, source, method)
		{
			Set(keyName);
		}

		private CannotOpenRegistryKeyException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string KeyName
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.KeyName, string.Empty); }
		}

		private void Set(string keyName)
		{
			SetPropertyValue(Constants.Exceptions.KeyName, keyName == null ? string.Empty : keyName);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.KeyName, System.TypeCode.String)
		};
	}

	#endregion

	#region CannotReadRegistryValueException

	[System.Serializable]
	public sealed class CannotReadRegistryValueException
		:	UtilityException
	{
		public CannotReadRegistryValueException()
			:	base(m_propertyInfos)
		{
		}

		public CannotReadRegistryValueException(System.Type source, string method, string keyName,
			string valueName, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(keyName, valueName);
		}

		public CannotReadRegistryValueException(System.Type source, string method, string keyName,
			string valueName)
			:	base(m_propertyInfos, source, method)
		{
			Set(keyName, valueName);
		}

		public CannotReadRegistryValueException(string source, string method, string keyName,
			string valueName, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(keyName, valueName);
		}

		public CannotReadRegistryValueException(string source, string method, string keyName,
			string valueName)
			:	base(m_propertyInfos, source, method)
		{
			Set(keyName, valueName);
		}

		private CannotReadRegistryValueException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string KeyName
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.KeyName, string.Empty); }
		}

		public string ValueName
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.ValueName, string.Empty); }
		}

		private void Set(string keyName, string valueName)
		{
			SetPropertyValue(Constants.Exceptions.KeyName, keyName == null ? string.Empty : keyName);
			SetPropertyValue(Constants.Exceptions.ValueName, valueName == null ? string.Empty : valueName);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.KeyName, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.ValueName, System.TypeCode.String)
		};
	}

	#endregion

	#region CannotOpenRegistryKeyException

	[System.Serializable]
	public sealed class PathNotFoundException
		:	UtilityException
	{
		public PathNotFoundException()
			:	base(m_propertyInfos)
		{
		}

		public PathNotFoundException(System.Type source, string method, string path,
			System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(path);
		}

		public PathNotFoundException(System.Type source, string method, string path)
			:	base(m_propertyInfos, source, method)
		{
			Set(path);
		}

		public PathNotFoundException(string source, string method, string path,
			System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(path);
		}

		public PathNotFoundException(string source, string method, string path)
			:	base(m_propertyInfos, source, method)
		{
			Set(path);
		}

		private PathNotFoundException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Path
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.Path, string.Empty); }
		}

		private void Set(string path)
		{
			SetPropertyValue(Constants.Exceptions.Path, path == null ? string.Empty : path);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.Path, System.TypeCode.String)
		};
	}

	#endregion

	#region DuplicateExistsException

	[System.Serializable]
	public sealed class DuplicateExistsException
		:	UtilityException
	{
		public DuplicateExistsException()
			:	base(m_propertyInfos)
		{
		}

		public DuplicateExistsException(System.Type source, string method, string key, object keyValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(key, keyValue);
		}

		public DuplicateExistsException(string source, string method, string key, object keyValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(key, keyValue);
		}

		public DuplicateExistsException(System.Type source, string method, string key, object keyValue)
			:	base(m_propertyInfos, source, method)
		{
			Set(key, keyValue);
		}

		public DuplicateExistsException(string source, string method, string key, object keyValue)
			:	base(m_propertyInfos, source, method)
		{
			Set(key, keyValue);
		}

		private DuplicateExistsException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Key
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.Key, string.Empty); }
		}

		public string KeyValue
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.KeyValue, string.Empty); }
		}

		private void Set(string key, object keyValue)
		{
			SetPropertyValue(Constants.Exceptions.Key, key == null ? "<null>" : key);
			SetPropertyValue(Constants.Exceptions.KeyValue, keyValue == null ? "<null>" : keyValue.ToString());
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Key, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.KeyValue, System.TypeCode.String),
		};
	}

	#endregion

	#region InvalidOperationException

	[System.Serializable]
	public abstract class InvalidOperationException
		:	UtilityException
	{
		protected InvalidOperationException(PropertyInfo[] propertyInfos, System.Type source, string method, System.Exception innerException)
			:	base(propertyInfos, source, method, innerException)
		{
		}

		protected InvalidOperationException(PropertyInfo[] propertyInfos, string source, string method, System.Exception innerException)
			:	base(propertyInfos, source, method, innerException)
		{
		}

		protected InvalidOperationException(PropertyInfo[] propertyInfos, System.Type source, string method)
			:	base(propertyInfos, source, method)
		{
		}

		protected InvalidOperationException(PropertyInfo[] propertyInfos, string source, string method)
		:	base(propertyInfos, source, method)
		{
		}

		protected InvalidOperationException(PropertyInfo[] propertyInfos, SerializationInfo info, StreamingContext context)
			:	base(propertyInfos, info, context)
		{
		}

		protected InvalidOperationException(PropertyInfo[] propertyInfos)
			:	base(propertyInfos)
		{
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
		};
	}

	#endregion

	#region OperationNotSupportedException

	[System.Serializable]
	public sealed class OperationNotSupportedException
		:	InvalidOperationException
	{
		public OperationNotSupportedException()
			:	base(m_propertyInfos)
		{
		}

		public OperationNotSupportedException(System.Type source, string method, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
		}

		public OperationNotSupportedException(string source, string method, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
		}

		public OperationNotSupportedException(System.Type source, string method)
			:	base(m_propertyInfos, source, method)
		{
		}

		public OperationNotSupportedException(string source, string method)
			:	base(m_propertyInfos, source, method)
		{
		}

		private OperationNotSupportedException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
		};
	}

	#endregion

	#region InvalidMoveException

	[System.Serializable]
	public abstract class InvalidMoveException
		:	InvalidOperationException
	{
		protected InvalidMoveException(PropertyInfo[] propertyInfos, System.Type source, string method, System.Exception innerException)
			:	base(propertyInfos, source, method, innerException)
		{
		}

		protected InvalidMoveException(PropertyInfo[] propertyInfos, string source, string method, System.Exception innerException)
			:	base(propertyInfos, source, method, innerException)
		{
		}

		protected InvalidMoveException(PropertyInfo[] propertyInfos, System.Type source, string method)
			:	base(propertyInfos, source, method)
		{
		}

		protected InvalidMoveException(PropertyInfo[] propertyInfos, string source, string method)
			:	base(propertyInfos, source, method)
		{
		}

		protected InvalidMoveException(PropertyInfo[] propertyInfos, SerializationInfo info, StreamingContext context)
			:	base(propertyInfos, info, context)
		{
		}

		protected InvalidMoveException(PropertyInfo[] propertyInfos)
			:	base(propertyInfos)
		{
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
		};
	}

	#endregion

	#region InvalidMoveNextException

	[System.Serializable]
	public sealed class InvalidMoveNextException
		:	InvalidMoveException
	{
		public InvalidMoveNextException()
			:	base(m_propertyInfos)
		{
		}

		public InvalidMoveNextException(System.Type source, string method, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
		}

		public InvalidMoveNextException(string source, string method, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
		}

		public InvalidMoveNextException(System.Type source, string method)
			:	base(m_propertyInfos, source, method)
		{
		}

		public InvalidMoveNextException(string source, string method)
			:	base(m_propertyInfos, source, method)
		{
		}

		private InvalidMoveNextException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
		};
	}

	#endregion

	#region InvalidMovePreviousException

	[System.Serializable]
	public sealed class InvalidMovePreviousException
		:	InvalidMoveException
	{
		public InvalidMovePreviousException()
			:	base(m_propertyInfos)
		{
		}

		public InvalidMovePreviousException(System.Type source, string method, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
		}

		public InvalidMovePreviousException(string source, string method, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
		}

		public InvalidMovePreviousException(System.Type source, string method)
			:	base(m_propertyInfos, source, method)
		{
		}

		public InvalidMovePreviousException(string source, string method)
			:	base(m_propertyInfos, source, method)
		{
		}

		private InvalidMovePreviousException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
		};
	}

	#endregion

	#region InternalOutOfRangeException

	[System.Serializable]
	public sealed class InternalOutOfRangeException
		:	InvalidOperationException
	{
		public InternalOutOfRangeException()
			:	base(m_propertyInfos)
		{
		}

		public InternalOutOfRangeException(System.Type source, string method, string variable, object variableValue, object minValue, object maxValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(variable, variableValue, minValue, maxValue);
		}

		public InternalOutOfRangeException(string source, string method, string variable, object variableValue, object minValue, object maxValue, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(variable, variableValue, minValue, maxValue);
		}

		public InternalOutOfRangeException(System.Type source, string method, string variable, object variableValue, object minValue, object maxValue)
			:	base(m_propertyInfos, source, method)
		{
			Set(variable, variableValue, minValue, maxValue);
		}

		public InternalOutOfRangeException(string source, string method, string variable, object variableValue, object minValue, object maxValue)
			:	base(m_propertyInfos, source, method)
		{
			Set(variable, variableValue, minValue, maxValue);
		}

		private InternalOutOfRangeException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Variable
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Variable, string.Empty); }
		}

		public string VariableValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.VariableValue, string.Empty); }
		}

		public string MinValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.MinValue, string.Empty); }
		}

		public string MaxValue
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.MaxValue, string.Empty); }
		}

		private void Set(string variable, object variableValue, object minValue, object maxValue)
		{
			SetPropertyValue(Constants.Exceptions.Variable, variable == null ? "<null>" : variable);
			SetPropertyValue(Constants.Exceptions.VariableValue, variableValue == null ? "<null>" : variableValue.ToString());
			SetPropertyValue(Constants.Exceptions.MinValue, minValue == null ? "<null>" : minValue.ToString());
			SetPropertyValue(Constants.Exceptions.MaxValue, maxValue == null ? "<null>" : maxValue.ToString());
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Variable, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.VariableValue, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.MinValue, System.TypeCode.String),
				new PropertyInfo(Constants.Exceptions.MaxValue, System.TypeCode.String),
		};
	}

	#endregion

	#region UninitialisedException

	[System.Serializable]
	public sealed class UninitialisedException
		:	InvalidOperationException
	{
		public UninitialisedException()
			:	base(m_propertyInfos)
		{
		}

		public UninitialisedException(System.Type source, string method, string variable, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(variable);
		}

		public UninitialisedException(string source, string method, string variable, System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(variable);
		}

		public UninitialisedException(System.Type source, string method, string variable)
			:	base(m_propertyInfos, source, method)
		{
			Set(variable);
		}

		public UninitialisedException(string source, string method, string variable)
			:	base(m_propertyInfos, source, method)
		{
			Set(variable);
		}

		private UninitialisedException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Variable
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Variable, string.Empty); }
		}

		private void Set(string variable)
		{
			SetPropertyValue(Constants.Exceptions.Variable, variable == null ? "<null>" : variable);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.Variable, System.TypeCode.String)
		};
	}

	#endregion

	#region CannotLoadTypeException

	[System.Serializable]
	public sealed class CannotLoadTypeException
		:	UtilityException
	{
		public CannotLoadTypeException()
			:	base(m_propertyInfos)
		{
		}

		public CannotLoadTypeException(System.Type source, string method, string type, string assembly,
			System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(type, assembly);
		}

		public CannotLoadTypeException(System.Type source, string method, string type, string assembly)
			:	base(m_propertyInfos, source, method)
		{
			Set(type, assembly);
		}

		public CannotLoadTypeException(string source, string method, string type, string assembly,
			System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(type, assembly);
		}

		private CannotLoadTypeException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Type
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.Type, string.Empty); }
		}

		public string Assembly
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.Assembly, string.Empty); }
		}

		private void Set(string type, string assembly)
		{
			SetPropertyValue(Constants.Exceptions.Type, type == null ? "<null>" : type);
			SetPropertyValue(Constants.Exceptions.Assembly, assembly == null ? "<null>" : assembly);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.Type, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.Assembly, System.TypeCode.String)
		};
	}

	#endregion

	#region CannotCreateTypeInstanceException

	[System.Serializable]
	public sealed class CannotCreateTypeInstanceException
		:	UtilityException
	{
		public CannotCreateTypeInstanceException()
			:	base(m_propertyInfos)
		{
		}

		public CannotCreateTypeInstanceException(System.Type source, string method, string type, string assembly,
			System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(type, assembly);
		}

		public CannotCreateTypeInstanceException(System.Type source, string method, string type, string assembly)
			:	base(m_propertyInfos, source, method)
		{
			Set(type, assembly);
		}

		public CannotCreateTypeInstanceException(string source, string method, string type, string assembly,
			System.Exception innerException)
			:	base(m_propertyInfos, source, method, innerException)
		{
			Set(type, assembly);
		}

		private CannotCreateTypeInstanceException(SerializationInfo info, StreamingContext context)
			:	base(m_propertyInfos, info, context)
		{
		}

		public string Type
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.Type, string.Empty); }
		}

		public string Assembly
		{
			get { return (string)GetPropertyValue(Constants.Exceptions.Assembly, string.Empty); }
		}

		private void Set(string type, string assembly)
		{
			SetPropertyValue(Constants.Exceptions.Type, type == null ? "<null>" : type);
			SetPropertyValue(Constants.Exceptions.Assembly, assembly == null ? "<null>" : assembly);
		}

		private static PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.Type, System.TypeCode.String),
			new PropertyInfo(Constants.Exceptions.Assembly, System.TypeCode.String)
		};
	}

	#endregion
}