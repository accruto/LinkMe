using System.Runtime.Serialization;

using LinkMe.Framework.Type;
using LinkMe.Framework.Type.Exceptions;

namespace LinkMe.Framework.Configuration.Exceptions
{
	#region Exception

	public abstract class Exception
		:	TypeException
	{
		protected Exception()
			:	base()
		{
		}

		protected Exception(System.Type source, string method, System.Exception innerException)
			:	base(source, method, innerException)
		{
		}

		protected Exception(string source, string method, System.Exception innerException)
			:	base(source, method, innerException)
		{
		}

		protected Exception(System.Type source, string method)
			:	base(source, method)
		{
		}

		protected Exception(string source, string method)
			:	base(source, method)
		{
		}

		protected Exception(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		protected override string GetResourceBaseName()
		{
			return "LinkMe.Framework.Configuration.Source.Exceptions";
		}
	}

	#endregion

	#region ElementAlreadyExistsException

	[System.Serializable]
	public class ElementAlreadyExistsException
		:	Exception
	{
		public ElementAlreadyExistsException()
			:	base()
		{
		}

		public ElementAlreadyExistsException(System.Type source, string method, string elementKey, string elementType, string existingElementType, Element parent, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementKey, elementType, existingElementType, parent);
		}

		public ElementAlreadyExistsException(string source, string method, string elementKey, string elementType, string existingElementType, Element parent, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementKey, elementType, existingElementType, parent);
		}

		public ElementAlreadyExistsException(System.Type source, string method, string elementKey, string elementType, string existingElementType, Element parent)
			:	base(source, method)
		{
			Set(elementKey, elementType, existingElementType, parent);
		}

		public ElementAlreadyExistsException(string source, string method, string elementKey, string elementType, string existingElementType, Element parent)
			:	base(source, method)
		{
			Set(elementKey, elementType, existingElementType, parent);
		}

