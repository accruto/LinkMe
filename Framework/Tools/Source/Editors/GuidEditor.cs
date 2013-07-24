using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Win32 = LinkMe.Framework.Utility.Win32;

namespace LinkMe.Framework.Tools.Editors
{
	/// <summary>
	/// A textbox for editing GUIDs that automatically validates them and also allows generating new GUIDs.
	/// </summary>
	[ToolboxBitmap(typeof(LinkMe.Framework.Tools.Bitmaps.Location), "GuidEditor.bmp")]
	public class GuidEditor : Editor
	{
		public event EventHandler ValueChanged;

		private Guid m_value = Guid.Empty;
		private Guid m_lastValidValue = Guid.Empty;
		private bool m_lastTextValid = false;

		private System.Windows.Forms.PictureBox picValid;
		private System.Windows.Forms.Button btnNewGuid;
		private GuidTextBox txtGuid;
		private System.Windows.Forms.PictureBox picInvalid;
		private System.Windows.Forms.ToolTip tipGuid;
		private System.ComponentModel.IContainer components;

		public GuidEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GuidEditor));
			this.picValid = new System.Windows.Forms.PictureBox();
			this.btnNewGuid = new System.Windows.Forms.Button();
			this.txtGuid = new GuidTextBox();
			this.picInvalid = new System.Windows.Forms.PictureBox();
			this.tipGuid = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// picValid
			// 
			this.picValid.Image = ((System.Drawing.Image)(resources.GetObject("picValid.Image")));
			this.picValid.Location = new System.Drawing.Point(224, 2);
			this.picValid.Name = "picValid";
			this.picValid.Size = new System.Drawing.Size(16, 16);
			this.picValid.TabIndex = 0;
			this.picValid.TabStop = false;
			this.tipGuid.SetToolTip(this.picValid, "The current text is a valid GUID.");
			this.picValid.Visible = false;
			// 
			// btnNewGuid
			// 
			this.btnNewGuid.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGuid.Image")));
			this.btnNewGuid.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnNewGuid.Location = new System.Drawing.Point(248, 0);
			this.btnNewGuid.Name = "btnNewGuid";
			this.btnNewGuid.Size = new System.Drawing.Size(24, 20);
			this.btnNewGuid.TabIndex = 1;
			this.tipGuid.SetToolTip(this.btnNewGuid, "Generate a new GUID.");
			this.btnNewGuid.Click += new System.EventHandler(this.btnNewGuid_Click);
			// 
			// txtGuid
			// 
			this.txtGuid.Location = new System.Drawing.Point(0, 0);
			this.txtGuid.Name = "txtGuid";
			this.txtGuid.Size = new System.Drawing.Size(224, 20);
			this.txtGuid.TabIndex = 0;
			this.txtGuid.Text = "";
			this.txtGuid.TextChanged += new System.EventHandler(this.txtGuid_TextChanged);
			// 
			// picInvalid
			// 
			this.picInvalid.Image = ((System.Drawing.Image)(resources.GetObject("picInvalid.Image")));
			this.picInvalid.Location = new System.Drawing.Point(224, 2);
			this.picInvalid.Name = "picInvalid";
			this.picInvalid.Size = new System.Drawing.Size(16, 16);
			this.picInvalid.TabIndex = 3;
			this.picInvalid.TabStop = false;
			this.tipGuid.SetToolTip(this.picInvalid, "The current text is not a valid GUID.");
			this.picInvalid.Visible = false;
			// 
			// GuidEditor
			// 
			this.Controls.Add(this.picInvalid);
			this.Controls.Add(this.txtGuid);
			this.Controls.Add(this.btnNewGuid);
			this.Controls.Add(this.picValid);
			this.MinimumSize = new System.Drawing.Size(272, 20);
			this.Name = "GuidEditor";
			this.Size = new System.Drawing.Size(272, 20);
			this.ResumeLayout(false);

		}
		#endregion

		public override bool Modified
		{
			get
			{
				if (!TextMayBeGuid(txtGuid.Text))
					return true;

				try
				{
					return (m_value != new Guid(txtGuid.Text));
				}
				catch (FormatException)
				{
					return true;
				}
			}
		}

		public override bool ReadOnly
		{
			set
			{
				base.ReadOnly = value;

				txtGuid.ReadOnly = value;
				btnNewGuid.Enabled = !value;

				ShowHideStatusIcons(IsValid);
			}
		}

		public override string Text
		{
			get { return txtGuid.Text; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Guid Value
		{
			get { return new Guid(txtGuid.Text); }
			set
			{
				m_value = value;
				txtGuid.Text = m_value.ToString();
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsValid
		{
			get
			{
				// Do a fast, simple check first.

				if (!TextMayBeGuid(txtGuid.Text))
					return false;

				// Try to create a GUID.

				try
				{
					new Guid(txtGuid.Text);
					return true;
				}
				catch (FormatException)
				{
					return false;
				}
			}
		}

		[DefaultValue(true)]
		public bool ShowNewGuid
		{
			get { return btnNewGuid.Visible; }
			set { btnNewGuid.Visible = value; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IgnoreUpKey
		{
			get { return txtGuid.IgnoreUpKey; }
			set { txtGuid.IgnoreUpKey = value; }
		}

		public void SelectValue()
		{
			txtGuid.Select();
			txtGuid.SelectAll();
		}

		#region Static methods

		private static bool TextMayBeGuid(string text)
		{
			const string validFirstChar = "{0123456789abcdefABCDEF";

			if (text == null || text.Length < 32)
				return false;

			return (validFirstChar.IndexOf(text[0]) >= 0);
		}

		#endregion

		public override void BeginEditNew()
		{
			txtGuid.Focus();
		}

		public override void Clear()
		{
			txtGuid.Text = string.Empty;
		}

		public override bool CanDisplay(System.Type type)
		{
			if (base.CanDisplay(type))
				return true;
			else
				return (type == typeof(Guid));
		}

		public override void DisplayValue(object value)
		{
			CheckValue(value);

			Value = (Guid)value;
		}

		public override object GetValue()
		{
			return Value;
		}

		protected virtual void OnValueChanged(EventArgs e)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, e);
			}
		}

		private void ShowHideStatusIcons(bool isValid)
		{
			if (ReadOnly || txtGuid.Text.Length == 0)
			{
				// Don't show either icon when the user is unable to change the text or has cleared it.

				picInvalid.Visible = false;
				picValid.Visible = false;
			}
			else
			{
				if (isValid)
				{
					picInvalid.Visible = false;
					picValid.Visible = true;
				}
				else
				{
					picInvalid.Visible = true;
					picValid.Visible = false;
				}
			}		
		}

		private void btnNewGuid_Click(object sender, EventArgs e)
		{
			txtGuid.Text = Guid.NewGuid().ToString();
		}

		private void txtGuid_TextChanged(object sender, EventArgs e)
		{
			bool isValid = false;
			bool isChanged = false;
			Guid currentGuid = Guid.Empty;

			if (TextMayBeGuid(txtGuid.Text))
			{
				try
				{
					currentGuid = Value;
					isValid = true;
				}
				catch (FormatException)
				{
				}
			}

			if (isValid)
			{
				// A valid GUID is entered - raise ValueChanged if it's different from the last value.

				if (!m_lastTextValid || currentGuid != m_lastValidValue)
				{
					m_lastTextValid = true;
					m_lastValidValue = currentGuid;
					isChanged = true;
				}
			}
			else
			{
				// An invalid GUID is entered - raise ValueChanged if the previous text was valid.

				if (m_lastTextValid)
				{
					m_lastTextValid = false;
					isChanged = true;
				}
			}

			ShowHideStatusIcons(isValid);

			if (isChanged)
			{
				OnValueChanged(EventArgs.Empty);
			}
		}

		protected override void OnFontChanged(EventArgs e)
		{
			txtGuid.Font = Font;
			base.OnFontChanged(e);
		}

		private class GuidTextBox
			:	LinkMe.Framework.Tools.Controls.TextBox
		{
			protected override void WndProc(ref System.Windows.Forms.Message m)
			{
				if ( m.Msg == Constants.Win32.Messages.WM_KEYUP && m_ignoreUpKey )
					return;
				base.WndProc(ref m); 
			}

			public bool IgnoreUpKey
			{
				get { return m_ignoreUpKey; }
				set { m_ignoreUpKey = value; }
			}

			private bool m_ignoreUpKey;	// This is used by the DataGridPrimitiveValueColumn.
		}
	}
}
