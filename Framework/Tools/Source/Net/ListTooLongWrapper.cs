using System;
using System.Diagnostics;

using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Tools.Net
{
	/// <summary>
	/// A fake "wrapper" for an error that is displayed when a list (array or IList) is too long.
	/// </summary>
	internal class ListTooLongWrapper : MemberWrapper
	{
		private string m_name;

		internal ListTooLongWrapper(string description)
		{
			m_name = "[" + description + "]";
		}

		internal ListTooLongWrapper(int index)
			: this(index.ToString())
		{
		}

		internal ListTooLongWrapper(int[] indices)
			: this(string.Join(",", TextUtil.ListToStringArray(indices)))
		{
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override string Name
		{
			get { return m_name; }
		}

		public override Type GetMemberType()
		{
			return null;
		}

		protected override object GetValueImpl()
		{
			throw new ApplicationException("Elements past the first " + GenericWrapper.MaxListElements.ToString()
				+ " are not shown.");
		}

		protected override void SetValueImpl(object value)
		{
			throw new NotSupportedException();
		}
	}
}
