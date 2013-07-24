using System;
using System.Collections;

using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Tools
{
	[Serializable]
	public class DescriptionText
	{
		[Serializable]
		internal struct SelectionInfo
		{
			public int Start;
			public int Length;

			public SelectionInfo(int start, int length)
			{
				Start = start;
				Length = length;
			}
		}

		private string m_text;
		private SelectionInfo[] m_selections;
		private Hashtable m_links = new Hashtable();

		internal DescriptionText(string text, SelectionInfo[] selections, Hashtable links)
		{
			m_text = text;
			m_selections = selections;
			m_links = links;
		}

		internal void SetText(RichTextBox textBox)
		{
			int selStart = textBox.SelectionStart;
			int selLength = textBox.SelectionLength;

			textBox.Rtf = m_text;

			foreach (SelectionInfo selection in m_selections)
			{
				textBox.SelectionStart = selection.Start;
				textBox.SelectionLength = selection.Length;
				textBox.SelectedTextToLink(true);
			}

			if (selStart != -1)
			{
				textBox.SelectionStart = selStart;
				textBox.SelectionLength = selLength;
			}

			textBox.Tag = m_links;
		}

		public string Text
		{
			get { return m_text; }
		}
	}
}
