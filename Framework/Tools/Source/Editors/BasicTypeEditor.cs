using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using LinkMe.Framework.Tools.Controls;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// Displays and allows editing values of the following types:
	/// Boolean, Byte, Decimal, Double, Int16, Int32, Int64, SByte, Single, String, UInt16, UInt32, UInt64.
	/// </summary>
	[ToolboxBitmap(typeof(LinkMe.Framework.Tools.Bitmaps.Location), "BasicTypeEditor.bmp")]
	public class BasicTypeEditor : Editor, IXmlTextBox
	{
		public const bool DefaultAutoFormatXml = XmlTextBox.DefaultAutoFormat;

		private Control m_currentControl;
		private object m_originalValue = string.Empty;

		private LinkMe.Framework.Tools.Controls.TextBox txtSingleLine;
		private System.Windows.Forms.Panel panBooleanValue;
		private System.Windows.Forms.RadioButton radFalse;
		private System.Windows.Forms.RadioButton radTrue;
		private LinkMe.Framework.Tools.Controls.XmlTextBox txtXml;
		private System.ComponentModel.Container components = null;

		public BasicTypeEditor()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
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
			this.txtSingleLine = new LinkMe.Framework.Tools.Controls.TextBox();
			this.panBooleanValue = new System.Windows.Forms.Panel();
			this.radFalse = new System.Windows.Forms.RadioButton();
			this.radTrue = new System.Windows.Forms.RadioButton();
			this.txtXml = new LinkMe.Framework.Tools.Controls.XmlTextBox();
			this.panBooleanValue.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtSingleLine
			// 
			this.txtSingleLine.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSingleLine.Location = new System.Drawing.Point(0, 0);
			this.txtSingleLine.Name = "txtSingleLine";
			this.txtSingleLine.Size = new System.Drawing.Size(368, 20);
			this.txtSingleLine.TabIndex = 3;
			this.txtSingleLine.Text = "";
			this.txtSingleLine.Visible = false;
			// 
			// panBooleanValue
			// 
			this.panBooleanValue.Controls.Add(this.radFalse);
			this.panBooleanValue.Controls.Add(this.radTrue);
			this.panBooleanValue.Location = new System.Drawing.Point(4, 4);
			this.panBooleanValue.Name = "panBooleanValue";
			this.panBooleanValue.Size = new System.Drawing.Size(144, 16);
			this.panBooleanValue.TabIndex = 27;
			this.panBooleanValue.Visible = false;
			// 
			// radFalse
			// 
			this.radFalse.Location = new System.Drawing.Point(72, 0);
			this.radFalse.Name = "radFalse";
			this.radFalse.Size = new System.Drawing.Size(64, 16);
			this.radFalse.TabIndex = 1;
			this.radFalse.Text = "FALSE";
			// 
			// radTrue
			// 
			this.radTrue.Location = new System.Drawing.Point(0, 0);
			this.radTrue.Name = "radTrue";
			this.radTrue.Size = new System.Drawing.Size(72, 16);
			this.radTrue.TabIndex = 0;
			this.radTrue.Text = "TRUE";
			// 
			// txtXml
			// 
			this.txtXml.AutoFormat = false;
			this.txtXml.AutoFormatDelay = 1000;
			this.txtXml.AutoFormatMaxSize = 40000;
			this.txtXml.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtXml.Indentation = 2;
			this.txtXml.Location = new System.Drawing.Point(0, 0);
			this.txtXml.MinimumSize = new System.Drawing.Size(0, 0);
			this.txtXml.Name = "txtXml";
			this.txtXml.ReadOnly = false;
			this.txtXml.Size = new System.Drawing.Size(368, 88);
			this.txtXml.TabIndex = 29;
			this.txtXml.Visible = false;
			// 
			// BasicTypeEditor
			// 
			this.Controls.Add(this.txtXml);
			this.Controls.Add(this.panBooleanValue);
			this.Controls.Add(this.txtSingleLine);
			this.Name = "BasicTypeEditor";
			this.Size = new System.Drawing.Size(368, 88);
			this.panBooleanValue.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region IXmlTextBox Members

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(DefaultAutoFormatXml)]
		public bool AutoFormatXml
		{
			get { return txtXml.AutoFormat; }
			set { txtXml.AutoFormat = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string XmlText
		{
			get { return (CurrentControl == txtXml ? txtXml.Text : string.Empty); }
			set
			{
				if (CurrentControl == txtXml)
				{
					txtXml.Text = value;
				}
			}
		}

		public void SelectAndSetTextColor(Color color, int startIndex, int length)
		{
			if (CurrentControl == txtXml)
			{
				txtXml.SelectAndSetTextColor(color, startIndex, length);
			}
		}

		#endregion

		public override bool Modified
		{
			get { return !object.Equals(m_originalValue, GetValue()); }
		}

		public override bool ReadOnly
		{
			set
			{
				base.ReadOnly = value;

				txtSingleLine.ReadOnly = value;
				txtXml.ReadOnly = value;
				panBooleanValue.Enabled = !value;
			}
		}

		[DefaultValue(true)]
		public bool HideSelection
		{
			get { return txtXml.HideSelection; }
			set
			{
				txtXml.HideSelection = value;
				txtSingleLine.HideSelection = value;
			}
		}

		private Control CurrentControl
		{
			get { return m_currentControl; }
			set
			{
				if (m_currentControl != value)
				{
					if (m_currentControl != null)
					{
						m_currentControl.Visible = false;
					}

					m_currentControl = value;

					if (m_currentControl != null)
					{
						m_currentControl.Visible = true;
					}
				}
			}
		}

		public override bool CanDisplay(System.Type type)
		{
			if (base.CanDisplay(type))
				return true;
			else
			{
				return (type == typeof(bool) || type == typeof(byte) || type == typeof(decimal)
					|| type == typeof(double) || type == typeof(short) || type == typeof(int)
					|| type == typeof(long) || type == typeof(sbyte) || type == typeof(float)
					|| type == typeof(string) || type == typeof(ushort) || type == typeof(uint)
					|| type == typeof(ulong));
			}
		}

		public override void Clear()
		{
			txtSingleLine.Text = string.Empty;
			txtXml.Text = string.Empty;
			radTrue.Checked = false;
			radFalse.Checked = false;
		}

		public override void DisplayValue(object value)
		{
			CheckValue(value);

			System.Type type = value.GetType();

			if (type == typeof(string))
			{
				// Note that AutoFormat is set to false on the XmlTextBox, so we essentially just use
				// it as a RichTextBox here, but the user MAY manually format the text as XML using the
				// context menu.

				CurrentControl = txtXml;
				txtXml.Text = (string)value;
			}
			else if (type == typeof(bool))
			{
				CurrentControl = panBooleanValue;
				if ((bool)value)
				{
					radTrue.Checked = true;
				}
				else
				{
					radFalse.Checked = true;
				}
			}
			else
			{
				CurrentControl = txtSingleLine;
				txtSingleLine.Text = value.ToString();
			}

			m_originalValue = value;
		}

		public override object GetValue()
		{
			return GetValue(m_originalValue.GetType());
		}

		public override object GetValue(System.Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			if (type == typeof(bool))
			{
				if (CurrentControl != panBooleanValue)
				{
					throw new ApplicationException(string.Format("Unable to get the value as a '{0}',"
						+ " because the current control is a '{1}'.",
						type.ToString(), CurrentControl.GetType().FullName));
				}

				return radTrue.Checked;
			}
			else if (type == typeof(string))
			{
				if (CurrentControl != txtXml)
				{
					throw new ApplicationException(string.Format("Unable to get the value as a '{0}',"
						+ " because the current control is a '{1}'.",
						type.ToString(), CurrentControl.GetType().FullName));
				}

				return txtXml.Text;
			}
			else
			{
				if (CurrentControl != txtSingleLine)
				{
					throw new ApplicationException(string.Format("Unable to get the value as a '{0}',"
						+ " because the current control is a '{1}'.",
						type.ToString(), CurrentControl.GetType().FullName));
				}

				if (type == typeof(byte))
					return byte.Parse(txtSingleLine.Text);
				else if (type == typeof(decimal))
					return decimal.Parse(txtSingleLine.Text);
				else if (type == typeof(double))
					return double.Parse(txtSingleLine.Text);
				else if (type == typeof(short))
					return short.Parse(txtSingleLine.Text);
				else if (type == typeof(int))
					return int.Parse(txtSingleLine.Text);
				else if (type == typeof(long))
					return long.Parse(txtSingleLine.Text);
				else if (type == typeof(sbyte))
					return sbyte.Parse(txtSingleLine.Text);
				else if (type == typeof(float))
					return float.Parse(txtSingleLine.Text);
				else if (type == typeof(ushort))
					return ushort.Parse(txtSingleLine.Text);
				else if (type == typeof(uint))
					return uint.Parse(txtSingleLine.Text);
				else if (type == typeof(ulong))
					return ulong.Parse(txtSingleLine.Text);
				else
					throw new ApplicationException("Type '" + type.FullName + "' is not supported.");
			}
		}
	}
}
