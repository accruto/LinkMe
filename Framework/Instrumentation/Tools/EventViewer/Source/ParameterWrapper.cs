using System;
using System.Diagnostics;

using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class ParameterWrapper : MemberWrapper
	{
		private EventParameter m_parameter;

		public ParameterWrapper(EventParameter parameter)
		{
			Debug.Assert(parameter != null, "parameter != null");
			m_parameter = parameter;
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
			get { return m_parameter.Name; }
		}

		public override System.Type GetMemberType()
		{
			return typeof(object);
		}

        protected override string GetValueTypeDescription()
        {
            return m_parameter.Class;
        }

		protected override object GetValueImpl()
		{
			return m_parameter.Value;
		}

		protected override void SetValueImpl(object value)
		{
			throw new NotSupportedException();
		}
	}
}
