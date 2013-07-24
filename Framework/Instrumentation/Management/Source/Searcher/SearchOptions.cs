namespace LinkMe.Framework.Instrumentation.Management
{
	public class SearchOptions
	{
		private bool m_recursive = true;
		private bool m_ignoreCase = false;

		internal SearchOptions()
		{
		}

		public bool Recursive
		{
			get { return m_recursive; }
			set { m_recursive = value; }
		}

		public bool IgnoreCase
		{
			get { return m_ignoreCase; }
			set { m_ignoreCase = value; }
		}
	}
}
