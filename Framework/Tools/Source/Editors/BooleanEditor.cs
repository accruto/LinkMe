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
	public class BooleanEditor : Editor
	{
		public event EventHandler ValueChanged;

		private bool m_value = true;
		private BooleanComboBox cboValues;
		private System.ComponentModel.IContainer components = null;

		public BooleanEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			cboValues.SelectedIndex = 0;
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
			this.cboValues = new BooleanComboBox();
			this.SuspendLayout();
			// 
			// cboValues
			// 
			this.cboValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cboValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboValues.Items.AddRange(new object[] {
														   "True",
														   "False"});
			this.cboValues.Location = new System.Drawing.Point(0, 0);
			this.cboValues.Name = "cboValues";
			this.cboValues.Size = new System.Drawing.Size(121, 21);
			this.cboValues.TabIndex = 0;
			this.cboValues.SelectedIndexChanged += new System.EventHandler(this.cboValues_SelectedIndexChanged);
			// 
			// BooleanEditor
			// 
			this.Controls.Add(this.cboValues);
			this.Name = "BooleanEditor";
			this.Size = new System.Drawing.Size(121, 21);
			this.ResumeLayout(false);

		}
		#endregion

		public override bool ReadOnly
		{
			set
			{
				base.ReadOnly = value;
				cboValues.Enabled = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Value
		{
			get
			{
				return cboValues.SelectedIndex == 0;
			}
			set
			{
				m_value = value;
				cboValues.SelectedIndex = value ? 0 : 1;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IgnoreUpKey
		{
			get { return cboValues.IgnoreUpKey; }
			set { cboValues.IgnoreUpKey = value; }
		}

		protected virtual void OnValueChanged(EventArgs e)
		{
			if ( ValueChanged != null )
				ValueChanged(this, e);
		}

		private void cboValues_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_value = cboValues.SelectedIndex == 0;
			OnValueChanged(EventArgs.Empty);
		}

		protected override void OnFontChanged(EventArgs e)
		{
			cboValues.Font = Font;
			base.OnFontChanged(e);
		}

		private class BooleanComboBox
			:	System.Windows.Forms.ComboBox
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
