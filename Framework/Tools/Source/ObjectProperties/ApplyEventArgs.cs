using System;

namespace LinkMe.Framework.Tools.ObjectProperties
{
	public delegate void ApplyEventHandler(object sender, ApplyEventArgs e);

	/// <summary>
	/// Summary description for ApplyEventArgs.
	/// </summary>
	public class ApplyEventArgs
		:	System.EventArgs
	{
		public ApplyEventArgs(object obj)
		{
			m_object = obj;
		}

		public object Object
		{
			get { return m_object; }
		}

		object m_object;
	}
}
