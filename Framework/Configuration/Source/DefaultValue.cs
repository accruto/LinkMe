using System.IO;

using LinkMe.Framework.Type;
using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Configuration
{
	public interface IDefaultValueOwner
	{
		PrimitiveType PrimitiveType { get; }
	}

	public class DefaultValue : IInternable
	{
		public event System.EventHandler ValueChanged;

		public DefaultValue()
		{
		}

		public DefaultValue(IDefaultValueOwner owner)
		{
			m_owner = owner;
		}

		#region IInternable Members

		void IInternable.Intern(Interner interner)
		{
			m_valueAsXml = interner.Intern(m_valueAsXml);
		}

		#endregion

		public bool IsSet
		{
			get { return m_isSet; }
		}

		public void Set(object value)
		{
			m_value = TypeClone.Clone(value);
			m_valueAsXml = null;
			m_isSet = true;
			OnValueChanged(System.EventArgs.Empty);
		}

		public void SetXml(string valueAsXml)
		{
			m_value = null;
			m_valueAsXml = (valueAsXml != null && valueAsXml.Length == 0 ? string.Empty : valueAsXml);
			m_isSet = true;
			OnValueChanged(System.EventArgs.Empty);
		}

		public void Clear()
		{
			m_value = null;
			m_valueAsXml = null;
			m_isSet = false;
			OnValueChanged(System.EventArgs.Empty);
		}

		public object Value
		{
			get 
			{
				if ( m_value == null && m_valueAsXml != null )
					m_value = TypeXmlConvert.ToType(m_valueAsXml, m_owner == null ? PrimitiveType.String : m_owner.PrimitiveType);

				// Make sure the type is also set appropriately.

				if ( m_value != null && m_owner != null )
					m_value = TypeConvert.ToType(m_value, m_owner.PrimitiveType);

				return TypeClone.Clone(m_value);
			}
		}

		public string ValueAsXml
		{
			get
			{
				// Make sure the type is also set appropriately.

				if ( m_value != null && m_owner != null )
					m_value = TypeConvert.ToType(m_value, m_owner.PrimitiveType);

				if ( m_valueAsXml == null && m_value != null )
					m_valueAsXml = TypeXmlConvert.ToString(m_value);
				return m_valueAsXml;
			}
		}

		public void Copy(DefaultValue defaultValue)
		{
			m_value = TypeClone.Clone(defaultValue.m_value);
			m_valueAsXml = defaultValue.m_valueAsXml;
			m_isSet = defaultValue.m_isSet;
		}

		public DefaultValue Clone(IDefaultValueOwner owner)
		{
			DefaultValue newDefaultValue = new DefaultValue(owner);
			newDefaultValue.Copy(this);
			return newDefaultValue;
		}

		public override bool Equals(object other)
		{
			DefaultValue otherDefaultValue = other as DefaultValue;
			if ( otherDefaultValue == null )
				return false;

			if ( m_isSet )
			{
				if ( otherDefaultValue.m_isSet )
					return object.Equals(Value, otherDefaultValue.Value);
				else
					return false;
			}
			else
			{
				return !otherDefaultValue.IsSet;
			}
		}

		public override int GetHashCode()
		{
			int hash = m_isSet.GetHashCode();
			if ( m_value != null )
				hash ^= m_value.GetHashCode();
			if ( m_valueAsXml != null )
				hash ^= m_valueAsXml.GetHashCode();
			return hash;
		}

		protected virtual void OnValueChanged(System.EventArgs e)
		{
			if ( ValueChanged != null )
				ValueChanged(this, e);
		}

		public void Write(BinaryWriter writer)
		{
			writer.Write(m_isSet);
			if ( m_isSet )
				writer.Write(ValueAsXml);
		}

		public void Read(BinaryReader reader)
		{
			bool isSet = reader.ReadBoolean();
			if ( isSet )
				SetXml(reader.ReadString());
			else
				Clear();
		}

		private IDefaultValueOwner m_owner;
		private object m_value;
		private string m_valueAsXml;
		private bool m_isSet;
	}
}