		protected ElementAlreadyExistsException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string ElementKey
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementKey, string.Empty); }
		}

		public string ElementType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementType, string.Empty); }
		}

		public string ExistingElementType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ExistingElementType, string.Empty); }
		}

		public string ParentReference
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParentReference, string.Empty); }
		}

		public string ParentElementType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParentElementType, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string elementKey, string elementType, string existingElementType, Element parent)
		{
			SetPropertyValue(Constants.Exceptions.ElementKey, elementKey == null ? string.Empty : elementKey);
			SetPropertyValue(Constants.Exceptions.ElementType, elementType == null ? string.Empty : elementType);
			SetPropertyValue(Constants.Exceptions.ExistingElementType, existingElementType == null ? string.Empty : existingElementType);
			SetPropertyValue(Constants.Exceptions.ParentReference, parent == null ? string.Empty : ((IElementReference) parent).FullyQualifiedReference);
			SetPropertyValue(Constants.Exceptions.ParentElementType, parent == null ? string.Empty : ((IElementIdentification) parent).ElementType);
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.ElementKey, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ElementType, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ExistingElementType, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ParentReference, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ParentElementType, PrimitiveType.String)
		};
	}

	#endregion

	#region ElementAlreadyExistsWithDifferentCaseException

	[System.Serializable]
	public class ElementAlreadyExistsWithDifferentCaseException
		:	ElementAlreadyExistsException
	{
		public ElementAlreadyExistsWithDifferentCaseException()
			:	base()
		{
		}

		public ElementAlreadyExistsWithDifferentCaseException(System.Type source, string method,
			string elementKey, string elementType, string existingElementKey, string existingElementType,
			Element parent, System.Exception innerException)
			:	base(source, method, elementKey, elementType, existingElementType, parent, innerException)
		{
			Set(existingElementKey);
		}

		public ElementAlreadyExistsWithDifferentCaseException(string source, string method, string elementKey,
			string elementType, string existingElementKey, string existingElementType, Element parent,
			System.Exception innerException)
			:	base(source, method, elementKey, elementType, existingElementType, parent, innerException)
		{
			Set(existingElementKey);
		}

		public ElementAlreadyExistsWithDifferentCaseException(System.Type source, string method,
			string elementKey, string elementType, string existingElementKey, string existingElementType,
			Element parent)
			:	base(source, method, elementKey, elementType, existingElementType, parent)
		{
			Set(existingElementKey);
		}

		public ElementAlreadyExistsWithDifferentCaseException(string source, string method, string elementKey,
			string elementType, string existingElementKey, string existingElementType, Element parent)
			:	base(source, method, elementKey, elementType, existingElementType, parent)
		{
			Set(existingElementKey);
		}

		protected ElementAlreadyExistsWithDifferentCaseException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string ExistingElementKey
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ExistingElementKey, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string existingElementKey)
		{
			SetPropertyValue(Constants.Exceptions.ExistingElementKey, existingElementKey == null ? string.Empty : existingElementKey);
		}

		protected static new readonly PropertyInfo[] m_propertyInfos = AppendPropertyInfos(
			ElementAlreadyExistsException.m_propertyInfos,
			new PropertyInfo(Constants.Exceptions.ExistingElementKey, PrimitiveType.String));
	}

	#endregion

	#region ElementCannotBeRemovedException

	[System.Serializable]
	public class ElementCannotBeRemovedException
		:	Exception
	{
		public ElementCannotBeRemovedException()
			:	base()
		{
		}

		public ElementCannotBeRemovedException(System.Type source, string method, string elementKey, string elementType, Element parent, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementKey, elementType, parent);
		}

		public ElementCannotBeRemovedException(string source, string method, string elementKey, string elementType, Element parent, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementKey, elementType, parent);
		}

		public ElementCannotBeRemovedException(System.Type source, string method, string elementKey, string elementType, Element parent)
			:	base(source, method)
		{
			Set(elementKey, elementType, parent);
		}

		public ElementCannotBeRemovedException(string source, string method, string elementKey, string elementType, Element parent)
			:	base(source, method)
		{
			Set(elementKey, elementType, parent);
		}

		protected ElementCannotBeRemovedException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string ElementKey
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementKey, string.Empty); }
		}

		public string ElementType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementType, string.Empty); }
		}

		public string ParentElementKey
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParentReference, string.Empty); }
		}

		public string ParentElementType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParentElementType, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string elementKey, string elementType, Element parent)
		{
			SetPropertyValue(Constants.Exceptions.ElementKey, elementKey == null ? string.Empty : elementKey);
			SetPropertyValue(Constants.Exceptions.ElementType, elementType == null ? string.Empty : elementType);
			SetPropertyValue(Constants.Exceptions.ParentReference, parent == null ? string.Empty : ((IElementReference) parent).FullyQualifiedReference);
			SetPropertyValue(Constants.Exceptions.ParentElementType, parent == null ? string.Empty : ((IElementIdentification) parent).ElementType);
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.ElementKey, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ElementType, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ParentReference, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ParentElementType, PrimitiveType.String)
		};
	}

	#endregion

	#region ElementNotFoundException

	[System.Serializable]
	public class ElementNotFoundException
		:	Exception
	{
		public ElementNotFoundException()
			:	base()
		{
		}

		public ElementNotFoundException(System.Type source, string method, string elementKey, System.Enum elementType, Element parent, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementKey, elementType, parent);
		}

		public ElementNotFoundException(string source, string method, string elementKey, System.Enum elementType, Element parent, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementKey, elementType, parent);
		}

		public ElementNotFoundException(System.Type source, string method, string elementKey, System.Enum elementType, Element parent)
			:	base(source, method)
		{
			Set(elementKey, elementType, parent);
		}

		public ElementNotFoundException(string source, string method, string elementKey, System.Enum elementType, Element parent)
			:	base(source, method)
		{
			Set(elementKey, elementType, parent);
		}

		protected ElementNotFoundException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string ElementKey
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementKey, string.Empty); }
		}

		public string ElementType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementType, string.Empty); }
		}

		public string ParentReference
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParentReference, string.Empty); }
		}

		public string ParentElementType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ParentElementType, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string elementKey, System.Enum elementType, Element parent)
		{
			SetPropertyValue(Constants.Exceptions.ElementKey, elementKey == null ? string.Empty : elementKey);
			SetPropertyValue(Constants.Exceptions.ElementType, elementType.ToString());
			SetPropertyValue(Constants.Exceptions.ParentReference, ((IElementReference) parent).FullyQualifiedReference);
			SetPropertyValue(Constants.Exceptions.ParentElementType, ((IElementIdentification) parent).ElementType);
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.ElementKey, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ElementType, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ParentReference, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ParentElementType, PrimitiveType.String)
		};
	}

	#endregion

	#region ElementNotFoundInCatalogueException

	[System.Serializable]
	public class ElementNotFoundInCatalogueException
		:	Exception
	{
		public ElementNotFoundInCatalogueException()
			:	base()
		{
		}

		public ElementNotFoundInCatalogueException(System.Type source, string method, string elementReference, System.Enum elementType, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementReference, elementType);
		}

		public ElementNotFoundInCatalogueException(string source, string method, string elementReference, System.Enum elementType, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementReference, elementType);
		}

		public ElementNotFoundInCatalogueException(System.Type source, string method, string elementReference, System.Enum elementType)
			:	base(source, method)
		{
			Set(elementReference, elementType);
		}

		public ElementNotFoundInCatalogueException(string source, string method, string elementReference, System.Enum elementType)
			:	base(source, method)
		{
			Set(elementReference, elementType);
		}

		protected ElementNotFoundInCatalogueException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string ElementReference
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementReference, string.Empty); }
		}

		public string ElementType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementType, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string elementReference, System.Enum elementType)
		{
			SetPropertyValue(Constants.Exceptions.ElementReference, elementReference == null ? string.Empty : elementReference);
			SetPropertyValue(Constants.Exceptions.ElementType, elementType.ToString());
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.ElementReference, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ElementType, PrimitiveType.String)
		};
	}

	#endregion

	#region CannotClearReadOnlyException

	[System.Serializable]
	public class CannotClearReadOnlyException
		:	Exception
	{
		public CannotClearReadOnlyException()
			:	base()
		{
		}

		public CannotClearReadOnlyException(System.Type source, string method, string elementReference, System.Enum elementType, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementReference, elementType);
		}

		public CannotClearReadOnlyException(string source, string method, string elementReference, System.Enum elementType, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementReference, elementType);
		}

		public CannotClearReadOnlyException(System.Type source, string method, string elementReference, System.Enum elementType)
			:	base(source, method)
		{
			Set(elementReference, elementType);
		}

		public CannotClearReadOnlyException(string source, string method, string elementReference, System.Enum elementType)
			:	base(source, method)
		{
			Set(elementReference, elementType);
		}

		protected CannotClearReadOnlyException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string ElementReference
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementReference, string.Empty); }
		}

		public string ElementType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementType, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string elementReference, System.Enum elementType)
		{
			SetPropertyValue(Constants.Exceptions.ElementReference, elementReference == null ? string.Empty : elementReference);
			SetPropertyValue(Constants.Exceptions.ElementType, elementType.ToString());
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.ElementReference, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ElementType, PrimitiveType.String)
		};
	}

	#endregion

	#region CannotConnectToRepositoryException

	[System.Serializable]
	public class CannotConnectToRepositoryException
		:	Exception
	{
		public CannotConnectToRepositoryException()
			:	base()
		{
		}

		public CannotConnectToRepositoryException(System.Type source, string method, string module, string repositoryType, string initialisationString, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(module, repositoryType, initialisationString);
		}

		public CannotConnectToRepositoryException(string source, string method, string module, string repositoryType, string initialisationString, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(module, repositoryType, initialisationString);
		}

		public CannotConnectToRepositoryException(System.Type source, string method, string module, string repositoryType, string initialisationString)
			:	base(source, method)
		{
			Set(module, repositoryType, initialisationString);
		}

		public CannotConnectToRepositoryException(string source, string method, string module, string repositoryType, string initialisationString)
			:	base(source, method)
		{
			Set(module, repositoryType, initialisationString);
		}

		protected CannotConnectToRepositoryException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string Module
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Module, string.Empty); }
		}

		public string RepositoryType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.RepositoryType, string.Empty); }
		}

		public string InitialisationString
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.InitialisationString, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string module, string repositoryType, string initialisationString)
		{
			SetPropertyValue(Constants.Exceptions.Module, module == null ? string.Empty : module);
			SetPropertyValue(Constants.Exceptions.RepositoryType, repositoryType == null ? string.Empty : repositoryType);
			SetPropertyValue(Constants.Exceptions.InitialisationString, initialisationString == null ? string.Empty : initialisationString);
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.Module, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.RepositoryType, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.InitialisationString, PrimitiveType.String)
		};
	}

	#endregion

	#region CannotConnectToStoreException

	[System.Serializable]
	public class CannotConnectToStoreException
		:	Exception
	{
		public CannotConnectToStoreException()
			:	base()
		{
		}

		public CannotConnectToStoreException(System.Type source, string method, string module, string storeType, string initialisationString, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(module, storeType, initialisationString);
		}

		public CannotConnectToStoreException(string source, string method, string module, string storeType, string initialisationString, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(module, storeType, initialisationString);
		}

		public CannotConnectToStoreException(System.Type source, string method, string module, string storeType, string initialisationString)
			:	base(source, method)
		{
			Set(module, storeType, initialisationString);
		}

		public CannotConnectToStoreException(string source, string method, string module, string storeType, string initialisationString)
			:	base(source, method)
		{
			Set(module, storeType, initialisationString);
		}

		protected CannotConnectToStoreException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string Module
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Module, string.Empty); }
		}

		public string StoreType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.StoreType, string.Empty); }
		}

		public string InitialisationString
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.InitialisationString, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string module, string storeType, string initialisationString)
		{
			SetPropertyValue(Constants.Exceptions.Module, module == null ? string.Empty : module);
			SetPropertyValue(Constants.Exceptions.StoreType, storeType == null ? string.Empty : storeType);
			SetPropertyValue(Constants.Exceptions.InitialisationString, initialisationString == null ? string.Empty : initialisationString);
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.Module, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.StoreType, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.InitialisationString, PrimitiveType.String)
		};
	}

	#endregion

	#region RepositoryConnectionRequiredException

	[System.Serializable]
	public class RepositoryConnectionRequiredException
		:	Exception
	{
		public RepositoryConnectionRequiredException()
			:	base()
		{
		}

		public RepositoryConnectionRequiredException(System.Type source, string method, System.Exception innerException)
			:	base(source, method, innerException)
		{
		}

		public RepositoryConnectionRequiredException(string source, string method, System.Exception innerException)
			:	base(source, method, innerException)
		{
		}

		public RepositoryConnectionRequiredException(System.Type source, string method)
			:	base(source, method)
		{
		}

		public RepositoryConnectionRequiredException(string source, string method)
			:	base(source, method)
		{
		}

		protected RepositoryConnectionRequiredException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		protected static readonly PropertyInfo[] m_propertyInfos = PropertyInfo.EmptyInfos;
	}

	#endregion

	#region InitialisationStringMissingException

	[System.Serializable]
	public class InitialisationStringMissingException
		:	Exception
	{
		public InitialisationStringMissingException()
			:	base()
		{
		}

		public InitialisationStringMissingException(System.Type source, string method,
			string repositoryType, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(repositoryType);
		}

		public InitialisationStringMissingException(string source, string method, string repositoryType,
			System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(repositoryType);
		}

		public InitialisationStringMissingException(System.Type source, string method,
			string repositoryType)
			:	base(source, method)
		{
			Set(repositoryType);
		}

		public InitialisationStringMissingException(string source, string method, string repositoryType)
			:	base(source, method)
		{
			Set(repositoryType);
		}

		protected InitialisationStringMissingException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string RepositoryType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.RepositoryType, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string repositoryType)
		{
			SetPropertyValue(Constants.Exceptions.RepositoryType, repositoryType == null ? string.Empty : repositoryType);
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.RepositoryType, PrimitiveType.String)
		};
	}

	#endregion

	#region InvalidElementConfigurationXmlException

	[System.Serializable]
	public class InvalidElementConfigurationXmlException
		:	Exception
	{
		public InvalidElementConfigurationXmlException()
			:	base()
		{
		}

		public InvalidElementConfigurationXmlException(System.Type source, string method, string elementKey, System.Enum elementType, string xmlData, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementKey, elementType, xmlData);
		}

		public InvalidElementConfigurationXmlException(string source, string method, string elementKey, System.Enum elementType, string xmlData, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(elementKey, elementType, xmlData);
		}

		public InvalidElementConfigurationXmlException(System.Type source, string method, string elementKey, System.Enum elementType, string xmlData)
			:	base(source, method)
		{
			Set(elementKey, elementType, xmlData);
		}

		public InvalidElementConfigurationXmlException(string source, string method, string elementKey, System.Enum elementType, string xmlData)
			:	base(source, method)
		{
			Set(elementKey, elementType, xmlData);
		}

		protected InvalidElementConfigurationXmlException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string ElementKey
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementKey, string.Empty); }
		}

		public string ElementType
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ElementType, string.Empty); }
		}

		public string XmlData
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.XmlData, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string elementKey, System.Enum elementType, string xmlData)
		{
			SetPropertyValue(Constants.Exceptions.ElementKey, elementKey == null ? "<null>" : elementKey);
			SetPropertyValue(Constants.Exceptions.ElementType, elementType.ToString());
			SetPropertyValue(Constants.Exceptions.XmlData, xmlData == null ? "<null>" : xmlData);
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.ElementKey, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ElementType, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.XmlData, PrimitiveType.String)
		};
	}

	#endregion

    #region InvalidConfigurationValueException

    [System.Serializable]
	public class InvalidConfigurationValueException
		:	Exception
	{
		public InvalidConfigurationValueException()
		{
		}

		public InvalidConfigurationValueException(System.Type source, string method, string name, object value, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(name, value);
		}

		public InvalidConfigurationValueException(string source, string method, string name, object value, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(name, value);
		}

		public InvalidConfigurationValueException(System.Type source, string method, string name, object value)
			:	base(source, method)
		{
            Set(name, value);
		}

		public InvalidConfigurationValueException(string source, string method, string name, object value)
			:	base(source, method)
		{
            Set(name, value);
		}

		protected InvalidConfigurationValueException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string Name
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Name, string.Empty); }
		}

		public string Value
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.Value, string.Empty); }
		}

        protected override PropertyInfo[] PropertyInfos
        {
            get { return m_propertyInfos; }
        }

        private void Set(string name, object value)
		{
			SetPropertyValue(Constants.Exceptions.Name, name ?? "<null>");
			SetPropertyValue(Constants.Exceptions.Value, value == null ? "<null>" : value.ToString());
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
			{
				new PropertyInfo(Constants.Exceptions.Name, PrimitiveType.String),
				new PropertyInfo(Constants.Exceptions.Value, PrimitiveType.String),
			};
    }

	#endregion

}
