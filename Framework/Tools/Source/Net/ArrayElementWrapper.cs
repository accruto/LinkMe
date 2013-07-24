using System;
using System.Diagnostics;

using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Tools.Net
{
	/// <summary>
	/// Wraps a single element obtained from a GenericWrapper for an array.
	/// </summary>
	internal class ArrayElementWrapper : MemberWrapper
	{
		private Array m_array;
		private int[] m_indices;

		internal ArrayElementWrapper(Array array, int[] indices)
		{
			Debug.Assert(array != null && indices != null && indices.Length > 0,
				"array != null && indices != null && indices.Length > 0");

			m_array = array;
			m_indices = indices;
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return true; }
		}

		public override string Name
		{
			get { return "[" + string.Join(",", TextUtil.ListToStringArray(m_indices)) + "]"; }
		}

		public override Type GetMemberType()
		{
			return m_array.GetType().GetElementType();
		}

		protected override object GetValueImpl()
		{
			return m_array.GetValue(m_indices);
		}

		protected override void SetValueImpl(object value)
		{
			m_array.SetValue(value, m_indices);
		}
	}
}
