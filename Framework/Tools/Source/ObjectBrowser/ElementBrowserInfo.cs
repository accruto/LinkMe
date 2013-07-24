using System;

namespace LinkMe.Framework.Tools.ObjectBrowser
{
	/// <summary>
	/// A base implementation of IElementBrowserInfo.
	/// </summary>
	public abstract class ElementBrowserInfo : MarshalByRefObject, IElementBrowserInfo
	{
		private bool m_checked = false;

		protected ElementBrowserInfo()
		{
		}

		#region IElementBrowserInfo Members

		public bool Checked
		{
			get { return m_checked; }
			set { m_checked = value; }
		}

		public abstract string DisplayName
		{
			get;
		}

		public abstract string NodeText
		{
			get;
		}

		public abstract DescriptionText Description
		{
			get;
		}

		public abstract int ImageIndex
		{
			get;
		}

		#endregion

		#region IComparable Members

		/// <summary>
		/// The base implementation compares the node text first using case-insensitive comparison and then,
		/// if the elements are equal, case-sensitive comparison.
		/// </summary>
		public virtual int CompareTo(object obj)
		{
			IElementBrowserInfo other = obj as IElementBrowserInfo;
			if (other == null)
			{
				throw new ArgumentException(string.Format("Only another IElementBrowserInfo object can be"
					+ " compared to a '{0}' object, but '{1}' was passed in.", GetType().FullName,
					(obj == null ? "<null>" : obj.GetType().FullName)), "obj");
			}

			int result = string.Compare(NodeText, other.NodeText, true);
			if (result != 0)
				return result;

			return string.Compare(NodeText, other.NodeText, false);
		}

		#endregion

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
