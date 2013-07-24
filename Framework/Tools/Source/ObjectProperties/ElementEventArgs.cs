using System;

namespace LinkMe.Framework.Tools.ObjectProperties
{
	/// <summary>
	/// Provides data for the ElementActivate and ElementSelect events.
	/// </summary>
	public class ElementEventArgs : EventArgs
	{
		IElementPropertyInfo m_element;

		public ElementEventArgs(IElementPropertyInfo element)
		{
			m_element = element;
		}

		public IElementPropertyInfo Element
		{
			get { return m_element; }
		}
	}
}
