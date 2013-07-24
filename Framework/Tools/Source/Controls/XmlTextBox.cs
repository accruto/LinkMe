using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using ST = System.Threading;
using System.Windows.Forms;
using System.Xml;

using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Xml;
using LinkMe.Framework.Tools.Xml;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// A RichTextBox control that can format XML in colour using the XmlRichTextWriter class.
	/// </summary>
	[ToolboxBitmap(typeof(LinkMe.Framework.Tools.Bitmaps.Location), "XmlTextBox.bmp")]
	public class XmlTextBox : UserControl, IReadOnlySettable
	{
		private delegate void FormatDelegate(bool showException);

		public const bool DefaultAutoFormat = false;

		private const int m_defaultIndentation = 2;
		private const int m_defaultAutoFormatDelay = 1000;
		private const int m_defaultAutoFormatMaxSize = 40000;

		private int m_indentation = m_defaultIndentation;
		private bool m_autoFormat = DefaultAutoFormat;
		private int m_autoFormatDelay = m_defaultAutoFormatDelay;
		private int m_autoFormatMaxSize = m_defaultAutoFormatMaxSize;
		private ST.Timer m_formatTimer = null;
		private bool m_ignoreTextChanged = false;
		private bool m_ignoreNextAutoFormat = false;
		private bool m_timerSet = false;
		private bool m_formatOnHandleCreated = false;
		private string m_originalText = string.Empty;

		private RichTextBox rtfXml;
		private System.Windows.Forms.ContextMenu mnuTextBox;
		private System.Windows.Forms.MenuItem mnuFormatAll;
		private System.Windows.Forms.MenuItem mnuFormatSelection;
		private System.Windows.Forms.StatusBar sbStatus;
		private System.Windows.Forms.ToolTip tipXmlTextBox;
		private System.Windows.Forms.MenuItem mnuAutoFormat;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.ComponentModel.IContainer components;

		public XmlTextBox()
		{
			InitializeComponent();

			rtfXml.DragEnter += new DragEventHandler(rtfXml_DragEnter);
			rtfXml.DragDrop += new DragEventHandler(rtfXml_DragDrop);

			m_formatTimer = new ST.Timer(new ST.TimerCallback(FormatAutomatically), null,
				ST.Timeout.Infinite, ST.Timeout.Infinite);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (m_formatTimer != null)
			{
				m_formatTimer.Dispose();
				m_formatTimer = null;
			}

			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}

				mnuTextBox.Dispose();
			}

			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.rtfXml = new LinkMe.Framework.Tools.Controls.RichTextBox();
			this.mnuTextBox = new System.Windows.Forms.ContextMenu();
			this.mnuFormatAll = new System.Windows.Forms.MenuItem();
			this.mnuFormatSelection = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuAutoFormat = new System.Windows.Forms.MenuItem();
			this.sbStatus = new System.Windows.Forms.StatusBar();
			this.tipXmlTextBox = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// rtfXml
			// 
			this.rtfXml.AcceptsControlTab = false;
			this.rtfXml.ContextMenu = this.mnuTextBox;
			this.rtfXml.DetectUrls = false;
			this.rtfXml.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtfXml.Location = new System.Drawing.Point(0, 0);
			this.rtfXml.Name = "rtfXml";
			this.rtfXml.Size = new System.Drawing.Size(536, 208);
			this.rtfXml.TabIndex = 0;
			this.rtfXml.Text = "";
			this.rtfXml.SelectionChanged += new System.EventHandler(this.rtfXml_SelectionChanged);
			this.rtfXml.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtfXml_KeyPress);
			this.rtfXml.TextChanged += new System.EventHandler(this.rtfXml_TextChanged);
			// 
			// mnuTextBox
			// 
			this.mnuTextBox.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.mnuFormatAll,
																					   this.mnuFormatSelection,
																					   this.menuItem1,
																					   this.mnuAutoFormat});
			// 
			// mnuFormatAll
			// 
			this.mnuFormatAll.Enabled = false;
			this.mnuFormatAll.Index = 0;
			this.mnuFormatAll.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
			this.mnuFormatAll.Text = "Format As XML";
			this.mnuFormatAll.Click += new System.EventHandler(this.mnuFormatAll_Click);
			// 
			// mnuFormatSelection
			// 
			this.mnuFormatSelection.Enabled = false;
			this.mnuFormatSelection.Index = 1;
			this.mnuFormatSelection.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftF;
			this.mnuFormatSelection.Text = "Format Selection As XML";
			this.mnuFormatSelection.Click += new System.EventHandler(this.mnuFormatSelection_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Text = "-";
			// 
			// mnuAutoFormat
			// 
			this.mnuAutoFormat.Index = 3;
			this.mnuAutoFormat.Text = "Auto-format As XML";
			this.mnuAutoFormat.Click += new System.EventHandler(this.mnuAutoFormat_Click);
			// 
			// sbStatus
			// 
			this.sbStatus.Location = new System.Drawing.Point(0, 208);
			this.sbStatus.Name = "sbStatus";
			this.sbStatus.Size = new System.Drawing.Size(536, 16);
			this.sbStatus.SizingGrip = false;
			this.sbStatus.TabIndex = 1;
			this.sbStatus.Visible = false;
			// 
			// XmlTextBox
			// 
			this.Controls.Add(this.rtfXml);
			this.Controls.Add(this.sbStatus);
			this.Name = "XmlTextBox";
			this.Size = new System.Drawing.Size(536, 224);
			this.ResumeLayout(false);

		}
		#endregion

		// Make the TextChanged event appear in the Forms Designer and Intellisense.

		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>
		/// True to automatically format the text when it changes. The default is false.
		/// </summary>
		[DefaultValue(DefaultAutoFormat)]
		public bool AutoFormat
		{
			get { return m_autoFormat; }
			set
			{
				m_autoFormat = value;
				mnuAutoFormat.Checked = value;
			}
		}

		/// <summary>
		/// The length of time (in milliseconds) to wait before auto-formatting after the text changes. Any
		/// further changes reset the timer. The default is 1000 (one second).
		/// </summary>
		[DefaultValue(m_defaultAutoFormatDelay)]
		public int AutoFormatDelay
		{
			get { return m_autoFormatDelay; }
			set { m_autoFormatDelay = value; }
		}

		/// <summary>
		/// The maximum length of the text that can be formatted automatically. If the text is longer than this
		/// it will not be auto-formatted, but can still be formatted manually. The default is 40000 characters.
		/// </summary>
		[DefaultValue(m_defaultAutoFormatMaxSize)]
		public int AutoFormatMaxSize
		{
			get { return m_autoFormatMaxSize; }
			set { m_autoFormatMaxSize = value; }
		}

		/// <summary>
		/// The number of indent characters to write for every level in the hierarchy. The default is 2.
		/// </summary>
		[DefaultValue(m_defaultIndentation)]
		public int Indentation
		{
			get { return m_indentation; }
			set { m_indentation = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Modified
		{
			get { return (rtfXml.Text != m_originalText); }
			set { m_originalText = (value ? null : rtfXml.Text); }
		}

		#region RichTextBox properties

		/// <summary>
		/// Gets or sets a value indicating whether the control will allow text to be dropped onto it.
		/// The default is false.
		/// </summary>
		[DefaultValue(false)]
		public override bool AllowDrop
		{
			get { return rtfXml.AllowDrop; }
			set { rtfXml.AllowDrop = value; }
		}

		[DefaultValue(false)]
		public bool ReadOnly
		{
			get { return rtfXml.ReadOnly; }
			set
			{
				rtfXml.ReadOnly = value;
				rtfXml.BackColor = value ? Constants.Colors.ReadOnlyBackground : SystemColors.Window;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Rtf
		{
			get { return rtfXml.Rtf; }
			set
			{
				rtfXml.Rtf = value;
				m_originalText = rtfXml.Text;
			}
		}

		[DefaultValue(true)]
		public bool HideSelection
		{
			get { return rtfXml.HideSelection; }
			set { rtfXml.HideSelection = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string SelectedText
		{
			get { return rtfXml.SelectedText; }
			set { rtfXml.SelectedText = value; }
		}

		[Localizable(true)]
		public override string Text
		{
			get
			{
				// RichTextBox returns new lines as LF, but we want CRLF.
				return TextUtil.ReplaceLfWithCrlf(rtfXml.Text);
			}
			set
			{
				ResetStatusBar();

				m_ignoreTextChanged = true;
				try
				{
					rtfXml.Rtf = string.Empty; // Reset RTF formatting.

					if (AutoFormat && value != null && value.Length > 0 && value.Length <= AutoFormatMaxSize)
					{
						if (IsHandleCreated)
						{
							FormatAndSetText(value);
						}
						else
						{
							m_formatOnHandleCreated = true;
							rtfXml.Text = value;
						}
					}
					else
					{
						rtfXml.Text = value; // No auto-formatting, just set the text.
					}
				}
				finally
				{
					m_ignoreTextChanged = false;
				}

				m_originalText = rtfXml.Text; // Store the "original" after formatting.

				PrepareContextMenu();
				OnTextChanged(EventArgs.Empty);
			}
		}

		public override ContextMenu ContextMenu
		{
			get { return rtfXml.ContextMenu; }
			set { rtfXml.ContextMenu = (value == null ? mnuTextBox : value); }
		}

		#endregion

		#region Static methods

		private static bool TextMayBeXml(string text)
		{
			if (text == null || text.Length == 0)
				return false;

			// Skip leading whitespace.

			int index = 0;
			while (index < text.Length && XmlUtil.WhitespaceChars.IndexOf(text[index]) >= 0)
			{
				index++;
			}

			if (index == text.Length)
				return false; // All whitespace.

			return (text[index] == '<');
		}

		#endregion

		public bool IsWellFormed()
		{
			// Try to load it into the dom.

			if ( rtfXml.Text.Length == 0 )
				return true;

			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(rtfXml.Text);
				return true;
			}
			catch ( System.Exception )
			{
				return false;
			}
		}

		public void FormatAll()
		{
			FormatInternal(true);
		}

		public void FormatSelection()
		{
			using (new LongRunningMonitor(this))
			{
				sbStatus.Visible = false;
				m_ignoreTextChanged = true;

				try
				{
					rtfXml.SelectedRtf = FormatXml(rtfXml.SelectedText);
				}
				catch (System.Exception ex)
				{
					SetStatusBarText("Error: " + ex.Message);
					sbStatus.Visible = true;
				}
				finally
				{
					m_ignoreTextChanged = false;
				}
			}
		}

		public void LoadFile(string filePath)
		{
			if (filePath == null)
				throw new ArgumentNullException("filePath");

			using (StreamReader reader = new StreamReader(filePath))
			{
				rtfXml.Text = reader.ReadToEnd();
			}
		}

		public void SaveFile(string filePath)
		{
			if (filePath == null)
				throw new ArgumentNullException("filePath");

			using (StreamWriter writer = new StreamWriter(filePath, false, XmlWriteAdaptor.XmlEncoding))
			{
				writer.Write(rtfXml.Text);
			}
		}

		/// <summary>
		/// Set the text colour to <paramref name="color"/> for all the text in the textbox.
		/// </summary>
		/// <param name="color">The colour to apply.</param>
		public void SetTextColor(Color color)
		{
			rtfXml.SetTextColor(color);
		}

		/// <summary>
		/// Set the text colour to <paramref name="color"/> for <paramref name="length"/> characters starting
		/// from <paramref name="startIndex"/> and not counting line-break (CR and LF) characters.
		/// </summary>
		/// <param name="color">The colour to apply.</param>
		/// <param name="startIndex">The index of the first character to which the colour applies.</param>
		/// <param name="length">The number of characters following <paramref name="startIndex"/> to which the
		/// colour applies.</param>
		public void SetTextColor(Color color, int startIndex, int length)
		{
			AdjustStartAndLengthForRichTextBox(ref startIndex, ref length);
			rtfXml.SetTextColor(color, startIndex, length);
		}

		/// <summary>
		/// Selects <paramref name="length"/> characters starting from <paramref name="startIndex"/> and sets
		/// their colour.
		/// </summary>
		/// <param name="color">The colour to apply.</param>
		/// <param name="startIndex">The index of the first character to select.</param>
		/// <param name="length">The number of characters following <paramref name="startIndex"/> to select.</param>
		public void SelectAndSetTextColor(Color color, int startIndex, int length)
		{
			AdjustStartAndLengthForRichTextBox(ref startIndex, ref length);
			rtfXml.SelectAndSetTextColor(color, startIndex, length);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			if (m_formatOnHandleCreated)
			{
				m_formatOnHandleCreated = false;
				FormatAutomatically(null);
			}
		}

		private string FormatXml(string text)
		{
			if (text == null || text.Length == 0)
				return string.Empty;

			XmlTextReader reader = new XmlTextReader(new StringReader(text));
			reader.WhitespaceHandling = WhitespaceHandling.Significant;

			using (StringWriter stringWriter = new StringWriter())
			{
				XmlTextWriter writer = new XmlRichTextWriter(stringWriter);
				writer.Formatting = Formatting.Indented;
				writer.Indentation = m_indentation;

				writer.WriteNode(reader, true);

				writer.Close();

				return stringWriter.ToString();
			}
		}

		private void PrepareContextMenu()
		{
			mnuFormatAll.Enabled = (rtfXml.Text.Length > 0);
			mnuFormatSelection.Enabled = (rtfXml.SelectionLength > 0);
		}

		private void ResetStatusBar()
		{
			sbStatus.Visible = false;
			sbStatus.Text = string.Empty;
		}

		private void SetStatusBarText(string text)
		{
			sbStatus.Text = text;
			tipXmlTextBox.SetToolTip(sbStatus, text);
		}

		private void FormatAndSetText(string text)
		{
			using (new LongRunningMonitor(this))
			{
				sbStatus.Visible = false;

				// Do a quick check to see if there's any chance of the text being XML. If not just set it,
				// otherwise try to format it.

				if (TextMayBeXml(text))
				{
					try
					{
						rtfXml.Rtf = FormatXml(text);
					}
					catch (System.Exception)
					{
						rtfXml.Text = text; // Formatting failed - show plain text.
					}
				}
				else
				{
					rtfXml.Text = text;
				}
			}
		}

		private void FormatInternal(bool showException)
		{
			using (new LongRunningMonitor(this))
			{
				sbStatus.Visible = false;
				int selStart = rtfXml.SelectionStart;
				int selLength = rtfXml.SelectionLength;

				m_ignoreTextChanged = true;

				try
				{
					rtfXml.Rtf = FormatXml(rtfXml.Text);
				}
				catch (System.Exception ex)
				{
					if (showException)
					{
						SetStatusBarText("Error: " + ex.Message);
						sbStatus.Visible = true;
					}
				}
				finally
				{
					m_ignoreTextChanged = false;
				}

				if (selStart >= 0)
				{
					rtfXml.SelectionStart = selStart;
					rtfXml.SelectionLength = selLength;
				}
			}
		}

		private void FormatAutomatically(object state)
		{
			try
			{
				// Only do the format if this control still has a handle - the timer callback may be
				// called after the control is disposed. Also do a preliminary check to make sure there's
				// some chance of the text being XML.

				if (IsHandleCreated && TextMayBeXml(rtfXml.Text))
				{
					Invoke(new FormatDelegate(FormatInternal), new object[] { false });
				}
			}
			catch (System.Exception)
			{
			}
			finally
			{
				m_timerSet = false;
			}
		}

		private void AdjustStartAndLengthForRichTextBox(ref int startIndex, ref int length)
		{
			// This control's Text property replaces LF with CRLF, so the startIndex and length need to be
			// adjusted for the RichTextBox.

			string text = Text;

			int startOffset = TextUtil.GetCharacterCount(text, '\r', 0, startIndex);
			int lengthOffset = TextUtil.GetCharacterCount(text, '\r', startIndex, length);

			startIndex -= startOffset;
			length -= lengthOffset;
		}

		private void mnuFormatAll_Click(object sender, EventArgs e)
		{
			FormatAll();
		}

		private void mnuFormatSelection_Click(object sender, EventArgs e)
		{
			FormatSelection();
		}

		private void rtfXml_TextChanged(object sender, EventArgs e)
		{
			if (m_ignoreTextChanged)
				return;

			PrepareContextMenu();

			// If the text change couldn't have changed the XML formatting then don't auto-format, but if
			// an auto-format is already pending then still reset the timer to start from now. This prevents
			// an auto-format from happening while the user is still typing.

			if (m_ignoreNextAutoFormat && !m_timerSet)
			{
				m_ignoreNextAutoFormat = false;
			}
			else if (AutoFormat && rtfXml.TextLength > 0 && rtfXml.TextLength <= AutoFormatMaxSize)
			{
				m_formatTimer.Change(AutoFormatDelay, ST.Timeout.Infinite);
				m_timerSet = true;
			}

			OnTextChanged(EventArgs.Empty);
		}

		private void rtfXml_SelectionChanged(object sender, EventArgs e)
		{
			PrepareContextMenu();
		}

		private void mnuAutoFormat_Click(object sender, EventArgs e)
		{
			AutoFormat = !mnuAutoFormat.Checked;
		}

		private void rtfXml_DragEnter(object sender, DragEventArgs e)
		{
			bool dataPresent = (e.Data.GetDataPresent(DataFormats.Text)
				|| e.Data.GetDataPresent(DataFormats.UnicodeText) || e.Data.GetDataPresent(DataFormats.FileDrop));
			e.Effect = (dataPresent ? DragDropEffects.Copy : DragDropEffects.None);
		}

		private void rtfXml_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.UnicodeText))
			{
				Text = (string)e.Data.GetData(typeof(string));
			}
			else if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				// Open the first of the files dragged in (there may be more, but we ignore them).

				object[] files = (object[])e.Data.GetData(DataFormats.FileDrop);
				Debug.Assert(files.Length > 0, "files.Length > 0");
				string filename = (string)files[0];

				// Read the content.

				string content;
				using (StreamReader reader = File.OpenText(filename))
				{
					content = reader.ReadToEnd();
				}

				// If the content looks like RTF then behave like a normal RichTextBox - just display the rich text,
				// otherwise display it as plain text (and format if auto-formatting is enabled).

				if (content.StartsWith(@"{\rtf"))
				{
					Rtf = content;
				}
				else
				{
					Text = content;
				}
			}
			else
			{
				Debug.Fail("DragDrop event occurred when none of the expected data formats are present.");
			}
		}

		private void rtfXml_KeyPress(object sender, KeyPressEventArgs e)
		{
			// Avoid auto-formatting when the user types characters that won't affect the colours anyway.

			if (char.IsLetterOrDigit(e.KeyChar))
			{
				int previousIndex = rtfXml.SelectionStart - 1;
				if (previousIndex >= 0)
				{
					char previousChar = rtfXml.Text[previousIndex];
					if (char.IsLetterOrDigit(previousChar))
					{
						m_ignoreNextAutoFormat = true;
					}
				}
			}
		}
	}
}
