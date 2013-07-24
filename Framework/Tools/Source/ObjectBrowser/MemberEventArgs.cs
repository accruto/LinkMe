using System;

namespace LinkMe.Framework.Tools.ObjectBrowser
{
	/// <summary>
	/// Provides data for the MemberActivate and MemberSelect events.
	/// </summary>
	public class MemberEventArgs : EventArgs
	{
		IMemberBrowserInfo m_member;

		public MemberEventArgs(IMemberBrowserInfo member)
		{
			m_member = member;
		}

		public IMemberBrowserInfo Member
		{
			get { return m_member; }
		}
	}
}
