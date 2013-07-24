using System;
using System.Collections;
using System.Diagnostics;

namespace LinkMe.Framework.Tools.Net
{
	/// <summary>
	/// Wraps a single element obtained from a GenericWrapper for an IList object.
	/// </summary>
	internal class ListElementWrapper : MemberWrapper
	{
		private IList m_list;
		private int m_index;

		internal ListElementWrapper(IList list, int index)
		{
			Debug.Assert(list != null, "list != null");

			m_list = list;
			m_index = index;
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return !m_list.IsReadOnly; }
		}

		public override string Name
		{
			get { return "[" + m_index.ToString() + "]"; }
		}

		public override Type GetMemberType()
		{
			return typeof(object);
		}

		protected override object GetValueImpl()
		{
			return m_list[m_index];
		}

		protected override void SetValueImpl(object value)
		{
			m_list[m_index] = value;
		}
	}
}
