using System;
using System.Diagnostics;

namespace LinkMe.Framework.Tools.Net
{
	/// <summary>
	/// Provides access to members of a base class of an object wrapped by GenericWrapper.
	/// </summary>
	internal class BaseWrapper : MemberWrapper
	{
		private Type m_type;
		private object m_instance;

		internal BaseWrapper(Type type, object instance)
		{
			Debug.Assert(type != null && instance != null, "type != null && instance != null");

			m_type = type;
			m_instance = instance;
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override bool CanCreateWrapper
		{
			get { return true; }
		}

		public override string Name
		{
			get { return m_type.FullName; }
		}

		public override string DisplayName
		{
			get { return m_type.FullName; }
		}

		public override bool ValueIsNullOrUnavailable
		{
			get { return false; }
		}

		public override Type GetMemberType()
		{
			return m_type;
		}

		public override GenericWrapper CreateWrapper()
		{
			return new GenericWrapper(m_instance, m_type);
		}

		protected override object GetValueImpl()
		{
			return m_instance;
		}

		protected override void SetValueImpl(object value)
		{
			throw new NotSupportedException();
		}
	}
}
