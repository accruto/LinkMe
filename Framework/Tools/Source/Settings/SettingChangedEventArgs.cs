using System;

namespace LinkMe.Framework.Tools.Settings
{
	public delegate void SettingChangedEventHandler(object sender, SettingChangedEventArgs e);

	/// <summary>
	/// Provides data for the SettingChanged event.
	/// </summary>
	public class SettingChangedEventArgs : EventArgs
	{
		private string m_settingName;
		private object m_previousValue;

		public SettingChangedEventArgs(string settingName, object previousValue)
		{
			m_settingName = settingName;
			m_previousValue = previousValue;
		}

		public string SettingName
		{
			get { return m_settingName; }
		}

		public object PreviousValue
		{
			get { return m_previousValue; }
		}
	}
}
