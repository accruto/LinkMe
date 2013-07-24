using System.Collections;
using System.Text;

using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Tools
{
	/// <summary>
	/// A class used to build the description text for an object browser element (DescriptionText object).
	/// </summary>
	public class DescriptionBuilder
	{
		private StringBuilder m_sb = new StringBuilder();
		private int m_length = 0;
		private ArrayList m_selections = new ArrayList();
		private Hashtable m_links = new Hashtable();
		private bool m_rtf;

		/// <summary>
		/// If the rtf parameters is true the description is in Rich Text Format, otherwise it is in plain
		/// text format (ie. this class acts as a simplified StringBuilder).
		/// </summary>
		public DescriptionBuilder(bool rtf)
		{
			m_rtf = rtf;

			if (rtf)
			{
				// Write the RTF header.

				m_sb.Append(@"{\rtf1\ansi\deff0{\fonttbl{\f0\fnil\fcharset0 Tahoma;}{\f1\fswiss\fcharset0 Arial;}}"
					+ @"{\colortbl ;\red0\green0\blue0;\red0\green128\blue0;}\viewkind4\uc1\pard\cf1\f0\fs18 ");
			}
		}

		public void Append(string value)
		{
			const string method = "Append";

			if (value == null)
				throw new NullParameterException(GetType(), method, "value");

			if (m_rtf)
			{
				foreach (char c in value)
				{
					AppendEncoded(c);
				}
			}
			else
			{
				m_sb.Append(value);
			}

			m_length += value.Length;
		}

		public void AppendName(string name)
		{
			const string method = "AppendName";

			if (name == null)
				throw new NullParameterException(GetType(), method, "name");

			if (m_rtf)
			{
				m_sb.Append(@"\b ");
				m_sb.Append(name);
				m_sb.Append(@"\b0 ");
			}
			else
			{
				m_sb.Append(name);
			}

			m_length += name.Length;
		}

		public void EndFirstLine()
		{
			m_sb.Append(m_rtf ? @"\par \fs16 " : System.Environment.NewLine);
			m_length++;
		}

		public void EndLine()
		{
			m_sb.Append(m_rtf ? @"\par " : System.Environment.NewLine);
			m_length++;
		}

		public void AppendHeading(string heading)
		{
			const string method = "AppendHeading";

			if (heading == null)
				throw new NullParameterException(GetType(), method, "heading");

			if (m_rtf)
			{
				m_sb.Append(@"\b\par ");
				Append(heading);
				m_sb.Append(@"\par \b0");
			}
			else
			{
				m_sb.Append(System.Environment.NewLine);
				Append(heading);
				m_sb.Append(System.Environment.NewLine);
			}

			m_length += 2;
		}

		public void AppendLink(string linkText, object linkTarget)
		{
			const string method = "AppendLink";

			if (linkText == null)
				throw new NullParameterException(GetType(), method, "linkText");
			if (linkTarget == null)
				throw new NullParameterException(GetType(), method, "linkTarget");

			if (!m_rtf)
			{
				throw new System.InvalidOperationException("Unable to append a link when RTF was set to false"
					+ " in the constructor.");
			}

			m_sb.Append(@"\cf2\ul\b ");
			m_sb.Append(linkText);
			m_sb.Append(@"\cf1\ulnone\b0 ");

			m_selections.Add(new DescriptionText.SelectionInfo(m_length, linkText.Length));
			m_length += linkText.Length;

			m_links[linkText] = linkTarget;
		}

		public DescriptionText GetText()
		{
			DescriptionText.SelectionInfo[] selections = new DescriptionText.SelectionInfo[m_selections.Count];
			m_selections.CopyTo(selections);
			return new DescriptionText(m_sb.ToString(), selections, m_links);
		}

		public override string ToString()
		{
			return m_sb.ToString();
		}

		private void AppendEncoded(char value)
		{
			// Encode special characters \, { and } by prefixing them with \. Also encode ASCII
			// characters below 0x20 and above or equal to 0x80. Encode Unicode characters with no ASCII
			// representation (ie. above 0xFF) as \uHHHHH? where ? is the character that will be picked up by
			// readers that don't support Unicode).

			if (value == '\\' || value == '{' || value == '}')
			{
				m_sb.Append('\\'); // Encode with \
				m_sb.Append(value);
			}
			else if (value >= '\x20' && value < '\x80')
			{
				m_sb.Append(value); // No encoding needed
			}
			else if (value > '\0' && value <= '\xFF')
			{
				m_sb.Append(string.Format("\\'{0:x2}", (int)value)); // Encode as \'HH
			}
			else
			{
				m_sb.Append(string.Format("\\u{0:d}?", (int)value)); // Encode as \uDDDDD?
			}
		}
	}
}
