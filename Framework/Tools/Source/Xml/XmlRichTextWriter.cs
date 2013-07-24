using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace LinkMe.Framework.Tools.Xml
{
	/// <summary>
	/// An XmlWriter implementation that outputs in RTF (Rich Text Format) instead of plain text. The result
	/// looks very similar to the way the Visual Studio.NET editor formats XML, except for CDATA blocks, DOCTYPE
	/// declarations and comments, which use slightly different colour combinations.
	/// </summary>
	public class XmlRichTextWriter : XmlTextWriter
	{
		#region Nested classes

		private enum FormatterState
		{
			None,
			AttributeBody,
			AttributeName,
			CDATABody,
			Comment,
			DocTypeBody,
			DocTypeName,
			DocTypeReference,
			ElementBody,
			EndAttribute,
			EndElement,
			InstructionBody,
			InstructionName,
			QualifiedName,
			StartAttribute,
			StartCDATA,
			StartDocType,
			StartElement,
			StartInstruction,
			Text
		}

		private class RtfWriter : TextWriter
		{
			private const string m_rtfHeader = @"{\rtf1\ansi\ansicpg1252\deff0\deflang3081{\fonttbl{\f0\fnil\fprq1\"
				+ @"fcharset0 Courier New;}}{\colortbl ;\red0\green0\blue255;\red128\green0\blue0;\red255\green0\"
				+ @"blue255;\red255\green0\blue0;\red0\green128\blue0;}\viewkind4\pard\f0\fs20";
			private const string m_newLine = "\\par\r\n";
			private const string m_colourBlack = @"\cf0 ";
			private const string m_colourBlue = @"\cf1 ";
			private const string m_colourMaroon = @"\cf2 ";
			private const string m_colourPink = @"\cf3 ";
			private const string m_colourRed = @"\cf4 ";
			private const string m_colourGreen = @"\cf5 ";

			private TextWriter m_writer;
			private Stack m_stateStack = new Stack();
			private string m_currentColour = null;
			private string m_attributeBuffer = null;

			public RtfWriter(TextWriter writer)
			{
				Debug.Assert(writer != null, "writer != null");

				CoreNewLine = m_newLine.ToCharArray();
				m_writer = writer;
				m_writer.NewLine = m_newLine;
				m_writer.Write(m_rtfHeader);
			}

			public override Encoding Encoding
			{
				get { return m_writer.Encoding; }
			}

			public override IFormatProvider FormatProvider
			{
				get { return m_writer.FormatProvider; }
			}

			public override void Close()
			{
				m_writer.Close();
			}

			public override void Flush()
			{
				m_writer.Flush();
			}

			public override string NewLine
			{
				set { } // Ignore changes to the NewLine value.
			}

			public override void Write(char value)
			{
				PrepareToWrite(value);
				WriteEncoded(value);
			}

			public override void Write(string value)
			{
				if (value != null && PrepareToWrite(value))
				{
					WriteEncoded(value);
				}
			}

			public override void WriteLine()
			{
				m_writer.Write(m_newLine);
			}

			public override void WriteLine(string value)
			{
				Write(value);
				WriteLine();
			}

			public void ClearState()
			{
				m_stateStack.Clear();
			}

			public void PushState(FormatterState value)
			{
				m_stateStack.Push(value);
			}

			private void WriteEncoded(char value)
			{
				// Encode special characters \, { and } by prefixing them with \. Also encode ASCII
				// characters below 0x20 and above or equal to 0x80. Encode Unicode characters with no ASCII
				// representation (ie. above 0xFF) as \uHHHHH? where ? is the character that will be picked up by
				// readers that don't support Unicode).

				if (value == '\\' || value == '{' || value == '}')
				{
					m_writer.Write('\\'); // Encode with \
					m_writer.Write(value);
				}
				else if (value >= '\x20' && value < '\x80')
				{
					m_writer.Write(value); // No encoding needed
				}
				else if (value <= '\xFF')
				{
					// Write a new line for LF and ignore CR.

					if (value == '\n')
					{
						WriteLine();
					}
					else if (value != '\r')
					{
						m_writer.Write(string.Format("\\'{0:x2}", (int)value)); // Encode as \'HH
					}
				}
				else
				{
					m_writer.Write(string.Format("\\u{0:d}?", (int)value)); // Encode as \uDDDDD?
				}
			}

			private void WriteEncoded(string value)
			{
				foreach (char c in value)
				{
					WriteEncoded(c);
				}
			}

			// This is where the real work is done - work out what colour to use based on the current state
			// and the string value being written. XmlTextWriter has an AutoComplete() function, so often
			// we may be writing a value that SHOULD have been written in the previous state. This makes it
			// necessary to check for "xmlns" for the previous element when writing the start of the next one,
			// for example.

			// There is one method for a char value and one for a string value. This improves performance
			// as it avoids unnecessary conversion from char to string.

			private void PrepareToWrite(char value)
			{
				FormatterState state = PeekState();

				switch (state)
				{
					case FormatterState.StartElement:
						SetColour(value == '<' || value == '>' ? m_colourBlue : m_colourMaroon);
						break;

					case FormatterState.EndElement:
						SetColour(value == '<' || value == '>' || value == '/' ? m_colourBlue : m_colourMaroon);
						break;

					case FormatterState.ElementBody:
						SetColour(value == '>' ? m_colourBlue : m_colourBlack);
						break;

					case FormatterState.StartAttribute:
						if (value == ':')
						{
							Debug.Assert(m_attributeBuffer != null, "m_attributeBuffer != null");

							// The buffer was a prefix - write it now.

							SetColour(m_colourMaroon);
							WriteEncoded(m_attributeBuffer);
							m_attributeBuffer = null;
							SetColour(m_colourPink);

							// The attribute name should follow.

							PopState();
							PushState(FormatterState.AttributeName);
						}
						else if (value == '=')
						{
							// The buffer was a name - write it now.

							SetColour(m_colourRed);
							WriteEncoded(m_attributeBuffer);
							m_attributeBuffer = null;

							// The body should follow.

							SetColour(m_colourBlue);
							PopState();
							PushState(FormatterState.AttributeBody);
						}
						else
						{
							Debug.Assert(value == ' ', "Unexpected character value in StartAttribute state: '"
								+ value.ToString() + "'");
						}
						break;

					case FormatterState.AttributeName:
						if (value == '=')
						{
							SetColour(m_colourBlue);
							PopState();
							PushState(FormatterState.AttributeBody);
						}
						else
						{
							SetColour(m_colourRed);
						}
						break;

					case FormatterState.AttributeBody:
						SetColour(m_colourBlue);
						if (value == '>')
						{
							PopState();
						}
						break;

					case FormatterState.EndAttribute:
						SetColour(m_colourBlue);
						break;

					case FormatterState.Text:
						SetColour(m_colourBlack);
						break;

					case FormatterState.StartCDATA:
						if (value == '>')
						{
							SetColour(m_colourBlue); // The > ending the start of the element
						}
						else
						{
							Debug.Fail("Unexpected text in StartCDATA state: '" + value.ToString() + "'");
						}
						break;

					case FormatterState.CDATABody:
						SetColour(m_colourBlack);
						break;

					case FormatterState.Comment:
						SetColour(m_colourGreen);
						break;

					case FormatterState.StartInstruction:
						// No character values for this state.
						break;

					case FormatterState.InstructionName:
						if (value == '=')
						{
							SetColour(m_colourBlue);
							PushState(FormatterState.InstructionBody);
						}
						else
						{
							SetColour(m_colourRed);
						}
						break;

					case FormatterState.InstructionBody:
						if (value == ' ')
						{
							PopState();
						}
						else
						{
							// This may actually be the end of the processing instruction, but the colour is blue
							// either way.
							SetColour(m_colourBlue);
						}
						break;

					case FormatterState.StartDocType:
						SetColour(m_colourBlue);
						PushState(FormatterState.DocTypeName);
						break;

					case FormatterState.DocTypeName:
						if (value == '>')
						{
							SetColour(m_colourBlue);
						}
						else if (value == '[')
						{
							SetColour(m_colourBlue);
							PushState(FormatterState.DocTypeBody);
						}
						else if (value == '\"')
						{
							SetColour(m_colourBlue);
							PushState(FormatterState.DocTypeReference);
						}
						else
						{
							SetColour(m_colourRed);
						}
						break;

					case FormatterState.DocTypeBody:
						SetColour(value == ']' || value == '>' ? m_colourBlue : m_colourPink);
						break;

					case FormatterState.DocTypeReference:
						SetColour(m_colourBlue);
						break;

					case FormatterState.QualifiedName:
						// Nothing to do for a qualified name - it should be part of an attribute or
						// element value and will have the same formatting as any other value.
						break;

					case FormatterState.None:
						// This case occurs when the XML ends abruptly (ie. it's invalid), but we still
						// to auto-complete the last element.
						SetColour(value == '>' ? m_colourBlue : m_colourBlack);
						break;

					default:
						Debug.Fail("Unexpected formatter state: " + state.ToString());
						break;
				}
			}

			private bool PrepareToWrite(string value)
			{
				FormatterState state = PeekState();

				switch (state)
				{
					case FormatterState.StartElement:
						if (value == " xmlns")
						{
							PushState(FormatterState.StartAttribute);
							return PrepareToWrite(value);
						}
						else
						{
							SetColour(m_colourMaroon);
						}
						break;

					case FormatterState.EndElement:
						SetColour(value == " /" ? m_colourBlue : m_colourMaroon);
						break;

					case FormatterState.ElementBody:
						if (value == " xmlns")
						{
							PushState(FormatterState.StartAttribute);
							return PrepareToWrite(value);
						}
						else
						{
							SetColour(m_colourBlack);
						}
						break;

					case FormatterState.StartAttribute:
						// We don't know at this stage whether this is the attribute prefix or the attribute
						// name, so don't write it yet, just buffer it.

						Debug.Assert(m_attributeBuffer == null, "m_attributeBuffer == null");
						m_attributeBuffer = value;
						return false;

					case FormatterState.AttributeName:
						SetColour(m_colourRed);
						break;

					case FormatterState.AttributeBody:
						SetColour(m_colourBlue);
						break;

					case FormatterState.EndAttribute:
						SetColour(m_colourBlue);
						break;

					case FormatterState.Text:
						SetColour(m_colourBlack);
						break;

					case FormatterState.StartCDATA:
						Debug.Assert(value == "<![CDATA[", "Unexpected text in StartCDATA state: '" + value + "'");
						SetColour(m_colourPink);
						PushState(FormatterState.CDATABody);
						break;

					case FormatterState.CDATABody:
						SetColour(value == "]]>" ? m_colourPink : m_colourBlack);
						break;

					case FormatterState.Comment:
						SetColour(value == "<!--" || value == "-->" ? m_colourBlue : m_colourGreen);
						break;

					case FormatterState.StartInstruction:
						if (value == "<?")
						{
							SetColour(m_colourBlue);
						}
						else if (value == "xml")
						{
							SetColour(m_colourMaroon);
							PushState(FormatterState.InstructionName);
						}
						break;

					case FormatterState.InstructionName:
						if (value == "?>")
						{
							SetColour(m_colourBlue);
						}
						else
						{
							WriteXmlInstructions(value);
							return false;
						}
						break;

					case FormatterState.InstructionBody:
						// This may actually be the end of the processing instruction, but the colour is blue
						// either way.
						SetColour(m_colourBlue);
						break;

					case FormatterState.StartDocType:
						SetColour(m_colourBlue);
						PushState(FormatterState.DocTypeName);
						break;

					case FormatterState.DocTypeName:
						SetColour(m_colourRed);
						break;

					case FormatterState.DocTypeBody:
						SetColour(m_colourPink);
						break;

					case FormatterState.DocTypeReference:
						SetColour(m_colourBlue);
						break;

					case FormatterState.QualifiedName:
						// Nothing to do for a qualified name - it should be part of an attribute or
						// element value and will have the same formatting as any other value.
						break;

					default:
						Debug.Fail("Unexpected formatter state: " + state.ToString());
						break;
				}

				return true;
			}

			private void WriteXmlInstructions(string value)
			{
				// The XmlTextWriter sends all the instruction in one go here, eg.:
				// version="1.0" encoding="utf-16"
				// so we need to split up the string to write attribute names in red and values in blue.

				SetColour(m_colourRed);

				int equals = value.IndexOf('=');
				int start = 0;
				while (equals != -1)
				{
					WriteEncoded(value.Substring(start, equals - start));

					int quote = value.IndexOfAny(new char[] { '\"', '\'' }, equals);
					if (quote != -1)
					{
						char quoteChar = value[quote];
						quote = value.IndexOf(quoteChar, quote + 1);
						if (quote != -1)
						{
							SetColour(m_colourBlue);
							WriteEncoded(value.Substring(equals, quote - equals + 1));
							start = quote + 1;
						}
					}

					SetColour(m_colourRed);
					equals = value.IndexOf('=', equals + 1);
				}
			}

			private void SetColour(string colour)
			{
				if (colour != m_currentColour)
				{
					m_writer.Write(colour);
					m_currentColour = colour;
				}
			}

			private FormatterState PeekState()
			{
				return (m_stateStack.Count == 0 ? FormatterState.None : (FormatterState)m_stateStack.Peek());
			}

			private void PopState()
			{
				Debug.Assert(m_stateStack.Count > 0, "m_stateStack.Count > 0");
				m_stateStack.Pop();
			}
		}

		#endregion

		private RtfWriter m_writer;

		#region Constructors

		public XmlRichTextWriter(TextWriter writer)
			: this(new RtfWriter(writer))
		{
		}

		public XmlRichTextWriter(Stream stream, Encoding encoding)
			: this(new StreamWriter(stream, encoding))
		{
		}

		public XmlRichTextWriter(string filename, Encoding encoding)
			: this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None), encoding)
		{
		}

		private XmlRichTextWriter(RtfWriter writer)
			: base(writer)
		{
			m_writer = writer;
		}

		#endregion

		public override void WriteCData(string text)
		{
			m_writer.PushState(FormatterState.StartCDATA);
			base.WriteCData(text);
			m_writer.ClearState();
		}

		public override void WriteComment(string text)
		{
			m_writer.PushState(FormatterState.Comment);
			base.WriteComment(text);
			m_writer.ClearState();
		}

		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			m_writer.PushState(FormatterState.StartDocType);
			base.WriteDocType(name, pubid, sysid, subset);
			m_writer.ClearState();
		}

		public override void WriteEndAttribute()
		{
			m_writer.PushState(FormatterState.EndAttribute);
			base.WriteEndAttribute();
			m_writer.ClearState();
		}

		public override void WriteEndElement()
		{
			m_writer.PushState(FormatterState.EndElement);
			base.WriteEndElement();
			m_writer.ClearState();
		}

		public override void WriteFullEndElement()
		{
			m_writer.PushState(FormatterState.EndElement);
			base.WriteFullEndElement();
			m_writer.ClearState();
		}

		public override void WriteProcessingInstruction(string name, string text)
		{
			m_writer.PushState(FormatterState.StartInstruction);
			base.WriteProcessingInstruction(name, text);
			m_writer.ClearState();
		}

		public override void WriteQualifiedName(string localName, string ns)
		{
			m_writer.PushState(FormatterState.QualifiedName);
			base.WriteQualifiedName(localName, ns);
			m_writer.ClearState();
		}

		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			m_writer.PushState(FormatterState.StartAttribute);
			base.WriteStartAttribute(prefix, localName, ns);
			m_writer.ClearState();
		}

		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			m_writer.PushState(FormatterState.StartElement);
			base.WriteStartElement(prefix, localName, ns);
			m_writer.ClearState();
		}

		public override void WriteStartDocument()
		{
			m_writer.PushState(FormatterState.StartInstruction);
			base.WriteStartDocument();
			m_writer.ClearState();
		}

		public override void WriteStartDocument(bool standalone)
		{
			m_writer.PushState(FormatterState.StartInstruction);
			base.WriteStartDocument();
			m_writer.ClearState();
		}

		public override void WriteString(string text)
		{
			switch (WriteState)
			{
				case WriteState.Attribute:
					m_writer.PushState(FormatterState.AttributeBody);
					break;

				case WriteState.Element:
					m_writer.PushState(FormatterState.ElementBody);
					break;

				case WriteState.Content:
					m_writer.PushState(FormatterState.Text);
					break;

				default:
					Debug.Fail("Unexpected WriteState in WriteString(): " + WriteState.ToString());
					break;
			}
			base.WriteString(text);
			m_writer.ClearState();
		}
	}
}
