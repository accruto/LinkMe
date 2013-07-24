using System;
using System.Windows.Forms;
using System.Xml;

namespace LinkMe.Framework.Tools.Settings
{
	/// <summary>
	/// Contains window settings: size, position and state.
	/// </summary>
	public class WindowSettings : ISettingsObject
	{
		private const int DefaultHeight = 500;
		private const int DefaultWidth = 700;
		private const int DefaultLeft = 0;
		private const int DefaultTop = 0;
		private const FormWindowState DefaultWindowState = FormWindowState.Normal;
		private const int OffsetIncrement = 22;
		private const int MaxIncrements = 8;

		private int m_height;
		private int m_width;
		private int m_left;
		private int m_top;
		private FormWindowState m_state;
		private int m_offset = 0;

		public WindowSettings()
			: this(DefaultWindowState)
		{
		}

		public WindowSettings(FormWindowState state)
			: this(DefaultWidth, DefaultHeight, DefaultLeft, DefaultTop, state)
		{
		}

		public WindowSettings(int width, int height, int left, int top, FormWindowState state)
		{
			Height = height;
			Width = width;
			Left = left;
			Top = top;
			State = state;
		}

		#region ISettingsObject Members

		public bool SettingsEqual(ISettingsObject obj)
		{
			WindowSettings other = obj as WindowSettings;
			if (other == null)
				return false;

			return (Height == other.Height && Width == other.Width && Left == other.Left && Top == other.Top
				&& State == other.State);
		}

		public void ReadXmlSettings(XmlNode xmlSetting, XmlNamespaceManager xmlNsManager, string xmlPrefix,
			string readingFromPath)
		{
			XmlNode xmlHeight = xmlSetting.SelectSingleNode(xmlPrefix + "height", xmlNsManager);
			if (xmlHeight != null)
			{
				Height = XmlConvert.ToInt32(xmlHeight.InnerText);
			}

			XmlNode xmlWidth = xmlSetting.SelectSingleNode(xmlPrefix + "width", xmlNsManager);
			if (xmlWidth != null)
			{
				Width = XmlConvert.ToInt32(xmlWidth.InnerText);
			}

			XmlNode xmlLeft = xmlSetting.SelectSingleNode(xmlPrefix + "left", xmlNsManager);
			if (xmlLeft != null)
			{
				Left = XmlConvert.ToInt32(xmlLeft.InnerText);
			}

			XmlNode xmlTop = xmlSetting.SelectSingleNode(xmlPrefix + "top", xmlNsManager);
			if (xmlTop != null)
			{
				Top = XmlConvert.ToInt32(xmlTop.InnerText);
			}

			XmlNode xmlState = xmlSetting.SelectSingleNode(xmlPrefix + "state", xmlNsManager);
			if (xmlState != null)
			{
				State = (FormWindowState)XmlConvert.ToInt32(xmlState.InnerText);
			}
		}

		public void WriteXmlSettings(XmlWriter writer, string xmlns, string writingToPath)
		{
			writer.WriteStartElement(writer.LookupPrefix(xmlns), "windowSettings", xmlns);
			WriteXmlSettingContents(writer, xmlns, writingToPath);
			writer.WriteEndElement();
		}

		public void WriteXmlSettingContents(XmlWriter writer, string xmlns, string writingToPath)
		{
			writer.WriteElementString("width", xmlns, XmlConvert.ToString(Width));
			writer.WriteElementString("height", xmlns, XmlConvert.ToString(Height));
			writer.WriteElementString("left", xmlns, XmlConvert.ToString(Left));
			writer.WriteElementString("top", xmlns, XmlConvert.ToString(Top));
			writer.WriteElementString("state", xmlns, XmlConvert.ToString((int)State));
		}

		#endregion

		#region Properties for accessing settings

		public int Height
		{
			get { return m_height; }
			set { m_height = value; }
		}

		public int Width
		{
			get { return m_width; }
			set { m_width = value; }
		}

		public int Left
		{
			get { return m_left; }
			set
			{
				m_left = value;
				m_offset = 0;
			}
		}

		public int Top
		{
			get { return m_top; }
			set
			{
				m_top = value; 
				m_offset = 0;
			}
		}

		public FormWindowState State
		{
			get { return m_state; }
			set { m_state = value; }
		}

		#endregion

		public void ReadFromWindow(Form window)
		{
			State = window.WindowState;

			// If the window is minimized or maximised don't save it's size and location.
			if (State == FormWindowState.Normal)
			{
				Height = window.Height;
				Width = window.Width;
				Left = window.Left;
				Top = window.Top;
			}
		}

		public void ApplyToWindow(Form window)
		{
			// We need to set the start position to manual, otherwise it takes precedence over what we set here.

			window.StartPosition = FormStartPosition.Manual;

			// When creating several new forms Windows places each one a little below and to the right
			// of the previous one, so the user can see them all. Simulate this behaviour here.

			window.Height = Height;
			window.Width = Width;
			window.Left = Left + m_offset;
			window.Top = Top + m_offset;

            // Set the window state last, so it overrides the position.

            window.WindowState = State;

			if (m_offset == OffsetIncrement * MaxIncrements)
			{
				m_offset = 0;
			}
			else
			{
				m_offset += OffsetIncrement;
			}
		}
	}
}
