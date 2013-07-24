using System;
using System.Diagnostics;

using LinkMe.Framework.Tools.Net;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class ExceptionWrapper : MemberWrapper
	{
		private ExceptionInfo m_exception;

		public ExceptionWrapper(ExceptionInfo exception)
		{
			m_exception = exception;
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
			get { return "Exception"; }
		}

        public override bool MayHaveChildren
        {
            get { return false; } // Don't expand ExceptionInfo properties
        }

        public override System.Type GetMemberType()
		{
			return typeof(ExceptionInfo);
		}

		protected override object GetValueImpl()
		{
			return m_exception;
		}

		protected override void SetValueImpl(object value)
		{
			throw new NotSupportedException();
		}
	}
}
