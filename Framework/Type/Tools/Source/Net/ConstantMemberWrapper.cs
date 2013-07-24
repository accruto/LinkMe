using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Type.Tools.Net
{
	/// <summary>
	/// A MemberWrapper implementation for a constant value. The type name is returned as a simple name if it is
	/// in the LinkMe.Framework.Type namespace, otherwise as a full name.
	/// </summary>
	public class ConstantMemberWrapper : MemberWrapper
	{
		private string m_name;
		private object m_value;

		public ConstantMemberWrapper(string name, object value)
		{
			const string method = ".ctor";

			if (name == null)
				throw new NullParameterException(GetType(), method, "name");

			m_name = name;
			m_value = value;
		}

		public override string Name
		{
			get { return m_name; }
		}

		public override bool CanRead
		{
			get { return true; }
		}

		// Don't allow the user to change the value, though it can still be changed from code by calling
		// SetValue().
		public override bool CanWrite
		{
			get { return false; }
		}

		public override bool CanCreateWrapper
		{
			// Don't allow complex values (eg. DateTime) to be expanded.
			get { return false; }
		}

		public override System.Type GetMemberType()
		{
			return null;
		}

		protected override object GetValueImpl()
		{
			return m_value;
		}

		protected override void SetValueImpl(object value)
		{
			m_value = value;
		}

		protected override string GetTypeName(System.Type type)
		{
			return PrimitiveTypeInfo.TrimTypeNamespace(base.GetTypeName(type));
		}
	}
}
