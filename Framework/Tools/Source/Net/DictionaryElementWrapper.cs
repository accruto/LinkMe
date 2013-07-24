using System;
using System.Collections;
using System.Diagnostics;

using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Tools.Net
{
	/// <summary>
	/// Wraps a single element obtained from a GenericWrapper for an IDictionary object.
	/// </summary>
	internal class DictionaryElementWrapper : MemberWrapper
	{
		private IDictionary m_dictionary;
		private object m_key;

		internal DictionaryElementWrapper(IDictionary dictionary, object key)
		{
			Debug.Assert(dictionary != null && key != null, "dictionary != null && key != null");

			m_dictionary = dictionary;
			m_key = key;
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return !m_dictionary.IsReadOnly; }
		}

		public override string Name
		{
			get
			{
				if (m_key is string)
					return "[" + TextUtil.QuoteStringForCSharp((string)m_key) + "]";
				else if (m_key is ValueType)
				{
					// The key might be a numeric type. If so, display a type suffix to make it clear to the
					// user what the exact type of the value is (eg. to differentiate between int and long).

					return "[" + TextUtil.QuoteNumericLiteralForCSharp((ValueType)m_key) + "]";
				}
				else
					return "[" + m_key.ToString() + "]";
			}
		}

		public override Type GetMemberType()
		{
			return typeof(object);
		}

		protected override object GetValueImpl()
		{
			return m_dictionary[m_key];
		}

		protected override void SetValueImpl(object value)
		{
			m_dictionary[m_key] = value;
		}
	}
}
