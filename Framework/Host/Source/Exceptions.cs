using System.Runtime.Serialization;

using LinkMe.Framework.Type;
using LinkMe.Framework.Type.Exceptions;

namespace LinkMe.Framework.Host.Exceptions
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
			return "LinkMe.Framework.Host.Source.Exceptions";
		}
	}

	#endregion

	#region ContainerThreadPoolNotFoundException

	[System.Serializable]
	public class ContainerThreadPoolNotFoundException
		:	Exception
	{
		public ContainerThreadPoolNotFoundException()
			:	base()
		{
		}

		public ContainerThreadPoolNotFoundException(System.Type source, string method,
			string threadPoolName, string containerName, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(threadPoolName, containerName);
		}

		public ContainerThreadPoolNotFoundException(string source, string method, string threadPoolName,
			string containerName, System.Exception innerException)
			:	base(source, method, innerException)
		{
			Set(threadPoolName, containerName);
		}

		public ContainerThreadPoolNotFoundException(System.Type source, string method,
			string threadPoolName, string containerName)
			:	base(source, method)
		{
			Set(threadPoolName, containerName);
		}

		public ContainerThreadPoolNotFoundException(string source, string method, string threadPoolName,
			string containerName)
			:	base(source, method)
		{
			Set(threadPoolName, containerName);
		}

		protected ContainerThreadPoolNotFoundException(SerializationInfo info, StreamingContext context)
			:	base(info, context)
		{
		}

		public string ThreadPoolName
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ThreadPoolName, string.Empty); }
		}

		public string ContainerName
		{
			get { return (string) GetPropertyValue(Constants.Exceptions.ContainerName, string.Empty); }
		}

		protected override PropertyInfo[] PropertyInfos
		{
			get { return m_propertyInfos; }
		}

		private void Set(string threadPoolName, string containerName)
		{
			SetPropertyValue(Constants.Exceptions.ThreadPoolName, threadPoolName == null ? "<null>" : threadPoolName);
			SetPropertyValue(Constants.Exceptions.ContainerName, containerName == null ? "<null>" : containerName);
		}

		protected static readonly PropertyInfo[] m_propertyInfos = new PropertyInfo[]
		{
			new PropertyInfo(Constants.Exceptions.ThreadPoolName, PrimitiveType.String),
			new PropertyInfo(Constants.Exceptions.ContainerName, PrimitiveType.String)
		};
	}

	#endregion

	#region ContainerThreadPoolNotFoundException

	[System.Serializable]
	public class ContainerNotInitialisedException
		:	Exception
	{
		public ContainerNotInitialisedException()
			:	base()
		{
		}

		public ContainerNotInitialisedException(System.Type source, string method)
			:	base(source, method)
		{
		}

		public ContainerNotInitialisedException(string source, string method)
			:	base(source, method)
		{
		}

		protected ContainerNotInitialisedException(SerializationInfo info, StreamingContext context)
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
}
