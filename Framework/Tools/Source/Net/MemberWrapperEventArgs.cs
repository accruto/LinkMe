using System;

namespace LinkMe.Framework.Tools.Net
{
	public delegate void MemberWrapperEventHandler(object sender, MemberWrapperEventArgs e);

	public class MemberWrapperEventArgs : EventArgs
	{
		private MemberWrapper m_wrapper;

		public MemberWrapperEventArgs(MemberWrapper wrapper)
		{
			m_wrapper = wrapper;
		}

		public MemberWrapper Wrapper
		{
			get { return m_wrapper; }
		}
	}
}
