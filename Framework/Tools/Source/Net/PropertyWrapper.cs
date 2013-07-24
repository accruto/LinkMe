using System.Diagnostics;
using System.Reflection;

using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Tools.Net
{
	/// <summary>
	/// Wraps a property obtained from a GenericWrapper.
	/// </summary>
	public class PropertyWrapper : MemberWrapper
	{
		private const string m_exceptionFormatString = "<error: an exception of type '{0}' was thrown>";

		private PropertyInfo m_info;
		private object m_instance;
		private bool m_indexed;
		private string m_displayName;

		public PropertyWrapper(PropertyInfo info, object instance)
		{
			const string method = ".ctor";

			if (info == null)
				throw new NullParameterException(GetType(), method, "info");

			m_info = info;
			m_instance = instance;
			m_indexed = (m_info.GetIndexParameters().Length > 0);

			if (m_indexed)
			{
				ParameterInfo[] indexers = m_info.GetIndexParameters();
				string[] indexerTypes = new string[indexers.Length];

				for (int index = 0; index < indexers.Length; index++)
				{
					indexerTypes[index] = GetTypeName(indexers[index].ParameterType);
				}

				m_displayName = m_info.Name + "[" + string.Join(", ", indexerTypes) + "]";
			}
			else
			{
				m_displayName = null;
			}
		}

		public override bool CanRead
		{
			get { return (m_info.CanRead && !m_indexed); }
		}

		public override bool CanWrite
		{
			get { return (m_info.CanWrite && !m_indexed); }
		}

		public override bool CanCreateWrapper
		{
			get { return !m_indexed; }
		}

		public override string Name
		{
			get { return m_info.Name; }
		}

		public override string DisplayName
		{
			get { return (m_displayName == null ? Name : m_displayName); }
		}

		public override bool ValueIsNullOrUnavailable
		{
			get { return (m_indexed ? true : base.ValueIsNullOrUnavailable); }
		}

		public override System.Type GetMemberType()
		{
			return m_info.PropertyType;
		}

		public override GenericWrapper CreateWrapper()
		{
			if (m_indexed)
				throw new System.InvalidOperationException("Unable to create a wrapper for an indexed property.");

			return base.CreateWrapper();
		}

		protected override object GetValueImpl()
		{
			if (m_indexed)
				throw new System.InvalidOperationException("Unable to access indexed property.");

			return m_info.GetValue(m_instance, null);
		}

		protected override void SetValueImpl(object value)
		{
			if (m_indexed)
				throw new System.InvalidOperationException("Unable to access indexed property.");

			m_info.SetValue(m_instance, value, null);
		}
	}
}
