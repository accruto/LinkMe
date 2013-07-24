using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Utility.Event;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class EventDetailMemberWrapper : MemberWrapper
	{
		private IEventDetail m_value;

		internal EventDetailMemberWrapper(IEventDetail value)
		{
			m_value = value;
		}

		public override string Name
		{
			get
            {
                if (m_value.Name != "Detail" && m_value.Name.EndsWith("Detail"))
                    return m_value.Name.Substring(0, m_value.Name.Length - "Detail".Length);
                return m_value.Name;
            }
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanWrite
		{
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
			throw new System.NotSupportedException();
		}

		public override GenericWrapper CreateWrapper()
		{
			ICustomWrapper custom = m_value as ICustomWrapper;
			if (custom == null)
				return new EventDetailGenericWrapper(m_value);
			else
				return custom.CreateWrapper();
		}
	}
}
