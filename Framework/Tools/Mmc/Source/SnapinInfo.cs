using System;

namespace LinkMe.Framework.Tools.Mmc
{
	/// <summary>
	/// Custom attribute applied to snapin classes.  The presence of this 
	/// attribute identifies it to the snapin registration utility and 
	/// associates attribute required during snapin installation to the 
	/// registry.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class SnapinInfoAttribute
		:	System.Attribute
	{
		public SnapinInfoAttribute()
		{
			m_name = "unknown";
			m_provider = "unknown";
			m_standAlone = true;
			m_version = null;
		}

		public SnapinInfoAttribute(string name, string provider)
		{
			m_name = name;
			m_provider = provider;
			m_standAlone = true;
			m_version = null;
		}

		public SnapinInfoAttribute(string name, string provider, string version)
		{
			m_name = name;
			m_provider = provider;
			m_standAlone = true;
			m_version = version;
		}

		public SnapinInfoAttribute(string name, string provider, string version, bool standAlone)
		{
			m_name = name;
			m_provider = provider;
			m_version = version;
			m_standAlone = standAlone;
		}

		public string Name
		{
			get { return m_name;  }
			set { m_name = value; }

		}

		public bool StandAlone
		{
			get { return m_standAlone; }
			set { m_standAlone = value; }
		}

		public string Provider
		{
			get { return m_provider; }
			set { m_provider = value; }
		}

		public string Version
		{
			get { return m_version; }
			set { m_version = value; }
		}

		private string m_name;
		private string m_provider;
		private string m_version;
		private bool m_standAlone;
	}


	/// <summary>
    /// Custom attribute applied to snapin about classes.  The presence 
    /// of this attribute identifies it to the snapin registration utility and 
    /// associates attribute required during snapin installation to the 
    /// registry.
    /// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class AboutSnapinAttribute
		:	System.Attribute
	{
        public AboutSnapinAttribute(System.Type type)
		{
			m_snapinType = type;
		}

		public System.Type SnapinType
		{
			get { return m_snapinType; }
		}

		protected System.Type m_snapinType;
	}
}
